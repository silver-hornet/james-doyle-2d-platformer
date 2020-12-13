using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] GameObject theBossBattle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theBossBattle.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.instance.PlayBossMusic();
        }
    }
}
