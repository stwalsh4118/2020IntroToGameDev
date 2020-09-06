using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class readMovementInput : MonoBehaviour
{
    public List<GameObject> inputFields;
    private float count = 0;
    public float[] inputValues;
    private float text = 0;
    GameObject glut;
    BossMovement BM;
    public string[] defaults;
    void Awake()
    {
        inputValues = new float[5];
        glut = GameObject.Find("Gluttony");
        BM = glut.GetComponent<BossMovement>();

    }
    void Start()
    {
        defaults = BM.defaults;
        SetDefaults();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (GameObject input in inputFields)
        {
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
