using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown_Timer : MonoBehaviour
{
    [SerializeField] private Image countdownCircleTimer;
    [SerializeField] private Text countdownText;
    [SerializeField] private float startTime = 30.0f;
    [SerializeField] private Confirm_Panel_Logic confirm_pl;
    [SerializeField] private GameObject result_VotingPanel ;
    [SerializeField] private GameObject thisTimerPanel;

    int isFinished=0;
    private float currentTime;
    private bool updateTime;
    private void Start()
    {
        currentTime = startTime;
        countdownCircleTimer.fillAmount = 1.0f;
        // Easy way to represent only the seconds and skip the
        // float     
        countdownText.text = (int)currentTime + "s";
        // update the countdown on the update
        updateTime = true;
    }
    void finishUpVoting()
    {
        if (isFinished == 1)
        {
            result_VotingPanel.SetActive(true);
            confirm_pl.Timeout_ConfirmChoice();            
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
                finishUpVoting();
            }
            countdownText.text = (int)currentTime + "s";
            float normalizedValue = Mathf.Clamp(
                     currentTime / startTime, 0.0f, 1.0f);
            countdownCircleTimer.fillAmount = normalizedValue;            
        }
    }
}
