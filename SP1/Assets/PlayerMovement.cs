using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 2.5f; // Distance between lanes
    private Rigidbody rb;
    private bool isCrouching = false;

    private int currentLane = 1; // 0 = left, 1 = center, 2 = right
    private Vector3 originalScale; // Store the original scale of the cube

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Store the original scale of the cube
        originalScale = transform.localScale;

        // Ensure the player starts in the center lane
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // Handle horizontal movement
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // Move Left
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // Move Right
        {
            MoveRight();
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space)) // Jump
        {
            Jump();
        }

        // Handle crouching and standing up
        if (Input.GetKeyDown(KeyCode.DownArrow)) // Bend Down
        {
            BendDown();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow)) // Stand Up
        {
            StandUp();
        }
    }

    public void MoveToCenter()
    {
        if (currentLane != 1) // Only move to center if not already in the center lane
        {
            currentLane = 1; // Set lane to center
            UpdateLanePosition();
        }
    }

    public void MoveLeft()
    {
        if (currentLane > 0) // Check if not already in the leftmost lane
        {
            currentLane--;
            UpdateLanePosition();
        }
    }

    public void MoveRight()
    {
        if (currentLane < 2) // Check if not already in the rightmost lane
        {
            currentLane++;
            UpdateLanePosition();
        }
    }

    private void UpdateLanePosition()
    {
        float targetX = (currentLane - 1) * laneDistance; // -1 for left, 0 for center, 1 for right
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }

    public void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f) // Only jump when nearly grounded
        {
            rb.AddForce(Vector3.up * 6f, ForceMode.VelocityChange); // Adjust force as needed
        }
    }

    public void BendDown()
    {
        if (!isCrouching)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y / 2, originalScale.z);
            isCrouching = true;
        }
    }

    public void StandUp()
    {
        if (isCrouching)
        {
            transform.localScale = originalScale; // Restore the original scale
            isCrouching = false;
            Debug.Log("StandUp() called: Cube scale restored.");
        }
    }
}
