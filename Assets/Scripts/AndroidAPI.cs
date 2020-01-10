using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;

/// <summary>
/// 同android端通讯类，供Unity调用android端的方法
/// </summary>
public class AndroidAPI
{
#if UNITY_ANDROID
    public static AndroidJavaObject picoUnityAcvity;
    private static AndroidJavaObject audioManager;
    private const string strGetStreamVolume = "getStreamVolume"; //当前音量
    private const string strGetStreamMaxVolume = "getStreamMaxVolume"; //最大音量
    private const string strSetStreamVolume = "setStreamVolume"; //设置当前音量
    private const int STREAM_MUSIC = 3;
  
    public static void Init()
    {
        UnityEngine.AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        picoUnityAcvity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        audioManager = picoUnityAcvity.Call<AndroidJavaObject>("getSystemService", new AndroidJavaObject("java.lang.String", "audio"));
    }
    
    public static void   StartActivityForUnityTV189(string contentId, int clickType, string clickParam, string title, int category)
    {
         picoUnityAcvity.Call("startActivityForUnityTV189", contentId, clickType, clickParam, title, category);
    }

    public static void ChangeLevelForUnityTV189(int level)
    {
        picoUnityAcvity.Call("ChangeLevelForUnityTV189", level);
    }
    /// <summary>
    /// 获取当前页面的四个视频信息
    /// </summary>
    /// <param name="clickParam"> "contenttype=100&screenType=4K&tag=动作" </param>

    /// <param name="currentPages">当前的页面数</param>
    /// showSize 指的是一行显示几个item
    public static  void GetCurrentPagesVideos(string clickParam, int currentPage, int showSize)
    {
        picoUnityAcvity.Call("GetCurrentPagesVideos", clickParam, currentPage, showSize);
        //  return null;
    }
    public static int getCurrentVolume()
    {
        return audioManager != null ? audioManager.Call<int>(strGetStreamVolume, STREAM_MUSIC) : 0; 
    }

    public static float getVolume()
    {
        return 10.0f * getCurrentVolume() / getMaxVolume();
    }

    public static void setVolume(float volume)
    {
        float maxVolume = getMaxVolume();
        volume *= maxVolume / 10.0f;
        if (volume > maxVolume)
        {
            volume = maxVolume;
        }
        if (volume < 0.0f)
        {
            volume = 0.0f;
        }
        if (audioManager != null)
        {
            audioManager.Call(strSetStreamVolume, STREAM_MUSIC, (int)volume, 0);
        }
    }

    public static float getMaxVolume()
    {
        return audioManager != null ? audioManager.Call<int>(strGetStreamMaxVolume, STREAM_MUSIC)*1.0f : 1.0f;
    }
#endif
}
