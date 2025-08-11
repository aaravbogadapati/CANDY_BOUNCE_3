using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text skinNameText;
    [SerializeField] private TMP_Text notEnoughPopup;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button equipButton;

    [SerializeField] private List<ShopItem> shopItems;

    [Header("Money Settings")]
    [SerializeField] private bool overrideMoneyFromEditor = false;
    [SerializeField] private int playerMoney = 1000;

    [Header("Default Item")]
    [SerializeField] private int defaultEquippedIndex = 0;

    private int currentIndex = 0;
    private HashSet<int> purchasedItems = new HashSet<int>();
    private int equippedItemIndex = -1;
    private Coroutine flashRoutine;

    void Start()
    {
        // Load or set player money
        if (overrideMoneyFromEditor)
        {
            PlayerPrefs.SetInt("Money", playerMoney);
            PlayerPrefs.Save();
        }
        else if (PlayerPrefs.HasKey("Money"))
        {
            playerMoney = PlayerPrefs.GetInt("Money");
        }

        // Load purchased items
        if (PlayerPrefs.HasKey("BoughtItems"))
        {
            string data = PlayerPrefs.GetString("BoughtItems");
            string[] parts = data.Split(',');
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int id))
                    purchasedItems.Add(id);
            }
        }

        // Load equipped item or set default
        if (PlayerPrefs.HasKey("EquippedItem"))
        {
            equippedItemIndex = PlayerPrefs.GetInt("EquippedItem");
        }
        else
        {
            // First-time setup: equip and unlock the default item
            equippedItemIndex = defaultEquippedIndex;
            purchasedItems.Add(defaultEquippedIndex);

            PlayerPrefs.SetInt("EquippedItem", equippedItemIndex);
            PlayerPrefs.SetString("BoughtItems", string.Join(",", purchasedItems));
            PlayerPrefs.Save();
        }

        leftButton.onClick.AddListener(PreviousItem);
        rightButton.onClick.AddListener(NextItem);
        buyButton.onClick.AddListener(BuyItem);
        equipButton.onClick.AddListener(EquipItem);

        UpdateUI();
    }

    void UpdateUI()
    {
        if (shopItems.Count == 0) return;

        var item = shopItems[currentIndex];
        skinNameText.text = item.itemName;
        itemImage.sprite = item.itemIcon;
        moneyText.text = "" + playerMoney;

        bool isBought = purchasedItems.Contains(currentIndex);
        TMP_Text buyButtonText = buyButton.GetComponentInChildren<TMP_Text>();

        if (!isBought)
        {
            buyButton.gameObject.SetActive(true);
            buyButtonText.text = "Buy for " + item.price;
            equipButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
            equipButton.GetComponentInChildren<TMP_Text>().text = (equippedItemIndex == currentIndex) ? "Equipped" : "Equip";
        }
    }

    void PreviousItem()
    {
        currentIndex = (currentIndex - 1 + shopItems.Count) % shopItems.Count;
        UpdateUI();
    }

    void NextItem()
    {
        currentIndex = (currentIndex + 1) % shopItems.Count;
        UpdateUI();
    }

    void BuyItem()
    {
        var item = shopItems[currentIndex];

        if (purchasedItems.Contains(currentIndex)) return;

        if (playerMoney >= item.price)
        {
            playerMoney -= item.price;
            purchasedItems.Add(currentIndex);

            PlayerPrefs.SetInt("Money", playerMoney);
            PlayerPrefs.SetString("BoughtItems", string.Join(",", purchasedItems));
            PlayerPrefs.Save();

            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough money!");

            if (flashRoutine != null)
                StopCoroutine(flashRoutine);

            flashRoutine = StartCoroutine(FlashRedMoney());

            if (notEnoughPopup != null)
                StartCoroutine(ShowNotEnoughPopup());
        }
    }

    void EquipItem()
    {
        equippedItemIndex = currentIndex;
        PlayerPrefs.SetInt("EquippedItem", equippedItemIndex);
        PlayerPrefs.Save();

        UpdateUI();
    }

    IEnumerator FlashRedMoney()
    {
        Color originalColor = moneyText.color;
        moneyText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        moneyText.color = originalColor;
    }

    IEnumerator ShowNotEnoughPopup()
    {
        notEnoughPopup.alpha = 1;
        yield return new WaitForSeconds(1f);
        notEnoughPopup.alpha = 0;
    }
}
