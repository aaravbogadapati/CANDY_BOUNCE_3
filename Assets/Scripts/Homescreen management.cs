using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class HomeMenuUI : MonoBehaviour
{
    [Header("Canvas to hide when Play is pressed")]
    public Canvas menuCanvas;

    [Header("Play Button")]
    public Button playButton;

    [Header("Shop")]
    public Button shopButton;
    public Object shopSceneAsset;

    [Header("Settings")]
    public Button settingsButton;
    public Object settingsSceneAsset;

    [Header("Optional: components to enable only AFTER Play")]
    public MonoBehaviour[] componentsToEnableOnPlay;   // drag PlayerInput, movement scripts, etc.

    private string shopSceneName;
    private string settingsSceneName;

    void Awake()
    {
        // ─── Pause the game and disable gameplay scripts ────────────────
        Time.timeScale = 0f;                     // freeze physics & Update
        foreach (var comp in componentsToEnableOnPlay)
            if (comp) comp.enabled = false;      // turn off player controls, etc.

#if UNITY_EDITOR
        // Convert SceneAsset references to scene names (editor-only)
        if (shopSceneAsset)
            shopSceneName = AssetDatabase.GetAssetPath(shopSceneAsset)
                                         .Replace("Assets/", "").Replace(".unity", "");
        if (settingsSceneAsset)
            settingsSceneName = AssetDatabase.GetAssetPath(settingsSceneAsset)
                                             .Replace("Assets/", "").Replace(".unity", "");
#endif

        // Wire up buttons
        if (playButton)      playButton.onClick.AddListener(PlayPressed);
        if (shopButton)      shopButton.onClick.AddListener(ShopPressed);
        if (settingsButton)  settingsButton.onClick.AddListener(SettingsPressed);
    }

    // ────────────────────────────────────────────────────────────────────
    //  PLAY  – unpause game and re-enable gameplay scripts
    // ────────────────────────────────────────────────────────────────────
    void PlayPressed()
    {
        if (menuCanvas) menuCanvas.gameObject.SetActive(false);

        Time.timeScale = 1f;                     // resume game

        foreach (var comp in componentsToEnableOnPlay)
            if (comp) comp.enabled = true;       // turn controls back on
    }

    // ────────────────────────────────────────────────────────────────────
    //  SHOP  – load shop scene
    // ────────────────────────────────────────────────────────────────────
    void ShopPressed()
    {
        if (!string.IsNullOrEmpty(shopSceneName))
            SceneManager.LoadScene(shopSceneName);
        else
            Debug.LogWarning("Shop scene not assigned!");
    }

    // ────────────────────────────────────────────────────────────────────
    //  SETTINGS  – load settings scene
    // ────────────────────────────────────────────────────────────────────
    void SettingsPressed()
    {
        if (!string.IsNullOrEmpty(settingsSceneName))
            SceneManager.LoadScene(settingsSceneName);
        else
            Debug.LogWarning("Settings scene not assigned!");
    }
}
