using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private Text _timerText;
    private static float _timer;
    private GameController _gameController;

    // Start is called before the first frame update
    void Start()
    {
        _timerText = GameObject.Find("TimerTextObject").GetComponent<Text>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameController.startGame)
            return;
        
        _timer += Time.deltaTime;
        string minutes = Mathf.Floor(_timer / 60).ToString("00");
        string seconds = (_timer % 60).ToString("00");
        
        _timerText.text = string.Format("{0}:{1}", minutes, seconds);
    }
}