using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
class EventNode
{
    public MonoBase data;//当前数据
    public EventNode next;//下一节点
    public EventNode (MonoBase tempMono)
    {
        this.data = tempMono;
        this.next = null;
    }
}
public class ManagerBase : MonoBase  {

    //存储注册消息 key value
    private  Dictionary<ushort, EventNode> eventTree = new Dictionary<ushort, EventNode>();
   // Dictionary<ushort, EventNode> aa = new Dictionary<ushort, EventNode>();
    //    public Dictionary<ushort, EventNode> eve = new Dictionary<ushort, EventNode>();
   //  Dictionary<ushort, EventNode> bb = new Dictionary<ushort, EventNode>();
    /// <summary>
    /// Regises the message.
    /// </summary>
    /// <param name="mono">Mono.要注册的脚本</param>
    /// <param name="msgs">Msgs.脚本可以注册多个msg</param>
	public void RegisMsg(MonoBase mono,params ushort [] msgs)
    {
        for (int i = 0; i < msgs.Length ; i++)
        {
            EventNode temp = new EventNode(mono );

            RegisMsg(msgs[i],temp );
        }
    }

  
    /// <summary>
    /// Regises the message.
    /// </summary>
    /// <param name="id">根据msgid.</param>
    /// <param name="node">Node 链表.</param>
    private  void RegisMsg (ushort id, EventNode node)
    {
        //数据链表里没有这个消息id
        if (!eventTree.ContainsKey (id ))
        {
            eventTree. Add(id,node );
        }
        else
        {
            EventNode temp = eventTree[id];
            //找到最后一节
            while (temp .next !=null )
            {
                temp = temp.next;
            }
            temp.next = node;
        }
    }
    /// <summary>
    /// 去掉一个脚本的若干个消息
    /// </summary>
    /// <param name="mon">Mon.</param>
    /// <param name="msgs">Msgs.</param>
    public void UnRegistMsg( MonoBase mon ,params ushort [] msgs)//params可变参数
    {
        if (msgs.Length>0)
        {
           // Debug.Log("msgs.Length"+msgs.Length);
            for (int i = 0; i < msgs.Length; i++)
            {
               // Debug.Log(i);
                UnRegistMsg(msgs[i], mon);

            }
        }
        else
        {
            Debug.Log("msgid == null");
        }


    }
    //去掉一个消息链表
    public void   UnRegistMsg(ushort id,MonoBase node)
    {

        if (!eventTree .ContainsKey (id ))
        {
            Debug.Log("not contain id :"+id );
        }
        else
        {
            //Debug.Log("0000");
            EventNode tmp = eventTree[id];
            if (tmp .data ==node )//去掉头部，包含两种情况
            {
               // Debug.Log("1111");
                EventNode header = tmp;
                //已经存在这个消息  头部
                if (header .next !=null )//后面多个节点
                {
                   
                    eventTree[id] = tmp.next;
                    header.next = null;
                
                    // header.data = tmp.next.data;
                    //header.next = tmp.next.next;

                }
                else//只有一个节点
                {
                   
                    header.next = null;
                    eventTree.Remove(id);
                   
                }
            }
            else//去掉尾部和中间
            {
               
                while (tmp .next !=null &&tmp .next .data !=node  )//下一个不是我要找的nodo
                {
                    tmp = tmp.next;
                }//表示找到该节点
                if (tmp .next .next !=null )//去掉中间的
                {
                    EventNode curNode = tmp.next;
                    tmp.next = curNode.next;
                    curNode.next = null;//把相关联的指针释放
                   // tmp.next = tmp.next.next;
                }
                else//去掉尾部的
                {
                    tmp.next = null;
                }
            }
        }
    }
    //来了消息通知整个消息链表
	public override void ProcessEvent(MsgBase tmpMsg)
	{
        
        if (!eventTree .ContainsKey (tmpMsg.msgid ))
        {
            Debug.Log("msg not contain mesid:"+tmpMsg .msgid );
            return;
        }
        else
        {
           // Debug.Log(4444);
            EventNode tmp = eventTree[tmpMsg .msgid  ];
            tmp.data.ProcessEvent(tmpMsg);
           // Debug.Log(5555);
            //do
            //{
            //    
            //    //策略模式
            //    tmp.data.ProcessEvent(tmpMsg );
            //    Debug.Log(6666);

            //} while (tmp !=null );
        }
    }
}
