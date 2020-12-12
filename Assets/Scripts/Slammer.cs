using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slammer : MonoBehaviour
{
    [SerializeField] Transform theSlammer, slammerTarget;
    Vector3 startPoint;

    [SerializeField] float slamSpeed, waitAfterSlam, resetSpeed;
    float waitCounter;
    bool slamming, resetting;

    void Start()
    {
        startPoint = theSlammer.position;
    }

    void Update()
    {
        if (!slamming && !resetting)
        {
            if (Vector3.Distance(slammerTarget.position, PlayerController.instance.transform.position) < 2f)
            {
                slamming = true;
                waitCounter = waitAfterSlam;
            }
        }

        if (slamming)
        {
            theSlammer.position = Vector3.MoveTowards(theSlammer.position, slammerTarget.position, slamSpeed * Time.deltaTime);

            if (theSlammer.position == slammerTarget.position)
            {
                waitCounter -= Time.deltaTime;
                if (waitCounter <= 0)
                {
                    slamming = false;
                    resetting = true;
                }
            }
        }

        if (resetting)
        {
            theSlammer.position = Vector3.MoveTowards(theSlammer.position, startPoint, resetSpeed * Time.deltaTime);

            if (theSlammer.position == startPoint)
            {
                resetting = false;
            }
        }
    }
}
