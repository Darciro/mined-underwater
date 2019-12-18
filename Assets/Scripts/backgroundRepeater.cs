using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour {

	public GameObject[] bgItems;
	public float respawnTimeStart = 3.0f;
	public float respawnTimeEnd = 6.0f;
	private float respawnTime;
	private Vector2 screenBounds;
	
	void Start () {
		StartCoroutine(bgWave());
	}
	
	void Update () {
	    respawnTime = Random.Range(respawnTimeStart, respawnTimeEnd);
	    screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    
    private void newBG(){
	    // GameObject a = Instantiate(minePrefab) as GameObject;
	    GameObject a = Instantiate( bgItems[Random.Range(0, bgItems.Length -1)] ) as GameObject;
	    Debug.Log( "New BG: " + a.name );
	    a.transform.position = new Vector2(screenBounds.x, Random.Range(-5.00f, 0));
    }
    
    IEnumerator bgWave(){
	    while(true){
		    yield return new WaitForSeconds(respawnTime);
		    newBG();
	    }
    }
}
