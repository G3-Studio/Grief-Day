using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float attackRange = 0.9f;
    public int shieldLife = 100;
    public int shieldLifeMax = 100;
    public int shieldLifeRegen = 1;
    public float attackAirReduce = 2f;
    public float bigAttackMultiplier = 1.5f;
    public bool cannotAttack = false;
    public float axisInputNormalizeX = 0.7f;
    public float axisInputNormalizeY = 1.5f;

    [SerializeField] Color32 flashColor;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        string[] array = { "Attack", "BigAttackIn", "BigAttackOut" };

        for(int i=0; i<animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];
            if(array.Contains(clip.name))
            {
                AnimationEvent animationEndEvent = new AnimationEvent();
                animationEndEvent.time = clip.length;
                animationEndEvent.functionName = "AnimationCompleteHandler";
                animationEndEvent.stringParameter = clip.name;
                
                clip.AddEvent(animationEndEvent);
            } 
        }
        
        InvokeRepeating("RegenShield", 0, 1); // shield regen every second
    }

    public void PerformShield(float value)
    {
        if(value == 1f && GetComponent<Movement>().Grounded){
            // TODO: decomment once animations are implemented DisableActions();

            // Play attack animation
            // animator.SetTrigger("Shield");

            // TODO: add UI for shield life
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
        // TODO: decomment once animations are implemented DisableActions();
        // Play attack animation
        // animator.SetTrigger("Attack");
        Attack();
    }

    // ATTENTION : le code qui va suivre peut comporter des images choquantes
    // Si vous vous considérez comme un développeur, vous pouvez ignorer cette partie
    // et continuer à travailler sur le code après cette fonction

    public void PerformBigAttack()
    {
        // TODO: add timer
        if(cannotAttack || !GetComponent<Movement>().Grounded) return;
        // TODO: decomment once animations are implemented DisableActions();
        // Play attack animation
        // animator.SetTrigger("BigAttackIn");

        Attack(bigAttackMultiplier);

        EnableActions();
    }


    public void AnimationCompleteHandler(string name)
    {
        Debug.Log($"{name} animation complete.");
        switch(name)
        {
            case "Attack":
                EnableActions();
                break;
            case "BigAttackIn":
                Attack(bigAttackMultiplier);
                // Play attack animation
                // animator.SetTrigger("BigAttackOut");
                break;
            case "BigAttackOut":
                EnableActions();
                break;
        }
    }


    public void Attack(float multiplier = 1.0f){
        Collider2D[] hitEnnemies;
        if(GetComponent<Movement>().axisInput.y == 0 && GetComponent<Movement>().axisInput.y == 0){
            hitEnnemies = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + (GetComponent<Movement>().isLeft ? -1 : 1) * axisInputNormalizeX, transform.position.y), GetComponent<Movement>().Grounded ? attackRange : attackRange / attackAirReduce);
        }else if(GetComponent<Movement>().axisInput.y == -1){
            // TODO: add things
            // Detect ennemies in range and deal damage
            hitEnnemies = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + GetComponent<Movement>().axisInput.x * axisInputNormalizeX, transform.position.y + GetComponent<Movement>().axisInput.y * axisInputNormalizeY), GetComponent<Movement>().Grounded ? attackRange : attackRange / attackAirReduce);
        }else{
            // Detect ennemies in range and deal damage
            hitEnnemies = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + GetComponent<Movement>().axisInput.x * axisInputNormalizeX, transform.position.y + GetComponent<Movement>().axisInput.y * axisInputNormalizeY), GetComponent<Movement>().Grounded ? attackRange : attackRange / attackAirReduce);
        }

        foreach (Collider2D enemy in hitEnnemies)
        {
            if (enemy.name == "Player " + (GetComponent<Player>().isPlayer1 ? "2" : "1"))
            {   
                Debug.Log("Hit " + enemy.name);
                enemy.GetComponent<FlashEffect>().Flash(flashColor);
                int damage = (int) Math.Round(GetComponent<Player>().attack * multiplier);
                if(shieldLife > 0) {
                    shieldLife -= damage;
                    if(shieldLife < 0) {
                        damage += shieldLife;
                        shieldLife = 0;
                        EnableActions();
                    }
                }

                enemy.gameObject.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }
}
