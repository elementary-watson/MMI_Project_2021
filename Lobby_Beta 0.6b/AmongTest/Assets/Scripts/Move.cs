using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Photon.Pun.MonoBehaviourPun
{
    private float speed = 8;
    //Vector2 velocity;
    //private Rigidbody2D rb;
    private Rigidbody2D myRigidbody;
    private Vector3 change;

    /*private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
    }*/

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");

            if (change != Vector3.zero)
            {
                MoveCharacter();
            }

            //velocity.x = Input.GetAxisRaw("Horizontal");
            //velocity.y = Input.GetAxisRaw("Vertical");
        }
    }

    private void MoveCharacter()
    {
        if (photonView.IsMine)
        {
            myRigidbody.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
        }
    }


    /*private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            rb.MovePosition(rb.position + velocity * speed * Time.fixedDeltaTime);
        }
    }*/

}