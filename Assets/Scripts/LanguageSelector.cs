using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class LanguageOption
{
    public string languageName;
    public string languageCode;
}

public class LanguageSelector : MonoBehaviour
{
    [SerializeField] private TMP_Text languageNameText;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private TMP_Text selectButtonText;
    [SerializeField] private List<LanguageOption> languageOptions;

    private int currentIndex = 0;

    void Start()
    {
        // Load saved language or use default index 0
        if (PlayerPrefs.HasKey("SelectedLanguage"))
        {
            string savedCode = PlayerPrefs.GetString("SelectedLanguage");
            int savedIndex = languageOptions.FindIndex(option => option.languageCode == savedCode);
            currentIndex = savedIndex != -1 ? savedIndex : 0;
        }
        else
        {
            currentIndex = 0;
            PlayerPrefs.SetString("SelectedLanguage", languageOptions[0].languageCode);
            PlayerPrefs.Save();
        }

        leftButton.onClick.AddListener(PreviousLanguage);
        rightButton.onClick.AddListener(NextLanguage);
        selectButton.onClick.AddListener(SelectLanguage);

        UpdateUI();
    }

    void UpdateUI()
    {
        languageNameText.text = languageOptions[currentIndex].languageName;

        string selectedCode = PlayerPrefs.GetString("SelectedLanguage");
        bool isSelected = languageOptions[currentIndex].languageCode == selectedCode;
        selectButtonText.text = isSelected ? "Selected" : "Select";
    }

    void PreviousLanguage()
    {
        currentIndex = (currentIndex - 1 + languageOptions.Count) % languageOptions.Count;
        UpdateUI();
    }

    void NextLanguage()
    {
        currentIndex = (currentIndex + 1) % languageOptions.Count;
        UpdateUI();
    }

    void SelectLanguage()
    {
        PlayerPrefs.SetString("SelectedLanguage", languageOptions[currentIndex].languageCode);
        PlayerPrefs.Save();
        UpdateUI();
    }
} 
