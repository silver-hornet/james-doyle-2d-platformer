using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform leftPoint, rightPoint;
    bool movingRight;
    Rigidbody2D theRB;
    [SerializeField] SpriteRenderer theSR;
    Animator anim;
    [SerializeField] float moveTime, waitTime;
    float moveCount, waitCount;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leftPoint.parent = null;
        rightPoint.parent = null;
        // The above two lines detach these child objects from the parent so that the leftPoint and rightPoint stays fixed,
        // rather than constantly being the same distance away from the parent object

        movingRight = true;
        moveCount = moveTime;
    }

    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingRight)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                theSR.flipX = true;
                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                theSR.flipX = false;
                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range (waitTime * 0.75f, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", true);
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, moveTime * 1.25f);
            }
            anim.SetBool("isMoving", false);
        }
    }
}
