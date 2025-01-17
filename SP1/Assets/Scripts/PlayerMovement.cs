using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 6;
    public float laneSwitchSpeed = 10f; // Speed for switching lanes
    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private float[] lanes = { -5f, 0f, 5f }; // Lane positions on x-axis

    // Update is called once per frame
    void Update()
    {
        // Continuous running
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Move left (A, Left Arrow)
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane > 0)
        {
            currentLane--;
        }

        // Move right (D, Right Arrow)
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane < lanes.Length - 1)
        {
            currentLane++;
        }

        // Smoothly transition to the target lane
        Vector3 targetPosition = new Vector3(lanes[currentLane], transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * laneSwitchSpeed);
    }
}
