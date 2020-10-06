using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

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
            string trimmedLine = Regex.Replace(line, @"\s+", "");
            if (trimmedLine.Contains("BossMovement"))
            {
                isMovement = 1;
                numBulletCommands = lineNum;
                lineNum = 0;
            }
            if (isMovement == 1)
            {
                if (trimmedLine == "" || trimmedLine.Contains("//") || trimmedLine.Contains("BossMovement"))
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
            //Debug.Log(trimmedLine);
        }
        if (isMovement == 0)
        {
            numBulletCommands = lineNum;
        }
        else
        {
            numMovementCommands = lineNum;
        }
        GameObject enemy = transform.gameObject;
        if(enemy.transform.name == "Boss") {
            if (enemy.GetComponent<BulletPatternGenerator>() != null)
            {
                enemy.GetComponent<BulletPatternGenerator>().LoadCommands();
                if(enemy.GetComponent<BossMovement>() != null) {
                    enemy.GetComponent<BossMovement>().LoadCommands();
                }

            }
            else
            {
                enemy.GetComponent<BPGInGame>().LoadCommands();
                if(enemy.GetComponent<BMInGame>() != null) {
                    enemy.GetComponent<BMInGame>().LoadCommands();
                }
            }
            isMovement = 0;
        }
    }
}
