using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            PlayerHealthController.instance.DealDamage();
            AudioManager.instance.PlaySFX(3);
        }
    }

    public void Explode()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(3);
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
