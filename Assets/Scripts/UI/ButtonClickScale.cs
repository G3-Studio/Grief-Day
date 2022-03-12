using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonClickScale : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    // Start is called before the first frame update
            Image image;
    AudioSource Buttonaudio ;
     
      public void OnPointerEnter(PointerEventData eventData)
     {
            
        image.color= new Color(255,255,255,1);
        transform.localScale = new Vector3(85,85,85);
        Buttonaudio.Play();
         Debug.Log("Entered");
     }
      public void OnPointerExit(PointerEventData eventData)
    {
        image.color= new Color(255,255,255,0.5f);
        transform.localScale = new Vector3(75.1f,75.1f,75.1f);
        Debug.Log("The cursor exited the selectable UI element.");
    }
    
    // Update is called once per frame
    
    public void onButtonSelected(){
        StartCoroutine(BeforeLoad()) ;
    }
     IEnumerator BeforeLoad(){
        
        transform.localScale = new Vector3(70f,70f,70f);
        image.color= new Color(255,255,255,0.5f); 
         yield return new WaitForSecondsRealtime(0.5f) ; 
         
     }
void Start() {
        
        Button buttonclicked = GetComponentInChildren<Button>();
        Buttonaudio = buttonclicked.GetComponent<AudioSource>();
        
         image = buttonclicked.GetComponent<Image>();
         
        
     
    }
}