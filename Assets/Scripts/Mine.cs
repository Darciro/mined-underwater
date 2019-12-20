using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class Mine : MonoBehaviour
{
    public float speed;
    public GameObject explosionEffect;

    private GameController gameController;
    
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update () {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            gameController.PlaySoundEffect("mineExplosion");
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            other.GetComponent<PlayerController>().TakeDamage(30);
            Destroy(gameObject);
        }   
    }
}
