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
        loadCommands();
    }

    public void loadCommands()
    {
        if (!dataFile)
        {
            dataLines = SubmitCommand.sub.grabCommands().Split('\n');
        }
        else
        {
            dataLines = dataFile.text.Split('\n');
        }
        dataPairs = new string[dataLines.Length][];
        movementPairs = new string[dataLines.Length][];

        int lineNum = 0;
        foreach (string line in dataLines)
        {
            string trimmedLine = line.Trim();
            if(trimmedLine.Contains("Boss Movement"))
            {
                isMovement = 1;
                numBulletCommands = lineNum;
                lineNum = 0;
            }
            if (isMovement == 1)
            {
                if (trimmedLine == "" || trimmedLine.Contains("//") || trimmedLine.Contains("Boss Movement") || trimmedLine == " ")
                {

                }
                else
                {
                    movementPairs[lineNum++] = trimmedLine.Split(',');
                }
            }
            else
            {
                if (trimmedLine == "" || trimmedLine.Contains("//"))
                {

                }
                else
                {
                    dataPairs[lineNum++] = trimmedLine.Split(',');
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
        GameObject glut = GameObject.Find("Boss");
        if (glut.GetComponent<BulletPatternGenerator>() != null)
        {
            glut.GetComponent<BulletPatternGenerator>().LoadCommands();
            glut.GetComponent<BossMovement>().LoadCommands();
        }
        else
        {
            glut.GetComponent<BPGInGame>().LoadCommands();
            glut.GetComponent<BMInGame>().LoadCommands();
        }
        isMovement = 0;
    }
}
