using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployMines : MonoBehaviour
{
    public GameObject minePrefab;
    public float respawnTimeStart = 3.0f;
    public float respawnTimeEnd = 6.0f;
    private float respawnTime;
    private Vector2 screenBounds;

    // Use this for initialization
    void Start () {
        StartCoroutine(mineWave());
    }
    
    void Update () {
        respawnTime = Random.Range(respawnTimeStart, respawnTimeEnd);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    
    private void spawnMine(){
        GameObject a = Instantiate(minePrefab) as GameObject;
        a.transform.position = new Vector2(screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y));
    }
    
    IEnumerator mineWave(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnMine();
        }
    }
}
