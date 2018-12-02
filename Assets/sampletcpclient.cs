using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// inspired by https://gist.github.com/danielbierwirth/0636650b005834204cb19ef5ae6ccedb



public class sampletcpclient : MonoBehaviour
{
    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    private Thread clientReceiveThread2;
    #endregion
    // Use this for initialization 	
    void Start()
    {
        ConnectToTcpServer();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessage();
        }
    }
    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(EstablishConnection));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
            clientReceiveThread2 = new Thread(new ThreadStart(EstablishConnection2));
            clientReceiveThread2.IsBackground = true;
            clientReceiveThread2.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void EstablishConnection()
    {
        try
        {
            socketConnection = new TcpClient("localhost", 8052);
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    private void EstablishConnection2()
    {
        try
        {
            socketConnection = new TcpClient("localhost", 8053);
            Debug.Log("connection 2 established");
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    private void SendMessage()
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {

                string clientMessage = @"{""Finger1"": -1 , ""Finger2"": 1 , ""Finger3"": 1 , ""Finger4"": 1 , ""Finger5"": 1}";
                //string clientMessage = @"{""Finger1"": 1}";

                //string clientMessage = "This is a message from one of your clients.";
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}