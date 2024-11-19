using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 5f; // Speed at which the obstacle moves toward the player
    public float destroyZPosition = -20f; // Z position at which the obstacle should be destroyed (off-screen)

    void Update()
    {
        // Move the obstacle toward the player by translating it along the negative Z axis
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // If the obstacle goes off-screen (past the destroy position), destroy it to clean up
        if (transform.position.z <= destroyZPosition)
        {
            Destroy(gameObject); // Destroy the obstacle
        }
    }
}
