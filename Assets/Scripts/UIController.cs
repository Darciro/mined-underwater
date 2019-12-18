using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text timerText;
    private static float timer;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("TimerTextObject").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");
        
        timerText.text = string.Format("{0}:{1}", minutes, seconds);
    }
}