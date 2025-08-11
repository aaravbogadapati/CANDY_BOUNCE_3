using UnityEngine;
using TMPro; // Make sure TextMeshPro is imported

public class HomeSceneUIManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI highScoreText; // Reference to the Text component for high score
    public TextMeshProUGUI moneyText; // Reference to the Text component for money

    // Start is called before the first frame update
    void Start()
    {
        // Load the saved high score and money
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Default to 0 if no data is found
        int money = PlayerPrefs.GetInt("Money", 0); // Default to 0 if no data is found

        // Update UI with the loaded values
        highScoreText.text = "" + highScore.ToString();
        moneyText.text = "" + money.ToString();
    }
}
