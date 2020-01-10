using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NetMsgBase : MsgBase
{

    public byte[] buffer;
    public NetMsgBase(byte[] arr)
    {

        buffer = arr;
        this.msgid = BitConverter.ToUInt16(arr, 4);//前四个字节是数据长度，后面两个是usort id 

    }
    public byte [] getNetBytes()
    {
        return buffer;
    }
}
