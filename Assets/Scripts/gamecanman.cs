using UnityEngine;

public class DisableCanvasOnStart : MonoBehaviour
{
    [Header("Assign the Canvas you want to disable")]
    public GameObject targetCanvas;

    void Start()
    {
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No Canvas assigned in the Inspector.");
        }
    }
}
