using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

//inspired by https://gist.github.com/danielbierwirth/0636650b005834204cb19ef5ae6ccedb
//and more by http://www.java2s.com/Tutorial/CSharp/0580__Network/SimpleUdpserver.htm

/*public class handSpreading
{
    public float Finger1;
    public float Finger2;
    public float Finger3;
    public float Finger4;
    public float Finger5;
}*/

public class tcp_root_finger : MonoBehaviour
{
    #region private members
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private Thread tcpListenerThread2;
    private TcpClient connectedTcpClient;
    #endregion

    public Vector3 Pivot = Vector3.down;
    public float rotSpeed_x = 1;
    public bool newMessage;
    public handSpreading myHandSpreading;

    // Use this for initialization
    void Start()
    {
        // Start TcpServer background thread 		
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
        tcpListenerThread2 = new Thread(new ThreadStart(ListenForIncommingRequests2));
        tcpListenerThread2.IsBackground = true;
        tcpListenerThread2.Start();
        newMessage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (newMessage)
        {
            /*transform.position += (transform.rotation * Pivot);
            transform.rotation *= Quaternion.AngleAxis(rotSpeed_x * 1, Vector3.right);
            //transform.rotation *= Quaternion.AngleAxis(-1 * rotSpeed * 1, Vector3.forward);
            transform.position -= (transform.rotation * Pivot);*/

            if (myHandSpreading.Finger1 != 0)
            {
                BroadcastMessage("Finger1Spread", myHandSpreading.Finger1);
            }
            if (myHandSpreading.Finger2 != 0)
            {
                BroadcastMessage("Finger2Spread", myHandSpreading.Finger2);
            }
            if (myHandSpreading.Finger3 != 0)
            {
                BroadcastMessage("Finger3Spread", myHandSpreading.Finger3);
            }
            if (myHandSpreading.Finger4 != 0)
            {
                BroadcastMessage("Finger4Spread", myHandSpreading.Finger4);
            }
            if (myHandSpreading.Finger5 != 0)
            {
                BroadcastMessage("Finger5Spread", myHandSpreading.Finger5);
            }



            newMessage = false;
            
            //Movie m = JsonUtility.FromJson<Movie>(json);
            
        }
    }
    
    /// <summary> 	
    /// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
    /// </summary> 	
    private void ListenForIncommingRequests()
    {
        try
        {
            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(IPAddress.Parse("0.0.0.0"), 8052);
            tcpListener.Start();
            Debug.Log("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0 && newMessage == false)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = Encoding.ASCII.GetString(incommingData);
                            Debug.Log("client message received as: " + clientMessage);
                            try { 
                            myHandSpreading = JsonUtility.FromJson<handSpreading>(clientMessage);
                            }
                            catch(Exception e)
                            {
                                Debug.Log(e);
                            }
                            Debug.Log("Finger spreadings: ");
                            Debug.Log(myHandSpreading.Finger1);
                            Debug.Log(myHandSpreading.Finger2);
                            Debug.Log(myHandSpreading.Finger3);
                            newMessage = true;
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }
    private void ListenForIncommingRequests2()
    {
        try
        {
            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(IPAddress.Parse("0.0.0.0"), 8053);
            tcpListener.Start();
            Debug.Log("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0 && newMessage == false)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = Encoding.ASCII.GetString(incommingData);
                            Debug.Log("client message received as: " + clientMessage);
                            try
                            {
                                myHandSpreading = JsonUtility.FromJson<handSpreading>(clientMessage);
                            }
                            catch (Exception e)
                            {
                                Debug.Log(e);
                            }
                            Debug.Log("Finger spreadings: ");
                            Debug.Log(myHandSpreading.Finger1);
                            Debug.Log(myHandSpreading.Finger2);
                            Debug.Log(myHandSpreading.Finger3);
                            newMessage = true;
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }
}