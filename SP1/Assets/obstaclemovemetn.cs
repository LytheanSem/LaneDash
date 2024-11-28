using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 5f; // Speed at which the obstacle moves
    public float destroyZPosition = -20f; // Position to destroy the obstacle

    void Update()
    {
        // Move the obstacle toward the player
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Destroy the obstacle if it moves off-screen
        if (transform.position.z <= destroyZPosition)
        {
            Destroy(gameObject);
        }
    }
}
