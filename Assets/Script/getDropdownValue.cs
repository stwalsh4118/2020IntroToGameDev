using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getDropdownValue : MonoBehaviour
{
    Dropdown D;
    public string value;
    // Start is called before the first frame update
    void Start()
    {
        D = GetComponent<Dropdown>();
        setDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        value = D.options[D.value].text;
    }

    void setDropdown()
    {
        string[] fileN;
        fileN = GetComponent<loadTextFiles>().fileNames;
        foreach(string fileName in fileN)
        {
            Dropdown.OptionData m_NewData = new Dropdown.OptionData();
            m_NewData.text = fileName;
            D.options.Add(m_NewData);
        }
    }
}
