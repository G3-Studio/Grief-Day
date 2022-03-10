using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {
    private static TimeSpan STUN_TIME = new TimeSpan(0, 0, 1);
    private Rigidbody2D rb;
    private Animator animator;
    public bool canMove = true;
    public bool additionalJumpAvailable = false;
    public bool isLeft = true;
    private bool isJumping = false;
    bool Grounded;
    int Stuck;
    private DateTime stunedAt;
    private Vector2 axisInput;
    private ArrayList skills = new ArrayList();

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stunedAt = DateTime.MinValue;
    }

    public void SetInputMoveVector(Vector2 input) {
        axisInput = input;
    }

    private void OnEnable() {
        // Register skills and skill keybindings here
        SkillEffect dashSkill = new DashSkill();
        skills.Add(dashSkill);
    }

    private void Update(){
        int horizontalInput = 0;
        if (this.stunedAt + STUN_TIME < DateTime.Now) {
            horizontalInput = Math.Sign(axisInput.x);
        }

        // Animate player
        animator.SetBool("Running", horizontalInput != 0 && canMove);
        animator.SetBool("Grounded", Grounded);
        animator.SetBool("Jumping", this.isJumping);

        if (!canMove) return;

        Player player = gameObject.GetComponent<Player>();
        rb.velocity = new Vector2(Stuck == 0 || Stuck != horizontalInput ? horizontalInput * player.speed : 0, rb.velocity.y);

        // Rotate the player model left or right depending on the input
        if (horizontalInput > 0f && isLeft) {
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

    public void Jump() {
        if (!canMove) return;
        if (IsGrounded() && this.stunedAt + STUN_TIME < DateTime.Now) {
            rb.AddForce(Vector2.up * gameObject.GetComponent<Player>().jumpForce, ForceMode2D.Impulse);
            additionalJumpAvailable = true;
            this.isJumping = true;
            // TODO: isTranformed
            // Double Jump
        }
        else if (!IsGrounded() && additionalJumpAvailable) {
            rb.AddForce(Vector2.up * gameObject.GetComponent<Player>().jumpForce, ForceMode2D.Impulse);
            additionalJumpAvailable = false;
            this.isJumping = true;
        }
    }

    public void triggerSkill(int skill) {
        if (this.GetComponent<Collider2D>().)
        if (other.gameObject.tag == "Demon") {
            Demon demon = other.gameObject.transform.parent.GetComponent<Demon>();
            other.gameObject.transform.GetSiblingIndex()
        }
        (this.skills[0] as SkillEffect).execute(gameObject.GetComponent<Player>(), rb, axisInput);
    }

    bool IsGrounded() {
        Collider2D collider = GetComponent<Collider2D>();
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            new Vector2(collider.bounds.center.x, collider.bounds.min.y),
            new Vector2(0, -1),
            0.1f
        );

        foreach (RaycastHit2D hit in hits) {
            if (hit.collider.tag.EndsWith("Stair")) continue;
            if (hit.collider.tag == "Player") continue;
            if (hit.collider.tag == "Shrine") continue;
            return true;
        }

        return false;
    }
    
    void isStuck(Collision2D collision) {
        int relativeX = 0;
        if (!collision.collider.tag.EndsWith("Stair") && collision.collider.tag != "Player" && collision.collider.tag != "Shrine") {
            foreach (ContactPoint2D contactPoint in collision.contacts) {
                int localRelativeX = Math.Sign(contactPoint.point.x - this.transform.position.x);
                if (localRelativeX != relativeX && relativeX != 0) return;
                relativeX = localRelativeX;
            }
            Stuck = relativeX;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Grounded = IsGrounded();
        isStuck(collision);
    }
    
    void OnCollisionEnter2D(Collision2D other) {
        this.isJumping = false;
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        Grounded = IsGrounded();
        Stuck = 0;
    }
}