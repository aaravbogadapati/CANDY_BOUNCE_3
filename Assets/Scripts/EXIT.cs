using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    [Tooltip("Drag your Restart Button here")]
    public Button restartButton;

    private void Start()
    {
        if (restartButton != null)
        {
            // Add the RestartScene method to the button's onClick event
            restartButton.onClick.AddListener(RestartScene);
        }
        else
        {
            Debug.LogWarning("Restart Button is not assigned in the inspector!");
        }
    }

    private void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void OnDestroy()
    {
        // Clean up listener when this object is destroyed to avoid memory leaks
        if (restartButton != null)
        {
            restartButton.onClick.RemoveListener(RestartScene);
        }
    }
}
