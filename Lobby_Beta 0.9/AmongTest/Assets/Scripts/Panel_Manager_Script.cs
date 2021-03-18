using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Panel_Manager_Script : MonoBehaviour
{
    [SerializeField] private Multiplayer_Reference m_reference;
    [SerializeField] private Network _network;
    [SerializeField] private ChatController cc;
    
    [SerializeField] private Confirm_Panel_Logic confirm_pl;
    [SerializeField] private GameObject result_VotingPanel;    
    [SerializeField] private GameObject chat_panel;    
    [SerializeField] private GameObject voting_panel;
    [SerializeField] private GameObject s30postVotingTimer;
    [SerializeField] private GameObject s15postVotingTimer;
    [SerializeField] private GameObject s9_votetimerPanel;
    [SerializeField] private GameObject second_9s_Timer_Panel;

    [SerializeField] private Stage_Panel_Script sss_object;
    [SerializeField] GameObject Stage_Panel;
    
    [SerializeField] private TextMeshProUGUI resultTxt;

    [SerializeField] private GameObject Summary_Panel;
    [SerializeField] private GameObject Score_Panel;
    [SerializeField] private GameObject Player_Panel;
    [SerializeField] private GameObject main_Progressbar;

    Panel_Voting_Logic pvl;
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
            print("Next stage");
            s9_votetimerPanel.SetActive(false);
            if (m_reference.getCurrentStage() == 1)
            {
                m_reference.setCurrentStage(2);
                sss_object.switchOnNextstage(); //start the stage panel
                chat_panel.SetActive(true); //reihenfolge beachten!!
                voting_panel.SetActive(false);
                result_VotingPanel.SetActive(false);
            }
            else if (m_reference.getCurrentStage() == 3)
            {
                //Panel_Voting_Logic pvl = voting_panel.GetComponent<Panel_Voting_Logic>();
                pvl.enableButtons(); // Button-Votes erlauben
                print("enter third stage");
                
                if (m_reference.getMaxRounds() == 5)//BestofFive
                { 
                    thirdPhase(3);
                }
                else if (m_reference.getMaxRounds() == 2) //BestofThree 
                {
                    thirdPhase(2);
                }
                Invoke("closeVoting", 3);
                //Summary_Panel.SetActive(true);
            }
        }
        else if (referenced == "ChatManager") // wenn chat vorbei ist dann...
        {
            m_reference.setCurrentStage(3);
            sss_object.switchOnNextstage();
            cc.startNextPhase();
            voting_panel.SetActive(true);
            
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
    public void closeVoting()
    {
        result_VotingPanel.SetActive(false);
        voting_panel.SetActive(false);
    }
    void thirdPhase(int maxrounds)
    {
        print("ThirdPhasecalled");
        if (m_reference.getSaboteurPoints() == maxrounds || m_reference.getCrewPoints() == maxrounds) // Spiel vorbei und letztes Voting
        {
            print("final reached");
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
            m_reference.setCurrentStage(1); // phase zurücksetzten
            _network.setPlayerMovement(true);
            main_Progressbar.SetActive(true);
            Score_Panel.SetActive(true);
            Player_Panel.SetActive(true);
            print("called this again");

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pvl = voting_panel.GetComponent<Panel_Voting_Logic>();
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
