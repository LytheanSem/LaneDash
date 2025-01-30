using System.Collections;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 6f;
    public float laneSwitchSpeed = 10f;
    private int currentLane = 1;
    private float[] lanes = { -5f, 0f, 5f };
    public bool isJumping = false;
    public bool isSliding = false;
    public Animator animator;

    private Vector3 originalScale;

    // UDP Receiver
    private UdpClient udpClient;
    private Thread receiveThread;
    private string receivedMessage = "";

    void Start()
    {
        originalScale = transform.localScale;

        // Start UDP Receiver
        udpClient = new UdpClient(65432);
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    void Update()
    {
        // Continuous forward movement
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Process received message for movement
        ProcessUDPInput();
        ProcessKeyboardInput();
    }

    void ProcessKeyboardInput()
    {
        // Lane Switching
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentLane > 0)
            currentLane--;
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentLane < lanes.Length - 1)
            currentLane++;

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

        // Slide (Bend Down)
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !isSliding && !isJumping)
        {
            isSliding = true;
            animator.Play("Standing Dive Forward");
            StartCoroutine(SlideSequence());
        }
    }

    void ProcessUDPInput()
    {
        string message;
        lock (receivedMessage)
        {
            message = receivedMessage;
        }

        if (!string.IsNullOrEmpty(message))
        {
            string[] data = message.Split(',');

            if (data.Length == 2)
            {
                string position = data[0].Trim();
                string action = data[1].Trim();

                // Lane Switching
                if (position == "Left" && currentLane > 0)
                    currentLane--;
                else if (position == "Right" && currentLane < lanes.Length - 1)
                    currentLane++;
                else if (position == "Center") // Move back to the middle lane
                    currentLane = 1;

                float targetXPosition = lanes[currentLane];
                float newX = Mathf.Lerp(transform.position.x, targetXPosition, Time.deltaTime * laneSwitchSpeed);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);

                // Jump
                if (action == "Jump" && !isJumping && !isSliding)
                {
                    isJumping = true;
                    animator.Play("Jump");
                    StartCoroutine(JumpSequence());
                }

                // Slide (Bend Down)
                if (action == "Bend Down" && !isSliding && !isJumping)
                {
                    isSliding = true;
                    animator.Play("Standing Dive Forward");
                    StartCoroutine(SlideSequence());
                }
            }
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
        transform.localScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
        yield return new WaitForSeconds(0.5f);
        transform.localScale = originalScale;
        isSliding = false;
        animator.Play("Fast Run");
    }

    void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 65432);
        while (true)
        {
            try
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data);
                lock (receivedMessage)
                {
                    receivedMessage = message;
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("UDP Receive Error: " + e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        receiveThread.Abort();
        udpClient.Close();
    }
}
