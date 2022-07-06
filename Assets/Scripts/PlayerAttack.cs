using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask EnemyLayers;


    public float AttackRange = 0.5f;
    public int AttackDamage = 20;
    
    
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
    
        
    
}
