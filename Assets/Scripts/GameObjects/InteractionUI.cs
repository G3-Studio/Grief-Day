using UnityEngine;
using System;

public class InteractionUI : MonoBehaviour
{
    private bool hasCollidedP1 = false;
    private bool hasCollidedP2 = false;
    private string labelText = "Interact";

    
    private static float boxWidth =Screen.height *0.1f;
    private static float boxHeight = Screen.height *0.1f ;
    private Rect player1Rect = new Rect((Screen.width / 2) - (boxWidth*3.1f), Screen.height *0.85f, boxWidth,boxHeight );
    private Rect player2Rect = new Rect((Screen.width / 2)*1.12f, Screen.height*0.85f , boxWidth, boxHeight);
     [SerializeField] Font GUIfont;
        
    
    void OnGUI()
    {
         GUIStyle myStyle = new GUIStyle(); 
         myStyle.fontSize = Mathf.FloorToInt(Screen.height *0.1f);
         myStyle.font= GUIfont;
         myStyle.normal.textColor= new Color(255,255,255) ;
        
         
        if (hasCollidedP1 ==true) {
             
                
                GUI.Box(player1Rect,(labelText),myStyle);
        } 
        if (hasCollidedP2 ==true){
               
                GUI.Box(player2Rect,(labelText),myStyle);
        } 
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
