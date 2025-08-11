using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private int currentMoney;

    void Start()
    {
        // Load saved money from PlayerPrefs
        currentMoney = PlayerPrefs.GetInt("Money", 0);
        UpdateMoneyDisplay();
    }

    // Optional: Call this to refresh money value, e.g. after earning or spending
    public void RefreshMoney()
    {
        currentMoney = PlayerPrefs.GetInt("Money", 0);
        UpdateMoneyDisplay();
    }

    void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = "" + currentMoney;
        }
    }

    // Optional: If you want to test increasing or decreasing money from here
    public void AddMoney(int amount)
    {
        currentMoney += amount;
        PlayerPrefs.SetInt("Money", currentMoney);
        PlayerPrefs.Save();
        UpdateMoneyDisplay();
    }

    public void SubtractMoney(int amount)
    {
        currentMoney = Mathf.Max(0, currentMoney - amount);
        PlayerPrefs.SetInt("Money", currentMoney);
        PlayerPrefs.Save();
        UpdateMoneyDisplay();
    }
}
