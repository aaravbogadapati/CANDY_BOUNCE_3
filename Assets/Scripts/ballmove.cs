using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private Collider2D ballCollider;
    private Collider2D floorCollider;

    [SerializeField] private float moveDownDistance = 1f;

    private bool hasMovedDown = false;

    void Awake()
    {
        // This runs ONLY when prefab instance exists in scene
        if (ballCollider == null)
        {
            GameObject ballObj = GameObject.Find("ball");
            if (ballObj != null)
                ballCollider = ballObj.GetComponent<Collider2D>();
        }

        if (floorCollider == null)
        {
            GameObject floorObj = GameObject.Find("floor");
            if (floorObj != null)
                floorCollider = floorObj.GetComponent<Collider2D>();
        }
    }

    void Update()
    {
        if (!hasMovedDown && ballCollider != null && floorCollider != null)
        {
            if (ballCollider.IsTouching(floorCollider))
            {
                MoveDown();
                hasMovedDown = true;
            }
        }
        else if (hasMovedDown && ballCollider != null && floorCollider != null)
        {
            if (!ballCollider.IsTouching(floorCollider))
            {
                hasMovedDown = false;
            }
        }
    }

    private void MoveDown()
    {
        transform.position += Vector3.down * moveDownDistance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == ballCollider)
        {
            // Destroy(gameObject); // ← Removed as requested
        }
    }
}
