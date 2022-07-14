using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask EnemyLayers;
    public Collider2D PlayerCollider;
    public float AttackRange = 0.5f;
    public int AttackDamage = 20;

    public int MaxHealth = 100;
    int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }

        
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemyPetrolAi>().TakeDamage(AttackDamage);
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
        //PlayerCollider.enabled = false;
    }
}
