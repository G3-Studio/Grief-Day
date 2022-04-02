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
    
    public void UpdateObject(PlayerInventory playerInventory) {
        ReloadSlot(0, playerInventory.GetSkillInSlot(0)?.name);
        ReloadSlot(1, playerInventory.GetSkillInSlot(1)?.name);
    }

    private void ReloadSlot(int slotIndex, string spriteName) {
        Image slot = this.gameObject.transform.GetChild(slotIndex).GetChild(1).GetComponent<Image>();
        if (spriteName != null) {
            slot.enabled = true;
            slot.sprite = Sprites.FromName(spriteName);
        } else {
            slot.enabled = false;
        }
    }

    
}
