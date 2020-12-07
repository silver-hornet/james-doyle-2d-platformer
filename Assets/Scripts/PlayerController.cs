using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D theRB;
    [SerializeField] float jumpForce;
    bool isGrounded;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask whatIsGround;
    bool canDoubleJump;
    Animator anim;
    SpriteRenderer theSR;
    [SerializeField] float knockBackLength, knockBackForce;
    float knockBackCounter;
    [SerializeField] float bounceForce;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (knockBackCounter <= 0)
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
                    AudioManager.instance.PlaySFX(10);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        AudioManager.instance.PlaySFX(10);
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
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            if (!theSR.flipX) // Player is facing right
            {
                theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
            }
            else // Player is facing left
            {
                theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
            }
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x)); // Gets the absolute value of velocity.x so that animation works in both directions
        anim.SetBool("isGrounded", isGrounded);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, theRB.velocity.y);
        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }
}
