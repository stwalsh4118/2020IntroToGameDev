using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : Interactable {
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
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

    public override void Interact()
    {
        DialogTrigger DT = GetComponent<DialogTrigger>();
        if(DT)
        {
            DT.TriggerDialog();
        }
    }
}
