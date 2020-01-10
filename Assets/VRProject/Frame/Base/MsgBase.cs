using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgBase  {

    //表示65535个消息
    public ushort msgid;
    public ManagerID GetManager()
    {
        int tempid = msgid / FrameTools.MsgSpan;
        return (ManagerID)(tempid *FrameTools .MsgSpan );
    }

    public MsgBase()
    {

        msgid = 0;
    }
    public MsgBase (ushort tempmsg)
    {

        msgid = tempmsg;
    }
}
