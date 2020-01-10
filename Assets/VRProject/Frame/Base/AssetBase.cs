﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBase : MonoBase {
    public override void ProcessEvent(MsgBase tmpMag)
    {
      


    }
    public void RegistSelf(MonoBase mono, params ushort[] msgs)
    {

        AssetManager.instance.RegisMsg(mono, msgs);
    }
    public void UnRegistSelf(MonoBase mono, params ushort[] msgs)
    {

        AssetManager.instance.UnRegistMsg(mono, msgs);
    }
    public void SendMsg(MsgBase msg)
    {

        AssetManager.instance.SendMsg(msg);
    }
    public ushort[] msgids;
    private void OnDestroy()
    {
        if (msgids != null)
        {
            UnRegistSelf(this, msgids);
        }
       
    }
}