using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Debuger  {

    public static bool EnableDebug = true;
    public static UDPSocket uDPSocket=null ;
    public static UDPSocket UDPSocket 
    {
        get 
        {

            if (uDPSocket ==null )
            {
                uDPSocket = new UDPSocket();
                uDPSocket.BindSocket(18001,1024,null );
            }
            return uDPSocket;
        }
    }
    public static void Log( object message,Object context )
    {
        if (EnableDebug )
        {
            if (Application .platform ==RuntimePlatform .WindowsEditor ||
               Application .platform ==RuntimePlatform .OSXEditor 
               )
            {
                Debug.Log(message ,context );
            }
            else
            {
                byte[] datas = Encoding.Default.GetBytes(message .ToString() );

                //ip自己定义
               // uDPSocket.SendData("", datas, 18001);

                //只要端口号为18001的局域网内的机器都可以发送
                uDPSocket.SendData("255.255.255.255", datas, 18001);
            }
        }
    }
    public static void LogError(object message, Object context)
    {
        if (EnableDebug)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor ||
               Application.platform == RuntimePlatform.OSXEditor
               )
            {
                Debug.LogError (message, context);
            }
            else
            {
                byte[] datas = Encoding.Default.GetBytes(message.ToString());

                //ip自己定义
               // uDPSocket.SendData("", datas, 18001);
                //只要端口号为18001的局域网内的机器都可以发送
                uDPSocket.SendData("255.255.255.255", datas, 18001);
            }
        }
    }
}
