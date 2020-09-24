using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float baseMoveSpeed = 10f;
    public float moveSpeed = 10f;
    public Rigidbody2D rb;

    Vector2 movement;
    public Animator animator;

    public float rollingTime = .5f;
    public float rollTime = .5f;
    public bool isRolling = false;
    public float rollSpeed = 15f;

    void loseSpeed()
    {
        moveSpeed = baseMoveSpeed;
        isRolling = false;
    }

    // Update is called once per frame. Don't use it for physics-related functions. 
    // Use for registering input.
    void Update()
    {
        if (!isRolling)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isRolling = true;
                    animator.SetTrigger("Roll");
                    moveSpeed = rollSpeed;
                    //movement = GetDirectionToMouse();
                }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else {
            rollingTime -= Time.deltaTime;
            if(rollingTime < 0) {
                rollingTime = rollTime;
                loseSpeed();
            }
        }
    }

    //Movement only.
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public Vector2 GetDirectionToMouse() {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 DToM = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
        return DToM.normalized;
    }
}
