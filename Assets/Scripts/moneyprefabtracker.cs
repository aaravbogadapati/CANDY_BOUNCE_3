using UnityEngine;
using TMPro;

public class MoneyCollect : MonoBehaviour
{
    [Header("Settings")]
    public string ballTag = "ball2";              // Tag of the ball
    public string moneyTMPTag = "moneycounter";   // Tag of the TMP text object
    public int moneyValue = 1;                    // Value to add when collected

    private TextMeshProUGUI moneyTMP;

    void Start()
    {
        // Find the TMP UI object
        GameObject tmpObject = GameObject.FindGameObjectWithTag(moneyTMPTag);
        if (tmpObject != null)
        {
            moneyTMP = tmpObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("⚠ No TMP object found with tag: " + moneyTMPTag);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ballTag))
        {
            AddMoney();
            Destroy(gameObject);
        }
    }

    private void AddMoney()
    {
        if (moneyTMP == null) return;

        int currentMoney = 0;
        int.TryParse(moneyTMP.text.Trim(), out currentMoney);

        currentMoney += moneyValue;
        moneyTMP.text = currentMoney.ToString();
    }
}
