using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss : MonoBehaviour
{
    [SerializeField] private float maxHP = 1000f;
    public float Health;
    public float moveSpeed = 5f;
    public Transform player;
    public GameObject damagePopUp;

    public HP healthBar;

    public virtual void Start()
    {
        Health = maxHP;
        healthBar.SetMaxHealth(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            OnDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnHit(other);
    }

    public virtual void OnHit(Collider2D other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            GameObject dpu = (GameObject)Instantiate(damagePopUp, new Vector3(transform.position.x, transform.position.y + (GetComponent<SpriteRenderer>().bounds.size.y / 2f), other.transform.position.z), Quaternion.identity);
            dpu.GetComponent<TextMeshPro>().text = other.gameObject.GetComponent<Bullet>().bulletDamage.ToString();
            Health -= other.gameObject.GetComponent<Bullet>().bulletDamage;
            healthBar.SetHealth(Health);
        }
    }

    public virtual void OnDeath()
    {
        Destroy(transform.gameObject);
        Destroy(healthBar.transform.gameObject);
    }
}
