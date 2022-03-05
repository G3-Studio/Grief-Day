using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 200;
    [SerializeField] private int maxHealth = 200;
    [SerializeField] public float speed = 20.0f;
    [SerializeField] public float jumpForce = 5.0f;
    [SerializeField] private int attack = 10;
    [SerializeField] private PlayerEffectUI effectUI;
    [SerializeField] private StatsUI statsUI;

    public bool isPlayer1 { get; private set; }
    public Inventory inventory;

    private void Awake()
    {
        isPlayer1 = this.gameObject.name == "Player 1";
        inventory = new Inventory();
        this.statsUI.UpdateAll(this.health, this.maxHealth, this.speed, this.attack);
    }
    

    // Collect item when walking on it 
    public void CollectItem(JsonUtils.CollectableItemJson.Buff buff) {
        inventory.AddBuff(buff);
        this.effectUI.GetComponent<PlayerEffectUI>().UpdateObject(inventory);
        switch (buff.buff.name)
        {
            case "pv":
                health += buff.buff.value;
                maxHealth += buff.buff.value;
                statsUI.UpdatePV(health, maxHealth);
                break;
            case "speed":
                speed += buff.buff.value;
                statsUI.UpdateSpeed(speed);
                break;
            case "attack":
                attack += buff.buff.value;
                statsUI.UpdateSpeed(attack);
                break;
            default:
                Debug.LogWarning(buff.buff.name + " is not implemented");
                break;
        }
    }

    // Add skill according to item
    public bool CollectItem(JsonUtils.CollectableItemJson.Skill skill)
    {
        if (inventory.isSkillInventoryFull()) {
            return false;
        }
        inventory.AddSkill(skill);
        this.effectUI.GetComponent<PlayerEffectUI>().UpdateObject(inventory);
        return true;
    }
}