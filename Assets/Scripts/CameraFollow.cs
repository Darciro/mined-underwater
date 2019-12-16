using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        // transform.position = target.position;
        // transform.position = new Vector3(target.position.x,target.position.y,-10f);
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            float clampedX = Mathf.Clamp(target.position.x + 5.0f, minX, maxX);
            float clampedY = Mathf.Clamp(target.position.y, minY, maxY);
            transform.position = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, -10f), speed);
        }
    }
}
