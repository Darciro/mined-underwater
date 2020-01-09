using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject minePrefab;
    
    private float _respawnTime;
    private Vector2 _screenBounds;
    private GameController _gameController;
    [SerializeField]
    private string type;
    
    void Start () {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(Respawner());
    }
    
    void Update () {
        switch (type)
        {
            case "mine":
                _respawnTime = Random.Range(_gameController.GetMinMineSpawnTime(), _gameController.GetMaxMineSpawnTime()) - _gameController.gameDifficulty;
                break;
            case "egg":
                _respawnTime = Random.Range(_gameController.GetMinEggSpawnTime(), _gameController.GetMaxEggSpawnTime());
                break;
            default:
                _respawnTime = Random.Range(1f, 3f);
                break;
        }
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    
    private void SpawnObject(){
        if (!_gameController.startGame)
            return;
        
        GameObject a = Instantiate(minePrefab);
        a.transform.position = new Vector2(_screenBounds.x, Random.Range(-_screenBounds.y, _screenBounds.y));
    }
    
    IEnumerator Respawner(){
        while(true){
            yield return new WaitForSeconds(_respawnTime);
            SpawnObject();
        }
    }
}
