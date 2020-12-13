using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    [SerializeField] enum bossStates { shooting, hurt, moving };
    [SerializeField] bossStates currentState;
    [SerializeField] Transform theBoss;
    [SerializeField] Animator anim;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] Transform leftPoint, rightPoint;
    bool moveRight;
    [SerializeField] GameObject mine;
    [SerializeField] Transform minePoint;
    [SerializeField] float timeBetweenMines;
    float mineCounter;

    [Header("Shooting")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] float timeBetweenShots;
    float shotCounter;

    [Header("Hurt")]
    [SerializeField] float hurtTime;
    float hurtCounter;
    [SerializeField] GameObject hitBox;

    void Start()
    {
        currentState = bossStates.shooting; // This could also be set as = 0, since shooting is the first element in the enum, but that would make the code less readable
    }

    void Update()
    {
        switch (currentState)
        {
            case bossStates.shooting:
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }
                break;
            case bossStates.hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;
                    if (hurtCounter <= 0)
                    {
                        currentState = bossStates.moving;
                        mineCounter = 0;
                    }
                }
                break;
            case bossStates.moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = Vector3.one; // This is the same as: new Vector3(1f, 1f, 1f);
                        moveRight = false;
                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;
                        EndMovement();
                    }
                }
                mineCounter -= Time.deltaTime;
                if (mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }
                break;
        }

#if UNITY_EDITOR // Ensures that the code below only runs within the Unity editor

        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
    }

#endif

    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;
        anim.SetTrigger("Hit");
        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if (mines.Length > 0)
        {
            foreach (BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }
    }

    void EndMovement()
    {
        currentState = bossStates.shooting;
        shotCounter = 0f;
        anim.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }
}
