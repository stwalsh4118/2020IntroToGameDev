using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;


    private Queue<string> sentences;

    public static DialogManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set StateManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
        sentences = new Queue<string>();
        nameText = GameObject.Find("SpeakerName").GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialog(Dialog dialog)
    {
        sentences.Clear();
        nameText.text = dialog.name;
        MessagePromptUI.ChangeSpeaker(dialog.speak);

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
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

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        sentences = new Queue<string>();
        nameText = GameObject.Find("SpeakerName").GetComponent<TextMeshProUGUI>();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

}
