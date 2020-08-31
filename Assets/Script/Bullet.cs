using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private float moveDirection;
    public float bulletLife = 5f;
    public float defaultMS = 5f;
    public float countTime = 0;
    public float acceleration = 0;
    public float curve = 0;
    public float x1;
    public float y1;


    private void OnEnable()
    {
        //Invoke("Destroy", 3f);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = moveDirection + curve * Time.deltaTime;
        moveSpeed = moveSpeed + acceleration * Time.deltaTime;


        float bulDirX = xDir(moveDirection);
        float bulDirY = yDir(moveDirection);

        float x = transform.position.x + bulDirX * moveSpeed * Time.deltaTime;
        float y = transform.position.y + bulDirY * moveSpeed * Time.deltaTime;

        Vector3 move = new Vector3(x, y, 1f);
        transform.position = move;
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

    public void SetMoveDirection(float dir)
    {
        moveDirection = dir;
    }
        
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetAcceleration(float accel)
    {
        acceleration = accel;
    }

    public void SetCurve(float cur)
    {
        curve = cur;
    }

    public void SetTimeZero()
    {
        countTime = 0;
    }

    public void SetDefault()
    {
        moveSpeed = defaultMS;
    }

    public void SetXY(float x, float y) 
    {
        x1 = x;
        y1 = y;
    }

    public void SetBulletLife(float life)
    {
        bulletLife = life;
    }

    public float xDir(float angle)
    {
        float radians = angle * Mathf.PI / 180;
        return Mathf.Cos(radians);
    }

    public float yDir(float angle)
    {
        float radians = angle * Mathf.PI / 180;
        return -Mathf.Sin(radians);

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
