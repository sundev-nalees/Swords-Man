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
    public LayerMask PlayerLayer;
    public Collider2D BodyCollider;
    public Collider2D GroundCollider;
    public Collider2D EnemyCollider;
    

    public Animator animator;
    public GameObject smallEnemy;

    public float AttackRange = 0.5f;
    public int AttackDamage = 20;
    public Transform AttackPoint;

    public float DeathDelay;
    public int MaxHealth = 100;
    int CurrentHealth;

    public AudioClip AttackSound;

    float AttackDelay=1.4f;
    float Timer=0f;
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

        //if (EnemyCollider.IsTouchingLayers(PlayerLayer))
        //{
           // Attack();
       // }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&Timer<Time.time)
        {
            Attack();
            Timer = Time.time + AttackDelay;
        }
        Debug.Log("hi");
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
       // animator.SetBool("IsMoving", true);
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



        Invoke("deactivate", DeathDelay);
        

       
    }
    void deactivate()
    {
        this.enabled = false;
        smallEnemy.SetActive(false);
        //BodyCollider.enabled = false;
        //GroundCollider.enabled = false;
        
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        SoundManager.instance.PlaySound(AttackSound);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerAttack>().TakeDamage(AttackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);

    }
}
