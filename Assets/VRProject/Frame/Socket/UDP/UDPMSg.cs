using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPMSg : MsgBase {

    public ushort port;
    public int recvBufferLength;
    public UDPSocket.UDPSocketDelegate recvDelegate;
    public UDPMSg   (ushort masgId,ushort port,int recvLength ,UDPSocket .UDPSocketDelegate  tmpDelegate)
    {
        this.msgid = masgId;
        this.port = port;
        this.recvBufferLength = recvLength;
        this.recvDelegate = tmpDelegate;

    }
}
