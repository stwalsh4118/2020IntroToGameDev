using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    public float moveDirection;
    float bulDirX;
    float bulDirY;
    public float bulletLife = 5f;
    public float defaultMS = 5f;
    public float countTime = 0;
    public float acceleration = 0;
    public float curve = 0;
    public float x1;
    public float y1;
    public float baseScale;
    public int bulletDamage = 0;
    public int numBounces = 0;
    public int numAvailBounces = 0;
    public bool bounced = false;
    public List<string> bulletProperties;


    private void OnEnable()
    {
        StartCoroutine(AccelerateDamage());
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        baseScale = transform.localScale.x;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        moveDirection = moveDirection + curve * Time.deltaTime;
        moveSpeed = moveSpeed + acceleration * Time.deltaTime;
        if (!bounced)
        {
            bulDirX = xDir(moveDirection);
            bulDirY = yDir(moveDirection);
        }

        float x = transform.position.x + bulDirX * moveSpeed * Time.deltaTime;
        float y = transform.position.y + bulDirY * moveSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(bulDirY, bulDirX) * Mathf.Rad2Deg - 90)));

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

    public void SetDefaultScale()
    {
        transform.localScale = new Vector3(baseScale, baseScale, 0f);
    }

    public void SetDefault()
    {
        moveSpeed = defaultMS;
        bounced = false;
        numBounces = numAvailBounces;
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

    public void SetDamage(int bd)
    {
        bulletDamage = bd;
    }

    public void OnHit()
    {
               
    }

    public void OnHit(Collision2D collision)
    {
        OnHit();
        if (bulletProperties.Exists(x => x == "bounce"))
        {
            Vector3 reflectDirection = Vector3.Reflect(new Vector3(bulDirX, bulDirY, 0f), collision.GetContact(0).normal);
            bulDirX = reflectDirection.x;
            bulDirY = reflectDirection.y;
            bounced = true;
            bulletDamage = (int)((float)bulletDamage * (Inventory.Instance.IV.onHitDamageInstances.Find(x => x.damageSource == "bounce").damageIncrease/100));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            OnHit(other);
        }
        else
        {
            //Debug.Log(other.transform.name);
        }
    }

    public IEnumerator AccelerateDamage()
    {
        int baseBulletDamage = bulletDamage;
        for(; ; )
        {
            yield return new WaitForSeconds(.5f);
            bulletDamage += (int)((float)baseBulletDamage * (acceleration/2f));
        } 

    }

    protected void Destroy()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    protected void OnDisable()
    {
        CancelInvoke();
    }

}
