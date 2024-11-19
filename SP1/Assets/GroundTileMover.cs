using UnityEngine;

public class GroundMover : MonoBehaviour
{
    public float speed = 5f; // Speed at which the ground moves backward
    public float resetPositionZ = -20f; // Position at which the ground should reset
    public float startPositionZ = 20f; // Position to move the ground back to

    void Update()
    {
        // Move the ground backward
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // If the ground goes past the reset position, move it back to the start position
        if (transform.position.z <= resetPositionZ)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = startPositionZ;
            transform.position = newPosition;
        }

    
    }
}
