using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.WebPages.Razor.Configuration;
using UnityEngine;
using UnityEngine.UI;

public class UI_test : MonoBehaviour
{
    [SerializeField] Camera camera1,camera2 ;
    int oldmask;
    Animator ArrowAnimator ;
    GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        oldmask = camera1.cullingMask;
        ArrowAnimator = GetComponent<Animator>() ;
        arrow = transform.GetChild(0).gameObject;
        arrow.SetActive(false); 
     
    }

    void OnTriggerEnter2D(Collider2D c)
     {
         
        if(c.gameObject.name =="Player 1" ){
            camera1.cullingMask = -1 ;
            arrow.SetActive(true);
        ArrowAnimator.SetBool("ShouldDisplay",true);
        }
         if(c.gameObject.name =="Player 2"){
            camera2.cullingMask = -1 ;
            arrow.SetActive(true);
        ArrowAnimator.SetBool("ShouldDisplay",true);

        }
     }
    void OnTriggerExit2D( Collider2D c ) {
        if(c.gameObject.name =="Player 1" ){
            camera1.cullingMask = oldmask ;
            arrow.SetActive(false);
        ArrowAnimator.SetBool("ShouldDisplay",false);
        }
         if(c.gameObject.name =="Player 2"){
            camera2.cullingMask = oldmask ;
            arrow.SetActive(false);
        ArrowAnimator.SetBool("ShouldDisplay",false);

        }


    }   
        
    }
 
   

