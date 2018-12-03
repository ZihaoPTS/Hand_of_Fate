using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

/*public class handSpreading
{
    public float Finger1;//
    public float Finger2;//
    public float Finger3;//
    public float Finger4;//
    public float Finger5;//
}

public class handBending
{
    public float Finger1;
    public float[] Finger2;
    public float[] Finger3;
    public float[] Finger4;
    public float Finger5;
}*/

public class fingerMaster_tcp : MonoBehaviour {

    private TcpListener tcpListener_spread;
    private TcpListener tcpListener_bend;
    private TcpClient connectedTcpClient_spread;
    private TcpClient connectedTcpClient_bend;

    bool newSpreadingMessage;
    bool newBendingMessage;
    handSpreading myHandSpreading;
    handBending myHandBending;

    private Thread listenerThreadSpread;
    private Thread listenerThreadBend;

    // Use this for initialization
    void Start () {
        listenerThreadSpread = new Thread(new ThreadStart(ListenForIncommingRequests));
        listenerThreadSpread.IsBackground = true;
        listenerThreadSpread.Start();
        listenerThreadBend = new Thread(new ThreadStart(ListenForIncommingRequests2));
        listenerThreadBend.IsBackground = true;
        listenerThreadBend.Start();
        newSpreadingMessage = false;
        newBendingMessage = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (newSpreadingMessage)
        {
            BroadcastMessage("Finger1Spread", myHandSpreading.Finger1);
            BroadcastMessage("Finger2Spread", myHandSpreading.Finger2);
            BroadcastMessage("Finger3Spread", myHandSpreading.Finger3);
            BroadcastMessage("Finger4Spread", myHandSpreading.Finger4);
            BroadcastMessage("Finger5Spread", myHandSpreading.Finger5);
            newSpreadingMessage = false;
        }
        if (newBendingMessage)
        {
            BroadcastMessage("Finger1Bend", myHandBending.Finger1);
            BroadcastMessage("Finger21Bend", myHandBending.Finger2[0]);
            BroadcastMessage("Finger22Bend", myHandBending.Finger2[1]);
            BroadcastMessage("Finger31Bend", myHandBending.Finger3[0]);
            BroadcastMessage("Finger32Bend", myHandBending.Finger3[1]);
            BroadcastMessage("Finger41Bend", myHandBending.Finger4[0]);
            BroadcastMessage("Finger42Bend", myHandBending.Finger4[1]);
            BroadcastMessage("Finger5Bend", myHandBending.Finger5);
            newBendingMessage = false;
        }
    }

    private void ListenForIncommingRequests()
    {
        try
        {
            // Create listener on localhost port 8052. 			
            tcpListener_spread = new TcpListener(IPAddress.Parse("0.0.0.0"), 8052);
            tcpListener_spread.Start();
            Debug.Log("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient_spread = tcpListener_spread.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient_spread.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0 && newSpreadingMessage == false)
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
                            newSpreadingMessage = true;
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
            tcpListener_bend = new TcpListener(IPAddress.Parse("0.0.0.0"), 8053);
            tcpListener_bend.Start();
            Debug.Log("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient_bend = tcpListener_bend.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient_bend.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0 && newBendingMessage == false)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = Encoding.ASCII.GetString(incommingData);
                            Debug.Log("client message received as: " + clientMessage);
                            try
                            {
                                myHandBending = JsonUtility.FromJson<handBending>(clientMessage);
                            }
                            catch (Exception e)
                            {
                                Debug.Log(e);
                            }
                            Debug.Log("Finger spreadings: ");
                            Debug.Log(myHandSpreading.Finger1);
                            Debug.Log(myHandSpreading.Finger2);
                            Debug.Log(myHandSpreading.Finger3);
                            newBendingMessage = true;
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
