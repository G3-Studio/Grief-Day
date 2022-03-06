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

        gameObject.GetComponent<Player>().inventory.AddSkill(selectedShrine.GetComponent<Shrine>().skill);
        Destroy(selectedShrine);
        GameObject.Find("ItemSpawner").GetComponent<ItemChoosing>().skillSelected();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Shrine") {
            selectedShrine = other.gameObject;
        } 
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Shrine") {
            selectedShrine = null;
        }
    }
}
