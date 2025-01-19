using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 6f;
    public float laneSwitchSpeed = 10f; // Speed for switching lanes
    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private float[] lanes = { -5f, 0f, 5f }; // Lane positions on x-axis
    public bool isJumping = false;
    public bool comingDown = false;
    public bool comingUp = false; // New state for sliding transition
    public bool isSliding = false; // Check if player is sliding
    public GameObject playerObject;

    private float originalYPosition; // To store the player's original Y position (before sliding)

    // Update is called once per frame
    void Update()
    {
        // Continuous running
        if (!isSliding) // Don't move forward if sliding
        {
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);
        }

        // Switch lane left (A, Left Arrow)
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane > 0)
        {
            currentLane--;
        }

        // Switch lane right (D, Right Arrow)
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane < lanes.Length - 1)
        {
            currentLane++;
        }

        // Smoothly move to the target lane
        float targetXPosition = lanes[currentLane];
        float newX = Mathf.Lerp(transform.position.x, targetXPosition, Time.deltaTime * laneSwitchSpeed);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            if (!isJumping) // Prevent multiple jumps
            {
                isJumping = true;
                playerObject.GetComponent<Animator>().Play("Jump");
                StartCoroutine(JumpSequence()); // Start the Jump Coroutine
            }
        }

        // Handle jumping and coming down
        if (isJumping)
        {
            if (!comingDown)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 3, Space.World); // Move up
            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * -3, Space.World); // Move down
            }
        }

        // Sliding (Left Control or Shift)
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.C)) && !isSliding && !isJumping)
        {
            isSliding = true;
            playerObject.GetComponent<Animator>().Play("Falling To Roll"); // Play the slide animation
            StartCoroutine(SlideSequence()); // Start the slide Coroutine
        }

        // Smoothly come up after sliding
        if (comingUp)
        {
            // Smoothly rise back to the original Y position after sliding
            float riseSpeed = 2f; // Speed at which the player rises back to original position
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, originalYPosition, Time.deltaTime * riseSpeed), transform.position.z);

            // If we are close enough to the original position, stop the transition
            if (Mathf.Abs(transform.position.y - originalYPosition) < 0.1f)
            {
                comingUp = false; // Stop coming up when we are near the target Y position
                transform.position = new Vector3(transform.position.x, originalYPosition, transform.position.z); // Ensure perfect alignment
            }
        }
    }

    // Jump sequence with delays for upward and downward movement
    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(0.45f); // Wait for upward movement
        comingDown = true; // Start moving down after jumping

        yield return new WaitForSeconds(0.45f); // Wait before landing

        isJumping = false; // End the jump
        comingDown = false; // Reset the fall state

        // Play the "Fast Run" animation after the jump
        playerObject.GetComponent<Animator>().Play("Fast Run");
    }

    // Slide sequence with a delay to simulate sliding motion
    IEnumerator SlideSequence()
    {
        // Store the player's original Y position before sliding
        originalYPosition = transform.position.y;

        // Adjust the player's speed during the slide (faster while sliding)
        float originalSpeed = playerSpeed;
        playerSpeed *= 2f; // Increase the speed for the slide duration

        // Slide duration (can be adjusted)
        yield return new WaitForSeconds(1f); // Keep sliding for 1 second

        // End the slide and restore normal speed
        isSliding = false;
        playerSpeed = originalSpeed;

        // Enable the comingUp flag for smooth transition after sliding
        comingUp = true;

        // Wait a short time before transitioning fully up
        yield return new WaitForSeconds(0.3f); // Adjust the time as needed

        comingUp = false; // Reset the comingUp flag after transition
        // Play the "Fast Run" animation after sliding
        playerObject.GetComponent<Animator>().Play("Fast Run");
    }
}