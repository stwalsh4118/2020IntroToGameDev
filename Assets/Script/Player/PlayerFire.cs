using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    [SerializeField]
    private int bulletsAmount = 1;

    [SerializeField]
    private float startAngle = 135f, endAngle = 235f;
    public float bulletMS = 20;

    public float firerate = .1f;
    public float count = 0;

    private Vector2 bulletMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(count >= firerate && !MessagePromptUI.InDialog && !GetComponent<playerMovement>().isRolling)
        {
            Fire();
            count = 0;
        }
        count += Time.deltaTime;
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        //float angle = startAngle;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 directionVector = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(directionVector.x, directionVector.y);
        angle = angle * 180 / Mathf.PI;
        angle = angle - 90;


        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < bulletsAmount; i++)
            {


                GameObject bul = BulletPool.bulletPoolInstance.GetBullet("Arrow");
                bul.GetComponent<Bullet>().SetTimeZero();
                bul.transform.position = transform.position;
                bul.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg) - 90));
                bul.GetComponent<Bullet>().SetMoveSpeed(bulletMS);
                bul.GetComponent<Bullet>().SetDamage(GetComponentInChildren<Player>().CalculateDamage());
                bul.SetActive(true);
                bul.GetComponent<Bullet>().SetMoveDirection(angle);
            }
        }


    }
}
