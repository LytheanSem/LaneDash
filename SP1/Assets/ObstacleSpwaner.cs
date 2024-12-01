using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array of different obstacle prefabs
    public float spawnInterval = 2f;     // Time between obstacle spawns
    public float laneWidth = 3f;         // Distance between lanes (assuming 3 lanes)
    public float spawnHeight = 1f;       // Height where obstacles are spawned
    public float spawnZOffset = 20f;     // Z position offset to spawn obstacles ahead of the player

    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        // Randomly select a type of obstacle from the array
        int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacle = obstaclePrefabs[randomObstacleIndex];

        // Randomize lane position (-1 for left, 0 for middle, 1 for right)
        int randomLane = Random.Range(-1, 2);
        float spawnXPosition = randomLane * laneWidth; // Calculate X position based on lane width

        // Create the spawn position for the obstacle
        Vector3 spawnPosition = new Vector3(spawnXPosition, spawnHeight, transform.position.z + spawnZOffset);

        // Instantiate the obstacle at the calculated position
        Instantiate(obstacle, spawnPosition, Quaternion.identity);
    }
}
