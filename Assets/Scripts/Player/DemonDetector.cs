using UnityEngine;

public class DemonDetector : MonoBehaviour {
    
    private GameObject demon; // If the player is in a downStairTrigger, this will be the target gameObject
 
    public void Interact() {
        if (demon == null) return;
        if (!gameObject.GetComponent<Movement>().canMove) return;
        if (TradingManager.isTrading) return;
        if (this.gameObject.GetComponent<Player>().hasAlreadyTraded) return;
        TradingManager.StartTrading(this.gameObject.GetComponent<Player>(), demon.GetComponent<Demon>());
        this.gameObject.GetComponent<Player>().hasAlreadyTraded = true;
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