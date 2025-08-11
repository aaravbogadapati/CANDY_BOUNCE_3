using UnityEngine;

public class HideCanvasOnStart : MonoBehaviour
{
    [Header("Assign your Canvas here")]
    public Canvas targetCanvas;

    void Start()
    {
        if (targetCanvas != null)
        {
            targetCanvas.enabled = false; // Hides the assigned Canvas on scene start
        }
        else
        {
            Debug.LogWarning("No Canvas assigned in the Inspector!");
        }
    }
}
