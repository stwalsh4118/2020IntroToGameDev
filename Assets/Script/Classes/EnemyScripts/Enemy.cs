using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHP = 100f;
    public float Health;
    public float moveSpeed = 5f;
    public Transform player;
    public float aggroRange = 10f;
    public bool inAggroRange = false;
    public float firerate = 2f;
    public float bulletMS = 10f;
    public bool isShooting = false;
    public GameObject damagePopUp;

    public HP healthBar;

    public virtual void Start()
    {
        Health = maxHP;
        healthBar.SetMaxHealth(maxHP);
        player = GameObject.Find("Character").transform;

    }

    public virtual void Update()
    {
        Move();
        if (Health <= 0)
        {
            OnDeath();
        }
        if (CheckInAggroRange() && !isShooting)
        {
            InvokeRepeating("Shoot", Random.Range(0f, firerate), firerate);
            isShooting = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
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
        if (CheckInAggroRange())
        {
            GetComponent<BPGInGame>().LoadCommands();
        }
    }

    public virtual void OnHit(Collision2D other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            if (CheckInAggroRange())
            {
                GameObject dpu = (GameObject)Instantiate(damagePopUp, new Vector3(transform.position.x,transform.position.y + (GetComponent<SpriteRenderer>().bounds.size.y/2f),other.transform.position.z), Quaternion.identity);
                dpu.GetComponent<TextMeshPro>().text = other.gameObject.GetComponent<Bullet>().bulletDamage.ToString();
                Health -= other.gameObject.GetComponent<Bullet>().bulletDamage;
                healthBar.SetHealth(Health);
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
        return inAggroRange;
    }
}
