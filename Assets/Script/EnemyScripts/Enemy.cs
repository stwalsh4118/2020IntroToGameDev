using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP = 100f;
    public float moveSpeed = 5f;
    public Transform player;
    public float aggroRange = 10f;
    public bool inAggroRange = false;
    public float firerate = 2f;
    public float bulletMS = 10f;

    public virtual void Start()
    {
        player = GameObject.Find("Character").transform;
        InvokeRepeating("Shoot", 0f, firerate);
    }

    public virtual void Update()
    {
        Move();
        if(HP <= 0)
        {
            OnDeath();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnHit(other);
    }


    //
    public virtual void Move()
    {
        if (CheckInAggroRange())
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    public virtual void Shoot()
    {
        if(CheckInAggroRange())
        {

            Vector3 playerPos = player.position;
            playerPos.z = 0;

            Vector3 directionVector = (playerPos - transform.position).normalized;
            float angle = Mathf.Atan2(directionVector.x, directionVector.y) * (180 / Mathf.PI);
            angle -= 90;
            GameObject bul = BulletPool.bulletPoolInstance.GetBullet("Bone");
            bul.GetComponent<Bullet>().SetTimeZero();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.GetComponent<Bullet>().SetMoveSpeed(bulletMS);
            bul.GetComponent<Bullet>().SetBulletLife(1f);
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(angle);
        }
    }

    public virtual void OnHit(Collider2D other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            if (CheckInAggroRange())
            {
                HP -= 10;
            }
            other.gameObject.SetActive(false);
        }
    }

    public virtual void OnDeath()
    {
        Destroy(transform.gameObject);
    }

    public virtual bool CheckInAggroRange()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= aggroRange)
        {
            inAggroRange = true;
        }
        else
        {
            inAggroRange = false;
        }
        return inAggroRange;
    }
}
