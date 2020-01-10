using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBase
{
    public  override  void ProcessEvent(MsgBase tmpMag)
    {


    }
    public void RegistSelf(MonoBase mono,params ushort []msgs)
    {

        UIManager.instance.RegisMsg(mono ,msgs );
    }
    public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
    {

        UIManager.instance.UnRegistMsg (mono, msgs);
    }
   public void SendMsg(MsgBase msg)
    {
      //  Debug.Log(0000);
        UIManager.instance.SendMsg(msg );
    }
    public ushort[] msgids;
	private void OnDestroy()
	{
        if (msgids!=null )
        {
            UnRegistSelf(this, msgids);
        }
	}
}
