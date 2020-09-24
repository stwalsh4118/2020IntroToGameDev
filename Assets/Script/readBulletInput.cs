using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class readBulletInput : MonoBehaviour
{
    public List<GameObject> inputFields;
    public GameObject bulletSpriteDropdown;
    private float count = 0;
    public float[] inputValues;
    private float text = 0;
    GameObject glut;
    BulletPatternGenerator BPG;
    public string[] defaults;
    public string tag;
    void Awake()
    {
        inputValues = new float[22];
        glut = GameObject.Find("Boss");
        BPG = glut.GetComponent<BulletPatternGenerator>();
        defaults = BPG.defaults;
        SetDefaults();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
            foreach (GameObject input in inputFields) {
                string preParse = input.GetComponent<TMP_InputField>().text;
                if (preParse == "")
                {

                }
                else
                {
                    text = float.Parse(preParse);
                }
                inputValues[i] = text;
                text = 0;
                i++;
            }
            //Debug.Log(bulletSpriteDropdown.GetComponent<Dropdown>().options[bulletSpriteDropdown.GetComponent<Dropdown>().value].text);
            tag = bulletSpriteDropdown.GetComponent<Dropdown>().options[bulletSpriteDropdown.GetComponent<Dropdown>().value].text;

    }

    public void SetDefaults()
    {
        int i = 0;
        foreach (GameObject input in inputFields)
        {
            input.GetComponent<TMP_InputField>().text = defaults[i];
            i++;
        }
    }
}
