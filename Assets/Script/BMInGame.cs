using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BMInGame : MonoBehaviour
{
    //inputs
    public float x = 0;
    public float y = 0;
    public float speed = 0;
    public float direction = 0;
    public float commandLength = 5f;
    public int followPlayer = 0;

    //other stuff
    [SerializeField]
    private float commandTime = 6f;
    private int commandNumber = 0;
    private string[][] commands;
    CommandReader commandReader;
    private int numCommands;
    private float[] inputs;
    public string[] defaults;
    public bool isRunningCommands = false;


    public bool Submit = false;

    public GameObject Arena;
    private float arenaCenterX;
    private float arenaCenterY;

    public Transform player;


    // Start is called before the first frame update
    void Awake()
    {
        sendDefaults();
    }
    void Start()
    {
        commandReader = GetComponent<CommandReader>();
        commands = commandReader.movementPairs;
        numCommands = commandReader.numMovementCommands;
        arenaCenterX = Arena.transform.position.x;
        arenaCenterY = Arena.transform.position.y;
        x = arenaCenterX;
        y = arenaCenterY;
    }

    // Update is called once per frame
    void Update()
    {

        if (isRunningCommands)
        {
            if (((commandTime >= commandLength) || (commandNumber == 0)) && !(numCommands == 0))
            {
                changeCommand(float.Parse(commands[commandNumber][0]), float.Parse(commands[commandNumber][1]), float.Parse(commands[commandNumber][2]), float.Parse(commands[commandNumber][3]), float.Parse(commands[commandNumber][4]), int.Parse(commands[commandNumber][5]));
                commandNumber++;
                commandTime = 0;
            }
            if (commandNumber >= numCommands)
            {
                isRunningCommands = !isRunningCommands;
            }
        }


        if (followPlayer == 0)
        {
            transform.position = new Vector2(x, y);
        }
        else
        {
            MoveTowardsPlayer();
        }
        commandTime = commandTime + Time.deltaTime;
    }

    private void changeCommand(float X, float Y, float dir, float spd, float cmdLength, int followP)
    {
        x = (X * Arena.transform.localScale.x) + arenaCenterX;
        y = (Y * Arena.transform.localScale.y) + arenaCenterY;
        direction = dir;
        speed = spd;
        commandLength = cmdLength;
        followPlayer = followP;

    }

    private void sendDefaults()
    {
        defaults = new string[6];
        defaults[0] = (x - arenaCenterX).ToString();
        defaults[1] = (y - arenaCenterY).ToString();
        defaults[2] = speed.ToString();
        defaults[3] = direction.ToString();
        defaults[4] = commandLength.ToString();
        defaults[5] = followPlayer.ToString();
    }

    public void setDefaults()
    {
        x = float.Parse(defaults[0]) + arenaCenterX;
        y = float.Parse(defaults[1]) + arenaCenterY;
        speed = float.Parse(defaults[2]);
        direction = float.Parse(defaults[3]);
        commandLength = float.Parse(defaults[4]);
        followPlayer = int.Parse(defaults[5]);

        isRunningCommands = false;
    }

    public void TestCommand()
    {
        changeCommand(inputs[0], inputs[1], inputs[2], inputs[3], inputs[4], (int)inputs[5]);
    }

    public void SubmitTheCommand()
    {
        string subcom = ((x - arenaCenterX) + "," + (y - arenaCenterY) + "," + speed + "," + direction + "," +
                commandLength + "," + followPlayer + "\n");
        SubmitCommand.sub.submitCommand(subcom);
    }

    public void LoadCommands()
    {
        commands = commandReader.movementPairs;
        commandNumber = 0;
        numCommands = commandReader.numMovementCommands;
        commandTime = 1000;
        if (isRunningCommands)
        {

        }
        else
        {
            isRunningCommands = !isRunningCommands;
        }

    }

    public void UpdateArenaCenter()
    {
        x = Arena.transform.position.x;
        transform.localScale = transform.localScale * Arena.transform.localScale.x;
        arenaCenterX = x;
        y = Arena.transform.position.y;
        arenaCenterY = y;
        transform.position = new Vector3(x, y, 0f);
    }

    public void MoveTowardsPlayer()
    {
        Vector3 playerPos = player.position;
        playerPos.z = 0;

        transform.position = Vector3.MoveTowards(transform.position, playerPos, 4f * Time.deltaTime);
    }




}
