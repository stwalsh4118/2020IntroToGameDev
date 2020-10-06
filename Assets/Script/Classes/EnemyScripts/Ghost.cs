using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ghost : Enemy
{
    public float rotationAngle = 0f;
    public float radius = 6f;
    public float rotationSpeed = 1.5f;
    bool isMoving = false;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rotationAngle = Random.Range(0f, 360f);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Move()
    {
        if (CheckInAggroRange())
        {
            Tween anim = transform.DOLocalMove(new Vector3(player.position.x + (radius * Mathf.Cos(rotationAngle)), player.position.y + (radius * Mathf.Sin(rotationAngle)), player.position.z), .5f, false);

            //transform.position = new Vector3(player.position.x + (radius * Mathf.Cos(rotationAngle)), player.position.y + (radius * Mathf.Sin(rotationAngle)), player.position.z);
            rotationAngle += rotationSpeed * Time.deltaTime;
        }
    }

}
