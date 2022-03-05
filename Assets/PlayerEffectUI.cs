using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class PlayerEffectUI : MonoBehaviour
{

    private static Dictionary<string, int> effectUIsIndex = new() {
        {"PV", 2},
    };

    private static int effectIconSize = 50;
    private static int gapSize = 10;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update(Inventory inventory) {
        ReloadSkills(inventory);
        ReloadBuffs(inventory);
    }

    private void ReloadSkills(Inventory inventory) {
        Image slot1 = this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        Image slot2 = this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<Image>();
        JsonUtils.CollectableItemJson.Buff buff1 = inventory.GetBuffInSlot(0);
        JsonUtils.CollectableItemJson.Buff buff2 = inventory.GetBuffInSlot(1);
        if (buff1 != null) {
            slot1.enabled = true;
            slot1.sprite = getSpriteOfSkill(buff1.name);
        } else {
            slot1.enabled = false;
        }
        if (buff2 != null) {
            slot2.enabled = true;
            slot2.sprite = getSpriteOfSkill(buff2.name);
        } else {
            slot2.enabled = false;
        }
    }

    private void ReloadBuffs(Inventory inventory) {
        
    }

    private Sprite getSpriteOfSkill(string name) {
        switch (name) {
            case "health_boost": return Sprites.HEALTH_BOOST;
            case "swiftness_boots": return Sprites.SWIFTNESS_BOOTS;
            default:
                Debug.LogWarning("This buff is not implemented");
                return null;
        }
    }
}
