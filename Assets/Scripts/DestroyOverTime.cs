using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] float lifeTime;

    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
