using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : Photon.Pun.MonoBehaviourPun
{
    // Start is called before the first frame update
    public GameObject interactIcon;
    [SerializeField] SpriteRenderer Halo;
    [SerializeField] private Camera cam;
    private Animator anim;
    private float moveSpeed = 8;
    private float xVal;
    private bool isWalking;
    //Untere Werte 0.1f, 1f
    private Vector2 boxSize = new Vector2(1f, 1f);
    public Map_Control_Script mcs_object;
    [SerializeField] Multiplayer_Reference m_reference;
    [SerializeField] float incrementTaskPower;
    [SerializeField] int actorID;
    [SerializeField] Main_Console_Script mainConsole_object;
    [SerializeField] string currentTask;
    [SerializeField] Game_Info_Script gInfoScript_object;

    public void resetTask()
    {
        currentTask = "null";
    }
    public void setMCSScript(Map_Control_Script mcs_object)
    {
        this.mcs_object = mcs_object;
    }
    public void setMultiplayerReference(Multiplayer_Reference m_reference)
    {
        this.m_reference = m_reference;
    }
    public void setStatusToGhost()
    {
        Halo.enabled = true;
        incrementTaskPower = m_reference.getGhostIncrementPower();
    }    
    public void setStatusToSaboteur()
    {        
        incrementTaskPower = m_reference.getSaboteurDecrementPower();
    }
    public void setMainConsoleScript(Main_Console_Script mainConsole_object)
    {
        this.mainConsole_object = mainConsole_object;
    }
    public void setGameInfoScript(Game_Info_Script gInfoScript_object)
    {
        this.gInfoScript_object = gInfoScript_object;
    }
    public void setPosition() 
    {
        RectTransform rt = (RectTransform)interactIcon.transform;
        float xValue = (float)(Screen.width * 0.75 - rt.rect.width * 0.5);
        float yValue = (float)(Screen.height * 0.1 + rt.rect.height * 0.5);
        //Vector3 icon_position = new Vector3(cam.WorldToViewportPoint())
        interactIcon.transform.position = new Vector2(xValue,yValue);
    }
    public void resetPosition()
    {
        RectTransform rt = (RectTransform)interactIcon.transform;
        float xValue = (float)(Screen.width*2);
        float yValue = (float)(Screen.height*2);
        //Vector3 icon_position = new Vector3(cam.WorldToViewportPoint())
        interactIcon.transform.position = new Vector2(xValue,yValue);
    }
    public float getIncrementPower()
    {
        if (actorID == m_reference.getSaboteurActorID())
        {
            return m_reference.getSaboteurDecrementPower();
        }
        return incrementTaskPower;
    }
    public int getActorID()
    {
        return actorID;
    }    
    public void setActorID(int actorID)
    {
        this.actorID = actorID;
    }

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        if (photonView.IsMine) 
        {
            incrementTaskPower = m_reference.getPlayerIncrementPower();
            anim = GetComponent<Animator>();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)  //XOF
    {
        if (photonView.IsMine)
        {
            
            if (collision.gameObject.tag == "Tag_MainConsole")
            {
                if (mainConsole_object.getIsInteractable())
                {
                    print("Hello Worlds");
                    OpenInteractableIcon();
                    
                }
            }
            else if (collision.gameObject.tag == currentTask) 
            {
                if (!mainConsole_object.getIsInteractable()) { 
                    OpenInteractableIcon();
                    //CheckInteraction();
                }
            }
            else if (collision) { }
        }

        if (photonView.IsMine)
        {

            if (collision.gameObject.CompareTag("Collidor_Hauptraum"))
            {
                mcs_object.SetRoomName("Hauptraum", 0, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Freizeitraum"))
            {
                mcs_object.SetRoomName("Freizeitraum", 1, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_NR_no"))
            {
                mcs_object.SetRoomName("", 2, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Elektrik"))
            {
                mcs_object.SetRoomName("Elektrikraum", 3, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_NR_so"))
            {
                mcs_object.SetRoomName("", 4, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Energie"))
            {
                mcs_object.SetRoomName("Energieraum", 5, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Medizinraum"))
            {
                mcs_object.SetRoomName("Medizinraum", 6, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_NR_sw"))
            {
                mcs_object.SetRoomName("", 7, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Labor"))
            {
                mcs_object.SetRoomName("Labor", 8, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_NR_nw"))
            {
                mcs_object.SetRoomName("", 9, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Flur"))
            {
                mcs_object.SetRoomName("", 10, currentTask);
            }
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Tag_MainConsole")
            {
                CloseInteractableIcon();
            }
            else if (collision.gameObject.tag == currentTask)
            {
                CloseInteractableIcon();
            }
            else if (collision) { }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            //interactIcon = GameObject.FindGameObjectWithTag("test");
            if (Input.GetKeyDown(KeyCode.Space)) CheckInteraction();
         
        }
        //xVal = Input.GetAxisRaw
    }
    public void OpenInteractableIcon()
    {
        if (photonView.IsMine)
        {
            interactIcon.GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(100,100);
            //interactIcon.GetComponentInChildren<Text>().text = "Use";
            //interactIcon.SetActive(true);
            setPosition();
            //interactIcon.transform.position = new Vector2(1650, 10);
            //image_interactable_controll.
        }

    }
    public void CloseInteractableIcon()
    {
        if (photonView.IsMine)
        {
            //interactIcon.transform.position = new Vector2(Screen.width, Screen.height);
            //interactIcon.SetActive(false);
            resetPosition();
        }
    }
    public void CheckInteraction()
    {
        if (photonView.IsMine)
        {
                RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

            if (hits.Length > 0)
            {
                foreach (RaycastHit2D rc in hits)
                {
                    if (rc.transform.GetComponent<Interactable>())
                    {
                        rc.transform.GetComponent<Interactable>().Interact();
                        mcs_object.resetTargetImages();
                        currentTask = mainConsole_object.getCurrentTask();
                        CloseInteractableIcon();
                        mcs_object.setTargetRoom(currentTask);
                        gInfoScript_object.shortNotification("goCenter");
                        return;
                    }
                    else if (rc.transform.GetComponent<BoxCollider2D>().CompareTag("Tag_MainConsole"))
                    {
                        currentTask = mainConsole_object.Interact();
                        mcs_object.resetTargetImages();
                        mcs_object.setTargetRoom(currentTask);
                        gInfoScript_object.shortNotification("backCenter");
                    }
                }
            }
        }
    }
}
