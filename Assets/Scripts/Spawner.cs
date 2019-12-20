using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject minePrefab;
    
    private float _respawnTime;
    private Vector2 screenBounds;
    private GameController gameController;
    [SerializeField]
    private string type;
    
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(mineWaves());
    }
    
    void Update () {
        switch (type)
        {
            case "mine":
                _respawnTime = Random.Range(gameController.GetMinMineSpawnTime(), gameController.GetMaxMineSpawnTime());
                break;
            case "egg":
                _respawnTime = Random.Range(gameController.GetMinEggSpawnTime(), gameController.GetMaxEggSpawnTime());
                break;
            default:
                _respawnTime = Random.Range(1f, 3f);
                break;
        }
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    
    private void spawnMine(){
        GameObject a = Instantiate(minePrefab);
        a.transform.position = new Vector2(screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y));
    }
    
    IEnumerator mineWaves(){
        while(true){
            yield return new WaitForSeconds(_respawnTime);
            spawnMine();
        }
    }
}
