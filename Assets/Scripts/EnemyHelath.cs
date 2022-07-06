using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelath : MonoBehaviour
{
    public Animator animator;
    public GameObject smallEnemy;

    public int MaxHealth = 100;
    int CurrentHealth;
    void Start()
    {
        CurrentHealth = MaxHealth;
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

        smallEnemy.SetActive(false);
    }
}
