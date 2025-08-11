using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonLoader : MonoBehaviour
{
    // Call this from the button and type the scene name in the Inspector
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Optional: call this to load by scene index from Build Settings
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Optional: Quit the application
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
