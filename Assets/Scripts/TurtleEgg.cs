using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEgg : MonoBehaviour
{
    public float speed;
    
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
            gameController.PlaySoundEffect("eggColected");
            other.GetComponent<PlayerController>().GetEgg(1);
            Destroy(gameObject);
        }   
    }
}
