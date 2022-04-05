using System;
using System.Linq;
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
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] int knockbackX;
    [SerializeField] int knockbackY;

    private Rigidbody2D rb;
    private Animator animator;

    private GameObject shield;
    public bool shielded = false;

    private void Awake() {
        GameManager.OnGameStateChanged += OnGameStateChanged;

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

    void OnDestroy() {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state) {
        if(state == GameState.Depression) this.animator.SetBool("isDemon", true);
    }

    public void PerformShield(float value)
    {
        if(value == 1f && GetComponent<Movement>().Grounded && shieldLife > 0){
            DisableActions();

            if(shielded) {
                shield.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            } else {
                shield = (GameObject)Instantiate(shieldPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation, gameObject.transform);
                shield.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                shielded = true;
            }

            // Play shield animation
            animator.SetTrigger("Shield");
        }else{
            Destroy(shield);
            shielded = false;
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
        DisableActions();
        
        // Play attack animation
        animator.SetTrigger("Attack");
        Attack();
    }

    // ATTENTION : le code qui va suivre peut comporter des images choquantes
    // Si vous vous considérez comme un développeur, vous pouvez ignorer cette partie
    // et continuer à travailler sur le code après cette fonction

    public void PerformBigAttack()
    {
        if(cannotAttack || !GetComponent<Movement>().Grounded) return;
        DisableActions();
        
        // Play attack animation
        animator.SetTrigger("BigAttackIn");

        Attack(bigAttackMultiplier);
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
                animator.SetTrigger("BigAttackOut");
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
            // TODO: add things -> balayette
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
                int damage = (int) Math.Round(GetComponent<Player>().attack * multiplier);

                // enemy is blocking
                if (enemy.GetComponent<Combat>().shielded) {
                    enemy.GetComponent<Combat>().shieldLife -= damage;
                    if(enemy.GetComponent<Combat>().shieldLife < 0) {
                        damage = -enemy.GetComponent<Combat>().shieldLife;
                        enemy.GetComponent<Combat>().shieldLife = -3; // cooldown before enemy can use shield again * (-1)
                        enemy.GetComponent<Combat>().shielded = false;
                        Destroy(enemy.GetComponent<Combat>().shield);
                    } else {
                        damage = 0;
                    }
                }

                Debug.Log("Dealt " + damage + " damages.");

                if (damage > 0) {
                    enemy.GetComponent<FlashEffect>().Flash(flashColor);
                    enemy.gameObject.GetComponent<Player>().TakeDamage(damage);

                    float rotation;
                    if (transform.transform.eulerAngles.y <= 180f) {
                        rotation = transform.eulerAngles.y;
                    }
                    else{
                        rotation = transform.eulerAngles.y - 360f;
                    }
                    rotation = rotation > 0 ? 1 : -1;
                    Vector3 knockback = new Vector3(knockbackX*rotation, knockbackY, 0);
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    enemy.GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Force);
                }
            }
        }

        EnableActions();
    }
}
