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

    [SerializeField] private Stage_Panel_Script stagePanel_object;
    [SerializeField] GameObject Stage_Panel;
    
    [SerializeField] private TextMeshProUGUI resultTxt;

    [SerializeField] private GameObject Summary_Panel;
    [SerializeField] private Summary_Panel_Script SummaryP_object;
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
                stagePanel_object.switchOnNextstage(); //start the stage panel
                chat_panel.SetActive(true); //reihenfolge beachten!!
                voting_panel.SetActive(false);
                result_VotingPanel.SetActive(false);
            }
            else if (m_reference.getCurrentStage() == 3)
            {
                //Panel_Voting_Logic pvl = voting_panel.GetComponent<Panel_Voting_Logic>();
                //pvl.enableButtons(); // Button-Votes erlauben
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
            stagePanel_object.switchOnNextstage();
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
        // INFO: Die Spielrunden werden nur hier erhöht!
        // Sobald Result_Vote_Script fertig ist geht dieses script die endphase durch

        print("ThirdPhasecalled");
        if (_network.getIsGameOver()) // check wenn saboteur gekickt wurde!!
        {
            print("FINAL: Saboteur was kicked");
            int currentGameRound = m_reference.getGameRound();
            m_reference.setGameRound(currentGameRound + 1);
            result_VotingPanel.SetActive(false);
            voting_panel.SetActive(false);
            stagePanel_object.setup();
            Stage_Panel.SetActive(false);
            Summary_Panel.SetActive(true);
            SummaryP_object.setNextMode(true, true); //caught - final

        }
        else if (m_reference.getSaboteurPoints() == maxrounds || m_reference.getCrewPoints() == maxrounds) // Spiel vorbei und letztes Voting
        {
            print("FINAL: Final round reached");
            int currentGameRound = m_reference.getGameRound();
            m_reference.setGameRound(currentGameRound + 1); 
            result_VotingPanel.SetActive(false);
            voting_panel.SetActive(false);
            stagePanel_object.setup();
            Stage_Panel.SetActive(false);
            Summary_Panel.SetActive(true);
            SummaryP_object.setNextMode(false, true); //escaped - final
            // summary rufen und dann game over
            //Last vote
            //Spiel endet gewinner bildschirm und forschungsfrage!
        }
        else // Das Post-Voting einleiten und Spieler kicken
        {
            int currentGameRound = m_reference.getGameRound();
            m_reference.setGameRound(currentGameRound + 1);
            m_reference.setCurrentStage(1); // phase zurücksetzten
            stagePanel_object.setup();
            Stage_Panel.SetActive(false);
            _network.setPlayerMovement(true);
            main_Progressbar.SetActive(true);
            Score_Panel.SetActive(true);
            Player_Panel.SetActive(true);
            Summary_Panel.SetActive(true);// summary rufen und dann nächste runde
            SummaryP_object.setNextMode(false, false); //escaped - nextRound
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
