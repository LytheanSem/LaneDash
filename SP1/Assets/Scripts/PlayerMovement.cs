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
    public bool isSliding = false; // Check if player is sliding
    public GameObject playerObject;

    // Update is called once per frame
    void Update()
    {
        // Continuous running
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

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
        // Sliding (Left Control or Shift)
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.C)) && !isSliding && !isJumping)
        {
            isSliding = true;
            playerObject.GetComponent<Animator>().Play("Standing Dive Forward"); // Play the sliding animation
            StartCoroutine(SlideSequence()); // Start the Slide Coroutine
        }

        // Handle sliding and returning to normal position
        if (isSliding)
        {
            if (!comingDown)
            {
                transform.Translate(Vector3.down * Time.deltaTime * 2, Space.World); // Move slightly downward
            }
            else
            {
                transform.Translate(Vector3.up * Time.deltaTime * 2, Space.World); // Move back up
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

    // Slide sequence with delays for starting and ending the slide
    IEnumerator SlideSequence()
    {
        // Slide downward for a brief moment
        comingDown = true; // Start moving down
        yield return new WaitForSeconds(0.25f); // Time to stay low

        comingDown = false; // Start moving back up
        yield return new WaitForSeconds(0.25f); // Time to return to original position

        isSliding = false; // End sliding state

        // Play the "Fast Run" animation after sliding
        playerObject.GetComponent<Animator>().Play("Fast Run");
    }
}
