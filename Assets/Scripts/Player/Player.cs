using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 200;
    [SerializeField] private int maxHealth = 200;
    [SerializeField] public float speed = 20.0f;
    [SerializeField] public float jumpForce = 5.0f;
    [SerializeField] private int attack = 10;
    [SerializeField] private GameObject effectUI;

    public bool isPlayer1 { get; private set; }

    // variables for CollectableItems
    private List<JsonUtils.CollectableItemJson.Buff> itemList;
    private JsonUtils.CollectableItemJson itemDictionary;

    private void Awake()
    {
        isPlayer1 = this.gameObject.name == "Player 1";
        // Open JSON file & load content in itemDictionnary
        string json = System.IO.File.ReadAllText("Assets/Scenes/CollectableItems.json");
        // For some reason, unity can only read objects as root types, not arrays
        itemDictionary = JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json);
        itemList = new List<JsonUtils.CollectableItemJson.Buff>();
    }
    
    // Collect item when walking on it 
    public void CollectItem(JsonUtils.CollectableItemJson.Buff buff)
    {
        itemList.Add(buff);
        AddItemEffect(buff);
    }

    // Add effect according to item
    public void AddItemEffect(JsonUtils.CollectableItemJson.Buff buff)
    {
        switch (buff.buff.name)
        {
            case "pv":
                health += buff.buff.value;
                maxHealth += buff.buff.value;
                break;
            case "speed":
                speed += buff.buff.value;
                break;
            case "attack":
                attack += buff.buff.value;
                break;
            default:
                Debug.LogWarning(buff.buff.name + " is not implemented");
                break;
        }
        // TODO: this.effectUI.GetComponent<PlayerEffectUI>().AddEffect(buff.buff.name, buff.buff.value);
    }  
}