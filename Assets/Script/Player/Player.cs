using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float IFrames = 1f;
    public float count = 0;
    public bool Invuln = false;
    public Weapons weapon;


    // Start is called before the first frame update
    void Start()
    {
        weapon = WeaponPool.Instance.AllWeapons.Find(x => x.name == "Bow");
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;
        if(count<0) {
            Invuln = false;
            GetComponentInParent<playerMovement>().animator.SetBool("OnHit", Invuln);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.parent.GetComponent<playerMovement>().isRolling == false && !Invuln)
        {
            if ((other.gameObject.tag == "Donut") || (other.gameObject.tag == "Pizza") || (other.gameObject.tag == "Bone") 
             || (other.gameObject.tag == "Scythe") || (other.gameObject.tag == "Green"))
            {
                Invuln = true;
                GetComponentInParent<playerMovement>().animator.SetBool("OnHit", Invuln);
                count =IFrames;
                HealthBar.playerHP.TakeDamage();
            }
        }

    }

    public int CalculateDamage() {
        float maxWeaponDamage = weapon.maxDamage;
        float minWeaponDamage = weapon.minDamage;

        float cal = Random.Range(minWeaponDamage, maxWeaponDamage);

        return (int)Mathf.Ceil(cal);
    }
}
