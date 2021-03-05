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
    // Start is called before the first frame update

    public void Setup()
    {
        int i = 0;
        player = m_reference.getPlayers();
        foreach (KeyValuePair<int, string> item in player )
        {
            string filename = "Player Color/" + item.Value +"_Char";
            Sprite sp = Resources.Load<Sprite>(filename);
            buttonVotings[i].image.sprite = sp;
            buttonVotings[i].interactable = true;
            svl[i].setClassValues(item.Value,item.Key,i);//set (color, id, index) of buttons
            i++;
        }
        for (i=i; i < 10; i++)
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
