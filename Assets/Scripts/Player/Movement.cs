
using System;
using System.Windows.Forms.DataVisualization.Charting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private static TimeSpan STUN_TIME = new TimeSpan(0, 0, 1);
    private Rigidbody2D rb;
    public bool canMove = true;
    public bool additionalJumpAvailable = false;
    private bool isLeft = true;
    private bool isPlayer1;
    bool Grounded, Stuck;
    private DateTime stunedAt;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        isPlayer1 = (gameObject.name == "Player 1");
        stunedAt = DateTime.MinValue;
    }

    void OnMove(InputValue value){
        if (!canMove) return;
        float horizontalInput = 0;
        if (this.stunedAt + STUN_TIME < DateTime.Now) {
            // Apply movement
            Vector2 axisInput = value.Get<Vector2>();
            horizontalInput = axisInput.x;
        }

        rb.velocity = new Vector2(Grounded || !Stuck ? horizontalInput * gameObject.GetComponent<Player>().speed * Time.deltaTime*100 : 0, rb.velocity.y);

        // Rotate the player model left or right depending on the input
        if(horizontalInput > 0f && isLeft){
            isLeft = false;
            transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        }

        if (horizontalInput < 0f && !isLeft) {
            isLeft = true;
            transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
        }
    }

    void OnJump(){
        if (!canMove) return;
        if(IsGrounded() && this.stunedAt + STUN_TIME < DateTime.Now) {
            rb.AddForce(Vector2.up * gameObject.GetComponent<Player>().jumpForce, ForceMode2D.Impulse);
            additionalJumpAvailable = true;
        // TODO: isTranformed
        // Double Jump
        }else if(!IsGrounded() && additionalJumpAvailable){
            rb.AddForce(Vector2.up * gameObject.GetComponent<Player>().jumpForce, ForceMode2D.Impulse);
            additionalJumpAvailable = false;
        }
    }

    bool IsGrounded() {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll (positionToCheck, new Vector2 (0, -1), 0.1f);

        foreach (RaycastHit2D hit in hits) {
            if (hits[0].collider.tag.EndsWith("Stair")) continue;
            if (hits[0].collider.tag.EndsWith("Player")) break;
            return true;
        }

        return false;
    }

    bool isStuck() {
        RaycastHit2D[] hits;

        Vector2 positionToCheck = transform.position + new Vector3(0, 2);
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

    void OnCollisionExit2D(Collision2D collider)
    {
        Grounded = false;
        Stuck = false;
    }
}