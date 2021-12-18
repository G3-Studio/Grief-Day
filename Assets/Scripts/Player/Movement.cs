using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private Player player;
    
    private Rigidbody2D rb;
    private GameInputs inputs;
    private InputAction movement;
    private bool isLeft = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = new GameInputs();
    }

    private void OnEnable()
    {
        // Check which player is currently selected and check for the movement inputs
        if(player.isPlayer1){
            movement = inputs.Player1.Move;
        }else{
            movement = inputs.Player2.Move;
        }
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Apply movement
        Vector2 axisInput = movement.ReadValue<Vector2>();
        float horizontalInput = axisInput.x;
        rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime*100, rb.velocity.y);

        // Rotate the player model left or right depending on the input
        if(horizontalInput == 1.0f && isLeft){
            isLeft = false;
            transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        }
        if(horizontalInput == -1.0f && !isLeft){
            isLeft = true;
            transform.Rotate(new Vector3(0.0f, -180.0f, 0.0f));
        }
    }

    
}
