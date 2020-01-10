using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class NetWorkToSever
{
    private Queue<NetMsgBase> recvMsgPool = null;
    private Queue<NetMsgBase> sendMsgPool = null;
    NetSocket clientSocket;
    Thread sendthread;
    public NetWorkToSever(string ip, ushort port)
    {

        recvMsgPool = new Queue<NetMsgBase>();
        sendMsgPool = new Queue<NetMsgBase>();
        clientSocket = new NetSocket();
        clientSocket.AsynConnect(ip, port, AsysnConnectCallBack, AsysnRecvCallBack);

    }
    void AsysnConnectCallBack(bool sucess, ErrorSocket error, string exception)
    {
        
        if (sucess)
        {
            sendthread = new Thread(LoopSendMsg);
            sendthread.Start();
        }

    }

    #region 接收数据
    void AsysnRecvCallBack(bool sucess, ErrorSocket error, string exception, byte[] byteMessage, string strMsg)
    {
        if (sucess)
        {
            NetMsgBase tmp = new NetMsgBase(byteMessage);
        }
        else
        {
            //处理错误信息
        }
    }
    #endregion
    #region Send
    public void PutSendMsgToPool(NetMsgBase msg)
    {
        lock (sendMsgPool)
        {
            sendMsgPool.Enqueue(msg);
        }
    }
public  void Update()
    {

        if (recvMsgPool != null)
        {
            while (recvMsgPool.Count > 0)
            {
                NetMsgBase tmp = recvMsgPool.Dequeue();
                MsgCenter.instance.SendToMsg(tmp);
            }
        }
    }
    void CallBackSend(bool sucess, ErrorSocket error, string exception)
    {

    }
    void LoopSendMsg()
    {
        while (clientSocket != null && clientSocket.IsConnect())
        {
            lock (sendMsgPool)
            {
                while (sendMsgPool.Count > 0)
                {
                    NetMsgBase tmpBody = sendMsgPool.Dequeue();
                    clientSocket.AsynSend(tmpBody.getNetBytes(), CallBackSend);
                }
            }
            Thread.Sleep(100);

        }
    }
    #endregion
    #region //Discconnect

    void CallBackDisconnect(bool sucess, ErrorSocket error, string exception)
    {
        if (sucess )
        {
            sendthread.Abort();
        }
    }
    public void DisConnect()
    {
        if (clientSocket != null && clientSocket .IsConnect())
      {

            clientSocket.AsynDisconnect(CallBackDisconnect);
        }
       // clientSocket.AsynDisconnect(CallBackDisconnect );
    }
    #endregion

}
