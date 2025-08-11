using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    [Header("Settings")]
    public string scoreTMPTag = "ScoreText";  // Tag of the TMP score object in scene
    public string ballTag = "ball2";            // Tag of the ball

    [Header("HP Sprites")]
    public Sprite[] hpSprites;                 // Index 0 = HP 1, Index 1 = HP 2, etc.

    private int hp;
    private SpriteRenderer sr;
    private TextMeshProUGUI scoreTMP;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Find TMP score object by tag
        GameObject scoreObj = GameObject.FindGameObjectWithTag(scoreTMPTag);
        if (scoreObj != null)
        {
            scoreTMP = scoreObj.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogWarning($"No GameObject with tag '{scoreTMPTag}' found! Defaulting HP to 1.");
        }

        hp = CalculateStartingHPFromTMP();
        hp = Mathf.Clamp(hp, 1, hpSprites.Length);
        UpdateSprite();
    }

    int CalculateStartingHPFromTMP()
    {
        if (scoreTMP == null)
        {
            return 1;
        }

        string raw = scoreTMP.text.Replace("Score: ", "").Trim();
        if (int.TryParse(raw, out int score))
        {
            return Mathf.Clamp(score / 5 + 1, 1, hpSprites.Length);
        }

        Debug.LogWarning("Failed to parse TMP score text! Defaulting to 1 HP.");
        return 1;
    }

    void UpdateSprite()
    {
        int index = Mathf.Clamp(hp - 1, 0, hpSprites.Length - 1);

        if (hpSprites != null && hpSprites.Length > 0 && hpSprites[index] != null)
        {
            sr.sprite = hpSprites[index];
        }
        else
        {
            Debug.LogWarning($"Missing sprite for HP {hp} at index {index}!");
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            UpdateSprite();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ballTag))
        {
            TakeDamage(1);
        }
    }
}
