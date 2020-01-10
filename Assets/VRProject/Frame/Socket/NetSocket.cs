using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public enum ErrorSocket
{
    Sucess = 0,
    TimeOut,
    SocketNUll,
    SocketUnConnect,

    ConnectSucess,
    ConnectUnsucessUnKnow,
    ConnectError,
    SendSucess,
    SendUnSucessUnKnow,
    RecvUnsucessUnknow,
    DisConnectSucess,
    DisConnectUnknow,
}
public class NetSocket
{

    public delegate void CallBackNormal(bool sucess, ErrorSocket error, string exception);

    //  public delegate void CallBackSend(bool sucess, ErrorSocket error, string exception);
    public delegate void CallBackRecv(bool sucess, ErrorSocket error, string exception, byte[] byteMessage, string stirMsg);
    // public delegate void CallBackDisConnect(bool sucess, ErrorSocket error, string exception);


    private CallBackNormal callBackConnect;
    private CallBackNormal callBackSend;
    private CallBackNormal callBackDisConnect;
    private CallBackRecv callBackRecv;
  

    private ErrorSocket errorSocket;
    private Socket clientSocket;
    private string addressIP;
    private ushort port;
    SocketBuffer recvBuffer;
    public NetSocket()
    {
        recvBuffer = new SocketBuffer(6, RecvMsgOver);

        recvBytes = new byte[1024];


    }

    public bool IsConnect()
    {
        if (clientSocket !=null &&clientSocket .Connected )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AsynConnect(string ip, ushort port, CallBackNormal connectBack, CallBackRecv tmpRecvback)
    {
        errorSocket = ErrorSocket.Sucess;
        this.callBackConnect = connectBack;
        this.callBackRecv = tmpRecvback;
        if (clientSocket != null && clientSocket.Connected)
        {
            this.callBackConnect(false, ErrorSocket.ConnectError, "Connect repat");
        }
        else if (clientSocket == null || !clientSocket.Connected)
        {
            
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse(ip);

            IPEndPoint endPoint = new IPEndPoint(iPAddress, port);

            IAsyncResult connect = clientSocket.BeginConnect(endPoint, ConnectCallBack, clientSocket);
            //超时检测
            if (!WriteDot(connect))
            {
                this.callBackConnect(false, errorSocket, "连接超时");
            }
        }


    }
    private void ConnectCallBack(IAsyncResult asyconnect)
    {

        try
        {
            clientSocket.EndConnect(asyconnect);
            if (clientSocket.Connected == false)
            {
                errorSocket = ErrorSocket.ConnectUnsucessUnKnow;
                this.callBackConnect(false, errorSocket, "连接超时");
                return;
            }
            else
            {
                //接收消息

                errorSocket = ErrorSocket.ConnectSucess;
                this.callBackConnect(true, errorSocket, "sucess");

            }
        }
        catch (Exception ex)
        {
            this.callBackConnect(false, errorSocket, ex.ToString());
        }
    }
    #region //接收数据
    byte[] recvBytes;
    public void Receive()
    {

        if (clientSocket !=null &&clientSocket .Connected )
        {
         IAsyncResult asyrecv=   clientSocket.BeginReceive(recvBytes ,0,recvBytes .Length ,SocketFlags .None ,ReciveCallBack,clientSocket );
       
        
            if (!WriteDot(asyrecv))
            {
                callBackRecv (false, ErrorSocket.RecvUnsucessUnknow, "receive failed",null ,"");
            }
        }
      
    }

    private void ReciveCallBack(IAsyncResult ar)
    {


        try
        {
            clientSocket.EndReceive(ar );
          
            if (!clientSocket .Connected )
            {
                callBackRecv(false ,ErrorSocket .RecvUnsucessUnknow ,"connect false",null,"");

                return;
            }
            int length = clientSocket.EndReceive(ar);//阻塞

            if (length ==0)
            {
                return;
            }
            recvBuffer.RecvByte(recvBytes ,length );
        }
        catch (Exception ex)
        {
            callBackRecv(false, ErrorSocket.RecvUnsucessUnknow,ex.ToString (), null, "");
        }
        Receive();
    }
    #endregion
    #region //接收完成后的回调
    public void RecvMsgOver(byte[] allByte)
    {

        callBackRecv(true, ErrorSocket.Sucess, "", null, "recv back sucess");
    }
    #endregion
    #region //发送数据
    public void SendCallBack(IAsyncResult ar)
    {
        try
        {
            int byteSend = clientSocket.EndSend(ar );

            if (byteSend >0)
            {
                callBackSend(true, ErrorSocket.SendSucess , "");
            }
            else
            {
                callBackSend(false, ErrorSocket.SendUnSucessUnKnow , "");
            }
        }
        catch (Exception ex)
        {
            callBackSend(false, ErrorSocket.SendUnSucessUnKnow, "");
        }
    }
    public void AsynSend(byte [] sendBuffer,CallBackNormal tmpSendBack)
    {


        errorSocket = ErrorSocket.Sucess;
        this.callBackSend = tmpSendBack;
        if (clientSocket ==null )
        {
            this.callBackSend(false ,ErrorSocket .SocketNUll ,"");
        }
        else if (!clientSocket .Connected )
        {
            callBackSend(false, ErrorSocket.SocketUnConnect , "");
        }
        else
        {
       IAsyncResult asySend=     clientSocket.BeginSend(sendBuffer ,0,sendBuffer .Length ,SocketFlags .None ,SendCallBack ,clientSocket );

            if (!WriteDot (asySend ))
            {
                callBackSend(false, ErrorSocket.SendUnSucessUnKnow , "send failed");
            }

        }
    }
    #endregion

    #region 断开连接

    public void DisconnectCallBack(IAsyncResult ar)
    {

        try
        {

            clientSocket.EndDisconnect(ar);


            clientSocket.Close();
            clientSocket = null;
            this.callBackDisConnect(true , ErrorSocket.DisConnectSucess, "");


        }
        catch (Exception ex)
        {
            this.callBackDisConnect(false, ErrorSocket.DisConnectUnknow , ex.ToString ());
        }
    }
    public void AsynDisconnect(CallBackNormal tmpDisconnect)
    {

        try
        {
            errorSocket = ErrorSocket.Sucess;
            this.callBackDisConnect = tmpDisconnect;
            if (clientSocket == null)
            {
                this.callBackDisConnect (false, ErrorSocket.DisConnectUnknow , "client is null");
            }
            else if (!clientSocket.Connected)
            {
                this.callBackDisConnect(false, ErrorSocket.DisConnectUnknow, "client is Unconnect");
            }
            else
            {
                 IAsyncResult asyDisconnect  =  clientSocket.BeginDisconnect(false ,DisconnectCallBack,clientSocket );

                if (!WriteDot(asyDisconnect))
                {
                    callBackSend(false, ErrorSocket.DisConnectUnknow , "disconnect failed");
                }
            }
        }
        catch (Exception ex)
        {
            callBackSend(false, ErrorSocket.DisConnectUnknow, "disconnect failed");
        }
    }
    #endregion
    #region //Timeout check
    //true 表示可以写入读取， false 表示超时
    bool WriteDot(IAsyncResult ar)
    {


        int i = 0;

        while (ar.IsCompleted == false)
        {
            i++;
            if (i > 20)
            {
                errorSocket = ErrorSocket.TimeOut;
                return false;
            }
            Thread.Sleep(100);
        }
        return true;
    }
#endregion 
}
