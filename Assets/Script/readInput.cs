using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class readInput : MonoBehaviour
{
    public List<GameObject> inputFields;
    private float count = 0;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count >= 1)
        {
            foreach (GameObject input in inputFields) {
                Debug.Log("wtf");
                string text = input.GetComponent<TMP_InputField>().text;
                Debug.Log(text);
                count = 0;
                if (text == "load")
                {
                    SceneManager.LoadScene("sceneexperiment");
                }
            }
        }
    }
}
