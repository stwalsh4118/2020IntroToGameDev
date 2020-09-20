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
        base.Update();
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
