using UnityEngine;
using TMPro;


public class InteractionUI : MonoBehaviour
{
    private bool hasCollidedP1 = false;
    private bool hasCollidedP2 = false;
    

    [SerializeField] TextMeshProUGUI InteractText1 ,InteractText2 ;
    
    void Awake(){
        InteractText1.enabled =false ; 
        InteractText2.enabled =false ;
    }
    
    
 
    void OnTriggerEnter2D(Collider2D c)
     {
         
        if(c.gameObject.name == "Player 1") {
          InteractText1.enabled = true ;
          hasCollidedP1 = true;
        }
        if(c.gameObject.name == "Player 2") {
          InteractText2.enabled = true ;
          hasCollidedP2 = true;
        }
    }
    
    void OnTriggerExit2D( Collider2D c ) {
      if(c.gameObject.name =="Player 1") {
        InteractText1.enabled = false;
        hasCollidedP1 = false;
      }
      if(c.gameObject.name =="Player 2"){
        InteractText2.enabled = false ;
        hasCollidedP2 = false;
      }
    }
   
}
