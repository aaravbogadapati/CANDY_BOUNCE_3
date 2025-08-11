using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Canvas References")]
    public GameObject homeMenuCanvas;
    public GameObject gameOverlayCanvas;
    public GameObject pauseMenuCanvas;  // New: Pause menu

    [Header("UI Buttons")]
    public Button playButton;
    public Button pauseButton;  // New: Pause button

    void Start()
    {
        // Initialize canvas visibility
        homeMenuCanvas.SetActive(true);
        gameOverlayCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);  // Ensure pause menu starts off

        // Register play button
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayClicked);
        else
            Debug.LogError("Play Button is not assigned.");

        // Register pause button
        if (pauseButton != null)
            pauseButton.onClick.AddListener(OnPauseClicked);
        else
            Debug.LogError("Pause Button is not assigned.");
    }

    void OnPlayClicked()
    {
        homeMenuCanvas.SetActive(false);
        gameOverlayCanvas.SetActive(true);
    }

    void OnPauseClicked()
    {
        pauseMenuCanvas.SetActive(true);  // Show pause menu
        Time.timeScale = 0f;              // Optional: pause the game
    }

    // Optional: Resume button logic
    public void OnResumeClicked()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;              // Resume the game
    }
}
