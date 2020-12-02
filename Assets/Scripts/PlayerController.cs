using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D theRB;
    [SerializeField] float jumpForce;

    bool isGrounded;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask whatIsGround;

    bool canDoubleJump;

    Animator anim;
    SpriteRenderer theSR;

    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
        // Creates a circle with a radius of 0.2f at the referenced position, and checks whether there are any objects it is overlapping with on the Ground layer.
        // If there are, isGrounded is set to true.

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
            else
            {
                if (canDoubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
        }

        if (theRB.velocity.x < 0)
        {
            theSR.flipX = true;
        }
        else if (theRB.velocity.x > 0)
        {
            theSR.flipX = false;
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x)); // Gets the absolute value of velocity.x so that animation works in both directions
        anim.SetBool("isGrounded", isGrounded);
    }
}
