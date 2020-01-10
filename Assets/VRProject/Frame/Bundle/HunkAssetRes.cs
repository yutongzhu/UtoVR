using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//上层需要发给我们的
public class HunkAssetRes : MsgBase
{

    public string scenceName;
    public string bundleName;
    public string resName;
    public ushort backMsgId;
    public bool isSingle;
    public HunkAssetRes (bool tempSingle,string tempScenceName,string tempBundle,string tempResName,ushort tempBackid) 
    {

        this.isSingle = tempSingle;
        this.scenceName = tempScenceName ;
        this.bundleName  = tempBundle ; 
        this.resName = tempResName;
        this.backMsgId = tempBackid;

    }
}
//返回给上层的消息
public class HunkAssetResBack : MsgBase
{
    public Object[] value;
    public HunkAssetResBack ()
    {

        this.msgid  = 0;
        this.value = null;
    }
    public void Changer(ushort TemMsgid,params Object [] temValue)
    {
        this.msgid  = TemMsgid;
        this.value = temValue;

    }
    public void Changer(ushort msid)
    {

        this.msgid  = msid;
    }
    public void Changer(params Object [] temVale)
    {

        this.value = temVale;
    }
}
