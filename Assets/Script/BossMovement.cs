using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    //inputs
    public float x = 0;
    public float y = 8;
    public float direction;
    public float speed;
    public float commandLength = 5f;

    //other stuff
    [SerializeField]
    private float commandTime = 6f;
    private int commandNumber = 0;
    private string[][] commands;
    BossMovementCommandReader commandReader;
    private int numCommands;
    public bool Submit = false;

    private float arenaCenterX;
    private float arenaCenterY;


    // Start is called before the first frame update
    void Start()
    {
        commandReader = GetComponent<BossMovementCommandReader>();
        commands = commandReader.dataPairs;
        numCommands = commandReader.numCommands;
        arenaCenterX = GetArenaCenter.centerCoordinates.x;
        arenaCenterY = GetArenaCenter.centerCoordinates.y;
    }

    // Update is called once per frame
    void Update()
    {
        commandTime = commandTime + Time.deltaTime;
        if (commandTime >= commandLength)
        {
            if (!(commandNumber >= numCommands) && !(numCommands == 0))
            {
                //changeCommand(float.Parse(commands[commandNumber][0]), float.Parse(commands[commandNumber][1]), float.Parse(commands[commandNumber][2]), float.Parse(commands[commandNumber][3]), float.Parse(commands[commandNumber][4]));
            }

            commandNumber++;
            commandTime = 0;
        }
        

        if (Submit)
        {
            Debug.Log(x + "," + y + "," + speed + "," + direction + "," + commandLength);
            Submit = false;
        }
        transform.position = new Vector2(x, y);
    }

    private void changeCommand(float X, float Y, float dir, float spd,
                                float cmdLength)
    {
        x = X + arenaCenterX;
        y = Y + arenaCenterY;
        direction = dir;
        speed = spd;
        commandLength = cmdLength;

    }



}
