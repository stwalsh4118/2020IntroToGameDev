using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetCommands : MonoBehaviour
{
    public static GetCommands getcmd;
    public GameObject inputField;
    // Start is called before the first frame update
    void Start()
    {
        getcmd = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string grabCommands()
    {
        return inputField.GetComponent<TMP_InputField>().text;
    }
}
