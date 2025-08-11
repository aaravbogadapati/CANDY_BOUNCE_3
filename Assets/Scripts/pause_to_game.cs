using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcherWithTwoButtons : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button forwardButton;  // Show target canvas
    [SerializeField] private Button backButton;     // Return to original canvas

    [Header("Canvases")]
    [SerializeField] private Canvas canvasA;        // Current/default canvas
    [SerializeField] private Canvas canvasB;        // Target/next canvas

    private void Awake()
    {
        if (forwardButton != null)
            forwardButton.onClick.AddListener(SwitchToCanvasB);

        if (backButton != null)
            backButton.onClick.AddListener(SwitchToCanvasA);
    }

    private void SwitchToCanvasB()
    {
        if (canvasA != null) canvasA.enabled = false;
        if (canvasB != null) canvasB.enabled = true;
    }

    private void SwitchToCanvasA()
    {
        if (canvasB != null) canvasB.enabled = false;
        if (canvasA != null) canvasA.enabled = true;
    }
}
