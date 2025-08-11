using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleLocalizationManager : MonoBehaviour
{
    public static SimpleLocalizationManager Instance;

    // Store all translations: LanguageCode => (Key => Value)
    private Dictionary<string, Dictionary<string, string>> languages = new Dictionary<string, Dictionary<string, string>>();

    public string currentLanguage = "en";

    // Keep track of all texts in scene to update on language change
    private List<LocalizedText> localizedTexts = new List<LocalizedText>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupLanguages();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Setup translations here for all supported languages
    private void SetupLanguages()
    {
        languages = new Dictionary<string, Dictionary<string, string>>();

        // English
        languages["en"] = new Dictionary<string, string>()
        {
            { "play_button", "Play" },
            { "settings_button", "Settings" },
            { "exit_button", "Exit" },
            { "money_text", "Money: {0}" },
            { "not_enough", "Not enough money!" }
        };

        // Spanish (es)
        languages["es"] = new Dictionary<string, string>()
        {
            { "play_button", "Jugar" },
            { "settings_button", "Configuración" },
            { "exit_button", "Salir" },
            { "money_text", "Dinero: {0}" },
            { "not_enough", "¡No tienes suficiente dinero!" }
        };

        // French (fr)
        languages["fr"] = new Dictionary<string, string>()
        {
            { "play_button", "Jouer" },
            { "settings_button", "Paramètres" },
            { "exit_button", "Quitter" },
            { "money_text", "Argent : {0}" },
            { "not_enough", "Pas assez d'argent !" }
        };

        // Portuguese (pt)
        languages["pt"] = new Dictionary<string, string>()
        {
            { "play_button", "Jogar" },
            { "settings_button", "Configurações" },
            { "exit_button", "Sair" },
            { "money_text", "Dinheiro: {0}" },
            { "not_enough", "Dinheiro insuficiente!" }
        };

        // German (de)
        languages["de"] = new Dictionary<string, string>()
        {
            { "play_button", "Spielen" },
            { "settings_button", "Einstellungen" },
            { "exit_button", "Beenden" },
            { "money_text", "Geld: {0}" },
            { "not_enough", "Nicht genug Geld!" }
        };

        // Russian (ru)
        languages["ru"] = new Dictionary<string, string>()
        {
            { "play_button", "Играть" },
            { "settings_button", "Настройки" },
            { "exit_button", "Выход" },
            { "money_text", "Деньги: {0}" },
            { "not_enough", "Недостаточно денег!" }
        };

        // Chinese Simplified (zh)
        languages["zh"] = new Dictionary<string, string>()
        {
            { "play_button", "开始" },
            { "settings_button", "设置" },
            { "exit_button", "退出" },
            { "money_text", "金币: {0}" },
            { "not_enough", "金币不足！" }
        };

        // Japanese (ja)
        languages["ja"] = new Dictionary<string, string>()
        {
            { "play_button", "プレイ" },
            { "settings_button", "設定" },
            { "exit_button", "終了" },
            { "money_text", "所持金: {0}" },
            { "not_enough", "お金が足りません！" }
        };

        // Korean (ko)
        languages["ko"] = new Dictionary<string, string>()
        {
            { "play_button", "플레이" },
            { "settings_button", "설정" },
            { "exit_button", "종료" },
            { "money_text", "돈: {0}" },
            { "not_enough", "돈이 부족합니다!" }
        };
    }

    public void RegisterText(LocalizedText text)
    {
        if (!localizedTexts.Contains(text))
            localizedTexts.Add(text);
    }

    public void UnregisterText(LocalizedText text)
    {
        if (localizedTexts.Contains(text))
            localizedTexts.Remove(text);
    }

    public string GetTranslation(string key)
    {
        if (languages.TryGetValue(currentLanguage, out var dict))
        {
            if (dict.TryGetValue(key, out var value))
                return value;
        }
        // fallback to key if not found
        return key;
    }

    // Change language and update all texts
    public void ChangeLanguage(string langCode)
    {
        currentLanguage = langCode;
        foreach (var text in localizedTexts)
        {
            text.UpdateText();
        }
    }
}

// Attach this script to any TMP_Text component that needs localization
[RequireComponent(typeof(TMP_Text))]
public class LocalizedText : MonoBehaviour
{
    public string key;
    private TMP_Text tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        SimpleLocalizationManager.Instance.RegisterText(this);
    }

    private void OnDestroy()
    {
        if (SimpleLocalizationManager.Instance != null)
            SimpleLocalizationManager.Instance.UnregisterText(this);
    }

    private void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        string translated = SimpleLocalizationManager.Instance.GetTranslation(key);
        tmpText.text = translated;
    }
}
