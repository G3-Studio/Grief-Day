using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] public float speed = 20.0f;
    [SerializeField] public int health = 200;
    [SerializeField] public int maxHealth = 200;

    private bool isPlayer1 = false;
    private Rigidbody2D rb;
    private GameInputs inputs;
    private InputAction movement;
    private float oldHorizontal;
    private bool isLeft = true;

    // variables for CollectableItems
    private List<string> itemList;
    private dynamic itemDictionary;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = new GameInputs();
        isPlayer1 = this.gameObject.name == "Player 1";

        // Open JSON file & load content in itemDictionnary
        string json = System.IO.File.ReadAllText("Assets/Scenes/CollectableItems.json");
        itemDictionary = UnityEngine.JsonUtility.ToJson(json);
    }

    private void OnEnable()
    {
        // Check which player is currently selected and check for the movement inputs
        if(isPlayer1){
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
        oldHorizontal = horizontalInput;
        float verticalInput = axisInput.y;
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

    // Collect item when walking on it 
    public void CollectItem(string name)
    {
        itemList.Add(name);
        AddItemEffect(name);
    }

    // Add effect according to item
    public void AddItemEffect(string name)
    {
        foreach(dynamic o in itemDictionary){
            if(o.name == name){
                switch (o.buff.name)
                {
                    case "PV":
                        health += o.buff.value;
                        maxHealth += o.buff.value;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
