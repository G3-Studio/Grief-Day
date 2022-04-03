using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public bool canMove = true;
    public bool isLeft = true;
    private bool isJumping = false;
    public bool Grounded { get; private set; }
    private int Stuck = 0;
    public Vector2 axisInput;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void SetInputMoveVector(Vector2 input) {
        axisInput = input;
    }

    private void Update(){
        int horizontalInput = Math.Sign(axisInput.x);
        
        if (this.gameObject.GetComponent<Player>().currentUI == CurrentUI.CHOOSE_DEMON_ITEM) {
            if (horizontalInput > 0f) {
                TradingManager.ChangeSelection(1);
            } else if (horizontalInput < 0f) {
                TradingManager.ChangeSelection(0);
            }
            return;
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
    }

    public void Jump(){
        if(canMove && IsGrounded())
            this.ApplyJump(); // TODO: isTranformed
    }

    /// <summary>ApplyJump makes no test</summary>
    public void ApplyJump() {
        rb.AddForce(Vector2.up * gameObject.GetComponent<Player>().jumpForce, ForceMode2D.Impulse);
        this.isJumping = true;
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
        int currentStuck = 0;
        bool initial = true;
        if (!collision.collider.tag.EndsWith("Stair") && collision.collider.tag != "Player" && collision.collider.tag != "Shrine") {
            for (int i = 0; i < collision.contactCount; i++) {
                // Not using Collision2D#contacts because it produces memory garbage
                ContactPoint2D contactPoint = collision.GetContact(i);
                int localStuck = -Math.Sign(contactPoint.normal.x);
                if (!(contactPoint.normal.y < .1f) || (!initial && currentStuck != localStuck)) return;
                currentStuck = localStuck;
                initial = false;
            }
            Stuck = currentStuck;
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