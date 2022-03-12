using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitUI : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{

        AudioSource Buttonaudio ;
        Button buttonclicked ;
        TextMeshProUGUI textquitter;
        
     public void OnPointerEnter(PointerEventData eventData)
     {
            
        textquitter.color = new Color(255,255,255,1f);
        transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        Buttonaudio.Play();
         Debug.Log("Entered");
     }
      public void OnPointerExit(PointerEventData eventData)
    {
        textquitter.color= new Color(255,255,255,0.5f);
        transform.localScale = new Vector3(1,1,1);
        Debug.Log("The cursor exited the selectable UI element.");
    }
    public void Quit(){
        Debug.Log("Quitting Application.");
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        buttonclicked = GetComponentInChildren<Button>();
        Buttonaudio = buttonclicked.GetComponent<AudioSource>();
        textquitter = buttonclicked.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(textquitter);
    }

   
}
