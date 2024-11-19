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
    private bool bendDown = false;
    private bool center = false;

    void Start()
    {
        try
        {
            udpClient = new UdpClient(listenPort);
            udpClient.BeginReceive(OnReceive, null);
        }
        catch (Exception e)
        {
            Debug.LogError("UDP initialization error: " + e.Message);
        }
    }

    void Update()
    {
        // Trigger player actions based on the received signals
        if (moveLeft) { playerMovement.MoveLeft(); moveLeft = false; }
        if (moveRight) { playerMovement.MoveRight(); moveRight = false; }
        if (jump) { playerMovement.Jump(); jump = false; }
        if (bendDown) { playerMovement.BendDown(); bendDown = false; }
        if (center) { playerMovement.ResetToCenter(); center = false; }
    }

    private void OnReceive(IAsyncResult result)
    {
        try
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, listenPort);
            byte[] data = udpClient.EndReceive(result, ref ipEndPoint);
            string message = Encoding.ASCII.GetString(data);
            string[] parts = message.Split(',');

            string position = parts[0].Trim();
            string action = parts.Length > 1 ? parts[1].Trim() : "";

            // Set the appropriate actions based on the received message
            moveLeft = position == "Left";
            moveRight = position == "Right";
            center = position == "Center";
            jump = action == "Jump";
            bendDown = action == "Bend Down";
        }
        catch (Exception e)
        {
            Debug.LogError("Receive error: " + e.Message);
        }
        finally
        {
            if (udpClient != null) udpClient.BeginReceive(OnReceive, null);
        }
    }

    private void OnApplicationQuit()
    {
        udpClient?.Close();
    }
}
