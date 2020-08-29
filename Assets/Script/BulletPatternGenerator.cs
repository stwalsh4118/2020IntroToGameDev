using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternGenerator : MonoBehaviour
{

    [SerializeField]
    public int patternArrays = 2; //Total bullet arrays
    [SerializeField]
    public int bulletsPerArrays = 10; //Bullets per Array

    //Angle Variables
    [SerializeField]
    public float spreadBetweenArray = 180; //spread between arrays
    [SerializeField]
    public float spreadWithinArray = 90; //spread between the last and the first bullet of an array
    [SerializeField]
    public float startAngle = 0; //Start angle
    [SerializeField]
    public float defaultAngle = 0;

    //Spinning Variables
    [SerializeField]
    public float beginSpinSpeed = 1;
    [SerializeField]
    public float spinRate = 0; // The rate at which the pattern is spinning
    [SerializeField]
    public float spinModificator = 0; // The modificator of the spin rate
    [SerializeField]
    public int invertSpin = 1; // (1 = spinRate gets inversed once SpinRate >= maxSpinRate  || 0 = Spin doesn't invert at all)
    [SerializeField]
    public float maxSpinRate = 10; //The max spin rate ->if SpinRate >= maxSpinRate --> inverts spin

    //Fire Rate Variables
    [SerializeField]
    public float fireRate = 5;

    //Offsets 
    [SerializeField]
    public float objectWidth = 0; //Width of the bullet firing object
    [SerializeField]
    public float objectHeight = 0; //Height of the bullet firing object
    [SerializeField]
    public float xOffset = 0; //Shift spawn point of the bullets along the X-Axis
    [SerializeField]
    public float yOffset = 0; //Shift spawn point of the bulltes along the Y-Axis

    //Bullet Variables
    [SerializeField]
    public float bulletSpeed = 1;
    [SerializeField]
    public float bulletAcceleration = 0;
    [SerializeField]
    public float bulletCurve = 0;
    [SerializeField]
    public float bulletTTL = 3000;


    public int bulletLength;

    [SerializeField]
    public int arrrayLength = 0;

    public float arrayAngle;
    public float bulletAngle;

    [SerializeField]
    public float patternTime = 0.1f;
    public float countTime = 0;





    [SerializeField]
    private int bulletsAmount = 4;


    [SerializeField]
    private float startAngle1 = 135f, endAngle = 235f;

    private Vector2 bulletMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Fire", .01f, .05f);
    }




    void Update()
    {
        bulletLength = bulletsPerArrays - 1;
        if (bulletLength == 0)
        {
            bulletLength = 1;
        }

        arrrayLength = patternArrays - 1 * patternArrays;

        if (arrrayLength == 0)
        {
            arrrayLength = 1;
        }
        
        arrayAngle = (spreadWithinArray / bulletLength); //Calculates the spread between each array
        bulletAngle = (spreadBetweenArray / arrrayLength); //Calcualtes the spread within the bullets in the arrays

        countTime = countTime + Time.deltaTime;
        if (countTime >= patternTime)
        {
            countTime = 0;
            Generate();
        }
       
    }



    private void Generate()
    {
        for (int i = 0; i < patternArrays; i++)
        { //For each bullet array in pattern
            for (int j = 0; j < bulletsPerArrays; j++)
            { //For each bullet in bullet array
                calculation(i, j, arrayAngle, bulletAngle);
            }
        }

        //If Default Angle > 360 , set it to 0
        if (defaultAngle > 360)
        {
            defaultAngle = 0;
        }
        defaultAngle += spinRate; //Make the pattern spin
        spinRate += spinModificator; //Apply the spin modifier

        //Invert the spin if set to 1
        if (invertSpin == 1)
        {
            if (spinRate < -maxSpinRate || spinRate > maxSpinRate)
            {

                spinModificator = -spinModificator;
            }
        }
    }



    private void calculation(int i, int j, float arrayAngle, float bulletAngle)
    {
        //Calcuate the X and Y vales of the spawning points of each bullet
        float x1 = transform.position.x + xOffset;// + lengthdir_x(objectWidth, defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle);
        float y1 = transform.position.y + yOffset;// + lengthdir_y(objectHeight, defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle);

        //Calculate the direction for each bullets which it will move along
        float direction = defaultAngle + (bulletAngle * i) + (arrayAngle   * j) + startAngle;

        float bulDirX = x1 + Mathf.Sin((direction * Mathf.PI) / 180f);
        float bulDirY = y1 + Mathf.Cos((direction * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - new Vector3(x1, y1, 0f)).normalized;

        //Create a new bullet
        GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
        bul.GetComponent<Bullet>().SetTimeZero();
        bul.transform.position = new Vector3(x1, y1, 1f);
        bul.transform.rotation = transform.rotation;
        bul.SetActive(true);
        bul.GetComponent<Bullet>().SetMoveDirection(bulDir);


        //let Bullet = new bullet(x1, y1, direction, bulletSpeed, bulletAcceleration, bulletCurve, bulletTTL);

        //Spawn the newly created bullet
        //Bullet.spawn();

        //Push each bullet into the bullet array
        //bulletArray.push(Bullet);


    }


    private float lengthdir_x(float dist, float angle)
    {
        return dist * Mathf.Cos((angle * Mathf.PI) / 180);
    }


    private float lengthdir_y(float dist, float angle)
    {
        return dist * -(Mathf.Sin((angle * Mathf.PI) / 180));
    }




    private void Fire()
    {
        float angleStep = (endAngle - startAngle1) / bulletsAmount;
        float angle = startAngle1;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
    }
}
