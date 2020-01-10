using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPathTools
{

    public static string GetPlatformFolderName(RuntimePlatform platform)
    {

        switch (platform)
        {


            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.IPhonePlayer:
                return "IOS";
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                return "Windows";
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                return "OSX";
            default:
                return null;
        }

    }



    public static string GetAppFilePath()
    {

        string tempath = "";
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor )
        {
            tempath = Application.streamingAssetsPath;
        }
        else
        {
            tempath = Application.persistentDataPath;
        }
        return tempath;
    }



    public static string GetAssetBundlePath()
    {

        string plaFolder = GetPlatformFolderName(Application.platform);

        string allPath = GetAppFilePath() + "/" + plaFolder;
        return allPath;



    }


    public static string GetWWWAssetBundlePath()
    {


        string path = "";
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor )
        {
            path = "file:///" + GetAssetBundlePath();

        }
        else
        {
            string tempath = GetAssetBundlePath();
#if UNITY_ANDROID

            path = "jar:file://" + tempath;

#elif UNITY_STANDALONE_WIN

            path = "file:///" + tempath;
#else  

            path = "file://" + tempath;

#endif
        }

        return path;


    }
}
