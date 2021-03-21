using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Confirm_Panel_Logic : MonoBehaviour
{
    //string playerColor, int photonActorID, int indexPosition
    string playerColor; 
    int photonActorID;
    int indexPosition;
    bool isSubmitted = false;
    [SerializeField] private Result_Voting_Panel result_vp;
    [SerializeField] private Network _network;
    //[SerializeField] private GameObject timerPanel;
    [SerializeField] TextMeshProUGUI confirmTxt;
    [SerializeField] Multiplayer_Reference m_reference;


    public void setTemporaryVote(string playerColor, int photonActorID, int indexPosition)
    {
        if (m_reference.getCurrentStage() == 1)
            confirmTxt.text = "Spieler als Saboteur verdaechtigen ?";
        else
            confirmTxt.text = "Spieler als Saboteur beschuldigen ?";
        this.playerColor = playerColor;
        this.photonActorID = photonActorID;
        this.indexPosition = indexPosition;
    }

    public void prevote_Timeout_ConfirmChoice()
    {
        if(isSubmitted)
            print("isSubmitted == True");
        if (isSubmitted)
            result_vp.submitVote(_network.getActorId(), _network.getPlayerColor(), playerColor, photonActorID, indexPosition);
        else
            result_vp.submitVote(_network.getActorId(), _network.getPlayerColor(), "", 0, 0);
        this.playerColor = "";
        this.photonActorID = 0;
        this.indexPosition = 0;
    }
    public void photon_Timeout_ConfirmChoice()//läuft die zeit aus werden die auwahldaten geschickt
    {
        if (isSubmitted)
           _network.callSubmitVote(true,playerColor,photonActorID,indexPosition);
        else
           _network.callSubmitVote(false,"",0,0);
        this.playerColor = "";
        this.photonActorID = 0;
        this.indexPosition = 0;
    }
    public void setIsSubmitted(bool choice)
    {
        this.isSubmitted = choice;
    }
    // Start is called before the first frame update
    void Start()
    {
        this.playerColor = "";
        this.photonActorID = 0;
        this.indexPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
