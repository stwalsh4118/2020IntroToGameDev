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
        if (Application.isEditor)
        {
            filePaths = Directory.GetFiles(Application.dataPath + "/BossCommands", "*.txt");
        }
        else
        {
            filePaths = Directory.GetFiles(Application.dataPath, "*.txt");
        }
        fileNames = new string[filePaths.Length];
        int i = 0;
        foreach (string files in filePaths)
        {
            Debug.Log(Path.GetFileNameWithoutExtension(files));
            fileNames[i] = Path.GetFileNameWithoutExtension(files);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateFiles()
    {
        if (Application.isEditor)
        {
            filePaths = Directory.GetFiles(Application.dataPath + "/BossCommands", "*.txt");
        }
        else
        {
            filePaths = Directory.GetFiles(Application.dataPath, "*.txt");
        }
        fileNames = new string[filePaths.Length];
        int i = 0;
        foreach (string files in filePaths)
        {
            Debug.Log(Path.GetFileNameWithoutExtension(files));
            fileNames[i] = Path.GetFileNameWithoutExtension(files);
            i++;
        }
    }
}
