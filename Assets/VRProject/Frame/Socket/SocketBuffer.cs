using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public delegate void CallBackRecvOver(byte[] allData);
public class SocketBuffer  {

    //定义消息头
    private byte[] headByte;
    private byte headLength = 6;

    private byte[] allRecvData;//接收到的数据

    private int curRecvLength;//当前接受到的数据长度
    private int allDataLength;//总共接收到的长度
    public SocketBuffer (byte tmpHeadLength,CallBackRecvOver tmpOver)
    {

        headLength = tmpHeadLength;
        headByte = new byte[headLength ];
        callBackRecvOver = tmpOver;
    
    }
    public void RecvByte(byte []recvByte,int realLength)
    {

        if (realLength ==0)
        {
            return;
        }
         if (curRecvLength <headByte .Length )
        {
            RecvHead(recvByte ,realLength );
        }
        else
        {
            //接收总长度
            int tmplength = curRecvLength + realLength;
            if (tmplength ==allDataLength )
            {
                //刚好相等的情况

                RecvOneAll(recvByte ,realLength );
            }
            else  if (tmplength > allDataLength)//接收的数据比这个消息长
            {
               

                RecvLonger(recvByte, realLength);
            }
            else
            {
                RecvLess(recvByte, realLength);
            }



        }
    }
    private void RecvLonger(byte[] recvByte, int realLength)
    {

        int tmpLengh = allDataLength - curRecvLength;
        Buffer.BlockCopy(recvByte, 0, allRecvData, curRecvLength, tmpLengh);
        curRecvLength += tmpLengh;
        RecvOneMsgOver();

        int remainLengh = realLength - tmpLengh;
        byte[] remainByte = new byte[remainLengh];
        Buffer.BlockCopy(recvByte, tmpLengh, remainByte, 0, remainLengh);

        //看成从socket里取出来处理
        RecvByte(remainByte, remainLengh);

    }
    private void RecvLess(byte[] recvByte, int realLength)
    {

        Buffer.BlockCopy(recvByte, 0, allRecvData, curRecvLength, realLength);
        curRecvLength += realLength;
      
    }
    private void RecvOneAll(byte[] recvByte, int realLength)
    {

        Buffer.BlockCopy(recvByte, 0, allRecvData , curRecvLength, realLength);
        curRecvLength += realLength;
        RecvOneMsgOver();
    }
    private void RecvHead(byte []recvByte,int realLength)
    {
        int tmpReal = headByte.Length - curRecvLength;//差多少个字节组成头

        //现在接收的和已经接收的总长度
        int tmpLength = curRecvLength + realLength;
        //z
        if (tmpLength <headByte .Length )
        {
            Buffer.BlockCopy(recvByte ,0,headByte ,curRecvLength ,realLength );
            curRecvLength += realLength;
        }
        else//大于或者等于头的长度
        {
            Buffer.BlockCopy(recvByte, 0, headByte, curRecvLength, tmpReal);
            curRecvLength += tmpReal;//t头部已经凑齐
                             //取出四个字节转换int
            allDataLength  = BitConverter.ToInt32(headByte ,0)+headLength ;

            allRecvData = new byte[allDataLength ];//body+head
            //alldata 已经包含头部了
            Buffer.BlockCopy(headByte, 0, allRecvData, 0, headLength );

            int tmpremain = realLength - tmpReal;

            //表示recybyte是否还有数据
            if (tmpremain>0)
            {
                byte[] tmpByte = new byte[tmpremain ];
                //表示经剩下的字节送入tmpbyte
                Buffer.BlockCopy(recvByte, tmpReal , tmpByte , 0, tmpremain );
                RecvByte(tmpByte, tmpremain);
            
            }
            else
            {

                //只有消息头的情况
                RecvOneMsgOver();
            }
        }
    }

  


    CallBackRecvOver callBackRecvOver;
    //表示一条消息接收完了
    private void RecvOneMsgOver()
    {
        if (callBackRecvOver !=null )
        {
            callBackRecvOver(allRecvData );
        }

        curRecvLength = 0;
        allDataLength = 0;
        allRecvData = null;
    }
}
