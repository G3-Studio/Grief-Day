using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class PlayerEffectUI : MonoBehaviour
{

    public void UpdateObject(Inventory inventory) {
        ReloadSlot(1, inventory.GetBuffInSlot(0)?.name);
        ReloadSlot(2, inventory.GetBuffInSlot(1)?.name);
        ReloadSlot(3, inventory.GetSkillInSlot(0)?.name);
        ReloadSlot(4, inventory.GetSkillInSlot(1)?.name);
    }

    private void ReloadSlot(int slotIndex, string spriteName) {
        Image slot = this.gameObject.transform.GetChild(slotIndex).GetChild(0).GetComponent<Image>();
        if (spriteName != null) {
            slot.enabled = true;
            slot.sprite = getSprite(spriteName);
        } else {
            slot.enabled = false;
        }
    }

    private Sprite getSprite(string name) {
        switch (name) {
            case "health_boost": return Sprites.HEALTH_BOOST;
            case "swiftness_boots": return Sprites.SWIFTNESS_BOOTS;
            default:
                Debug.LogWarning("This buff is not implemented");
                return null;
        }
    }
}
