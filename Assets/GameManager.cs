using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class FixCoilWhine : MonoBehaviour
{
    void Start()
    {
        var ap = Holder.Instance;
        if (ap != null && ap.DevicePerformanceControl != null)
        {
            ap.DevicePerformanceControl.AutomaticPerformanceControl = false;
            ap.DevicePerformanceControl.CpuLevel = 1;
            ap.DevicePerformanceControl.GpuLevel = 1;
            Debug.Log("Locked CPU/GPU levels to reduce coil whine.");
        }
    }
}
