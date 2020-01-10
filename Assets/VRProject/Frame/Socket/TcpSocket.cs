using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TCPEvent
{

    TcpConnect = ManagerID.NetManager + 1,
    TcpSendMsg,
    MaxValue
}
//连接信息类
public class TCPConnectMsg:MsgBase 
{
    public string ip;
    public ushort port;
    public TCPConnectMsg (ushort id,string tmpip,ushort tmpport)
    {

        this.msgid = id;
        this.ip = tmpip;
        this.port = tmpport;
    }
}
//发送信息
public class TCPMsg : MsgBase
{
    public  NetMsgBase  netMsg ;
   
    public TCPMsg(ushort tmpid, NetMsgBase tmpbase)
    {

        this.msgid = tmpid ;
        this.netMsg  = tmpbase ;
       
    }
}
public class TcpSocket :NetBase  {

    public override void ProcessEvent(MsgBase tmpMsg)
    {
        switch (tmpMsg.msgid )
        {
            case(ushort )TCPEvent .TcpConnect:
                {

                    TCPConnectMsg connectMsg = (TCPConnectMsg)tmpMsg;
                    socket = new NetWorkToSever(connectMsg .ip ,connectMsg .port );
                }
                break;
            case (ushort)TCPEvent.TcpSendMsg :

                {
                    TCPMsg sendMsg = (TCPMsg)tmpMsg;
                    socket.PutSendMsgToPool(sendMsg .netMsg);
                }
                break;
        }

    }
    NetWorkToSever socket=null ;
	private void Awake()
	{
        msgids = new ushort[]
    {
            (ushort )TCPEvent .TcpConnect ,

            (ushort )TCPEvent .TcpSendMsg  
    };
        RegistSelf(this ,msgids );
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (socket !=null )
        {
            socket.Update();
        }
	}
}
