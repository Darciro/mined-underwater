using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Game variables
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    
    private int _gameDifficulty = 1; 
    [SerializeField]
    private float _minMineSpawnTime = 1f;
    [SerializeField]
    private float _maxMineSpawnTime = 3f;
    [SerializeField]
    private float _minEggSpawnTime = 2f;
    [SerializeField]
    private float _maxEggSpawnTime = 5f;
    
    // Audio and FX
    public AudioClip mineExplosionSound;
    public AudioClip eggsColectedSound;
    private AudioSource audioSource;
    
    // Player and Score
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PauseResumeGame()
    {
        if (GameIsPaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        else
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }
    
    public void PlaySoundEffect(string effect)
    {
        switch (effect)
        {
            case "mineExplosion":
                audioSource.PlayOneShot(mineExplosionSound);
                break;
            case "eggColected":
                audioSource.PlayOneShot(eggsColectedSound);
                break;
        }
        
    }

    public float GetMinMineSpawnTime()
    {
        return _minMineSpawnTime;
    }

    public float GetMaxMineSpawnTime()
    {
        return _maxMineSpawnTime;
    }

    public float GetMinEggSpawnTime()
    {
        return _minEggSpawnTime;
    }

    public float GetMaxEggSpawnTime()
    {
        return _maxEggSpawnTime;
    }
}
