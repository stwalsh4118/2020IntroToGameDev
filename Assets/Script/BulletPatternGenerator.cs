using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPatternGenerator : MonoBehaviour
{
    

    [SerializeField]
    public int patternArrays = 4; //Total bullet arrays
    [SerializeField]
    public int bulletsPerArrays = 1; //Bullets per Array

    //Angle Variables
    [SerializeField]
    public float spreadBetweenArray = 90; //spread between arrays
    [SerializeField]
    public float spreadWithinArray = 0; //spread between the last and the first bullet of an array
    [SerializeField]
    public float startAngle = 0; //Start angle
    [SerializeField]
    public float defaultAngle = 0;
    public float endAngle = 360;

    //Spinning Variables
    [SerializeField]
    public float beginSpinSpeed = 0;
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
    public float fireRate = 1f;

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
    public float bulletSpeed = 5;
    [SerializeField]
    public float bulletAcceleration = 0;
    [SerializeField]
    public float bulletCurve = 0;
    [SerializeField]
    public float bulletTTL = 5;
    public float commandLength = 5f;
    public string tag = "Donut";

    private int bulletLength;

    private int arrrayLength = 0;

    private float arrayAngle;
    private float bulletAngle;

    private float countTime = 0;


    [SerializeField]
    public bool Throw = false;

    private Vector2 bulletMoveDirection;

    [SerializeField]
    private float commandTime = 6f;
    private int commandNumber = 0;
    private string[][] commands;

    Animator animator;
    CommandReader bulletcommandReader;
    private int numCommands;
    GameObject InputReader;
    readBulletInput rI;
    private float[] inputs;
    public string[] defaults;
    public bool isRunningCommands = false;
    string tagInput;



    // Start is called before the first frame update
    void Awake()
    {
        sendDefaults();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        bulletcommandReader = GetComponent<CommandReader>();
        commands = bulletcommandReader.dataPairs;
        numCommands = bulletcommandReader.numBulletCommands;
        InputReader = GameObject.Find("InputReader");
        rI = InputReader.GetComponent<readBulletInput>();
    }




    void Update()
    {
        inputs = rI.inputValues;
        tagInput = rI.tag;

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

        //run commands
        if (isRunningCommands)
        {
            if (((commandTime >= commandLength) || (commandNumber == 0)) && !(numCommands == 0))
            {
                changeCommand(int.Parse(commands[commandNumber][0]), int.Parse(commands[commandNumber][1]), float.Parse(commands[commandNumber][2]), float.Parse(commands[commandNumber][3]),
                              float.Parse(commands[commandNumber][4]), float.Parse(commands[commandNumber][5]), float.Parse(commands[commandNumber][6]), float.Parse(commands[commandNumber][7]),
                             float.Parse(commands[commandNumber][8]), float.Parse(commands[commandNumber][9]), int.Parse(commands[commandNumber][10]), float.Parse(commands[commandNumber][11]),
                             float.Parse(commands[commandNumber][12]), float.Parse(commands[commandNumber][13]), float.Parse(commands[commandNumber][14]), float.Parse(commands[commandNumber][15]),
                             float.Parse(commands[commandNumber][16]), float.Parse(commands[commandNumber][17]), float.Parse(commands[commandNumber][18]), float.Parse(commands[commandNumber][19]),
                              float.Parse(commands[commandNumber][20]), float.Parse(commands[commandNumber][21]), commands[commandNumber][22]);
                /*Debug.Log(patternArrays + "," + bulletsPerArrays + "," + spreadBetweenArray + "," + spreadWithinArray + "," +
                startAngle + "," + defaultAngle + "," + endAngle + "," + beginSpinSpeed + "," + spinRate + "," + spinModificator + "," +
                invertSpin + "," + maxSpinRate + "," + fireRate + "," + objectWidth + "," + objectHeight + "," + xOffset +
                "," + yOffset + "," + bulletSpeed + "," + bulletAcceleration + "," + bulletCurve + "," + bulletTTL + "," +
                 commandLength + "," + tag);
                 */
                commandNumber++;
                commandTime = 0;
            }
            if(commandNumber >= numCommands)
            {
                isRunningCommands = !isRunningCommands;
                setZeros();
            }
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

        
        countTime = countTime + Time.deltaTime;
        commandTime = commandTime + Time.deltaTime;
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
                spinRate += spinModificator;
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
        //Debug.Log(tag);
        GameObject bul = BulletPool.bulletPoolInstance.GetBullet(tag);
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
                                float cmdLength, string bulletTag)
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
        tag = bulletTag;

    }

    private void sendDefaults()
    {
        defaults = new string[23];
        defaults[0] = patternArrays.ToString();
        defaults[1] = bulletsPerArrays.ToString();
        defaults[2] = spreadBetweenArray.ToString();
        defaults[3] = spreadWithinArray.ToString();
        defaults[4] = startAngle.ToString();
        defaults[5] = defaultAngle.ToString();
        defaults[6] = endAngle.ToString();
        defaults[7] = beginSpinSpeed.ToString();
        defaults[8] = spinRate.ToString();
        defaults[9] = spinModificator.ToString();
        defaults[10] = invertSpin.ToString();
        defaults[11] = maxSpinRate.ToString();
        defaults[12] = fireRate.ToString();
        defaults[13] = objectWidth.ToString();
        defaults[14] = objectHeight.ToString();
        defaults[15] = xOffset.ToString();
        defaults[16] = yOffset.ToString();
        defaults[17] = bulletSpeed.ToString();
        defaults[18] = bulletAcceleration.ToString();
        defaults[19] = bulletCurve.ToString();
        defaults[20] = bulletTTL.ToString();
        defaults[21] = commandLength.ToString();
        defaults[22] = tag;

    }

    public void setDefaults()
    {
        patternArrays = int.Parse(defaults[0]);
        bulletsPerArrays = int.Parse(defaults[1]);
        spreadBetweenArray = float.Parse(defaults[2]);
        spreadWithinArray = float.Parse(defaults[3]);
        startAngle = float.Parse(defaults[4]);
        defaultAngle = float.Parse(defaults[5]);
        endAngle = float.Parse(defaults[6]);
        beginSpinSpeed = float.Parse(defaults[7]);
        spinRate = float.Parse(defaults[8]);
        spinModificator = float.Parse(defaults[9]);
        invertSpin = int.Parse(defaults[10]);
        maxSpinRate = float.Parse(defaults[11]);
        fireRate = float.Parse(defaults[12]);
        objectWidth = float.Parse(defaults[13]);
        objectHeight = float.Parse(defaults[14]);
        objectHeight = float.Parse(defaults[15]);
        yOffset = float.Parse(defaults[16]);
        bulletSpeed = float.Parse(defaults[17]);
        bulletAcceleration = float.Parse(defaults[18]);
        bulletCurve = float.Parse(defaults[19]);
        bulletTTL = float.Parse(defaults[20]);
        commandLength = float.Parse(defaults[21]);
        tag = (defaults[22]);
        isRunningCommands = false;
    }

    public void setZeros()
    {
        patternArrays = 0;
        bulletsPerArrays = 0;
        spreadBetweenArray = 0;
        spreadWithinArray = 0;
        startAngle = 0;
        defaultAngle = 0;
        endAngle = 0;
        beginSpinSpeed = 0;
        spinRate = 0;
        spinModificator = 0;
        invertSpin = 0;
        maxSpinRate = 0;
        fireRate = 0;
        objectWidth = 0;
        objectHeight = 0;
        objectHeight = 0;
        yOffset = 0;
        bulletSpeed = 0;
        bulletAcceleration = 0;
        bulletCurve = 0;
        bulletTTL = 0;
        commandLength = 0;
        isRunningCommands = false;
    }

    public void TestCommand()
    {
        changeCommand((int)inputs[0], (int)inputs[1], inputs[2], inputs[3], inputs[4],
                          inputs[5], inputs[6], inputs[7], inputs[8], inputs[9],
                     (int)inputs[10], inputs[11], inputs[12], inputs[13], inputs[14],
                          inputs[15], inputs[16], inputs[17], inputs[18], inputs[19],
                          inputs[20], inputs[21], tagInput);
    }

    public void SubmitTheCommand()
    {
        string subcom = (patternArrays + "," + bulletsPerArrays + "," + spreadBetweenArray + "," + spreadWithinArray + "," +
                startAngle + "," + defaultAngle + "," + endAngle + "," + beginSpinSpeed + "," + spinRate + "," + spinModificator + "," +
                invertSpin + "," + maxSpinRate + "," + fireRate + "," + objectWidth + "," + objectHeight + "," + xOffset +
                "," + yOffset + "," + bulletSpeed + "," + bulletAcceleration + "," + bulletCurve + "," + bulletTTL + "," +
                commandLength + "," + tag + "\n");
        SubmitCommand.sub.submitCommand(subcom);
    }

    public void LoadCommands()
    {
        commands = bulletcommandReader.dataPairs;
        commandNumber = 0;
        countTime = 0;
        numCommands = bulletcommandReader.numBulletCommands;
        commandTime = 1000;
        if(isRunningCommands)
        {

        }
        else
        {
            isRunningCommands = !isRunningCommands;
        }
        
    }

}
