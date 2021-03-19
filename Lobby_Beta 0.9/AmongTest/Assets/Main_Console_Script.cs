using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Console_Script : Interactable
{
    [SerializeField] string currentTask;
    [SerializeField] Single_Maptask_Script[] sms_objects = new Single_Maptask_Script[0];
    [SerializeField] Network _network;
    [SerializeField] SpriteRenderer[] the_active_state = new SpriteRenderer[0];
    [SerializeField] bool isInteractable;
    [SerializeField] BoxCollider2D mainCollider;

    public override void Interact()
    {
        if (isInteractable)
        {
            for (int i = 0; i < 4; i++)
            {
                the_active_state[i].enabled = false;
            }
            _network.getNextTask();
            isInteractable = false;
        }
    }
    public void setCurrentTask(string currentTask)
    {
        this.currentTask = currentTask;
        foreach (Single_Maptask_Script item in sms_objects)
        {
            if(item.getMyTag() == currentTask)
            {
                print("The Task was found in Multiplayer");
                item.setIsInteractable(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Player Trigger");
        if (isInteractable)
        {
            if (collision.CompareTag("Player"))
                collision.GetComponent<CharacterControl>().OpenInteractableIcon();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isInteractable)
        {
            if (collision.CompareTag("Player"))
                collision.GetComponent<CharacterControl>().CloseInteractableIcon();
        }
    }
    public void setIsInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
        
        if (isInteractable)
        {
            for (int i = 0; i < 4; i++)
            {
                the_active_state[i].enabled = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCollider.isTrigger = true;
        setIsInteractable(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
