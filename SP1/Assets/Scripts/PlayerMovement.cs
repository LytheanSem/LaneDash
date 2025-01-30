using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 6f;
    public float laneSwitchSpeed = 10f;
    private int currentLane = 1;
    private float[] lanes = { -5f, 0f, 5f };
    public bool isJumping = false;
    public bool comingDown = false;
    public bool isSliding = false;
    public Animator animator;
 
    private Vector3 originalScale;
 
    void Start()
    {
        originalScale = transform.localScale; // Store original player scale
    }
 
    void Update()
    {
        // Continuous forward movement
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);
 
        // Lane Switching
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane > 0)
        {
            currentLane--;
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane < lanes.Length - 1)
        {
            currentLane++;
        }
 
        // Smoothly move to target lane
        float targetXPosition = lanes[currentLane];
        float newX = Mathf.Lerp(transform.position.x, targetXPosition, Time.deltaTime * laneSwitchSpeed);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
 
        // Jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !isJumping && !isSliding)
        {
            isJumping = true;
            animator.Play("Jump");
            StartCoroutine(JumpSequence());
        }
 
        // Slide
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.C)) && !isSliding && !isJumping)
        {
            isSliding = true;
            animator.Play("Standing Dive Forward");
            StartCoroutine(SlideSequence());
        }
    }
 
    IEnumerator JumpSequence()
    {
        float jumpHeight = 2f;
        float jumpSpeed = 4f;
 
        float startY = transform.position.y;
        while (transform.position.y < startY + jumpHeight)
        {
            transform.Translate(Vector3.up * Time.deltaTime * jumpSpeed, Space.World);
            yield return null;
        }
 
        while (transform.position.y > startY)
        {
            transform.Translate(Vector3.down * Time.deltaTime * jumpSpeed, Space.World);
            yield return null;
        }
 
        isJumping = false;
        animator.Play("Fast Run");
    }
 
    IEnumerator SlideSequence()
    {
        // Shrink player while sliding
        transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
 
        yield return new WaitForSeconds(0.5f); // Slide duration
 
        // Reset player scale after sliding
        transform.localScale = originalScale;
 
        isSliding = false;
        animator.Play("Fast Run");
    }
}