using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Result_Voting_Panel : MonoBehaviour
{
    [Header("Voting Panel")]
    IDictionary<int, string> player; // 
    IDictionary<int, int> finalVotings = new Dictionary<int,int>(); //actor id , votings
    [SerializeField] private Multiplayer_Reference m_reference;
    [SerializeField] private Image img_votedPlayer;
    [SerializeField] private Network _network;
    private int receivedVotes;    
    [SerializeField] TextMeshProUGUI tmp_resultText;    
    [SerializeField] private GameObject thisVotingPanel;
    [SerializeField] private GameObject Voting_Panel;
    [SerializeField] private GameObject s9_votetimerPanel;
    [Header("Extern")]
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject ScorePanel;
    // Start is called before the first frame update
    public void submitVote(int myActorID, string myplayerColor, string playerColor, int photonActorID,int indexPosition)
    {
        int currentStage = m_reference.getCurrentStage();
        if (currentStage == 1) 
        {
            _network.addSuspectToList(photonActorID, playerColor);
            // XOF Hier muss geloggt werden!
        }
        if (currentStage == 3) 
        {
            _network.addSuspectToList(photonActorID, playerColor);
            // XOF Hier muss geloggt werden!
        
            //print("DEBUG: " + myActorID + " " + myplayerColor + " " + playerColor + " " + photonActorID + " " + indexPosition);
            if ((playerColor == "") || (photonActorID == 0))
                print("Actor " + myActorID + " - " + myplayerColor + " made no Choice");
            else {
                if (finalVotings == null) { //erster wert der dict
                    //KeyValuePair<int, int> item = 1,1;
                    finalVotings.Add(photonActorID, 1);
                }
                else if (!(finalVotings.Keys.Contains(photonActorID))) // farbe erstes mal gevoted
                    finalVotings.Add(photonActorID, 1);
                else
                    finalVotings[photonActorID] += 1; //vote zu einer farbe hinzufügen
            }
            receivedVotes += 1;
            print("Voting result \n-------------------------------------------\n" + "Actors: " + _network.getActorsInRoom() + "Submit: " + receivedVotes);
            if (receivedVotes == _network.getActorsInRoom()) //Wenn alle spieler einen vote gesendet haben ergebnis bildschirm öffnen
                finalReveal();
        }
    }
    public void submitSuspicion(string playerColor)
    {
        string filename = "Player Color/" + playerColor + "_Char";
        Sprite sp = Resources.Load<Sprite>(filename);
        img_votedPlayer.sprite = sp;
        tmp_resultText.text = "Du verdaechtigst diesen Spieler.\nFinale Entscheidung nach der folgenden Diskussionsrunde.";
    }
    public void finalReveal()
    {
        int i = 0;
        KeyValuePair<int, int> mostVoted;
        KeyValuePair<int, int> equal;
        player = m_reference.getPlayers();//Liste wird aktuell sein
        foreach (KeyValuePair<int, int> item in finalVotings)
        {
            if (i == 0)
                mostVoted = item;
            else if (item.Value > mostVoted.Value)
                mostVoted = item;
            else if (item.Value == mostVoted.Value)
                equal = item;
            i++;
            print("itemkey :" + item.Key + " itemvalue" + item.Value);
            print("mostvoted: " + mostVoted);
            print("equal: " + equal);
        }
        if (mostVoted.Value == 0)
            print("No on was voted");
        else if (mostVoted.Value == equal.Value)
            print("We have a tie between: " + player[mostVoted.Key] + " and " + player[equal.Key]);
        else if (mostVoted.Value > 0)
        {
            print("Most voted player: " + player[mostVoted.Key]);
            string filename = "Player Color/" + player[mostVoted.Key] + "_Char";
            Sprite sp = Resources.Load<Sprite>(filename);
            img_votedPlayer.sprite = sp;
        }
        tmp_resultText.text = "Dieser Spieler ist ausgeschieden.";
        //svl[i].setClassValues(item.Value, item.Key, i);//set (color, id, index) of buttons
    }
    public void nextPhase()//Wird von timerCountdown gerufen. Phase beenden und nächste beginnen.
    {
        int currentStage = m_reference.getCurrentStage();
        if (currentStage == 1) // In Chat Panel übergehen und dieses Panel schließen
        {
            currentStage += 1;
            m_reference.setCurrentStage(currentStage);
            chatPanel.SetActive(true); //reihenfolge beachten!!
            Voting_Panel.SetActive(false);
            thisVotingPanel.SetActive(false);
        }
        else if (currentStage == 3 && m_reference.getGameRound() < 2) //Spiel fortsetzten?
        {
            int crewPoints = m_reference.getCrewPoints();
            int saboteurPoints = m_reference.getSaboteurPoints();
            int currentGameRound = m_reference.getGameRound();
            m_reference.setGameRound(currentGameRound + 1);
            m_reference.setCurrentStage(1); // phase zurücksetzten

            ScorePanel.SetActive(true);
            Voting_Panel.SetActive(false);
            thisVotingPanel.SetActive(false);
        }
        else
        {
            print("Game Ends");
        }
    }
    public void Setup() 
    {
        
    }
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
