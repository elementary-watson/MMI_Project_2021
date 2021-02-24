using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public float moveSpeed = 5f;
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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 velocity = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        Vector3 scale = transform.localScale;
        scale.x = scaleX * (velocity.x >= 0 ? 1 : -1);
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
}
