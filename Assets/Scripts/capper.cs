using UnityEngine;

public class PerformanceLimiter : MonoBehaviour
{
    [Header("Performance Settings")]
    public int targetFrameRate = 60;   // Set your preferred cap
    public bool disableVSync = true;

    void Awake()
    {
        if (disableVSync)
        {
            QualitySettings.vSyncCount = 0; // Make sure VSync is off
        }

        Application.targetFrameRate = targetFrameRate;
        Debug.Log($"[PerformanceLimiter] FPS capped at {targetFrameRate}. VSync Off: {disableVSync}");
    }
}
