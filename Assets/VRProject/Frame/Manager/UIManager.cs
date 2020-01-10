using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager :ManagerBase  {

  public static   UIManager instance;
    //规定了开发方式，消耗内存换取速度
  private    Dictionary<string, GameObject> sonMembers = new Dictionary<string, GameObject>();
    void Awake()
    {

        instance = this;
    }
    public string TaskItemName;
    public void SendMsg(MsgBase msg )
    {
       
        if (msg .GetManager ()==ManagerID .UIManager )
        {
          //  Debug.Log("0000");
            ProcessEvent(msg );//本模块自己处理
        }
        else//mesCenter处理
        {
            Debug.Log("MsgCenter");
            MsgCenter.instance.SendToMsg(msg );
        }
    }
    public   void RegistGameObject(string name,GameObject obj)
    {
      
        if (!sonMembers .ContainsKey (name ))
        {
         
            sonMembers.Add(name ,obj );
        
        }
        else
        {
            Debug.Log("sonMembers had exited");
        }

    }
    public void UnRegistGameObject(string name, GameObject obj)
    {
        if (!sonMembers.ContainsKey (name ))
        {
            sonMembers.Remove(name);
        }

    }
    public GameObject GetGameObject(string name)
    {

        if (sonMembers .ContainsKey (name ))
        {
            return sonMembers[name ];
        }
        else
        {
            Debug.Log("this game is not exit :"+name );
            return null;
        }
    }
}
