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
    IDictionary<int, string> player;
    IDictionary<int, string> kickedPlayer;
    // Start is called before the first frame update

    public void Setup()
    {
        int i = 0;
        player = m_reference.getPlayers();
        kickedPlayer = m_reference.getKickedplayers();
        foreach (KeyValuePair<int, string> item in player)
        {
            if (kickedPlayer.ContainsKey(item.Key))
                buttonVotings[i].interactable = true;
            else
                buttonVotings[i].interactable = false;
            string filename = "Player Color/" + item.Value +"_Char";
            Sprite sp = Resources.Load<Sprite>(filename);
            buttonVotings[i].image.sprite = sp;
            
            svl[i].setClassValues(item.Value,item.Key,i);//set (color, id, index) of buttons
            i++;
        }
        for (i=i; i < 10; i++) // "ab i" restliche Buttons aussschalten.
        {
            buttonVotings[i].interactable = false;
        }
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
