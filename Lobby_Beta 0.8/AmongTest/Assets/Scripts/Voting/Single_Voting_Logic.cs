using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Single_Voting_Logic : MonoBehaviour
{
    string playerColor;
    int photonActorID;
    int indexPosition;
    [SerializeField] private Panel_Voting_Logic pvl;
    [SerializeField] private GameObject ConfirmPanel; 
    [SerializeField] private Confirm_Panel_Logic script_confirm_pl; 
    
    // Start is called before the first frame update

    public void setClassValues(string playerColor, int photonActorID, int indexPosition)
    {
        this.playerColor = playerColor; 
        this.photonActorID= photonActorID; 
        this.indexPosition = indexPosition; 
    }
    void Start()
    {
        
    }
    public void btn_clickedPlayer()
    {
        pvl.disableButtons(indexPosition);
        script_confirm_pl.setTemporaryVote(playerColor, photonActorID, indexPosition);//Temporär Auswahl setzen!
        ConfirmPanel.SetActive(true);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
