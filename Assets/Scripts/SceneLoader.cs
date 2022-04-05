using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    Animator TransitionAnimator ;
    
    public void LoadNextScene()
    {
       
       StartCoroutine(LoadNextSc());
       
    }

    IEnumerator LoadNextSc(){
        TransitionAnimator.SetTrigger("Transitrig");
        yield return new WaitForSeconds(1f);
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex + 1);
        
    }
    public void LoadStartScene()
    {
        
        SceneManager.LoadScene(0);
    }
    public void LoadCreditScene()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadCharactersScene()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadOptionsScene()
    {
        SceneManager.LoadScene(4);
    }
    void Awake(){
            TransitionAnimator = FindObjectOfType<Animator>();

    }
}
