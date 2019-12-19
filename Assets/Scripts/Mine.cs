using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class Mine : MonoBehaviour
{
    public float speed;
    public GameObject explosionEffect;
    /*public AudioClip explosionSound;
    private AudioSource audioSource;*/

    private GameController gameController;

    void Update () {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        // audioSource = GetComponent<AudioSource>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            // Instantiate(explosionSound, transform.position, Quaternion.identity);
            // other.GetComponent<PlayerController>().health--;
            // other.GetComponent<PlayerController>().camAnim.SetTrigger("shake");
            // audioSource.PlayOneShot(explosionSound);
            gameController.TriggerMineExplosion();
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            other.GetComponent<PlayerController>().TakeDamage(30);
            Destroy(gameObject);
        }   
    }
}
