using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Net;
public class UDPSocket : MonoBehaviour {


    public delegate void UDPSocketDelegate(byte [] proBuf,int deCount,string tmpIp,ushort   tmpPort);

    UDPSocketDelegate uDPSocketDelegate;

    IPEndPoint udpip;
    Socket udpSocket;
    byte[] recvData;
    Thread recvThread;
    public bool BindSocket(ushort port,int bufferLength,UDPSocketDelegate tmpDelegate)
    {
        udpip = new IPEndPoint(IPAddress .Any ,port );
        UDPConnect();
        uDPSocketDelegate = tmpDelegate;
        recvData = new byte[bufferLength ];
        recvThread = new Thread(RecvDataThread);
        recvThread.Start();
        return true;
    }

    public void UDPConnect()
    {
       
        udpSocket = new Socket(AddressFamily .InterNetwork ,SocketType .Dgram ,ProtocolType .Udp );
        //带有服务器功能的
        udpSocket.Bind(udpip );
    }
    bool isRunning = true;

  public void RecvDataThread()
    {
        while (isRunning )
        {
            if (udpSocket ==null ||udpSocket .Available <1)
            {
                Thread.Sleep(100);
                continue;
            }


            lock (this)
            {
                IPEndPoint sender = new IPEndPoint(IPAddress .Any ,0);
                EndPoint remote = (EndPoint)sender;
                int myCount = udpSocket.ReceiveFrom(recvData ,ref remote );
                if (uDPSocketDelegate !=null )
                {
                    uDPSocketDelegate(recvData ,myCount ,remote.AddressFamily .ToString (),(ushort )sender .Port );
                }

            }
        }
    }
    public int SendData(string ip,byte []data,ushort udport)
    {
        IPEndPoint sendIp = new IPEndPoint(IPAddress .Parse (ip),udport );
        if (!udpSocket .Connected )
        {
            UDPConnect();
        }

        int mySendCount= udpSocket.SendTo(data,data .Length ,SocketFlags .None ,sendIp );

        return mySendCount;//实际发送的字节长度
    }
}
