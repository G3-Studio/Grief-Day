using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkill : MonoBehaviour
{
    private bool isPlayer1;
    private GameInputs inputs;
    private GameObject selectedShrine;
    void Awake() {
        isPlayer1 = this.gameObject.name == "Player 1";
        inputs = new GameInputs();

        Debug.Log("Is Player 1 : " + isPlayer1);

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

    private void Interact()
    {
        if (!selectedShrine) {
            return;
        }

        Debug.Log("Trying select skill");

        gameObject.GetComponent<Player>().inventory.AddSkill(selectedShrine.GetComponent<Shrine>().skill);
        Destroy(selectedShrine);
        GameObject.Find("ItemSpawner").GetComponent<ItemChoosing>().skillSelected();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Shrine") {
            Debug.Log("Entering shrine");
            selectedShrine = other.gameObject;
        } 
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Shrine") {
            Debug.Log("Exiting shrine");
            selectedShrine = null;
        } 
    }
}
