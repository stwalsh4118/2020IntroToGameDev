using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;


    private Queue<string> sentences;

    // Update is called once per frame
    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialog(Dialog dialog)
    {
        sentences.Clear();
        nameText.text = dialog.name;
        MessagePromptUI.ChangeSpeaker(dialog.speak);

        foreach(string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        MessagePromptUI.SetText(sentence);

    }

    public void EndDialog()
    {
        MessagePromptUI.ErasePrompt();
    }
}
