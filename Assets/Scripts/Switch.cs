using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject objectToSwitch;
    SpriteRenderer theSR;
    [SerializeField] Sprite downSprite;
    bool hasSwitched;
    [SerializeField] bool deactivateOnSwitch;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hasSwitched)
        {
            if (deactivateOnSwitch)
            {
                objectToSwitch.SetActive(false);
                AudioManager.instance.PlaySFX(3);
            }
            else
            {
                objectToSwitch.SetActive(true);
            }

            theSR.sprite = downSprite;
            hasSwitched = true;
        }
    }
}



    
