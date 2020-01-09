using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Game variables
    public bool startGame = false;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public int gameDifficulty = 0;
    public int eggsToCollect = 10;
    public int level = 1;

    private static GameController _gameControllerInstance;
    [SerializeField]
    private float _minMineSpawnTime = 1f;
    [SerializeField]
    private float _maxMineSpawnTime = 3f;
    [SerializeField]
    private float _minEggSpawnTime = 2f;
    [SerializeField]
    private float _maxEggSpawnTime = 5f;
    private SceneTransitions _sceneTransitions;
    private GameObject _mainUI;
    private Text _curLevel;
    private Text _levelEggsToCollectObject;

    // Tutorial
    public bool tutorial = false;
    
    private static GameObject _tutorialUI;
    [SerializeField]
    private GameObject[] _tutorialSteps;
    private int _tutorialCurrentStep = 0;

    // Audio and FX
    public AudioClip mineExplosionSound;
    public AudioClip eggsColectedSound;
    
    private AudioSource audioSource;

    // Player and Score
    
    
    private void Awake()
    {
        _curLevel = GameObject.Find("LevelTextObject").GetComponent<Text>();
        _levelEggsToCollectObject = GameObject.Find("LevelEggsToCollectObject").GetComponent<Text>();
        
        if(_gameControllerInstance == null)
        {    
            _gameControllerInstance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
            _curLevel.text = "Level " + level;
            _levelEggsToCollectObject.text = eggsToCollect.ToString();
        }
        else if (_gameControllerInstance != this)
        {
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
            _curLevel.text = "Level " + _gameControllerInstance.level;
            _levelEggsToCollectObject.text = _gameControllerInstance.eggsToCollect.ToString();
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _sceneTransitions = FindObjectOfType<SceneTransitions>();
        _mainUI = GameObject.Find("UI");
        
        _mainUI.SetActive(false);
        StartCoroutine(StartLevel());

        if (tutorial)
        {
            _tutorialUI = GameObject.Find("TutorialUI");
            _tutorialUI.SetActive(false);

            StartCoroutine(StartTutorial(_tutorialCurrentStep));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _tutorialCurrentStep++;
            if (_tutorialCurrentStep <= _tutorialSteps.Length)
            {
                Tutorial(_tutorialCurrentStep);
            }
        }
    }

    public void PauseResumeGame(bool showPauseGui = true)
    {
        if (gameIsPaused)
        {
            if (showPauseGui)
                pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }
        else
        {
            if (showPauseGui)
                pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
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

    IEnumerator StartTutorial(int i)
    {
        yield return new WaitForSeconds(3f);
        Tutorial(i);
    }

    public void Tutorial(int i)
    {
        if (i == 0)
        {
            PauseResumeGame(false);
            _tutorialUI.SetActive(true);
        }
        
        if (i > 0)
        {
            _tutorialSteps[(i - 1)].SetActive(false);
        }
        
        if (i == _tutorialSteps.Length)
        {
            PauseResumeGame(false);
            _tutorialUI.SetActive(false);
            _sceneTransitions.LoadScene("Game");
            return;
        }
        
        _tutorialSteps[i].SetActive(true);
    }
    
    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(2f);
        startGame = true;
        GameObject.Find("LevelStartUI").SetActive(false);
        _mainUI.SetActive(true);
    }
}