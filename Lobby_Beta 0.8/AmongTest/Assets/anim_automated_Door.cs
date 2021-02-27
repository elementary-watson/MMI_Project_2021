using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class anim_automated_Door : MonoBehaviour
{
    [HideInInspector] public bool playerContact = false;
    public Animator animator;
    Network _network;
    
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
        animator.SetInteger("transition", 1);//("Horizontal", movement.x);
        //_network.;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetInteger("transition", 2);        
        Invoke("setIdle", 1);
    }
    private void setIdle()
    {
        animator.SetInteger("transition", 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
