using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioClip mineExplosionSound;
    private AudioSource audioSource;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TriggerMineExplosion()
    {
        audioSource.PlayOneShot(mineExplosionSound);
    }
}
