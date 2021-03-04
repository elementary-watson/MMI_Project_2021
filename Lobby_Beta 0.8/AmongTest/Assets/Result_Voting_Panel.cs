using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_Voting_Panel : MonoBehaviour
{
    IDictionary<int, string> player; // 
    IDictionary<int, int> finalVotings = new Dictionary<int,int>(); // actor id , votings
    [SerializeField] private Multiplayer_Reference m_reference;
    [SerializeField] private Image img_votedPlayer;
    [SerializeField] private Network _network;
    int receivedVotes;
    
    // Start is called before the first frame update

    public void submitVote(int myActorID, string myplayerColor, string playerColor, int photonActorID,int indexPosition)
    {
        print("DEBUG: " + myActorID + " " + myplayerColor + " " + playerColor + " " + photonActorID + " " + indexPosition);
        //Hier muss geloggt werden!
        if ((playerColor == "") || (photonActorID == 0))
            print("Actor " + myActorID + " - " + myplayerColor + " made no Choice");
        else {
            if (finalVotings == null) { 
                //KeyValuePair<int, int> item = 1,1;
                finalVotings.Add(photonActorID, 1);
            }
            else if (!(finalVotings.Keys.Contains(photonActorID)))
                finalVotings.Add(photonActorID, 1);
            else
                finalVotings[photonActorID] += 1;
        }
        receivedVotes += 1;
        print("Actors: " +_network.getActorInRoom());
        print("Submit: " + receivedVotes);
        if (receivedVotes == _network.getActorInRoom())
        {
            finalReveal();
        }

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
            
        


        //svl[i].setClassValues(item.Value, item.Key, i);//set (color, id, index) of buttons
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
