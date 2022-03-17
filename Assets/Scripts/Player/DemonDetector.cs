using UnityEngine;

public class DemonDetector : MonoBehaviour {
    
    private GameObject demon; // If the player is in a downStairTrigger, this will be the target gameObject
 
    public void Interact() {
        if (demon == null) return;
        if (!gameObject.GetComponent<Movement>().canMove) return;
        Inventory playerInventory = this.gameObject.GetComponent<Player>().inventory;
        SelectSkillUI.instance.Enable(playerInventory.GetSkillInSlot(0), playerInventory.GetSkillInSlot(1));
        this.gameObject.GetComponent<Player>().currentUI = CurrentUI.CHOOSE_DEMON_ITEM;
    }

    // When entering a collider, check if it's a ColliderUpStair or a ColliderDownStair, and set the teleport target accordingly
    private void OnTriggerEnter2D(Collider2D other) {
         if (other.tag == "Demon") {
            demon = other.gameObject;
         }
    }

    // When exiting a collider, check if it's a ColliderUpStair or a ColliderDownStair, and set the corresponding target variable to null
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Demon") {
            demon = null;
        }
    }
    
}