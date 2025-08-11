using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(LineRenderer))]
public class SimpleDragLaunchNewInput : MonoBehaviour
{
    [Header("Launch Settings")]
    public float maxDragDistance = 3f;
    public float launchForceMultiplier = 10f;

    [Header("Trajectory Settings")]
    public int trajectoryPoints = 30;
    public float trajectoryTimeStep = 0.1f;

    private Rigidbody2D rb;
    private Collider2D col;
    private LineRenderer lineRenderer;

    private Vector2 dragEndPos;
    private bool isDragging = false;
    private bool pointerWasDown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        lineRenderer = GetComponent<LineRenderer>();

        //rb.isKinematic = true;
       // col.enabled = false;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (!rb.isKinematic)
            return;

        Vector2? pointerPos = null;
        bool pointerDownNow = false;

        // Touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            pointerPos = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
            pointerDownNow = true;
        }
        // Mouse input
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            pointerPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pointerDownNow = true;
        }

        // BEGIN DRAG
        if (pointerDownNow && !pointerWasDown && pointerPos.HasValue)
        {
            if (Vector2.Distance(pointerPos.Value, rb.position) <= maxDragDistance)
            {
                isDragging = true;
                Debug.Log("Started dragging");
            }
        }

        // DRAGGING
        if (isDragging && pointerDownNow && pointerPos.HasValue)
        {
            Vector2 dragVector = pointerPos.Value - rb.position;

            if (dragVector.magnitude > maxDragDistance)
                dragVector = dragVector.normalized * maxDragDistance;

            dragEndPos = rb.position + dragVector;

            Vector2 launchVelocity = (rb.position - dragEndPos) * launchForceMultiplier;
            DrawTrajectory(rb.position, launchVelocity);
        }

        // RELEASE
        if (isDragging && !pointerDownNow && pointerWasDown)
        {
            Debug.Log("Launching ball!");
            LaunchBall();
        }

        // CANCEL
        if (!pointerDownNow && !isDragging)
        {
            lineRenderer.positionCount = 0;
        }

        pointerWasDown = pointerDownNow;
    }

    void LaunchBall()
    {
        isDragging = false;
        lineRenderer.positionCount = 0;

        rb.isKinematic = false;
        col.enabled = true;

        Vector2 launchVelocity = (rb.position - dragEndPos) * launchForceMultiplier;
        rb.linearVelocity = launchVelocity;

        Debug.Log($"Ball launched with velocity: {rb.linearVelocity}");
    }

    void DrawTrajectory(Vector2 startPos, Vector2 velocity)
    {
        lineRenderer.positionCount = trajectoryPoints;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector2 point = startPos + velocity * t; // straight line
            lineRenderer.SetPosition(i, point);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        col.enabled = false;
        lineRenderer.positionCount = 0;
    }
}
