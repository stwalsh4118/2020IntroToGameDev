﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{

    public string loadTo;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= radius)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    public override void Interact()
    {
        GameObject.FindObjectOfType<Player>().SavePlayerData();
        LoadManager.loadScene(loadTo);
    }

    
}