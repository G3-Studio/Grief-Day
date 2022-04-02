using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackRange = 1.5f;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        // Play attack animation
        // animator.SetTrigger("Attack");

        // Detect ennemies in range and deal damage
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D enemy in hitEnnemies)
        {
            if (enemy.gameObject.tag == "Player")
            {
                enemy.gameObject.GetComponent<Player>().TakeDamage(20);
            }
        }
    }

    public void PerformBigAttack()
    {
        // Play attack animation
        // animator.SetTrigger("BigAttack");

    }
}
