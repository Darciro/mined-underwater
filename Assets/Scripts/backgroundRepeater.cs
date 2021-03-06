﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeater : MonoBehaviour {

	public GameObject[] bgItems;
	public float respawnTimeStart = 3.0f;
	public float respawnTimeEnd = 6.0f;
	
	private float _respawnTime;
	private Vector2 _screenBounds;
	[SerializeField]
	private float _minY;
	[SerializeField]
	private float _maxY;
	
	void Start () {
		StartCoroutine(bgWave());
	}
	
	void Update () {
	    _respawnTime = Random.Range(respawnTimeStart, respawnTimeEnd);
	    _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    
    private void newBG()
    {
	    GameObject a = Instantiate(bgItems[Random.Range(0, bgItems.Length - 1)]);
	    a.transform.position = new Vector2( (_screenBounds.x + 5f), Random.Range(_minY, _maxY));
    }
    
    IEnumerator bgWave(){
	    while(true){
		    yield return new WaitForSeconds(_respawnTime);
		    newBG();
	    }
    }
}
