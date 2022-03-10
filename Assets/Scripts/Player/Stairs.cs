using UnityEngine;
using System.Collections.Generic;

public class Stairs : MonoBehaviour
{    
    // Declare the travel combinations, with [origin], [target]
    private readonly Dictionary<string, string> stairCouplesDictionnary = new Dictionary<string, string>()
    {
        {"F1UpStair","F1DownStair"},
        {"F1DownStair","F1UpStair"},
        {"F2UpStair", "F2DownStair"},
        {"F2DownStair", "F2UpStair"},
        {"F3UpStair", "F3DownStair"},
        {"F3DownStair", "F3UpStair"}
    };
    private GameObject upStairTarget; // If the player is in a upStairTrigger, this will be the target gameObject
    private GameObject downStairTarget; // If the player is in a downStairTrigger, this will be the target gameObject

    private bool isPlayer1;

    private void Awake() {
        isPlayer1 = this.gameObject.name == "Player 1";
    }

    public void Interact() {
        if (!gameObject.GetComponent<Movement>().canMove) return;
        if(upStairTarget != null) {  // The player will go upstair
            Collider2D collider = upStairTarget.GetComponent<Collider2D>();
            Collider2D playerCollider = this.gameObject.GetComponent<Collider2D>();
            this.gameObject.transform.position = new Vector3(
                collider.bounds.center.x,
                collider.bounds.center.y - collider.bounds.size.y / 2f + playerCollider.bounds.size.y / 2f,
                collider.bounds.center.z
            );
        } else if (downStairTarget != null) {  // The player will go downstair
            Collider2D collider = downStairTarget.GetComponent<Collider2D>();
            Collider2D playerCollider = this.gameObject.GetComponent<Collider2D>();
            this.gameObject.transform.position = new Vector3(
                collider.bounds.center.x,
                collider.bounds.center.y - collider.bounds.size.y / 2f + playerCollider.bounds.size.y / 2f,
                collider.bounds.center.z
            );
        }
    }

    // When entering a collider, check if it's a ColliderUpStair or a ColliderDownStair, and set the teleport target accordingly
    private void OnTriggerEnter2D(Collider2D other) {
         if (other.tag == "ColliderUpStair") {
            upStairTarget = GameObject.Find(stairCouplesDictionnary[other.gameObject.name]);
         } else if (other.tag == "ColliderDownStair") {
            downStairTarget = GameObject.Find(stairCouplesDictionnary[other.gameObject.name]);
         }
    }

    // When exiting a collider, check if it's a ColliderUpStair or a ColliderDownStair, and set the corresponding target variable to null
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "ColliderUpStair") {
            upStairTarget = null;
        } else if (other.tag == "ColliderDownStair") {
            downStairTarget = null;
        }
    }
}
