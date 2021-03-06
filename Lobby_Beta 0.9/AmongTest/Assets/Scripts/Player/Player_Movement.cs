using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Photon.Pun.MonoBehaviourPun
{
    public float moveSpeed = 8f;
    public Animator animator;
    public Rigidbody2D rb;
    float scaleX;

    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        scaleX = transform.localScale.x;
    }
    private void FixedUpdate()
    {
        if (photonView.IsMine) { 
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        /*Vector2 velocity = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        Vector3 scale = transform.localScale;
        
        scale.x = scaleX * (velocity.x >= 0 ? 1 : -1);
        transform.localScale = scale;*/
        //Input.GetKeyDown(KeyCode.Space);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) { 
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1  || Input.GetAxisRaw("Vertical") == -1)
            {
                animator.SetFloat("LastMoveX",Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("LastMoveY",Input.GetAxisRaw("Vertical"));
            }
        }
    }
}
