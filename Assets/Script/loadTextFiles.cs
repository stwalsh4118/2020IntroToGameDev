using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class loadTextFiles : MonoBehaviour
{
    public string[] filePaths;
    public string[] fileNames;
    // Start is called before the first frame update
    void Start()
    {
        filePaths = Directory.GetFiles(Application.dataPath + "/BossCommands", "*.txt");
        fileNames = new string[filePaths.Length];
        int i = 0;
        foreach (string files in filePaths)
        {
            Debug.Log(Path.GetFileName(files));
            fileNames[i] = Path.GetFileName(files);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
