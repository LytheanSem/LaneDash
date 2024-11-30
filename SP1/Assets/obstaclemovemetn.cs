using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;          // Speed at which the obstacle moves
    public float resetPosition = -20f; // Position where the obstacle resets (off-screen)
    public float fixedYPosition = 1f;  // Set to the Y position you want your obstacles at

    void Update()
    {
        // Move the obstacle towards the player (negative Z direction)
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Keep the Y position fixed
        transform.position = new Vector3(transform.position.x, fixedYPosition, transform.position.z);

        // Reset the obstacle position if it moves past the player
        if (transform.position.z <= resetPosition)
        {
            gameObject.SetActive(false); // Optionally, deactivate or return to object pool
        }
    }
}
