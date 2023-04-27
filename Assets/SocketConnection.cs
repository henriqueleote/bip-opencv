using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;



public class SocketConnection : MonoBehaviour
{
    Thread receiveThread;
    public int port = 5052;
    private bool startReceiving = false;
    public bool printToConsole = false;
    public string ReceivedData;
    public string lastReceivedData;
    public int positionX;

    Socket socket;
    UdpClient client;

    public string action;
    
    // Start is called before the first frame update
    public void Start()
    {
        action = "";
        startReceiving = false;
        StartConnection();
    }



    private void StartConnection()
    {
        client = new UdpClient(port);

        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        startReceiving = true;

        Debug.Log("Connected");
    }


    private void ReceiveData()
    {
        while (startReceiving)
        {
            try
            {
                byte[] dataByte = new byte[1024];

                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);

                dataByte = client.Receive(ref anyIP);
                ReceivedData = Encoding.UTF8.GetString(dataByte);


                if (printToConsole)
                {
                    //Debug.Log(ReceivedData);
                }

                if (!string.IsNullOrEmpty(ReceivedData))
                {
                    if (!ReceivedData.Equals("jump"))
                    {
                        string correctPosition = ReceivedData.Substring(1, ReceivedData.Length - 2);
                        positionX = int.Parse(correctPosition.Substring(0, 3));
                        Debug.Log(correctPosition + "..." + positionX);
                    }
                    else
                        action = "jump";
                }                  
                

            }
            catch (Exception err)
            {
                Debug.Log(err);
            }
        }



    }


    private void OnDestroy()
    {
        startReceiving = false;
        receiveThread.Abort();
    }
}