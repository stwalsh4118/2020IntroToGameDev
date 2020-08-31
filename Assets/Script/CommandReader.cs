using System.Collections;
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
        numCommands = dataLines.Length;
        dataPairs = new string[dataLines.Length][];

        int lineNum = 0;
        foreach (string line in dataLines)
        {
            dataPairs[lineNum++] = line.Split(',');
        }
        Debug.Log(dataPairs[0][0]);  // prints "fish"
    }
    // Update is called once per frame
    void Update()
    {

    }
}
