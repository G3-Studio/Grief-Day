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
    [SerializeField] public float dashStrength = 10.0f;

    public bool isPlayer1 { get; private set; }
    public PlayerInventory inventory;
    public CurrentUI currentUI;

    private void Awake()
    {
        isPlayer1 = this.gameObject.name == "Player 1";
        inventory = new PlayerInventory(this.effectUI);
        this.statsUI.UpdateAll(this.health, this.maxHealth, this.speed, this.attack);
        this.currentUI = CurrentUI.NONE;
    }
    
    public int GetPlayerIndex()
    {
        return isPlayer1 ? 0 : 1;
    }

    public void Update() {
        if (this.inventory.GetSkillInSlot(0) == null) {
            this.inventory.AddSkill(Skills.getSkill());
        }
    }

    // Collect item when walking on it 
    public void CollectItem(JsonUtils.CollectableItemJson.Buff buff) {
        inventory.AddBuff(buff);
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
                statsUI.UpdateAttack(attack);
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
        return true;
    }
}