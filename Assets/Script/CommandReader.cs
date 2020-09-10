using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandReader : MonoBehaviour
{
    public TextAsset dataFile;
    public string[] dataLines;
    public string[][] dataPairs;
    public string[][] movementPairs;
    public int numBulletCommands;
    public int numMovementCommands;
    public int isMovement = 0;
    void Start()
    {
  
    }

    public void loadCommands()
    {
        dataLines = SubmitCommand.sub.grabCommands().Split('\n');
        dataPairs = new string[dataLines.Length][];
        movementPairs = new string[dataLines.Length][];

        int lineNum = 0;
        foreach (string line in dataLines)
        {
            if(line.Contains("Boss Movement"))
            {
                isMovement = 1;
                numBulletCommands = lineNum;
                lineNum = 0;
            }
            if (isMovement == 1)
            {
                if (line == "" || line.Contains("//") || line.Contains("Boss Movement"))
                {

                }
                else
                {
                    movementPairs[lineNum++] = line.Split(',');
                }
            }
            else
            {
                if (line == "" || line.Contains("//"))
                {

                }
                else
                {
                    dataPairs[lineNum++] = line.Split(',');
                }
            }
        }
        if (isMovement == 0)
        {
            numBulletCommands = lineNum;
        }
        else
        {
            numMovementCommands = lineNum;
        }
        GameObject glut = GameObject.Find("Gluttony");
        glut.GetComponent<BulletPatternGenerator>().LoadCommands();
        glut.GetComponent<BossMovement>().LoadCommands();
        isMovement = 0;
    }
}
