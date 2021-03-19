using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_Maptask_Script : Interactable
{
    [SerializeField] Main_Console_Script mcs_object;
    [SerializeField] string mytag;
    [SerializeField] SpriteRenderer the_active_state;
    [SerializeField] bool isInteractable;
    [SerializeField] GameObject myPanel;
    [SerializeField] bool holdMovement;// damit kein (rpc) network calls pro frame passieren
    int updateHelper;// damit kein (rpc) network calls pro frame passieren

    public override void Interact()
    {
        if (isInteractable)
        {
            holdMovement = true;
            myPanel.SetActive(true);
            the_active_state.enabled = false;
            isInteractable = false;
            mcs_object.setIsInteractable(true);
        }        
    }
    public void Reset()
    {
        myPanel.SetActive(false);
        updateHelper = 0;
        holdMovement = false;
        the_active_state.enabled = false;
    }
    public void setIsInteractable(bool isInteractable)
    {
        the_active_state.enabled = true;
        this.isInteractable = isInteractable;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateHelper = 0;
        holdMovement = false;
        mytag = GetComponent<BoxCollider2D>().tag;
        the_active_state.enabled = false;
    }
    public string getMyTag() {return mytag;}
    // Update is called once per frame
    private void Update()
    {
        if (holdMovement)
        {
            if(myPanel.activeSelf == true)
            {
                if(updateHelper == 0) // damit kein (rpc) network calls pro frame passieren
                { 
                    mcs_object.playerMovement(true);
                    updateHelper = 1;
                }
            }
            else
            {
                if(updateHelper == 1) { 
                    mcs_object.playerMovement(false);
                    updateHelper = 0;
                    holdMovement = false;
                }
            }
        }
        
    }
}
