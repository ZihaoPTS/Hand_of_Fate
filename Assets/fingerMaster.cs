using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class handSpreading
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
}

public class fingerMaster : MonoBehaviour
{

    // Use this for initialization\



    Socket socket_spread;
    Socket socket_bend;
    EndPoint Remote;
    bool newSpreadingMessage;
    bool newBendingMessage;

    handSpreading myHandSpreading;
    handBending myHandBending;

    private Thread listenerThreadSpread;
    private Thread listenerThreadBend;

    void Start()
    {
        IPEndPoint ip = new IPEndPoint(IPAddress.Any, 5555);
        socket_spread = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket_spread.Bind(ip);

        ip = new IPEndPoint(IPAddress.Any, 5556);
        socket_bend = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket_bend.Bind(ip);

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        Remote = (EndPoint)(sender);

        newSpreadingMessage = false;
        newBendingMessage = false;

        listenerThreadSpread = new Thread(new ThreadStart(ListenForIncommingSpreadingRequests));
        listenerThreadSpread.IsBackground = true;
        listenerThreadSpread.Start();
        listenerThreadBend = new Thread(new ThreadStart(ListenForIncommingBendingRequests));
        listenerThreadBend.IsBackground = true;
        listenerThreadBend.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (newSpreadingMessage)
        {
            /*transform.position += (transform.rotation * Pivot);
            transform.rotation *= Quaternion.AngleAxis(rotSpeed_x * 1, Vector3.right);
            //transform.rotation *= Quaternion.AngleAxis(-1 * rotSpeed * 1, Vector3.forward);
            transform.position -= (transform.rotation * Pivot);*/

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

    //receivedDataLength = socket.ReceiveFrom(data, ref Remote)

    private void ListenForIncommingSpreadingRequests()
    {
        try
        {
            //Debug.Log("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                int length;
                // Read incomming stream into byte arrary. 						
                while ((length = socket_spread.ReceiveFrom(bytes, ref Remote)) != 0 && newSpreadingMessage == false)
                {
                    var incommingData = new byte[length];
                    Array.Copy(bytes, 0, incommingData, 0, length);
                    // Convert byte array to string message. 							
                    string clientMessage = Encoding.ASCII.GetString(incommingData);
                    //Debug.Log("client message received as: " + clientMessage);
                    try
                    {
                        myHandSpreading = JsonUtility.FromJson<handSpreading>(clientMessage);
                        newSpreadingMessage = true;
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }

                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

    private void ListenForIncommingBendingRequests()
    {
        try
        {
            Debug.Log("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                int length;
                // Read incomming stream into byte arrary. 						
                while ((length = socket_bend.ReceiveFrom(bytes, ref Remote)) != 0 && newBendingMessage == false)
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
                    Debug.Log(myHandBending.Finger1);
                    Debug.Log(myHandBending.Finger2[0]);
                    Debug.Log(myHandBending.Finger3[0]);
                    newBendingMessage = true;
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

}
