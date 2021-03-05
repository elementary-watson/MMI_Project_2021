using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Photon.Pun.MonoBehaviourPun
{
    public float speed = 5;
    Vector2 velocity;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
    }

    private void Update()
    {

        if (photonView.IsMine)
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");
        }
            
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            rb.MovePosition(rb.position + velocity * (speed/2) * Time.fixedDeltaTime);
        }
    }

}