using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] public float speed = 20.0f;
    [SerializeField] public bool isPlayer1 = true;
    [SerializeField] public int health = 200;
    [SerializeField] public int maxHealth = 200;

    private Rigidbody2D rb;
    private GameInputs inputs;
    private InputAction movement;
    private List itemList;
    private dynamic itemDictionary;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputs = new GameInputs();
        string json = System.IO.File.ReadAllText("CollectableItems.json");
        itemDictionary = JsonConvert.DeserializeObject(json);
    }

    private void OnEnable()
    {
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
        Vector2 axisInput = movement.ReadValue<Vector2>();
        float horizontalInput = axisInput.x;
        float verticalInput = axisInput.y;
        rb.velocity = new Vector2(horizontalInput * speed * Time.deltaTime, rb.velocity.y);
        if(horizontalInput > 0){
            transform.rotate(new Vector3(0, 180, 0));
        }else{
            transform.rotate(new Vector3(0, 0, 0));
        }
        
    }

    // Collect item when walking on it 
    public void CollectItem(string name)
    {
        itemList.Add(name);
    }

    // Add effect according to item
    public void AddItemEffect()
    {
        foreach(string name in itemList){
            foreach(object o in itemDictionary){
                if(o.name == name){
                    switch (o.buff["name"])
                    {
                        case "PV":
                            health += o.buff["value"];
                            maxHealth += o.buff["value"];
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
