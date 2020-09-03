﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandReader : MonoBehaviour
{
    public TextAsset dataFile;
    public string[] dataLines;
    public string[][] dataPairs;
    public int numCommands;
    void Start()
    {
        dataLines = dataFile.text.Split('\n');
        dataPairs = new string[dataLines.Length][];

        int lineNum = 0;
        foreach (string line in dataLines)
        {
            if (!(line == ""))
            {
                dataPairs[lineNum++] = line.Split(',');
            }
        }
        numCommands = lineNum;
    }

    public void loadCommands()
    {
        dataLines = SubmitCommand.sub.grabCommands().Split('\n');
        dataPairs = new string[dataLines.Length][];

        int lineNum = 0;
        foreach (string line in dataLines)
        {
            if (!(line == ""))
            {
                dataPairs[lineNum++] = line.Split(',');
            }
        }
        numCommands = lineNum;
        GameObject glut = GameObject.Find("Gluttony");
        glut.GetComponent<BulletPatternGenerator>().LoadCommands();
    }
}
