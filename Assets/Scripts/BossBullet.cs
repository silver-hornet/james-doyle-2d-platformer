using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
        // Multiplying by transform.localScale.x to ensure the bullet flips and faces in the right direction based on the boss's localScale
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DealDamage();
        }

        Destroy(gameObject);
    }
}
