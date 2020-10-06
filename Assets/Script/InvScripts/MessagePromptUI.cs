// Atach to an UI Text object placed wherever you like on the canvas.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MessagePromptUI : MonoBehaviour
{
    private static TextMeshProUGUI promptText;
    private static GameObject TextBubble;
    private static GameObject speaker;

    // Use this for initialization
    void Awake()
    {
        StateManager.Instance.inMenu = false;
        TextBubble = GameObject.Find("TextBubble");
        promptText = GameObject.Find("DialogBox").GetComponent<TextMeshProUGUI>();
        speaker = GameObject.Find("Speaker");
    }

    private void Start()
    {
        TextBubble.SetActive(false);
        speaker.SetActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }


    public static void PickUpPrompt(string itemName)
    {

        TextBubble.SetActive(true);
        speaker.SetActive(true);
        StateManager.Instance.inMenu = true;
        promptText.text = "Press E to pick the " + itemName;
    }

    public static void ClearAreaPrompt()
    {
        promptText.text = "This area looks good. Press H to signal the flare.";
    }

    public static void ErasePrompt()
    {
        promptText.text = "";
        TextBubble.SetActive(false);
        speaker.SetActive(false);
        StateManager.Instance.inMenu = false;
    }

    public static void SetText(string text)
    {
        StateManager.Instance.inMenu = true;
        TextBubble.SetActive(true);
        speaker.SetActive(true);
        promptText.text = text;
    }

    public static void ChangeSpeaker(Sprite speak)
    {
        speaker.GetComponent<SpriteRenderer>().sprite = speak;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        StateManager.Instance.inMenu = false;
        TextBubble = GameObject.Find("TextBubble");
        promptText = GameObject.Find("DialogBox").GetComponent<TextMeshProUGUI>();
        speaker = GameObject.Find("Speaker");
        TextBubble.SetActive(false);
        speaker.SetActive(false);

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

}
