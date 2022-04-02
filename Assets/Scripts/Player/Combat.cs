using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int shieldLife = 100;
    public int shieldLifeMax = 100;
    public int shieldLifeRegen = 1;
    public float attackAirReduce = 2f;
    public float bigAttackMultiplier = 1.5f;
    public bool cannotAttack = false;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        InvokeRepeating("RegenShield", 0, 1); // shield regen every second
    }

    public void PerformShield(float value)
    {
        if(value == 1f && GetComponent<Movement>().Grounded){
            DisableActions();

            // Play attack animation
            // animator.SetTrigger("Shield");
        }else{
            EnableActions();
        }
    }

    public void DisableActions(){
        GetComponent<Player>().cannotUseSkill = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Movement>().canMove = false;
        cannotAttack = true;
    }

    public void EnableActions(){
        GetComponent<Player>().cannotUseSkill = false;
        GetComponent<Movement>().canMove = true;
        cannotAttack = false;
    }

    public void RegenShield() {
        if(shieldLife < shieldLifeMax) {
            shieldLife += shieldLifeRegen;
        }
    }

    public void PerformAttack()
    {
        if(cannotAttack) return;
        // Play attack animation
        // animator.SetTrigger("Attack");

        // Detect ennemies in range and deal damage
        Collider2D[] hitEnnemies = Physics2D.OverlapCircleAll(transform.position, GetComponent<Movement>().Grounded ? attackRange : attackRange / attackAirReduce);

        attack(hitEnnemies);
    }

    public void PerformBigAttack()
    {
        if(cannotAttack || GetComponent<Movement>().Grounded) return;
        DisableActions();
        // Play attack animation
        // animator.SetTrigger("BigAttack");

        // Detect ennemies in range and deal damage
        Collider2D[] hitEnnemies = Physics2D.OverlapCircleAll(transform.position, GetComponent<Movement>().Grounded ? attackRange : attackRange / attackAirReduce);

        attack(hitEnnemies, bigAttackMultiplier);

        // TODO: add things
    }

    public void attack(Collider2D[] hitEnnemies, float multiplier = 1.0f){
        foreach (Collider2D enemy in hitEnnemies)
        {
            if (enemy.name == "Player " + (GetComponent<Player>().isPlayer1 ? "2" : "1"))
            {   
                int damage = (int) Math.Round(GetComponent<Player>().attack * multiplier);
                if(shieldLife > 0) {
                    shieldLife -= damage;
                    if(shieldLife < 0) {
                        damage += shieldLife;
                        shieldLife = 0;
                        ReleaseShield();
                    }
                }

                enemy.gameObject.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
}
