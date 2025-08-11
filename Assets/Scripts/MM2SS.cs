using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadShopScreen : MonoBehaviour
{
    void Start()
    {
        // Get the Button component attached to this GameObject
        Button button = GetComponent<Button>();

        // Add a listener to the button's onClick event
        if (button != null)
        {
            button.onClick.AddListener(LoadShop);
        }
        else
        {
            Debug.LogWarning("LoadShopScreen script is not attached to a UI Button.");
        }
    }

    void LoadShop()
    {
        SceneManager.LoadScene("Shop Screen");
    }
}
