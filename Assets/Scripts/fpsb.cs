using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    [Header("Performance Settings")]
    public int targetFPS = 60;
    public bool disableVSync = true;

    void Awake()
    {
        if (disableVSync)
        {
            QualitySettings.vSyncCount = 0; // Disable VSync so FPS cap works
        }

        Application.targetFrameRate = targetFPS;

        Debug.Log($"PerformanceManager: FPS capped at {targetFPS}. VSync Disabled: {disableVSync}");
    }
}
