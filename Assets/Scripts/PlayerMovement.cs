using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private  float moveSpeed=5f;
    [SerializeField] private float VerticalJump = 700f;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        localScale = transform.localScale;
        
    }

   
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0) 
        {
            rb.AddForce(Vector2.up * VerticalJump);
        }
        if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0) 
        {
            animator.SetBool("IsRunning", true);
        }
        else 
        {
            animator.SetBool("IsRunning",false);
        }
        if (rb.velocity.y == 0) 
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
        if (rb.velocity.y > 0) 
        {
            animator.SetBool("IsJumping", true);
        }
        if (rb.velocity.y < 0) 
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }
    private void LateUpdate()
    {
        if (dirX > 0)
        {
            facingRight = true;
        }
        else if(dirX<0)
        {
            facingRight = false;
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;

        }
        transform.localScale = localScale;
    }
}
