using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    private float timer = 0f;

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
        // Random spawn position on X and Z axis
        float spawnX = Random.Range(-5f, 5f);
        float spawnZ = Random.Range(10f, 20f);

        // Cast a ray downward to find the ground's Y position
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(spawnX, 50f, spawnZ), Vector3.down, out hit))
        {
            float spawnY = hit.point.y; // This is the Y value of the ground surface

            // Set the spawn position
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

            // Instantiate the obstacle at the spawn position
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
