using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    private bool hasCollidedP1 = false;
    private bool hasCollidedP2 = false;
    private string labelText = "Interact";

    
    private static float boxWidth = 70;
    private Rect player1Rect = new Rect((Screen.width / 2) - (boxWidth + 300), Screen.height -100, boxWidth,100 );
    private Rect player2Rect = new Rect((Screen.width / 2) + 90, Screen.height- 110 , boxWidth, 100);
     [SerializeField] Font GUIfont;
        
    
    void OnGUI()
    {
         GUIStyle myStyle = new GUIStyle(); 
         myStyle.fontSize = 100;
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
