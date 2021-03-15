using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Panel_Manager_Script : MonoBehaviour
{
    [SerializeField] private Multiplayer_Reference m_reference;

    [SerializeField] private Confirm_Panel_Logic confirm_pl;
    [SerializeField] private GameObject result_VotingPanel;    
    [SerializeField] private GameObject chat_panel;
    [SerializeField] private ChatController cc;
    [SerializeField] private GameObject voting_panel;
    [SerializeField] private GameObject s30postVotingTimer;
    [SerializeField] private GameObject s15postVotingTimer;
    [SerializeField] private GameObject s9_votetimerPanel;
    [SerializeField] private GameObject second_9s_Timer_Panel;
    
    [SerializeField] private TextMeshProUGUI resultTxt;

    [SerializeField] private GameObject Score_Panel;
    [SerializeField] private GameObject Player_Panel;
    [SerializeField] private GameObject main_Progressbar;

    string msg = "empty";
    bool showmessage = false;

    public void sendText(string msg)
    {
        this.msg = msg;
    }
    public void timeFinished(string referenced) 
    {
        if (referenced == "prevoting") // 15 Sekunden
        {
            result_VotingPanel.SetActive(true); //Ergebnisse zeigen
            confirm_pl.prevote_Timeout_ConfirmChoice(); // Nur lokal bei sich selbst voten
            //showmessage = true; //resultTxt.text = msg;            
            s15postVotingTimer.SetActive(false);
            s9_votetimerPanel.SetActive(true);
        }
        else if (referenced == "nextPhase")
        {
        s9_votetimerPanel.SetActive(false);
        int stage = m_reference.getCurrentStage();
            if (stage == 1)
            {
                m_reference.setCurrentStage(stage + 1);

                chat_panel.SetActive(true); //reihenfolge beachten!!
                voting_panel.SetActive(false);
                result_VotingPanel.SetActive(false);
            }
            else if (stage == 3)
            {
                voting_panel.SetActive(false);
                if (m_reference.getMaxRounds() == 5)
                { //BestofFive
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
            //showmessage = true;//resultTxt.text = msg;
            second_9s_Timer_Panel.SetActive(true);
        }
    }
    void thirdPhase(int maxrounds)
    {
        if (m_reference.getSaboteurPoints() == maxrounds || m_reference.getCrewPoints() == maxrounds) // Spiel vorbei und letztes Voting
        {
            int currentGameRound = m_reference.getGameRound();
            m_reference.setGameRound(currentGameRound + 1);
            result_VotingPanel.SetActive(false);
            voting_panel.SetActive(false);
            //Last vote
            //Spiel endet gewinner bildschirm und forschungsfrage!
        }
        else // Das Post-Voting einleiten und Spieler kicken
        {
            print("called this");
            int currentGameRound = m_reference.getGameRound();
            m_reference.setGameRound(currentGameRound + 1);
            Panel_Voting_Logic pvl = voting_panel.GetComponent<Panel_Voting_Logic>();
            pvl.enableButtons(); // Voting erlauben
            m_reference.setCurrentStage(1); // phase zurücksetzten
            result_VotingPanel.SetActive(false);
            voting_panel.SetActive(false);

            main_Progressbar.SetActive(true);
            Score_Panel.SetActive(true);
            Player_Panel.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showmessage) { 
            //resultTxt.text = msg;
            showmessage = false;
        }
    }
}
