using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBullet : Bullet
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
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
        if (countTime >= bulletLife)
        {
            countTime = 0;
            Destroy();
        }
        if (gameObject.activeInHierarchy == false)
        {
            countTime = 0;
        }
        spin();
    }

    public void spin() {
       transform.Rotate(0,0,3f);
    }
}
