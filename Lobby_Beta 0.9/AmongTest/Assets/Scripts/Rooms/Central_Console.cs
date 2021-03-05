using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Central_Console : Interactable
{
    public GameObject task;
    public override void Interact()
    {
        task.GetComponent<BoxCollider2D>().isTrigger = true;
        Task_Illuminate ti = task.GetComponent<Task_Illuminate>();
        ti.setStateActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
