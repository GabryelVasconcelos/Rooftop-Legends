using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform player;

    [Header("Obstacle Prefabs")]
    [SerializeField] private GameObject[] obstacles;

    [Header("Spawning")]
    [SerializeField] private float spawnDistance = 20f;
    [SerializeField] private float minInterval = 2f;
    [SerializeField] private float maxInterval = 5f;

    private float lastSpawnZ;

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>()?.transform;
        }

        if (player == null)
        {
            enabled = false;
            return;
        }

        lastSpawnZ = player.position.z + spawnDistance;
        InvokeRepeating(nameof(SpawnObstacle), 1f, 0.5f);
    }

    void SpawnObstacle()
    {
        if (player == null || obstacles == null || obstacles.Length == 0) return;

        if (player.position.z + spawnDistance > lastSpawnZ)
        {
            float spawnZ = lastSpawnZ;
            float randomX = Random.Range(-5.5f, 5.5f);
            Vector3 spawnPos = new Vector3(randomX, player.position.y, spawnZ);

            if (obstacles.Length > 0)
            {
                int index = Random.Range(0, obstacles.Length);
                Instantiate(obstacles[index], spawnPos, Quaternion.identity);
            }

            lastSpawnZ += Random.Range(minInterval, maxInterval) * 10f;
        }
    }
}
