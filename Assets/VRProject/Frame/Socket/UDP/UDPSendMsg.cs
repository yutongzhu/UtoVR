using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPSendMsg : MsgBase  {

    public string ip;
    public byte[] data;
    public ushort port;
    public UDPSendMsg (string ip ,byte [] datas,ushort port)
    {
        this.ip = ip;
        this.data = datas;
        this.port = port;
    }
}
