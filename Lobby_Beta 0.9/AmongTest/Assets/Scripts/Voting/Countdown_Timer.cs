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
    [SerializeField] private GameObject result_VotingPanel;
    [SerializeField] private GameObject thisTimerPanel;
    [SerializeField] private ChatController cc;
    [SerializeField] private Confirm_Panel_Logic confirm_pl;
    [SerializeField] private Multiplayer_Reference m_reference;

    [SerializeField] private GameObject voting_panel;
    [SerializeField] private GameObject s30postVotingTimer;
    [SerializeField] private GameObject chat_panel;

    [SerializeField] private GameObject Score_Panel;
    [SerializeField] private GameObject Player_Panel;
    [SerializeField] private GameObject main_Progressbar;

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
            case "15s_Timer_Panel":
                startTime = 15f;
                break;
            case "30s_Timer_Panel":
                startTime = 30f;
                break;
            case "60s_Timer_Panel":
                startTime = 60f;
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
            //try 
            //{
                if (referenced == "prevoting")
                {
                    //Result_Voting_Panel rv_pnl = result_VotingPanel.GetComponent<Result_Voting_Panel>();
                    result_VotingPanel.SetActive(true); //Ergebnisse zeigen
                    confirm_pl.photon_Timeout_ConfirmChoice();
                }
                else if (referenced == "nextPhase")
                {
                    int stage = m_reference.getCurrentStage();
                    if(stage == 1)
                    {
                        m_reference.setCurrentStage(stage + 1);

                        chat_panel.SetActive(true); //reihenfolge beachten!!
                        voting_panel.SetActive(false);
                        result_VotingPanel.SetActive(false);
                    }
                    else if (stage == 3 )
                    {
                        if (m_reference.getMaxRounds() == 5) { //BestofFive
                            thirdPhase(3);
                        }
                        else if (m_reference.getMaxRounds() == 3) //BestofThree 
                        {
                            thirdPhase(2);
                        }
                    }
                }
                else if (referenced == "ChatManager")
                {                
                    m_reference.setCurrentStage(3);
                    cc.startNextPhase();
                    voting_panel.SetActive(true);
                    Panel_Voting_Logic pvl = voting_panel.GetComponent<Panel_Voting_Logic>();
                    pvl.enableButtons();
                    s30postVotingTimer.SetActive(true);
                    chat_panel.SetActive(false);
                }
                else if (referenced == "postvoting")
                {
                    result_VotingPanel.SetActive(true);
                    confirm_pl.photon_Timeout_ConfirmChoice();
                }    
            //} catch (Exception e) 
            //{
            //    print("Failure" + e);
            //}
        }
    }
    void thirdPhase(int maxrounds)
    {
        if (m_reference.getSaboteurPoints() == maxrounds || m_reference.getCrewPoints() == maxrounds)
        {
            int currentGameRound = m_reference.getGameRound();
            m_reference.setGameRound(currentGameRound + 1);
            result_VotingPanel.SetActive(false);
            voting_panel.SetActive(false);
            //Last vote
            //Spiel endet gewinner bildschirm und forschungsfrage!
        }
        else
        {
            int currentGameRound = m_reference.getGameRound();
            m_reference.setGameRound(currentGameRound + 1);
            Panel_Voting_Logic pvl = voting_panel.GetComponent<Panel_Voting_Logic>();
            pvl.enableButtons();
            m_reference.setCurrentStage(1); // phase zurücksetzten
            result_VotingPanel.SetActive(false);
            voting_panel.SetActive(false);

            main_Progressbar.SetActive(true);
            Score_Panel.SetActive(true);
            Player_Panel.SetActive(true);
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
