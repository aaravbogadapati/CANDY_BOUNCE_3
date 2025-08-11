using UnityEngine;
using UnityEngine.UI;

public class ResetShopDataButton : MonoBehaviour
{
    [SerializeField] private Button resetButton;

    void Start()
    {
        if (resetButton != null)
            resetButton.onClick.AddListener(ResetShopData);
        else
            Debug.LogWarning("Reset button is not assigned in the inspector.");
    }

    public void ResetShopData()
    {
        PlayerPrefs.DeleteKey("Money");
        PlayerPrefs.DeleteKey("BoughtItems");
        PlayerPrefs.DeleteKey("EquippedItem");
        PlayerPrefs.Save();

        Debug.Log("Shop data has been reset.");

        // Optional: reload scene to refresh shop visuals
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
