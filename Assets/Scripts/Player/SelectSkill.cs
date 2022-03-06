using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkill : MonoBehaviour
{
    private GameObject selectedShrine;
    
    public void Interact()
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
