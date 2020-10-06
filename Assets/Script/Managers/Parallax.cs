using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float movementPercent = .3f;
    playerMovement pm;
    Vector2 movement;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindObjectOfType<playerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!pm.hitWall)
        {
            rb.MovePosition(rb.position + pm.movement.normalized * pm.moveSpeed * movementPercent * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }
}
