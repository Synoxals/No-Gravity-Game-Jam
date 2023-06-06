using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float blastSpeed = 1f;
    public float beamSpeed = 3f;

    public float xSpeed = 0f;
    public float ySpeed = 0f;
    public float minxSpeed = -6f;
    public float maxxSpeed = 6f;

    public Rigidbody2D rb;

    private string facingDirection;
    private string previousFacing = "right";

    // Update is called once per frame
    void Update()
    {
        // Handling inputs
        if (Input.GetKeyDown(KeyCode.D) && xSpeed > minxSpeed)
        {
            xSpeed -= blastSpeed;
            facingDirection = "right";
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.A) && xSpeed < maxxSpeed)
        {
            xSpeed += blastSpeed;
            facingDirection = "left";
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.W) && xSpeed > minxSpeed)
        {
            ySpeed -= blastSpeed;
        }
        if (Input.GetKeyDown(KeyCode.S) && xSpeed < maxxSpeed)
        {
            ySpeed += blastSpeed;
        }

    }

    private void FixedUpdate()
    {
        // Handling movement (In Fixed to avoid bugs)
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }

    private void Flip()
    {
        if ((facingDirection == "left" && previousFacing == "right") || (facingDirection == "right" && previousFacing == "left"))
        {
            transform.Rotate(0, 180f, 0);
            if(previousFacing == "left")
            {
                previousFacing = "right";
            }
            else if (previousFacing == "right")
            {
                previousFacing = "left";
            }
        }
    }


}
