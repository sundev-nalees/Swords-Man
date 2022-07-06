using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPetrolAi : MonoBehaviour
{
    public float WalkSpeed;

    [HideInInspector] public bool MustPatrol;
    bool MustTurn;

    public Rigidbody2D rb;
    public Transform GroundCheckPos;
    public LayerMask GroundLayer;
    public Collider2D BodyCollider;

    public Animator animator;
    public GameObject smallEnemy;

    public int MaxHealth = 100;
    int CurrentHealth;
    void Start()
    {
        MustPatrol = true;
        CurrentHealth = MaxHealth;
    }

    
    void Update()
    {
        if (MustPatrol)
        {
            Patrol();
        }    
    }

    private void FixedUpdate()
    {
        if (MustPatrol)
        {
            MustTurn = !Physics2D.OverlapCircle(GroundCheckPos.position, 0.1f, GroundLayer);

        }
    }

    void Patrol()
    {
        if (MustTurn||BodyCollider.IsTouchingLayers(GroundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(WalkSpeed * Time.fixedDeltaTime, rb.velocity.y);

    }

    void Flip()
    {
        MustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        WalkSpeed *= -1;
        MustPatrol = true; 
    }


    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (CurrentHealth < 0)
        {
            Die();
        }

    }

    void Die()
    {
        animator.SetBool("IsDead", true);

        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        //smallEnemy.SetActive(false);
    }
}
