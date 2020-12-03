using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance; // Set up a singleton

    public int currentHealth, maxHealth;

    void Awake()
    {
        instance = this; // Singleton, ensures there is only ever one PlayerHealthController
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            currentHealth = 0; // To protect against any weird errors that might happen if currentHealth somehow goes below 0
            gameObject.SetActive(false);
        }

        UIController.instance.UpdateHealthDisplay();
    }
}
