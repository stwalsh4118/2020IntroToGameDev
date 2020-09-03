using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class readInput : MonoBehaviour
{
    public List<GameObject> inputFields;
    private float count = 0;
    public float[] inputValues;
    private float text = 0;
    GameObject glut;
    BulletPatternGenerator BPG;
    public string[] defaults;
    void Awake()
    {
        inputValues = new float[22];
        glut = GameObject.Find("Gluttony");
        BPG = glut.GetComponent<BulletPatternGenerator>();
        defaults = BPG.defaults;
        SetDefaults();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        count += Time.deltaTime;
        if (count >= 1)
        {
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
            count = 0;
        }
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
