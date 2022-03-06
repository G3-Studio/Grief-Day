using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        if (SystemInfo.operatingSystem.Contains("MAC")) {
            SceneManager.LoadScene(5);
        }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
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
}
