using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitCommand : MonoBehaviour
{

    public GameObject inputField;
    public static SubmitCommand sub;
    // Start is called before the first frame update
    void Start()
    {
        sub = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void submitCommand(string command)
    {
        inputField.GetComponent<TMP_InputField>().MoveTextEnd(true);
        inputField.GetComponent<TMP_InputField>().text = inputField.GetComponent<TMP_InputField>().text.Insert( inputField.GetComponent<TMP_InputField>().caretPosition, command);
    }

    public void getFromTxt(string text)
    {
        Debug.Log(text);
        inputField.GetComponent<TMP_InputField>().text = text;
    }

    public string grabCommands()
    {
        return inputField.GetComponent<TMP_InputField>().text;
    }
}
