using UnityEngine;
using TMPro; // Ensure you have TextMeshPro imported

public class GameSceneManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro component that shows the score
    public TextMeshProUGUI moneyText; // Reference to the TextMeshPro component that shows the money

    private int currentScore = 0; // The current score
    private int currentMoney = 0; // The current money

    private void Start()
    {
        // Ensure the high score and money are initialized to 0 if PlayerPrefs doesn't have data
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }

        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
        }
    }

    private void Update()
    {
        // Get the current score and money from the TMP components
        if (scoreText != null && moneyText != null)
        {
            // Parse the score and money from the TextMeshPro components
            int.TryParse(scoreText.text, out currentScore); // Try parsing the score
            int.TryParse(moneyText.text, out currentMoney); // Try parsing the money
        }
    }

    private void OnApplicationQuit() // Called when the application is closed
    {
        SaveData();
    }

    private void OnDisable() // Called when the scene is unloaded or when switching scenes
    {
        SaveData();
    }

    // This function will save the high score and add the current money to the saved money
    private void SaveData()
    {
        // Load the current high score from PlayerPrefs
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // If the current score is higher than the saved high score, update the high score
        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }

        // Load the currently saved money from PlayerPrefs
        int savedMoney = PlayerPrefs.GetInt("Money", 0);

        // Add the current game money to the saved money
        savedMoney += currentMoney;

        // Save the updated money value
        PlayerPrefs.SetInt("Money", savedMoney);

        // Make sure the data is saved to disk
        PlayerPrefs.Save();

        Debug.Log("Game Data Saved - HighScore: " + currentScore + ", Money: " + savedMoney);
    }
}
