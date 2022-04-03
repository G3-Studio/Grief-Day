using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    private bool hasCollidedP1 = false;
    private bool hasCollidedP2 = false;
    private string labelText = "Interact";
    private static float boxWidth = 70;
    private Rect player1Rect = new Rect((Screen.width / 2) - (boxWidth + 20), Screen.height - 40, boxWidth, 20);
    private Rect player2Rect = new Rect((Screen.width / 2) + 20, Screen.height - 40, boxWidth, 20);
 
    void OnGUI()
    {
        if (hasCollidedP1 ==true) GUI.Box(player1Rect,(labelText));
        if (hasCollidedP2 ==true) GUI.Box(player2Rect,(labelText));
    }
 
    void OnTriggerEnter2D(Collider2D c)
     {
        if(c.gameObject.name =="Player 1") hasCollidedP1 = true;
        if(c.gameObject.name =="Player 2") hasCollidedP2 = true;
        
    }
    
    void OnTriggerExit2D( Collider2D c ) {
      if(c.gameObject.name =="Player 1") hasCollidedP1 = false;
      if(c.gameObject.name =="Player 2") hasCollidedP2 = false;
    }
}
