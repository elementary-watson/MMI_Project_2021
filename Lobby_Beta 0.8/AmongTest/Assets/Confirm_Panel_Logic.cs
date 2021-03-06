using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confirm_Panel_Logic : MonoBehaviour
{
    //string playerColor, int photonActorID, int indexPosition
    string playerColor; 
    int photonActorID;
    int indexPosition;
    bool isSubmitted = false;
    [SerializeField] private Result_Voting_Panel result_vp;
    [SerializeField] private Network _network;
    [SerializeField] private GameObject timerPanel;

    public void setTemporaryVote(string playerColor, int photonActorID, int indexPosition)
    {
        this.playerColor = playerColor;
        this.photonActorID = photonActorID;
        this.indexPosition = indexPosition;
    }

    public void Timeout_ConfirmChoice()//läuft die zeit aus werden die auwahldaten geschickt
    {
        timerPanel.SetActive(false);
        if (isSubmitted)
           _network.callSubmitVote(true,playerColor,photonActorID,indexPosition);
        else
           _network.callSubmitVote(false,"",0,0);
        
        /*if (isSubmitted)
            result_vp.submitVote(_network.getActorId(), _network.getPlayerColor(), playerColor, photonActorID, indexPosition);
        else
            result_vp.submitVote(_network.getActorId(), _network.getPlayerColor(), "", 0, 0);*/
    }
    public void setIsSubmitted(bool choice)
    {
        this.isSubmitted = choice;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
