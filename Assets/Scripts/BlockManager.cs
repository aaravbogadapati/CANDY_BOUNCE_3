using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject blockPrefab;
    public GameObject bombPrefab;      // ✅ Bomb prefab
    public GameObject moneyPrefab;     // ✅ New money prefab
    public Transform[] spawners;       // Exactly 7 spawner points
    public GameObject ball;            // The ball object

    private bool gameStarted = false;

    void Start()
    {
        if (spawners.Length != 7)
        {
            Debug.LogError("You must assign exactly 7 spawners!");
            return;
        }

        if (blockPrefab == null || ball == null)
        {
            Debug.LogError("Assign Block Prefab and Ball in Inspector!");
            return;
        }

        // Add floor detector to the ball (only for triggering block spawning)
        if (ball.GetComponent<BallFloorTrigger>() == null)
        {
            ball.AddComponent<BallFloorTrigger>().manager = this;
        }

        // ✅ Automatically start the game
        StartGame();
    }

    public void StartGame()
    {
        if (gameStarted) return;
        gameStarted = true;

        SpawnInitialBlocks();
    }

    void SpawnInitialBlocks()
    {
        foreach (Transform spawner in spawners)
        {
            SpawnBlockAt(spawner.position);
        }
    }
    void SpawnBlockAt(Vector3 position)
    {
        // Check for existing objects only with specific tags
        Collider2D hit = Physics2D.OverlapCircle(position, 0.1f);

        if (hit != null &&
           (hit.CompareTag("Block") || hit.CompareTag("Bomb") || hit.CompareTag("Money")))
        {
            return; // Skip spawn if a block, bomb, or coin is already here
        }

        // ✅ 7% chance to spawn money
        if (moneyPrefab != null && Random.value < 0.07f)
        {
            Instantiate(moneyPrefab, position, Quaternion.identity);
        }
        // ✅ 10% chance to spawn a bomb
        else if (bombPrefab != null && Random.value < 0.1f)
        {
            Instantiate(bombPrefab, position, Quaternion.identity);
        }
        else
        {
            Instantiate(blockPrefab, position, Quaternion.identity);
        }
    }


    public void OnBallHitFloor()
    {
        SpawnNewTopRow();
    }

    void SpawnNewTopRow()
    {
        foreach (Transform spawner in spawners)
        {
            if (Random.value < 0.3f) // 30% chance
            {
                SpawnBlockAt(spawner.position);
            }
        }
    }

    // Nested floor detector that triggers new row spawn
    private class BallFloorTrigger : MonoBehaviour
    {
        public BlockManager manager;

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Floor"))
            {
                manager.OnBallHitFloor();
            }
        }
    }
}
