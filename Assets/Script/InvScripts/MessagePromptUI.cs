// Atach to an UI Text object placed wherever you like on the canvas.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessagePromptUI : MonoBehaviour {
	private static TextMeshProUGUI promptText;
	private static GameObject TextBubble;
	private static GameObject speaker;
	public static bool InDialog;

	// Use this for initialization
	void Start () {
		InDialog = false;
		TextBubble = GameObject.Find("TextBubble");
		promptText = GameObject.Find("DialogBox").GetComponent<TextMeshProUGUI>();
		speaker = GameObject.Find("Speaker");
		TextBubble.SetActive(false);
		speaker.SetActive(false);
	}


	public static void PickUpPrompt (string itemName) {

		TextBubble.SetActive(true);
		speaker.SetActive(true);
		InDialog = true;
		promptText.text = "Press E to pick the " + itemName;
	}

	public static void ClearAreaPrompt () {
		promptText.text = "This area looks good. Press H to signal the flare.";
	}

	public static void ErasePrompt () {
		promptText.text = "";
		TextBubble.SetActive(false);
		speaker.SetActive(false);
		InDialog = false;
	}

	public static void SetText(string text)
    {
		InDialog = true;
		TextBubble.SetActive(true);
		speaker.SetActive(true);
		promptText.text = text;
    }

	public static void ChangeSpeaker(Sprite speak)
    {
		speaker.GetComponent<SpriteRenderer>().sprite = speak;
    }

}
