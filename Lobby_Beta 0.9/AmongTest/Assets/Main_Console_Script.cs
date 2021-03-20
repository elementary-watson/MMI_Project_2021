using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Console_Script : MonoBehaviour
{
    [SerializeField] string currentTask;
    [SerializeField] Single_Maptask_Script[] sms_objects = new Single_Maptask_Script[0];
    [SerializeField] Network _network;
    [SerializeField] SpriteRenderer[] the_active_state = new SpriteRenderer[0];
    [SerializeField] bool isInteractable;
    [SerializeField] BoxCollider2D mainCollider;


    void Start()
    {
        mainCollider.isTrigger = true;
        setIsInteractable(true);
        currentTask = "null";
    }

    public void Reset()
    {
        mainCollider.isTrigger = true;
        setIsInteractable(true);
        currentTask = "null";
    }
    public string Interact() // Wird von CharakterControl gerufen
    {
        if (isInteractable)
        {
            for (int i = 0; i < 4; i++)
            {
                the_active_state[i].enabled = false;
            }
            _network.getNextTask();
            isInteractable = false;
            return currentTask;
        }
        return "null";
    }

    public void setCurrentTask(string currentTask) // Von Network aufgerufen nach interact()
    {
        //XOF Hier wird die Karte aktualisiert
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

    internal void playerMovement(bool isDoingTask)
    {
        if (isDoingTask)
            _network.setPlayerMovement(false);
        else
            _network.setPlayerMovement(true);
    }

    public void setIsInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
        currentTask = "null";
        if (isInteractable)
        {
            for (int i = 0; i < 4; i++)
            {
                the_active_state[i].enabled = true;
            }
        }
    }
    public bool getIsInteractable()
    {
        return isInteractable;
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
        print("Player Trigger");
        if (isInteractable)
        {
            if (collision.CompareTag("Player"))
                collision.GetComponent<CharacterControl>().CloseInteractableIcon();
        }
    }

    public string getCurrentTask()
    {
        return currentTask;
    }
}
