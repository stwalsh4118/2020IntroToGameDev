﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Donut") || (other.gameObject.tag == "Pizza") || (other.gameObject.tag == "Bone"))
        {
            HealthBar.playerHP.TakeDamage();
        }

    }
}
