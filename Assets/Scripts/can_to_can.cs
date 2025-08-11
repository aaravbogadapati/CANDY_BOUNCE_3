using UnityEngine;

public class GameCanToPauseCan : MonoBehaviour
{
    [Header("Canvas to Show")]
    [SerializeField] private Canvas canvasToEnable;

    [Header("Optional: Canvas to Hide")]
    [SerializeField] private Canvas canvasToDisable;

    public void ShowCanvas()
    {
        if (canvasToEnable != null)
            canvasToEnable.enabled = true;

        if (canvasToDisable != null)
            canvasToDisable.enabled = false;
    }
}
