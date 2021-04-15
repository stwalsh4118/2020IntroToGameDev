using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopInteractable : Interactable
{

    public GameObject ShopInterface;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (promptRaised && distance > radius)
        {

            MessagePromptUI.ErasePrompt();
            promptRaised = false;
            isInteracting = false;
        }
        if (distance <= radius && !isInteracting)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                promptRaised = true;
                Interact();
                isInteracting = true;
            }
        }
    }

    public override void Interact()
    {
        StateManager.Instance.inMenu = true;
        ShopInterface.SetActive(true);
    }
}
