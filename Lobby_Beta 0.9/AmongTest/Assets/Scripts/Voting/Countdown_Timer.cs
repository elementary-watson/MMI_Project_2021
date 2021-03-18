using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown_Timer : MonoBehaviour
{
    [SerializeField] private Image countdownCircleTimer;
    [SerializeField] private Text countdownText;
    [SerializeField] private float startTime;
    [SerializeField] public String referenced;
    //[SerializeField] private GameObject referenced;
    [SerializeField] private GameObject thisTimerPanel;
    [SerializeField] private Panel_Manager_Script p_manager;
    public AudioSource voting_countdown_sound;
    int isFinished=0;
    private float currentTime;
    private bool updateTime;

    private void Start()
    {
        isFinished = 0;
        string findTimer = thisTimerPanel.GetComponentInParent<RectTransform>().name;
        switch (findTimer)
        {
            case "9s_Timer_Panel":
                startTime = 9f;
                break;
            case "second_9s_Timer_Panel":
                startTime = 9f;
                break;
            case "15s_Timer_Panel":
                startTime = 15f; voting_countdown_sound.PlayDelayed(9);
                break;
            case "30s_Timer_Panel":
                startTime = 30f; voting_countdown_sound.PlayDelayed(15);
                break;
            case "60s_Timer_Panel":
                startTime = 5f;
                break;
        }

        currentTime = startTime;
        countdownCircleTimer.fillAmount = 1.0f;
        // Easy way to represent only the seconds and skip the float
        countdownText.text = (int)currentTime + "s";
        // update the countdown on the update
        updateTime = true;
    }
    void finishedUp() // jed nachdem wo der der timer drin ist macht er die nächste phase auf
    {
        if (isFinished == 1) 
        { 
            voting_countdown_sound.Stop();
            p_manager.timeFinished(referenced);
        }
    }

    private void Update()
    {
        if (updateTime)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0.0f)
            {
                // Stop the countdown timer              
                updateTime = false;
                currentTime = 0.0f;
                isFinished += 1;
                finishedUp();
            }
            countdownText.text = (int)currentTime + "s";
            float normalizedValue = Mathf.Clamp(
                     currentTime / startTime, 0.0f, 1.0f);
            countdownCircleTimer.fillAmount = normalizedValue;            
        }
    }
}
