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
    [SerializeField] bool canInteract;
    [SerializeField] Image img_active;
    [SerializeField] Image img_inactive;
    bool mainConsoleInteractable = true;
    public bool cooldown = false;

    #region getta/setta
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
        gInfoScript_object.shortNotification("isGhost");
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
    public void toggleInteractFunction(bool canInteract)
    {
        this.canInteract = canInteract;
        mainConsoleInteractable = true;
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
    public void setInteractImages(Image img_active, Image img_inactive) { this.img_active = img_active; this.img_inactive = img_inactive; }

    void Start()
    {
        canInteract = true;
        if (cam == null)
            cam = Camera.main;

        if (photonView.IsMine)
        {
            incrementTaskPower = m_reference.getPlayerIncrementPower();
            anim = GetComponent<Animator>();
        }
    }
    #endregion

    public void setPosition() 
    {
        img_active.enabled = true;
        img_inactive.enabled = false;
        /*RectTransform rt = (RectTransform)interactIcon.transform;
        float xValue = (float)(Screen.width * 0.75 - rt.rect.width * 0.5);
        float yValue = (float)(Screen.height * 0.1 + rt.rect.height * 0.5);
        //Vector3 icon_position = new Vector3(cam.WorldToViewportPoint())
        interactIcon.transform.position = new Vector2(xValue,yValue);*/
    }
    public void resetPosition()
    {
        img_active.enabled = false;
        img_inactive.enabled = true;
        /*RectTransform rt = (RectTransform)interactIcon.transform;
        float xValue = (float)(Screen.width*2);
        float yValue = (float)(Screen.height*2);
        //Vector3 icon_position = new Vector3(cam.WorldToViewportPoint())
        interactIcon.transform.position = new Vector2(xValue,yValue);*/
    }
    public void start()
    {
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
                mcs_object.SetRoomName("Main room", 0, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Freizeitraum"))
            {
                mcs_object.SetRoomName("Recreation room", 1, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_NR_no"))
            {
                mcs_object.SetRoomName("", 2, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Elektrik"))
            {
                mcs_object.SetRoomName("Electrical room", 3, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_NR_so"))
            {
                mcs_object.SetRoomName("", 4, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Energie"))
            {
                mcs_object.SetRoomName("Energy room", 5, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Medizinraum"))
            {
                mcs_object.SetRoomName("Medical Room", 6, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_NR_sw"))
            {
                mcs_object.SetRoomName("", 7, currentTask);
            }
            else if (collision.gameObject.CompareTag("Collidor_Labor"))
            {
                mcs_object.SetRoomName("Laboratory", 8, currentTask);
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
    public void resetCooldown()
    {
        cooldown = false;
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space) && cooldown == false) {
                cooldown = true;
                CheckInteraction();
                Invoke("resetCooldown", 1f);
            }
         
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
                    if (canInteract)
                    {                        
                                                    
                        if (rc.transform.GetComponent<Interactable>()) // Checkt nach Task Collidern
                        {
                            rc.transform.GetComponent<Interactable>().Interact();
                            CloseInteractableIcon();
                            mcs_object.resetTargetImages();
                            currentTask = mainConsole_object.getCurrentTask();
                            mcs_object.setTargetRoom(currentTask);
                            gInfoScript_object.shortNotification("goCenter");
                            mainConsoleInteractable = true;
                            return;
                        }
                       
                        else if (rc.transform.GetComponent<BoxCollider2D>().CompareTag("Tag_MainConsole")) // Main-Console
                        {
                            if (mainConsoleInteractable)
                            {
                                mainConsoleInteractable = false;
                                currentTask = mainConsole_object.Interact();
                                mcs_object.resetTargetImages();
                                mcs_object.setTargetRoom(currentTask);
                                gInfoScript_object.shortNotification("goTask");
                            }
                        }                        
                        
                    }
                }
            }
        }
    }
}
