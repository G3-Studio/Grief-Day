
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameInputs inputs;
    private InputAction movement;
    private InputAction jump;
    private bool isLeft = true;
    private bool isPlayer1;
    bool Grounded, Stuck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = new GameInputs();

        isPlayer1 = (gameObject.name == "Player 1");
    }

    private void OnEnable()
    {
        // Check which player is currently selected and check for the movement inputs
        if(isPlayer1){
            movement = inputs.Player1.Move;
            jump = inputs.Player1.Jump;
        }else{
            movement = inputs.Player2.Move;
            jump = inputs.Player2.Jump;
        }
        movement.Enable();

        jump.performed += DoJump;
        jump.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        jump.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Apply movement
        Vector2 axisInput = movement.ReadValue<Vector2>();
        float horizontalInput = axisInput.x;

        rb.velocity = new Vector2(Grounded || !Stuck ? horizontalInput * gameObject.GetComponent<Player>().speed * Time.deltaTime*100 : 0, rb.velocity.y);

        // Rotate the player model left or right depending on the input
        if(horizontalInput > 0f && isLeft){
            isLeft = false;
            transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        }
        if(horizontalInput < 0f && !isLeft){
            isLeft = true;
            transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
        }
    }

    bool IsGrounded() {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll (positionToCheck, new Vector2 (0, -1), 0.1f);

        foreach (RaycastHit2D hit in hits) {
            if (hits[0].collider.tag.EndsWith("Stair")) continue;
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

    void DoJump(InputAction.CallbackContext context){
        if(IsGrounded()){
            rb.AddForce(Vector2.up * gameObject.GetComponent<Player>().jumpForce, ForceMode2D.Impulse);
        }
    }
}