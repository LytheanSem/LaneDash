using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 2.5f; // Distance between lanes
    private Rigidbody rb;
    private Vector3 targetPosition; // Keeps track of the player's target position
    private bool isCrouching = false;

    private float leftLaneX = -2.5f;
    private float centerLaneX = 0f;
    private float rightLaneX = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position; // Set initial position to current position
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

        // Check if the player is grounded and prevent them from moving up due to gravity
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset Y velocity if grounded
        }
    }

    public void MoveLeft()
    {
        // Only move left if not in the left lane
        if (transform.position.x > leftLaneX)
        {
            targetPosition.x = Mathf.Lerp(transform.position.x, leftLaneX, 0.5f); // Move smoothly to the left lane
            transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
        }
    }

    public void MoveRight()
    {
        // Only move right if not in the right lane
        if (transform.position.x < rightLaneX)
        {
            targetPosition.x = Mathf.Lerp(transform.position.x, rightLaneX, 0.5f); // Move smoothly to the right lane
            transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
        }
    }

    public void ResetToCenter()
    {
        // Reset to center lane
        targetPosition.x = centerLaneX;
        transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z); // Keep Y fixed
    }

    public void Jump()
    {
        if (rb.velocity.y == 0) // Only jump when on the ground
        {
            rb.velocity = new Vector3(rb.velocity.x, 5f, rb.velocity.z); // Apply vertical velocity
        }
    }

    public void BendDown()
    {
        if (!isCrouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
            isCrouching = true;
        }
    }

    public void StandUp()
    {
        if (isCrouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
            isCrouching = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset velocity when grounded
        }
    }
}
