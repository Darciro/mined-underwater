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
    public float timeToStartLevel = 5f;
    public int playerHealth = 100;

    private static GameController _gameControllerInstance;
    [SerializeField] private float _minMineSpawnTime = 1f;
    [SerializeField] private float _maxMineSpawnTime = 3f;
    [SerializeField] private float _minEggSpawnTime = 2f;
    [SerializeField] private float _maxEggSpawnTime = 5f;
    private SceneTransitions _sceneTransitions;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _startLevelUI;
    private Text _curLevel;
    private Text _levelEggsToCollectObject;
    private IEnumerator _countDownToStartLevel;

    // Tutorial
    public bool tutorial = false;

    private static GameObject _tutorialUI;
    [SerializeField] private GameObject[] _tutorialSteps;
    private int _tutorialCurrentStep = 0;

    // Audio and FX
    public AudioClip mineExplosionSound;
    public AudioClip eggsColectedSound;

    private AudioSource _audioSource;

    // Player and Score

    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (tutorial)
        {
            _sceneTransitions = FindObjectOfType<SceneTransitions>();
            _tutorialUI = GameObject.Find("TutorialUI");
            _tutorialUI.SetActive(false);

            StartCoroutine(StartTutorial(_tutorialCurrentStep));
        }
        else
        {
            if (_gameControllerInstance == null)
            {
                _gameControllerInstance = this; // In first scene, make us the singleton.
                DontDestroyOnLoad(gameObject);
            }
            else if (_gameControllerInstance != this)
            {
                Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
            }
            
            _sceneTransitions = _gameControllerInstance._sceneTransitions = FindObjectOfType<SceneTransitions>();
            _mainUI = _gameControllerInstance._mainUI = GameObject.Find("UI");
            _startLevelUI = _gameControllerInstance._startLevelUI = GameObject.Find("LevelStartUI");
            startGame = _gameControllerInstance.startGame = false;
            timeToStartLevel = _gameControllerInstance.timeToStartLevel = 2f;
            _curLevel = GameObject.Find("LevelTextObject").GetComponent<Text>();
            _levelEggsToCollectObject = GameObject.Find("LevelEggsToCollectObject").GetComponent<Text>();
            level = _gameControllerInstance.level;
            eggsToCollect = _gameControllerInstance.eggsToCollect;
            playerHealth = _gameControllerInstance.playerHealth;
        
            _curLevel.text = "Level " + level;
            _levelEggsToCollectObject.text = eggsToCollect.ToString();
            StartLevel();
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

    void StartLevel()
    {
        _countDownToStartLevel = _gameControllerInstance._countDownToStartLevel = StartLevelRoutine();
        
        if( _countDownToStartLevel != null )
            StopCoroutine(_countDownToStartLevel);

        _mainUI.SetActive(false);
        StartCoroutine( _countDownToStartLevel );
    }
    
    IEnumerator StartLevelRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds( timeToStartLevel );
            startGame = true;
            if (_mainUI != null)
            {
                _startLevelUI.SetActive(false);
                _mainUI.SetActive(true);   
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
                _audioSource.PlayOneShot(mineExplosionSound);
                break;
            case "eggColected":
                _audioSource.PlayOneShot(eggsColectedSound);
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
}