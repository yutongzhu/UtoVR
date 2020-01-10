using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ManagerID
{
    GameManager = 0,

    UIManager = FrameTools.MsgSpan,//3000
    AudioManager = FrameTools.MsgSpan * 2,//6000
    AssetManager = FrameTools.MsgSpan * 3,
    NPCManager = FrameTools.MsgSpan * 4,
    NetManager=FrameTools.MsgSpan * 5,

}
public class FrameTools 
{
    public const  int MsgSpan = 3000;

   
}
