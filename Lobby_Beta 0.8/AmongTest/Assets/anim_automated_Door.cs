using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class anim_automated_Door : MonoBehaviour
{
    private bool playerContact = false;
    public Animator animator;
    
    private void Reset()
    {
        //GetComponent<BoxCollider2D>().isTrigger = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("DEBUG: Collision DETECTED");

        if (playerContact==false)
        {
            GetComponent<Animator>().Play("open_Door");
            print("DEBUG: Collision ENTER");
            playerContact = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerContact == true)
        {
            print("DEBUG: Collision EXIT");            
            GetComponent<Animator>().Play("close_Door");
            playerContact = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
