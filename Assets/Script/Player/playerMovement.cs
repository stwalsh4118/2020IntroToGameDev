using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;
    public Animator animator;

    public float cooldownTime = 5f;
    public float nextFireTime = 0f;

    void loseSpeed()
    {
        moveSpeed -= 2f;
    }

    // Update is called once per frame. Don't use it for physics-related functions. 
    // Use for registering input.
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Time.time > nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveSpeed += 2f;
                Debug.Log("Button is pressed");
                nextFireTime = Time.time + cooldownTime;
                Invoke("loseSpeed", cooldownTime);
            }
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    //Movement only.
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}