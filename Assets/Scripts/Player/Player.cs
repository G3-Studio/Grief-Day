﻿using System.Collections.Generic;
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
    private List<string> itemList;
    private JsonUtils.CollectableItemJson itemDictionary;

    private void Awake()
    {
        isPlayer1 = this.gameObject.name == "Player 1";
        // Open JSON file & load content in itemDictionnary
        string json = System.IO.File.ReadAllText("Assets/Scenes/CollectableItems.json");
        // For some reason, unity can only read objects as root types, not arrays
        itemDictionary = JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json);
        itemList = new List<string>();
        CollectItem("health_boost");
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
        // foreach(JsonUtils.CollectableItemJson o in itemDictionary.buff){
        //     if(o.name == name){
        //         switch (o.buff.name)
        //         {
        //             case "PV":
        //                 health += o.buff.value;
        //                 maxHealth += o.buff.value;
        //                 break;
        //             case "speed":
        //                 speed += o.buff.value;
        //                 break;
        //             case "attack":
        //                 attack += o.buff.value;
        //                 break;
        //             default:
        //                 Debug.LogWarning(o.name + " is not implemented");
        //                 break;
        //         }
        //         this.effectUI.GetComponent<PlayerEffectUI>().AddEffect(o.buff.name, o.buff.value);
        //     }
        // }
    }
}