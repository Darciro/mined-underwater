using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDestroyerController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.tag);

        switch (other.tag)
        {
            case "Mine":
                break;
            case "PickUp":
                break;
            case "Egg":
                break;
        }
        
        Destroy(other.gameObject);

    }
}
