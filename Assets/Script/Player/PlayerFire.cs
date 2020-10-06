using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    [SerializeField] private int patternArrays = 1; //Total bullet arrays
    [SerializeField] private int bulletsPerArrays = 1; //Bullets per Array

    //Angle Variables
    [SerializeField] private float spreadBetweenArray = 90; //spread between arrays
    [SerializeField] private float spreadWithinArray = 20; //spread between the last and the first bullet of an array
    [SerializeField] private float startAngle = 0; //Start angle
    [SerializeField] private float defaultAngle = 0;
    [SerializeField] private float endAngle = 360;

    //Spinning Variables
    [SerializeField] private float beginSpinSpeed = 0;
    [SerializeField] private float spinRate = 0; // The rate at which the pattern is spinning
    [SerializeField] private float spinModificator = 0; // The modificator of the spin rate
    [SerializeField] private int invertSpin = 1; // (1 = spinRate gets inversed once SpinRate >= maxSpinRate  || 0 = Spin doesn't invert at all)
    [SerializeField] private float maxSpinRate = 10; //The max spin rate ->if SpinRate >= maxSpinRate --> inverts spin

    //Fire Rate Variables
    [SerializeField] private float fireRate = .1f;

    //Offsets 
    [SerializeField] private float objectWidth = 0; //Width of the bullet firing object
    [SerializeField] private float objectHeight = 0; //Height of the bullet firing object
    [SerializeField] private float xOffset = 0; //Shift spawn point of the bullets along the X-Axis
    [SerializeField] private float yOffset = 0; //Shift spawn point of the bulltes along the Y-Axis

    //Bullet Variables
    [SerializeField] private float bulletSpeed = 20;
    [SerializeField] private float bulletAcceleration = 0;
    [SerializeField] private float bulletCurve = 0;
    [SerializeField] private float bulletTTL = 5;
    [SerializeField] private float commandLength = 5f;
    [SerializeField] private string bullettag = "Arrow";

    private int bulletLength;

    private int arrrayLength = 0;

    private float arrayAngle;
    private float bulletAngle;


    private Vector2 bulletMoveDirection;
    private Vector3 directionVector;

    public float count = 0;
    PlayerState lpd;

    // Start is called before the first frame update
    private void Awake()
    {
        lpd = GetComponentInChildren<Player>().localPlayerData;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && GetComponentInChildren<Player>().localPlayerData.weapon != null)
        {
            if (count >= fireRate && !StateManager.Instance.inMenu && !GetComponent<playerMovement>().isRolling)
            {
                SoundManager.Instance.Play(GetComponentInChildren<Player>().localPlayerData.weapon.shootSound);
                Fire();
                count = 0;
            }
            count += Time.deltaTime;
        }
    }

    private void Fire()
    {
        spreadBetweenArray = 360f / patternArrays;

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
                spinRate += spinModificator;
            }
        }
    }

    private void calculation(int i, int j, float arrayAngle, float bulletAngle)
    {

        float x2 = transform.position.x + xOffset + lengthdir_x(objectWidth, defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle);
        float y2 = transform.position.y + yOffset + lengthdir_y(objectHeight, defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle);
        //Calculate the direction for each bullets which it will move along
        if (bulletsPerArrays == 1)
        {
            startAngle = ShootAtMouse();
        }
        else
        {
            startAngle = ShootAtMouse() - (spreadWithinArray) / 2f;
        }

        float direction = defaultAngle + (bulletAngle * i) + (arrayAngle * j) + startAngle;


        //Create a new bullet
        GameObject bul = BulletPool.bulletPoolInstance.GetBullet(bullettag);
        
        bul.GetComponent<Bullet>().bulletProperties = Inventory.Instance.IV.bulletProperties;
        bul.GetComponent<Bullet>().numAvailBounces = CalculateNumBounces();
        bul.GetComponent<Bullet>().SetDefault();
        bul.GetComponent<Bullet>().SetTimeZero();
        bul.GetComponent<Bullet>().SetAcceleration(Inventory.Instance.IV.bulletAccelerationIncrease);
        bul.transform.position = new Vector3(x2, y2, 1f);
        bul.GetComponent<Bullet>().SetXY(x2, y2);
        bul.GetComponent<Bullet>().SetMoveDirection(direction);
        bul.GetComponent<Bullet>().SetCurve(bulletCurve);
        bul.GetComponent<Bullet>().SetMoveSpeed(bulletSpeed);
        bul.GetComponent<Bullet>().SetBulletLife(bulletTTL);
        bul.GetComponent<Bullet>().SetDamage(CalculateDamage());
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

    private float ShootAtMouse()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        directionVector = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(directionVector.x, directionVector.y);
        angle = angle * 180 / Mathf.PI;
        angle = angle - 90;
        return angle;
    }

    private int CalculateDamage()
    {
        float maxWeaponDamage = lpd.weapon.maxDamage;
        float minWeaponDamage = lpd.weapon.minDamage;

        float cal = Random.Range(minWeaponDamage, maxWeaponDamage);
        cal += cal * (Inventory.Instance.IV.damageIncreaseAdditive / 100);

        foreach (float dmg in Inventory.Instance.IV.damageIncreaseMultiplicative)
        {
            cal *= (dmg / 100);
        }
        return (int)Mathf.Ceil(cal);
    }

    private int CalculateNumBounces()
    {
        if (Inventory.Instance.IV.bulletProperties.Exists(x => x == "bounce"))
        {
            return Inventory.Instance.inventory.Find(x => x.ItemName() == "Cheap Rubber Ball").numberInInventory;
        }
        else return 0;
    }

    public void UpdateWeaponShotPattern()
    {
        lpd = GetComponentInChildren<Player>().localPlayerData;
        Weapons newWeapon = lpd.weapon;
        bulletsPerArrays = newWeapon.numBullets;
        patternArrays = newWeapon.numBulletGroups;
        spreadWithinArray = newWeapon.spread;
    }
}
