using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class readInput : MonoBehaviour
{

    public GameObject inputField;
    private float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count >= 1)
        {
            string text = inputField.GetComponent<TMP_InputField>().text;
            Debug.Log(text);
            count = 0;
            if(text == "load")
            {
                SceneManager.LoadScene("sceneexperiment");
            }
        }
    }
}
