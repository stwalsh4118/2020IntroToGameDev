using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    [SerializeField]
    private int bulletsAmount = 1;

    [SerializeField]
    private float startAngle = 135f, endAngle = 235f;

    private Vector2 bulletMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", .01f, .1f);
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 directionVector = (mousePosition - transform.position).normalized;

        for (int i = 0; i < bulletsAmount; i++)
        {


            GameObject bul = PlayerBulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<PlayerBullet>().SetMoveDirection(directionVector);
        }


    }
}
