using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour
{
    [SerializeField] Vector2 minPos, maxPos;
    [SerializeField] Transform target;

    void LateUpdate()
    {
        float xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x);
        float yPos = Mathf.Clamp(target.position.y, minPos.y, maxPos.y);
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
