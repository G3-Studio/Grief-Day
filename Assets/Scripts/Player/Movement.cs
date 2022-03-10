
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private static TimeSpan STUN_TIME = new TimeSpan(0, 0, 1);
    private Rigidbody2D rb;
    private Animator animator;
    public bool canMove = true;
    public bool additionalJumpAvailable = false;
    private bool isLeft = true;
    private bool isJumping = false;
    bool Grounded, Stuck;
    private DateTime stunedAt;
    private Vector2 axisInput;
    private ArrayList skills = new ArrayList();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stunedAt = DateTime.MinValue;
    }

    public void SetInputMoveVector(Vector2 input)
    {
        axisInput = input;
    }

    private void OnEnable()
    {
        // Register skills and skill keybindings here
        SkillEffect dashSkill = new DashSkill();
        skills.Add(dashSkill);
    }

    private void Update(){
        if (!canMove) return;
        float horizontalInput = 0f;
        if (this.stunedAt + STUN_TIME < DateTime.Now) {
            horizontalInput = axisInput.x;
        }

        // Animate player
        animator.SetBool("Running", horizontalInput != 0);
        animator.SetBool("Grounded", Grounded);
        animator.SetBool("Jumping", this.isJumping);

        Player player = gameObject.GetComponent<Player>();
        rb.velocity = new Vector2(Grounded || !Stuck ? horizontalInput * player.speed : 0, rb.velocity.y);

        // Rotate the player model left or right depending on the input
        if(horizontalInput > 0f && isLeft){
            isLeft = false;
            transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        }

        if (horizontalInput < 0f && !isLeft) {
            isLeft = true;
            transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
        }

        // Execute skills at the end of the movement update as it may update values
        foreach (SkillEffect skill in this.skills) skill.update(player, rb, axisInput);
    }

    public void Jump(){
        if (!canMove) return;
        if(IsGrounded() && this.stunedAt + STUN_TIME < DateTime.Now) {
            rb.AddForce(Vector2.up * gameObject.GetComponent<Player>().jumpForce, ForceMode2D.Impulse);
            additionalJumpAvailable = true;
            this.isJumping = true;
        // TODO: isTranformed
        // Double Jump
        }else if(!IsGrounded() && additionalJumpAvailable){
            rb.AddForce(Vector2.up * gameObject.GetComponent<Player>().jumpForce, ForceMode2D.Impulse);
            additionalJumpAvailable = false;
            this.isJumping = true;
        }
    }

    public void triggerSkill() {
        (this.skills[0] as SkillEffect).execute(gameObject.GetComponent<Player>(), rb, axisInput);
    }

    bool IsGrounded() {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll (positionToCheck + new Vector2(0, -1.5f), new Vector2(0, -1), 0.1f);

        foreach (RaycastHit2D hit in hits) {
            if (hits[0].collider.tag.EndsWith("Stair")) continue;
            if (hits[0].collider.tag.EndsWith("Player")) continue;
            return true;
        }

        return false;
    }

    bool isStuck() {
        RaycastHit2D[] hits;

        Vector2 positionToCheck = transform.position + new Vector3(rb.velocity.x > 0 ? 1 : -1, 2);
        hits = Physics2D.RaycastAll (positionToCheck, rb.velocity, 0.1f);

        foreach (RaycastHit2D hit in hits) {
            if (hits[0].collider.tag.EndsWith("Stair")) continue;
            return true;
        }

        return false;
    }
    
    void OnCollisionStay2D(Collision2D collider)
    {
        Grounded = IsGrounded();
        Stuck = isStuck();
    }

    void OnCollisionEnter2D(Collision2D other) {
        this.isJumping = false;
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        Grounded = false;
        Stuck = false;
    }
}