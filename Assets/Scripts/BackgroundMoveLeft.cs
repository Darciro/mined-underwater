using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoveLeft : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    void Update () {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

}
