using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragToLaunchTouch : MonoBehaviour
{
    public float launchForce = 10f;
    public float maxDragDistance = 3f;
    public int trajectoryPointCount = 30;
    public float timeStep = 0.1f;

    [Tooltip("Assign the floor collider that stops the projectile")]
    public Collider2D wallCollider;

    [Header("Drag the Play Button here (optional)")]
    public Button playButton;

    private Rigidbody2D rb;
    private Collider2D col;

    private bool isDragging = false;
    private Vector2 dragPos;
    private bool inputAllowed = false;

    private LineRenderer lineRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.gravityScale = 0;
        rb.isKinematic = true;
        col.enabled = false;

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("Add a LineRenderer component!");
        }
        lineRenderer.positionCount = 0;

        // Optional: Hook up the Play button if assigned
        if (playButton != null)
        {
            playButton.onClick.AddListener(EnableInput);
        }

        // Enable input on start so player can drag immediately (remove if you want Play button only)
        EnableInput();
    }

    public void EnableInput()
    {
        inputAllowed = true;
        isDragging = false;
        col.enabled = false;
        rb.isKinematic = true;
    }

    void Update()
    {
        if (!inputAllowed)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        Vector2? inputPos = null;

        // Check touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            inputPos = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
        }
        // Check mouse input via new Input System
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            inputPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        if (inputPos.HasValue)
        {
            Vector2 touchPos = inputPos.Value;
            Vector2 currentPos = rb.position;

            if (!isDragging && Vector2.Distance(touchPos, currentPos) < 1.5f) // start drag radius
            {
                isDragging = true;
            }

            if (isDragging)
            {
                Vector2 dragOffset = touchPos - currentPos;
                if (dragOffset.magnitude > maxDragDistance)
                    dragOffset = dragOffset.normalized * maxDragDistance;

                dragPos = currentPos + dragOffset;

                Vector2 launchVelocity = (currentPos - dragPos).normalized * launchForce;
                DrawStraightTrajectory(currentPos, launchVelocity);
            }
        }
        else if (isDragging)
        {
            // On release — launch ball
            isDragging = false;
            rb.isKinematic = false;
            col.enabled = true;

            lineRenderer.positionCount = 0;

            Vector2 launchDir = (rb.position - dragPos).normalized;
            rb.linearVelocity = launchDir * launchForce;

            inputAllowed = false;
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
    }

    void DrawStraightTrajectory(Vector2 startPosition, Vector2 directionVelocity)
    {
        lineRenderer.positionCount = trajectoryPointCount;

        for (int i = 0; i < trajectoryPointCount; i++)
        {
            float t = i * timeStep;
            Vector2 point = startPosition + directionVelocity * t;
            lineRenderer.SetPosition(i, point);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == wallCollider)
        {
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
            col.enabled = false;

            // Automatically enable input so you can drag again after hitting the floor
            EnableInput();
        }
    }
}
