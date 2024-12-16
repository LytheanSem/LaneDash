using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SocketListener : MonoBehaviour
{
    private UdpClient udpClient;
    public int listenPort = 65432;
    public PlayerMovement playerMovement;

    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;
    private bool crouch = false;
    private bool center = false;

    void Start()
    {
        try
        {
            udpClient = new UdpClient(listenPort);
            udpClient.BeginReceive(OnReceive, null);
            Debug.Log($"UDP Listener started on port {listenPort}");
        }
        catch (Exception e)
        {
            Debug.LogError("UDP initialization error: " + e.Message);
        }
    }

    void Update()
    {
        // Trigger player actions based on the received signals
        if (moveLeft)
        {
            playerMovement.MoveLeft();
            moveLeft = false;
        }
        if (moveRight)
        {
            playerMovement.MoveRight();
            moveRight = false;
        }
        if (jump)
        {
            playerMovement.Jump();
            jump = false;
        }
        if (crouch)
        {
            playerMovement.Crouch();
        }
        else
        {
            playerMovement.StandUp();
        }
        if (center)
        {
            playerMovement.MoveToCenter();
            center = false;
        }
    }

    private void OnReceive(IAsyncResult result)
    {
        if (udpClient == null) return; // Exit if the socket is already closed

        try
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
            byte[] data = udpClient.EndReceive(result, ref ipEndPoint);
            string message = Encoding.ASCII.GetString(data);
            ProcessMessage(message);
        }
        catch (ObjectDisposedException)
        {
            Debug.LogWarning("UDP socket closed; stopping receive.");
        }
        catch (Exception e)
        {
            Debug.LogError("Receive error: " + e.Message);
        }
        finally
        {
            if (udpClient != null)
            {
                try
                {
                    udpClient.BeginReceive(OnReceive, null); // Safely restart receiving
                }
                catch (ObjectDisposedException)
                {
                    Debug.LogWarning("Attempted to restart receive on a closed socket.");
                }
            }
        }
    }

    private void ProcessMessage(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            Debug.LogWarning("Received an empty UDP message.");
            return;
        }

        string[] parts = message.Split(',');

        if (parts.Length == 0)
        {
            Debug.LogWarning("Invalid UDP message received: " + message);
            return;
        }

        string position = parts[0].Trim();
        string action = parts.Length > 1 ? parts[1].Trim() : "";

        // Set the appropriate actions based on the received message
        moveLeft = position.Equals("Left", StringComparison.OrdinalIgnoreCase);
        moveRight = position.Equals("Right", StringComparison.OrdinalIgnoreCase);
        center = position.Equals("Center", StringComparison.OrdinalIgnoreCase);
        jump = action.Equals("Jump", StringComparison.OrdinalIgnoreCase);
        crouch = action.Equals("Crouch", StringComparison.OrdinalIgnoreCase);
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    private void OnDestroy()
    {
        CloseSocket();
    }

    private void CloseSocket()
    {
        if (udpClient != null)
        {
            try
            {
                udpClient.Close();
                udpClient = null;
                Debug.Log("UDP socket closed successfully.");
            }
            catch (Exception e)
            {
                Debug.LogError("Error closing UDP socket: " + e.Message);
            }
        }
    }
}
