using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    [SerializeField] private GameObject thisResultVotingPanel;
    [SerializeField] private GameObject[] tmp_resultTexts = new GameObject[0]; // Prevote: 0 novote, 1 vote - Postbote 2 votekick, 3 tie, 4 novotes

    [SerializeField] private GameObject Voting_Panel;
    [SerializeField] private Panel_Manager_Script p_manager;
    [Header("Extern")]
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject ScorePanel;
    [SerializeField] Time_Game_Script timeGame_object;
    [SerializeField] private GameObject Summary_Panel;
    [SerializeField] WebRequestGame databaseLogger;
    // Start is called before the first frame update    

    public void submitVote(int myActorID, string myplayerColor, string playerColor, int photonActorID,int indexPosition)
    {
        int currentStage = m_reference.getCurrentStage();

        if (currentStage == 1) 
        {
            finalReveal(photonActorID, playerColor);
        }
        if (currentStage == 3) 
        {
            // XOF Hier muss geloggt werden!
            print("DEBUG: " + myActorID + " " + myplayerColor + " " + playerColor + " " + photonActorID + " " + indexPosition);
            if ((playerColor == "") && (photonActorID == 0))
                print("Actor " + myActorID + " - " + myplayerColor + " made no Choice");
            else {
                //_network.addSuspectToList(m_reference.getCurrentStage(), m_reference.getGameRound(), playerColor);
                if (finalVotings == null) { //erster wert der dict
                    //KeyValuePair<int, int> item = 1,1;
                    finalVotings.Add(photonActorID, 1);
                }
                else if (!(finalVotings.Keys.Contains(photonActorID))) // farbe erstes mal gevoted
                    finalVotings.Add(photonActorID, 1);
                else
                    finalVotings[photonActorID] += 1; //vote zu einer farbe hinzufügen
                string myPlayerColorNumber = "";
                string votePlayerColorNumber = "";
                #region ifs


                if (_network.getPlayerColor().Contains("Black"))
                {
                    myPlayerColorNumber = "7";
                }
                else if (_network.getPlayerColor().Contains("White"))
                {
                    myPlayerColorNumber = "6";
                }
                else if (_network.getPlayerColor().Contains("Pink"))
                {
                    myPlayerColorNumber = "8";
                }
                else if (_network.getPlayerColor().Contains("Orange"))
                {
                    myPlayerColorNumber = "9";
                }
                else if (_network.getPlayerColor().Contains("Red"))
                {
                    myPlayerColorNumber = "10";
                }
                if (_network.getPlayerColor().Contains("Purple"))
                {
                    myPlayerColorNumber = "1";
                }
                else if (_network.getPlayerColor().Contains("Purple"))
                {
                    myPlayerColorNumber = "1";
                }

                if (_network.getPlayerColor().Contains("Brown"))
                {
                    myPlayerColorNumber = "2";
                }
                else if (_network.getPlayerColor().Contains("Purple"))
                {
                    myPlayerColorNumber = "2";
                }


                if (_network.getPlayerColor().Contains("Green"))
                {
                    myPlayerColorNumber = "3";
                }
                else if (_network.getPlayerColor().Contains("Green"))
                {
                    myPlayerColorNumber = "3";
                }


                if (_network.getPlayerColor().Contains("Yellow"))
                {
                    myPlayerColorNumber = "4";
                }
                else if (_network.getPlayerColor().Contains("Yellow"))
                {
                    myPlayerColorNumber = "4";
                }

                if (_network.getPlayerColor().Contains("Blue"))
                {
                    myPlayerColorNumber = "5";
                }
                else if (_network.getPlayerColor().Contains("Blue"))
                {
                    myPlayerColorNumber = "5";
                }
                if (playerColor.Contains("Purple"))
                {
                    votePlayerColorNumber = "1";
                }


                if (playerColor.Contains("Brown"))
                {
                    votePlayerColorNumber = "2";
                }



                if (playerColor.Contains("Green"))
                {
                    votePlayerColorNumber = "3";
                }



                if (playerColor.Contains("Yellow"))
                {
                    votePlayerColorNumber = "4";
                }


                if (playerColor.Contains("Blue"))
                {
                    votePlayerColorNumber = "5";
                }
                if (playerColor.Contains("White"))
                {
                    votePlayerColorNumber = "6";
                }


                if (playerColor.Contains("Black"))
                {
                    votePlayerColorNumber = "7";
                }



                if (playerColor.Contains("Pink"))
                {
                    votePlayerColorNumber = "8";
                }



                if (playerColor.Contains("Orange"))
                {
                    votePlayerColorNumber = "9";
                }


                if (playerColor.Contains("red"))
                {
                    votePlayerColorNumber = "10";
                }
                #endregion
                int survived = 1;
                if (photonActorID == _network.getActorId())
                {
                    survived = 0;
                }
                string remainingPlayer = "";
                IDictionary<int, string> allplayers = m_reference.getPlayers();
                IDictionary<int, string> kickedPlayer = m_reference.getKickedplayers();
                int k = 0;
                foreach (KeyValuePair<int, string> item in allplayers)
                {
                    foreach (KeyValuePair<int, string> innerItem in kickedPlayer)
                    {
                        if (item.Value == innerItem.Value)
                        {

                        }
                        else
                            remainingPlayer = remainingPlayer + innerItem.Value + "-";
                    }
                }
                int durationTimestamp = (int)_network.getRPC_currentTimestampDouble() - _network.getRPC_GameStartTimestamp();
                string isSab;
                if (_network.getIsSaboteur())
                    isSab = "1";
                else
                    isSab = "0";
                databaseLogger.sendRequest(
                    _network.getActorId().ToString(), _network.getSessionID(), _network.getRPC_currentTimestampString(),
                    durationTimestamp.ToString(), m_reference.getGameRound().ToString(), _network.getMaxPlayer().ToString(), remainingPlayer, myPlayerColorNumber, votePlayerColorNumber, "1", survived.ToString(), "", isSab);
            }
            receivedVotes += 1;
        }        
        
        if (receivedVotes >= _network.getActorsInRoom())//Wenn alle spieler einen vote gesendet haben ergebnis bildschirm öffnen
        { 
            print("Voting result \n-------------------------------------------\n" + "Actors: " + _network.getActorsInRoom() + "Submit: " + receivedVotes);
            receivedVotes = 0;
            finalReveal(photonActorID, playerColor);
        }
    }
    public void submitSuspicion(string playerColor)
    {
        string filename = "Player Color/" + playerColor + "_Char";
        Sprite sp = Resources.Load<Sprite>(filename);
        img_votedPlayer.sprite = sp;
        p_manager.sendText("Du verdaechtigst diesen Spieler.\nFinale Entscheidung nach der folgenden Diskussionsrunde.");
    }
    public void finalReveal(int photonActorID, string playerColor)
    {
        print("DEBUG: finalReveal");

        if (m_reference.getCurrentStage() == 3){
            print("DEBUG: finalReveal Stage 3");
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
            if (mostVoted.Value == 0) { 
                print("No on was voted");
                //thisResultVotingPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Kein Spieler ist ausgeschieden.";
                string filename = "Player Color/Saboteur_Char";
                Sprite sp = Resources.Load<Sprite>(filename);
                img_votedPlayer.sprite = sp;
                Setup();
                tmp_resultTexts[4].SetActive(true);
            }
            else if (mostVoted.Value == equal.Value) { 
                print("We have a tie between: " + player[mostVoted.Key] + " and " + player[equal.Key]);
                string filename = "Player Color/Saboteur_Char";
                Sprite sp = Resources.Load<Sprite>(filename);
                img_votedPlayer.sprite = sp;
                Setup();
                tmp_resultTexts[3].SetActive(true);
            }
            else if (mostVoted.Value > 0)
            {
                m_reference.addKickedplayers(mostVoted.Key, player[mostVoted.Key]);// wird zur gekickten Liste geaddet
                print("Most voted player: " + player[mostVoted.Key]);
                string filename = "Player Color/" + player[mostVoted.Key] + "_Char";
                Sprite sp = Resources.Load<Sprite>(filename);
                img_votedPlayer.sprite = sp;
                Setup();
                tmp_resultTexts[2].SetActive(true);
                _network.setPlayerToGhost(m_reference.getPhotonIDbyActorID(mostVoted.Key));
            }            
        }
        else if (m_reference.getCurrentStage() == 1)
        {
            print("DEBUG: finalReveal Stage 1");
            if (playerColor =="" && photonActorID == 0) 
            {
                try
                {
                    tmp_resultTexts[0].SetActive(true);
                    Invoke("setPreNoChoiceActive", 0.5f);
                    p_manager.sendText( "Kein Spieler wurde ausgewählt.");

                }
                catch (Exception e)
                {
                    print("DEBUG AB HIER: " + e);
                }
            }
            else { 
                print("MyChoice: " + photonActorID);
                string filename = "Player Color/" + playerColor + "_Char";
                Sprite sp = Resources.Load<Sprite>(filename);
                img_votedPlayer.sprite = sp;
                Invoke("setPreChoiceActive", 0.5f); tmp_resultTexts[1].SetActive(true);
                string myPlayerColorNumber = "";
                string votePlayerColorNumber = "";
                #region ifs


                if (_network.getPlayerColor().Contains("Black"))
                {
                    myPlayerColorNumber = "7";
                }
                else if (_network.getPlayerColor().Contains("White"))
                {
                    myPlayerColorNumber = "6";
                }
                else if (_network.getPlayerColor().Contains("Pink"))
                {
                    myPlayerColorNumber = "8";
                }
                else if (_network.getPlayerColor().Contains("Orange"))
                {
                    myPlayerColorNumber = "9";
                }
                else if (_network.getPlayerColor().Contains("Red"))
                {
                    myPlayerColorNumber = "10";
                }
                if (_network.getPlayerColor().Contains("Purple"))
                {
                    myPlayerColorNumber = "1";
                }
                else if (_network.getPlayerColor().Contains("Purple"))
                {
                    myPlayerColorNumber = "1";
                }

                if (_network.getPlayerColor().Contains("Brown"))
                {
                    myPlayerColorNumber = "2";
                }
                else if (_network.getPlayerColor().Contains("Purple"))
                {
                    myPlayerColorNumber = "2";
                }


                if (_network.getPlayerColor().Contains("Green"))
                {
                    myPlayerColorNumber = "3";
                }
                else if (_network.getPlayerColor().Contains("Green"))
                {
                    myPlayerColorNumber = "3";
                }


                if (_network.getPlayerColor().Contains("Yellow"))
                {
                    myPlayerColorNumber = "4";
                }
                else if (_network.getPlayerColor().Contains("Yellow"))
                {
                    myPlayerColorNumber = "4";
                }

                if (_network.getPlayerColor().Contains("Blue"))
                {
                    myPlayerColorNumber = "5";
                }
                else if (_network.getPlayerColor().Contains("Blue"))
                {
                    myPlayerColorNumber = "5";
                }
                if (playerColor.Contains("Purple"))
                {
                    votePlayerColorNumber = "1";
                }


                if (playerColor.Contains("Brown"))
                {
                    votePlayerColorNumber = "2";
                }



                if (playerColor.Contains("Green"))
                {
                    votePlayerColorNumber = "3";
                }



                if (playerColor.Contains("Yellow"))
                {
                    votePlayerColorNumber = "4";
                }


                if (playerColor.Contains("Blue"))
                {
                    votePlayerColorNumber = "5";
                }
                if (playerColor.Contains("White"))
                {
                    votePlayerColorNumber = "6";
                }


                if (playerColor.Contains("Black"))
                {
                    votePlayerColorNumber = "7";
                }



                if (playerColor.Contains("Pink"))
                {
                    votePlayerColorNumber = "8";
                }



                if (playerColor.Contains("Orange"))
                {
                    votePlayerColorNumber = "9";
                }


                if (playerColor.Contains("red"))
                {
                    votePlayerColorNumber = "10";
                }
                #endregion
                int survived = 1;
                if (photonActorID == _network.getActorId())
                {
                    survived = 0;
                }
                string remainingPlayer = "";
                IDictionary<int, string> allplayers = m_reference.getPlayers();
                IDictionary<int, string> kickedPlayer = m_reference.getKickedplayers();
                int k = 0;
                foreach (KeyValuePair<int, string> item in allplayers)                
                {
                    foreach (KeyValuePair<int, string> innerItem in kickedPlayer)
                    {
                        if (item.Value == innerItem.Value)
                        {

                        }
                        else
                            remainingPlayer = remainingPlayer + innerItem.Value + "-";
                    }
                }
                int durationTimestamp = (int)_network.getRPC_currentTimestampDouble() - _network.getRPC_GameStartTimestamp();
                string isSab;
                if (_network.getIsSaboteur())
                    isSab = "1";
                else
                    isSab = "0";
                databaseLogger.sendRequest(
                    _network.getActorId().ToString(), _network.getSessionID(), _network.getRPC_currentTimestampString(),
                    durationTimestamp.ToString(), m_reference.getGameRound().ToString(), _network.getMaxPlayer().ToString(), remainingPlayer, myPlayerColorNumber, votePlayerColorNumber, "0", survived.ToString(), "", isSab);
            }
            // userID,  SessionID,  TimeStamp,  Duration,  Round,  NumberPlayers,  RemaingPlayers,  avatarColor,  value,  type,  survive,  sentiment,  topic
        } // else (stage 1)
    }
    public void setPreChoiceActive()
    {
        tmp_resultTexts[1].SetActive(true);
    }  
    public void setPreNoChoiceActive()
    {
        tmp_resultTexts[0].SetActive(true);
    }

    public void Setup() 
    {
        for(int i = 0; i < tmp_resultTexts.Length; i++)
        {
            tmp_resultTexts[i].SetActive(false);
        }
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
