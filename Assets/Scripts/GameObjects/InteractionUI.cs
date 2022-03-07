using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    private bool hasCollided = false;
    private string labelText = "";
 
    void OnGUI()
        {
            if (hasCollided ==true)
        {    
            GUI.Box(new Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
        }
    }
 
    void OnTriggerEnter2D(Collider2D c)
     {
        if(c.gameObject.tag =="Player") {
            hasCollided = true;
            labelText = "Hit E to pick up the key!";
        }
        
    }
    
    void OnTriggerExit2D( Collider2D c ) {
      if(c.gameObject.tag =="Player") hasCollided = false;
    }
}
