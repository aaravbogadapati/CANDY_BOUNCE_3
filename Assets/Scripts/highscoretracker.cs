using UnityEngine;
using TMPro;

public class BounceTracker : MonoBehaviour
{
    [Header("References")]
    public GameObject ball;                  // Drag the ball GameObject here
    public Collider2D floorCollider;         // Drag the floor's BoxCollider2D here
    public TextMeshProUGUI bounceText;       // Drag the TMP text UI element here

    private int bounceCount = 0;

    void OnEnable()
    {
        if (ball == null || floorCollider == null)
        {
            Debug.LogError("BounceTracker: Missing references.");
        }
    }

    void Update()
    {
        // This checks for collisions manually every frame using Physics2D
        if (ball != null && floorCollider != null)
        {
            Collider2D ballCollider = ball.GetComponent<Collider2D>();
            if (ballCollider != null && ballCollider.IsTouching(floorCollider))
            {
                bounceCount++;
                UpdateBounceText();
                StartCoroutine(WaitForExitThenEnableDetection());
                enabled = false; // Disable temporarily to avoid repeated counts
            }
        }
    }

    void UpdateBounceText()
    {
        if (bounceText != null)
        {
            bounceText.text = "" + bounceCount;
        }
    }

    System.Collections.IEnumerator WaitForExitThenEnableDetection()
    {
        // Wait until ball is no longer touching the floor
        while (ball.GetComponent<Collider2D>().IsTouching(floorCollider))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.05f); // small delay to avoid false triggers
        enabled = true;
    }
}
