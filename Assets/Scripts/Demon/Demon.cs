using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Utils;

public class Demon : MonoBehaviour {

    public DemonInventory inventory { get; private set; }

    private void Awake() {
        this.inventory = new DemonInventory();
        this.UpdateInventoryUI();
    }

    public void UpdateInventoryUI() {
        for (int i = 0; i < 2; i++) {
            this.transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().sprite = Sprites.FromName(this.inventory.GetSkill(i)?.name);
        }
    }
}