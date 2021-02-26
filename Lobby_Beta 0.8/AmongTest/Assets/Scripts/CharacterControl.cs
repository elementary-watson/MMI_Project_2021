﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : Photon.Pun.MonoBehaviourPun
{
    // Start is called before the first frame update
    public GameObject interactIcon;
    private Animator anim;
    private float moveSpeed = 8;
    private float xVal;
    private bool isWalking;
    //Untere Werte 0.1f, 1f
    private Vector2 boxSize = new Vector2(1f, 1f);
    Image temp;
    private void Awake()
    {
       /* print("FINDE MICH");
        if (temp == null)
        {
            try 
            { 
                //Image temp = GameObject.FindGameObjectWithTag("Respawn").GetComponentInChildren<Image>();
                temp = GetComponent<UnityEngine.UI.Image>();
                temp.enabled = false;
            }
            catch (Exception e)
            {
                print("Yoyo " + e);
            }
        }*/
    }
    void Start()
    {

        print("Gefunden");
        //GameObject temp = gameObject.AddComponent<Image>();
        //temp.enabled = true;

        if (photonView.IsMine) {
            interactIcon = GameObject.Find("test");//GetComponentInParent<GameObject>();
            if(interactIcon != null)
            {
                interactIcon.transform.position = new Vector2(792,-9999);
                print("FUUUUCKCKCKCK Print: " + interactIcon.GetComponentInChildren<Text>().text);
            }
            
            
            //canvasreference.SetActive(false);
            //interactIcon.SetActive(false);
            anim = GetComponent<Animator>();
            //Instantiate(interactIcon, new Vector2(792,-398), Quaternion.identity, GameObject.FindGameObjectWithTag("Respawn").transform);
            //GameObject temp =  temp.transform.SetParent(GameObject.FindGameObjectWithTag("Respawn").transform, false);
            //temp.transform.position = new Vector2(792, -398);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            interactIcon = GameObject.FindGameObjectWithTag("test");
            if (Input.GetKeyDown(KeyCode.Space)) CheckInteraction();
        }
        //xVal = Input.GetAxisRaw
    }

    public void OpenInteractableIcon()
    {
        if (photonView.IsMine)
        {
            //interactIcon.SetActive(true);
            interactIcon.transform.position = new Vector2(792, -398);
            //image_interactable_controll.
        }

    }
    public void CloseInteractableIcon()
    {
        if (photonView.IsMine)
        {
            interactIcon.transform.position = new Vector2(792, -9999);
            //interactIcon.SetActive(false);
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
                        return;
                    }
                }
            }
        }
    }
}
