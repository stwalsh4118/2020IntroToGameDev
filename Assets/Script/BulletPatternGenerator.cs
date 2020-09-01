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
    public float endAngle = 360;

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
    public float fireRate = .2f;

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
    public float bulletTTL = 10;


    private int bulletLength;

    private int arrrayLength = 0;

    private float arrayAngle;
    private float bulletAngle;

    private float countTime = 0;


    [SerializeField]
    public bool Throw = false;

    [SerializeField]
    public bool Submit = false;

    private Vector2 bulletMoveDirection;
    public float commandLength = 5f;
    [SerializeField]
    private float commandTime = 6f;
    private int commandNumber = 0;
    private string[][] commands;

    Animator animator;
    CommandReader commandReader;
    private int numCommands;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        commandReader = GetComponent<CommandReader>();
        commands = commandReader.dataPairs;
        numCommands = commandReader.numCommands;
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
        commandTime = commandTime + Time.deltaTime;
        if(commandTime >= commandLength)
        {
            if (!(commandNumber >= numCommands))
            {
                changeCommand(int.Parse(commands[commandNumber][0]), int.Parse(commands[commandNumber][1]), float.Parse(commands[commandNumber][2]), float.Parse(commands[commandNumber][3]),
                              float.Parse(commands[commandNumber][4]), float.Parse(commands[commandNumber][5]), float.Parse(commands[commandNumber][6]), float.Parse(commands[commandNumber][7]),
                             float.Parse(commands[commandNumber][8]), float.Parse(commands[commandNumber][9]), int.Parse(commands[commandNumber][10]), float.Parse(commands[commandNumber][11]),
                             float.Parse(commands[commandNumber][12]), float.Parse(commands[commandNumber][13]), float.Parse(commands[commandNumber][14]), float.Parse(commands[commandNumber][15]),
                             float.Parse(commands[commandNumber][16]), float.Parse(commands[commandNumber][17]), float.Parse(commands[commandNumber][18]), float.Parse(commands[commandNumber][19]),
                              float.Parse(commands[commandNumber][20]), float.Parse(commands[commandNumber][21]));
            }
                          
            commandNumber++;
            commandTime = 0;
        }


        if (countTime >= fireRate)
        {
            countTime = 0;
            Generate();
        }
       
        if (Throw)
        {
            animator.SetTrigger("Throw");
            Throw = false;
        }

        if (Submit)
        {
            Debug.Log(patternArrays + "," + bulletsPerArrays + "," + spreadBetweenArray + "," + spreadWithinArray + "," +
                startAngle + "," + defaultAngle + "," + endAngle + "," + beginSpinSpeed + "," + spinRate + "," + spinModificator + "," +
                invertSpin + "," + maxSpinRate + "," + fireRate + "," + objectWidth + "," + objectHeight + "," + xOffset +
                "," + yOffset + "," + bulletSpeed + "," + bulletAcceleration + "," + bulletCurve + "," + bulletTTL + "," +
                commandLength);
            Submit = false;
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
        if (defaultAngle > endAngle)
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


        float x2 = transform.position.x + xOffset + lengthdir_x(objectWidth, defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle);
        float y2 = transform.position.y + yOffset + lengthdir_y(objectHeight, defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle);
        //Calculate the direction for each bullets which it will move along
        float direction = defaultAngle + (bulletAngle * i) + (arrayAngle   * j) + startAngle;


        //Create a new bullet
        GameObject bul = BulletPool.bulletPoolInstance.GetBullet("Bullet2");
        bul.GetComponent<Bullet>().SetTimeZero();
        bul.transform.position = new Vector3(x2, y2, 1f);
        bul.transform.rotation = transform.rotation;
        bul.GetComponent<Bullet>().SetXY(x2, y2);
        bul.GetComponent<Bullet>().SetMoveDirection(direction);
        bul.GetComponent<Bullet>().SetCurve(bulletCurve);
        bul.GetComponent<Bullet>().SetAcceleration(bulletAcceleration);
        bul.GetComponent<Bullet>().SetMoveSpeed(bulletSpeed);
        bul.GetComponent<Bullet>().SetBulletLife(bulletTTL);
        bul.SetActive(true);


    }


    private float lengthdir_x(float dist, float angle)
    {
        return dist * Mathf.Cos((angle * Mathf.PI) / 180);
    }


    private float lengthdir_y(float dist, float angle)
    {
        return dist * -(Mathf.Sin((angle * Mathf.PI) / 180));
    }

    private void changeCommand(int patArrays, int bulPerArray, float spreadBetweenArr, float spreadWithinArr,
                                float startAng, float defaultAng, float endAng, float beginSpinSpd, float spinRat,
                                float spinMod, int invSpin, float maxSpin, float fireRat, float objWidth, float objHeight,
                                float xOff, float yOff, float bulletSpd, float bulletAccel, float bulletCurv, float bulletLife,
                                float cmdLength)
    {
        patternArrays = patArrays;
        bulletsPerArrays = bulPerArray;
        spreadBetweenArray = spreadBetweenArr;
        spreadWithinArray = spreadWithinArr;
        startAngle = startAng;
        defaultAngle = defaultAng;
        endAngle = endAng;
        beginSpinSpeed = beginSpinSpd;
        spinRate = spinRat;
        spinModificator = spinMod;
        invertSpin = invSpin;
        maxSpinRate = maxSpin;
        fireRate = fireRat;
        objectWidth = objWidth;
        objectHeight = objHeight;
        xOffset = xOff;
        yOffset = yOff;
        bulletSpeed = bulletSpd;
        bulletAcceleration = bulletAccel;
        bulletCurve = bulletCurv;
        bulletTTL = bulletLife;
        commandLength = cmdLength;

    }


}
