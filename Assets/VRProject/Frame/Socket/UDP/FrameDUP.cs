using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UDPEvent
{
    Initial=TCPEvent .MaxValue +1,
    SendTo,
    MaxValue,

}

public class FrameDUP : NetBase   {

    public override void ProcessEvent(MsgBase Msg)
    {
        switch (Msg .msgid)
        {
            case (ushort)UDPEvent.SendTo :
                {

                    UDPSendMsg tmpSendMsg = (UDPSendMsg )Msg;
                    uDPSocket.SendData(tmpSendMsg.ip ,tmpSendMsg .data ,tmpSendMsg .port );
                }
                break;
            case (ushort)UDPEvent .Initial :

                {
                    UDPMSg tmpMsg = (UDPMSg)Msg;
                    uDPSocket = new UDPSocket();
                    uDPSocket.BindSocket(tmpMsg.port ,tmpMsg .recvBufferLength,tmpMsg.recvDelegate );


                }
                break;
        }

    }

    UDPSocket uDPSocket;
    private void Awake()
    {
        msgids = new ushort[]
      {
            (ushort )UDPEvent .Initial ,
            (ushort )UDPEvent .SendTo 

      };
        RegistSelf(this, msgids);

    }
  
}
