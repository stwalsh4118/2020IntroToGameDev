using UnityEngine;
using System.Collections;

public class CompletePlayerController : MonoBehaviour
{

    float moveSpeed;          //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        moveSpeed = 8f;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        //Use the two store floats to create a new Vector2 variable movement.
        move = move.normalized * Time.deltaTime * moveSpeed;
        transform.Translate(move);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
    }
}