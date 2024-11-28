using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array of obstacle prefabs
    public float spawnInterval = 2f; // Time between obstacle spawns
    private float timer = 0f;

    // Lane positions for spawning
    private float[] lanePositions = new float[] { -2.5f, 0f, 2.5f };
    public float spawnZStart = 10f; // Minimum Z position for spawning
    public float spawnZEnd = 20f; // Maximum Z position for spawning
    public float fallbackY = 0.5f; // Default Y position if Raycast fails

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        // Select a random lane (X position)
        int randomLane = Random.Range(0, lanePositions.Length);
        float spawnX = lanePositions[randomLane];

        // Select a random Z position within range
        float spawnZ = Random.Range(spawnZStart, spawnZEnd);

        // Select a random obstacle prefab
        int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject selectedObstaclePrefab = obstaclePrefabs[randomObstacleIndex];

        // Raycast down to find the ground's Y position
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(spawnX, 50f, spawnZ), Vector3.down, out hit))
        {
            // Ground detected
            float spawnY = hit.point.y;
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);
            Instantiate(selectedObstaclePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // Fallback Y position
            Vector3 spawnPosition = new Vector3(spawnX, fallbackY, spawnZ);
            Instantiate(selectedObstaclePrefab, spawnPosition, Quaternion.identity);
        }

        // Debug the raycast visually in Scene view
        Debug.DrawRay(new Vector3(spawnX, 50f, spawnZ), Vector3.down * 50f, Color.red, 2f);
    }
}
