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
    private Inventory inventory;

    private void Awake()
    {
        isPlayer1 = this.gameObject.name == "Player 1";
        inventory = new Inventory();
        CollectItem(Inventory.BUFFS[0]);
    }
    

    // Collect item when walking on it 
    public bool CollectItem(JsonUtils.CollectableItemJson.Buff buff) {
        if (inventory.isBuffInventoryFull()) {
            return false;
        }
        inventory.AddBuff(buff);
        this.effectUI.GetComponent<PlayerEffectUI>().Update(inventory);
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
        return true;
    }

    // Add skill according to item
    public bool CollectItem(JsonUtils.CollectableItemJson.Skill skill)
    {
        if (inventory.isBuffInventoryFull()) {
            return false;
        }
        inventory.AddSkill(skill);
        this.effectUI.GetComponent<PlayerEffectUI>().Update(inventory);
        return true;
    }
    

}