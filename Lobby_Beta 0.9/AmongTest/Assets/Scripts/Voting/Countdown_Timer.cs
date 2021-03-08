using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown_Timer : MonoBehaviour
{
    [SerializeField] private Image countdownCircleTimer;
    [SerializeField] private Text countdownText;
    [SerializeField] private float startTime;
    [SerializeField] private GameObject referenced;
    [SerializeField] private GameObject result_VotingPanel;
    [SerializeField] private GameObject thisTimerPanel;
    [SerializeField] private Multiplayer_Reference m_reference;

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
    void finishedUp() // jed nachdem wo der der timer drin ist macht er die nächste phase auf
    {
        if (isFinished == 1)
        {
            if (referenced.GetComponent<Confirm_Panel_Logic>().tag == "Confirm_Panel")
            {
                result_VotingPanel.SetActive(true);
                Confirm_Panel_Logic confirm_pl = referenced.GetComponent<Confirm_Panel_Logic>();
                confirm_pl.photon_Timeout_ConfirmChoice();
            } 
            else if (referenced.GetComponent<GameObject>().tag == "Result_Panel")
            {
                Result_Voting_Panel rv_pnl = result_VotingPanel.GetComponent<Result_Voting_Panel>();
                rv_pnl.nextPhase();//wird in Voring Panel gehandelt

            }
            else if (referenced.GetComponent<GameObject>().tag == "ChatManager")
            {                
                m_reference.setCurrentStage(3);
                ChatController cc = referenced.GetComponent<ChatController>();
                cc.startNextPhase();
            }
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
