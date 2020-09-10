using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class getDropdownValue : MonoBehaviour
{
    Dropdown D;
    public string value;
    public string currentCommands;
    StreamReader SR;
    public GameObject inputFileName;
    // Start is called before the first frame update
    void Start()
    {
        D = GetComponent<Dropdown>();
        setDropdown();
        value = D.options[D.value].text;
        SR = new StreamReader(GetComponent<loadTextFiles>().filePaths[D.value]);
        currentCommands = SR.ReadToEnd();
        SR.Close();
        SubmitCommand.sub.getFromTxt(currentCommands);
    }

    // Update is called once per frame
    void Update()
    {
        value = D.options[D.value].text;
        SR = new StreamReader(GetComponent<loadTextFiles>().filePaths[D.value]);
        currentCommands = SR.ReadToEnd();
        SR.Close();
        //Debug.Log(currentCommands.text);
    }

    void setDropdown()
    {
        D.ClearOptions();
        string[] fileN;
        fileN = GetComponent<loadTextFiles>().fileNames;
        foreach(string fileName in fileN)
        {
            Dropdown.OptionData m_NewData = new Dropdown.OptionData();
            m_NewData.text = fileName;
            D.options.Add(m_NewData);
        }
        D.RefreshShownValue();
    }

    public void getCommandsFromDropdown()
    {
        value = D.options[D.value].text;
        SR = new StreamReader(GetComponent<loadTextFiles>().filePaths[D.value]);
        currentCommands = SR.ReadToEnd();
        SR.Close();
        SubmitCommand.sub.getFromTxt(currentCommands);
    }

    public void saveToFile()
    {
        File.WriteAllText(GetComponent<loadTextFiles>().filePaths[D.value], SubmitCommand.sub.grabCommands());
    }


    public void saveToNewFile()
    {
        string newFileName = inputFileName.GetComponent<TMP_InputField>().text;
        string filePath;
        if (Application.isEditor)
        {
            filePath = Application.dataPath + "/BossCommands/";
        }
        else
        {
            filePath = Application.dataPath + "/";
        }
        filePath = filePath + newFileName + ".txt";
        File.WriteAllText(filePath, SubmitCommand.sub.grabCommands());
        GetComponent<loadTextFiles>().UpdateFiles();
        setDropdown();
    }
}
