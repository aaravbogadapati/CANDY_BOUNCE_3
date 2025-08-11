using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NewCanvasManager : MonoBehaviour
{
    [Header("Buttons to Show Only One Canvas")]
    public List<Button> buttons;

    [Header("Canvases to Control")]
    public List<Canvas> canvases;

    [Header("Control Visibility from Inspector")]
    public List<bool> canvasVisibility;

    void Start()
    {
        // Auto-wire buttons to show only one canvas
        for (int i = 0; i < buttons.Count && i < canvases.Count; i++)
        {
            int index = i;
            if (buttons[i] != null)
                buttons[i].onClick.AddListener(() => ShowOnlyCanvas(index));
        }

        // Initialize canvas visibility list if needed
        SyncVisibilityList();
        ApplyVisibilityFromInspector();
    }

    void Update()
    {
        // Sync canvas visibility every frame in Play Mode (optional: optimize if needed)
        ApplyVisibilityFromInspector();
    }

    private void ApplyVisibilityFromInspector()
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            if (canvases[i] != null && i < canvasVisibility.Count)
            {
                canvases[i].enabled = canvasVisibility[i];
            }
        }
    }

    public void ShowOnlyCanvas(int index)
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            bool shouldShow = (i == index);
            canvases[i].enabled = shouldShow;

            if (i < canvasVisibility.Count)
                canvasVisibility[i] = shouldShow;
        }
    }

    private void SyncVisibilityList()
    {
        while (canvasVisibility.Count < canvases.Count)
        {
            canvasVisibility.Add(true); // Default to visible
        }
        while (canvasVisibility.Count > canvases.Count)
        {
            canvasVisibility.RemoveAt(canvasVisibility.Count - 1);
        }
    }
}
