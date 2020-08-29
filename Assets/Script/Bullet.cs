﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector2 moveDirection;
    private float bulletLife = 5f;
    public float countTime = 0;


    private void OnEnable()
    {
        //Invoke("Destroy", 3f);
    }
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        countTime = countTime + Time.deltaTime;
        if(countTime >= bulletLife)
        {
            countTime = 0;
            Destroy();
        }
        if(gameObject.activeInHierarchy == false)
        {
            countTime = 0;
        }
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }
        
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetTimeZero()
    {
        countTime = 0;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
