using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : ManagerBase {

    public static NPCManager instance;
    //规定了开发方式，消耗内存换取速度
    private Dictionary<string, GameObject> sonMembers = new Dictionary<string, GameObject>();
    void Awake()
    {

        instance = this;
    }
    public void SendMsg(MsgBase msg)
    {

        if (msg.GetManager() == ManagerID.UIManager)
        {
            ProcessEvent(msg);//本模块自己处理
        }
        else//mesCenter处理
        {
            MsgCenter.instance.SendToMsg(msg);
        }
    }
    public void RegistGameObject(string name, GameObject obj)
    {
        Debug.Log("000");
        if (!sonMembers.ContainsKey(name))
        {
            Debug.Log("111");
            sonMembers.Add(name, obj);
            Debug.Log("222");
        }
        else
        {
            Debug.Log("sonMembers had exited");
        }

    }
    public void UnRegistGameObject(string name, GameObject obj)
    {
        if (!sonMembers.ContainsKey(name))
        {
            sonMembers.Remove(name);
        }

    }
    public GameObject GetGameObject(string name)
    {

        if (sonMembers.ContainsKey(name))
        {
            return sonMembers[name];
        }
        else
        {
            Debug.Log("this game is not exit :" + name);
            return null;
        }
    }
}
