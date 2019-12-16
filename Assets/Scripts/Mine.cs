using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class Mine : MonoBehaviour
{
    public float speed;
    public GameObject effect;
    public GameObject explosionSound;

    void Update () {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Instantiate(explosionSound, transform.position, Quaternion.identity);
            // other.GetComponent<PlayerController>().health--;
            // other.GetComponent<PlayerController>().camAnim.SetTrigger("shake");
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            // Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }   
    }
}
