using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class Mine : MonoBehaviour
{
    public float speed;
    public GameObject explosionEffect;

    [SerializeField]
    private int _damageMin;
    [SerializeField]
    private int _damageMax;
    [SerializeField]
    private int _criticalPercent;
    private int _damage;
    private GameController _gameController;
    
    void Start () {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        _damage = Random.Range(_damageMin, _damageMax);
    }

    void Update () {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            _gameController.PlaySoundEffect("mineExplosion");
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            other.GetComponent<PlayerController>().TakeDamage(_damage);
            Destroy(gameObject);
        }   
    }
}
