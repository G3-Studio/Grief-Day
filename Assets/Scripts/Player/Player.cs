using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 200;
    [SerializeField] private int maxHealth = 200;

    public bool isPlayer1 { get; private set; }

    // variables for CollectableItems
    private List<string> itemList;
    private dynamic itemDictionary;

    private void Awake()
    {
        isPlayer1 = this.gameObject.name == "Player 1";
        // Open JSON file & load content in itemDictionnary
        string json = System.IO.File.ReadAllText("Assets/Scenes/CollectableItems.json");
        itemDictionary = UnityEngine.JsonUtility.ToJson(json);
        itemList = new List<string>();
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
                        Debug.LogWarning(o.name + " is not implemented");
                        break;
                }
            }
        }
    }
}