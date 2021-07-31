using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Voting_Logic : MonoBehaviour
{
    public Image[] buttonimages = new Image[10];
    public Button[] buttonVotings = new Button[10];
    [SerializeField] private Single_Voting_Logic[] svl = new Single_Voting_Logic[10];
    public Multiplayer_Reference m_reference;
    List<PlayerAttributes> fullPlayerList;
    IDictionary<int, string> player;
    IDictionary<int, string> kickedPlayer;
    // Start is called before the first frame update

    public void Setup()
    {
        //player = m_reference.getPlayers();
        //kickedPlayer = m_reference.getKickedplayers();
        int i = 0;
        fullPlayerList = m_reference.getFullPlayerList();
        for (i = 0; i < fullPlayerList.Count; i++)
        {
            if (fullPlayerList[i].getPlayerIsGhost())
            {
                buttonVotings[i].interactable = false;
            }
            else { buttonVotings[i].interactable = true; }
            string filename = "Player Color/" + fullPlayerList[i].getPlayerColor() + "_Char";
            Sprite sp = Resources.Load<Sprite>(filename);
            buttonVotings[i].image.sprite = sp;
            svl[i].setClassValues(fullPlayerList[i].getPlayerColor(), fullPlayerList[i].getActorID(), i);//set (color, id, index) of buttons
        }
        for (i = i; i < 10; i++) // "ab i" restliche Buttons aussschalten.
        {
            buttonVotings[i].interactable = false;
        }
        /*
                 int j = 0;
        foreach (KeyValuePair<int, string> item in player)
        {
            if (kickedPlayer.ContainsKey(item.Key))
                buttonVotings[j].interactable = false;
            else
                buttonVotings[j].interactable = true;
            string filename = "Player Color/" + item.Value +"_Char";
            Sprite sp = Resources.Load<Sprite>(filename);
            buttonVotings[j].image.sprite = sp;
            
            svl[j].setClassValues(item.Value,item.Key,j);//set (color, id, index) of buttons
            j++;
        }
        for (j=j; j < 10; j++) // "ab i" restliche Buttons aussschalten.
        {
            buttonVotings[j].interactable = false;
        }
         */

    }
    public void disableButtons(int ignoreIndex)
    {
        for (int i = 0; i < 10; i++)
        {
            if(!(i==ignoreIndex))
            buttonVotings[i].interactable = false;
        }
    }public void enableButtons()
    {
        Setup();
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
