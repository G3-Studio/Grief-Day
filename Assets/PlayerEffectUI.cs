using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class PlayerEffectUI : MonoBehaviour
{

    public void Awake() {
        ReloadSlot(0, null);
        ReloadSlot(1, null);
    }
    
    public void UpdateObject(Inventory inventory) {
        ReloadSlot(0, inventory.GetSkillInSlot(0)?.name);
        ReloadSlot(1, inventory.GetSkillInSlot(1)?.name);
    }

    private void ReloadSlot(int slotIndex, string spriteName) {
        Image slot = this.gameObject.transform.GetChild(slotIndex).GetChild(1).GetComponent<Image>();
        if (spriteName != null) {
            slot.enabled = true;
            slot.sprite = getSprite(spriteName);
        } else {
            slot.enabled = false;
        }
    }

    private Sprite getSprite(string name) {
        switch (name) {
            case "stun": return Sprites.STUN;
            case "dash": return Sprites.DASH;
            case "double_jump": return Sprites.DOUBLE_JUMP;
            case "demon_finger": return Sprites.DEMON_FINGER;
            case "health_boost": return Sprites.HEALTH_BOOST;
            case "swiftness_boots": return Sprites.SWIFTNESS_BOOTS;
            default:
                Debug.LogWarning("This skill sprite is not implemented");
                return null;
        }
    }
}
