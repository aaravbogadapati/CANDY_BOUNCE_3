using UnityEngine;

public class BombExplode : MonoBehaviour
{
    [Header("Explosion Settings")]
    public GameObject projectilePrefab; // Prefab to shoot when bomb is destroyed
    public float shootSpeed = 5f;       // Speed of the spawned objects
    public float destroyDelay = 0.5f;   // Time before spawned objects are destroyed

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball2"))
        {
            Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ball2"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // Directions: up, down, left, right
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 dir in directions)
        {
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Give it movement if it has Rigidbody2D
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = dir * shootSpeed;
            }

            // Destroy after delay
            Destroy(clone, destroyDelay);
        }

        // Destroy the bomb itself
        Destroy(gameObject);
    }
}
