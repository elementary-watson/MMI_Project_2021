using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : Photon.Pun.MonoBehaviourPun
{
    // Start is called before the first frame update
    public GameObject interactIcon;
    [SerializeField] private Camera cam;
    private Animator anim;
    private float moveSpeed = 8;
    private float xVal;
    private bool isWalking;
    //Untere Werte 0.1f, 1f
    private Vector2 boxSize = new Vector2(1f, 1f);
    Image temp;

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
    void Start()
    {
        if (cam == null)
            cam = Camera.main;
        //Invoke("resetPosition",1);
        print("Gefunden");
        //GameObject temp = gameObject.AddComponent<Image>();
        //temp.enabled = true;

        if (photonView.IsMine) {
            //interactIcon = GameObject.Find("test");//GetComponentInParent<GameObject>();
            if(interactIcon != null)
            {
                //interactIcon.transform.position = new Vector2(792,-9999);
                print("FUUUUCKCKCKCK Print: " + interactIcon.GetComponentInChildren<Text>().text);
            }
            else
            {
                print("DEBUG: InteractIcon not set yet");
            }
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
            interactIcon.GetComponentInChildren<Text>().text = "Use";
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
                        return;
                    }
                }
            }
        }
    }
}
