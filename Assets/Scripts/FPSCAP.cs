using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [Header("FPS Settings")]
    public int targetFPS = 60;

    void Start()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = 0; // Make sure VSync doesn't override the target
    }
}
