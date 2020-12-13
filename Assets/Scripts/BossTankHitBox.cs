using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
    public BossTankController bossCont;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.instance.transform.position.y > transform.position.y)
            //  Ensures that the HitBox can only be hit when the player is above the HitBox
        {
            bossCont.TakeHit();
            PlayerController.instance.Bounce();
            gameObject.SetActive(false);
        }
    }
}
