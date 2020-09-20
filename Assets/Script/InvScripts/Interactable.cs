using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

	public float radius = 1.5f;
	private Transform player;
	private bool isInteracting = false;
	public bool disappearOnExitScreen = false;

	private bool promptRaised = false;

	protected virtual void Start () {
		player = GameObject.Find("Character").transform;
	}
	public virtual void Interact(){
		// This method is meant to be overwritten by other classes.
		Debug.Log("Interacting with " + transform.name);
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, radius);		
	}

	protected virtual void Update(){

		float distance = Vector3.Distance(player.position, transform.position);

		if (promptRaised && distance > radius){

			MessagePromptUI.ErasePrompt();
			promptRaised = false;
			isInteracting = false;
		}
		if (distance <= radius && !isInteracting){ 

			if (Input.GetKeyDown(KeyCode.E)) {
				promptRaised = true;
				MessagePromptUI.SetText("");
				Interact();
				isInteracting = true;
			}
		}
	}

	void OnBecameInvisible()
    {
		if(disappearOnExitScreen)
        {
			transform.gameObject.SetActive(false);
        }
    }
}    

