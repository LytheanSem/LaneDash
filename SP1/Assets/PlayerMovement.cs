using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 2.5f; // Distance between lanes
    public float laneTransitionDuration = 0.2f; // Time to complete the lane transition
    private Rigidbody rb;
    private bool isCrouching = false;
    private bool isTransitioning = false; // To prevent simultaneous transitions

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
            StartCoroutine(SmoothLaneTransition((currentLane - 1) * laneDistance));
        }
    }

    public void MoveLeft()
    {
        if (!isTransitioning && currentLane > 0) // Check if not already in the leftmost lane
        {
            currentLane--;
            StartCoroutine(SmoothLaneTransition((currentLane - 1) * laneDistance));
        }
    }

    public void MoveRight()
    {
        if (!isTransitioning && currentLane < 2) // Check if not already in the rightmost lane
        {
            currentLane++;
            StartCoroutine(SmoothLaneTransition((currentLane - 1) * laneDistance));
        }
    }

    private IEnumerator SmoothLaneTransition(float targetX)
    {
        isTransitioning = true; // Prevent multiple transitions at the same time
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(targetX, startPosition.y, startPosition.z);

        float elapsedTime = 0f;

        while (elapsedTime < laneTransitionDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / laneTransitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure exact final position
        isTransitioning = false; // Allow new transitions
    }

    public void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f) // Only jump when nearly grounded
        {
            rb.velocity = new Vector3(rb.velocity.x, 6f, rb.velocity.z); // Set upward velocity for jump
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
