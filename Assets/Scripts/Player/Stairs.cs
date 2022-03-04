using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

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
    private GameInputs inputs;

    private void Awake() {
        isPlayer1 = this.gameObject.name == "Player 1";
        inputs = new GameInputs();

        // Subscribe for the Player's inputs event, and execute Interact() when detected
        if(isPlayer1){
            inputs.Player1.Interact.performed += _ => Interact();
        }else{
            inputs.Player2.Interact.performed += _ => Interact();
        }
    }

    private void OnEnable()
    {
        inputs.Enable(); // Start listening for inputs
    }

    private void OnDisable()
    {
        inputs.Disable(); // Stop listening for inputs
    }

    private void Interact() {
        if(upStairTarget != null) {  // The player will go upstairs
            this.gameObject.transform.position = new Vector3 (upStairTarget.GetComponent<Collider2D>().transform.position.x, upStairTarget.GetComponent<Collider2D>().transform.position.y - upStairTarget.GetComponent<Collider2D>().bounds.size.y / 2.05f, upStairTarget.GetComponent<Collider2D>().transform.position.z);
        } else if (downStairTarget != null) {  // The player will go downstair
            this.gameObject.transform.position = new Vector3 (downStairTarget.GetComponent<Collider2D>().transform.position.x, downStairTarget.GetComponent<Collider2D>().transform.position.y - downStairTarget.GetComponent<Collider2D>().bounds.size.y / 2.05f, downStairTarget.GetComponent<Collider2D>().transform.position.z);;
        }        
    }

    // When entering a collider, check if it's a ColliderUpStair or a ColliderDownStair, and set the teleport target accordingly
    void OnTriggerEnter2D(Collider2D other) {
         if (other.tag == "ColliderUpStair") {
            upStairTarget = GameObject.Find(stairCouplesDictionnary[other.gameObject.name]);
         } else if (other.tag == "ColliderDownStair") {
            downStairTarget = GameObject.Find(stairCouplesDictionnary[other.gameObject.name]);
         }
    }

    // When exiting a collider, check if it's a ColliderUpStair or a ColliderDownStair, and set the corresponding target variable to null
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "ColliderUpStair") {
            upStairTarget = null;
        } else if (other.tag == "ColliderDownStair") {
            downStairTarget = null;
        }
    }
}
