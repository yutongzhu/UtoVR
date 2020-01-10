using System.Collections;

using UnityEngine;
using System;
public class IABResLoad :IDisposable 
{
    public  AssetBundle assetBundle;
    public IABResLoad (AssetBundle bundle )
    {

        assetBundle = bundle; 

    }
    //加载单个资源
    public UnityEngine .Object LoadResoure(string resname,AssetBundle tempBundle)

    {
       // Debug.Log("resname"+resname);

      
        if (tempBundle==null ||!tempBundle.Contains (resname))
        {
            Debug.Log("res is not constain");
            return null;
        }
        Debug.Log("AssetBundle 加载："+resname );
        return tempBundle.LoadAsset(resname );
    }
    /// <summary>
    /// 加载多个资源文件
    /// </summary>
    /// <returns>The resoures.</returns>
    /// <param name="resname">Resname.</param>
    /// <param name="tempBundle">Temp bundle.</param>
    public UnityEngine.Object[] LoadResoures(string resname, AssetBundle tempBundle)

    {

        if (tempBundle == null || tempBundle.Contains(resname))
        {
            Debug.Log("res is not constain");
            return null;
        }
        return tempBundle.LoadAssetWithSubAssets  (resname);
    }
    /// <summary>
    /// 卸载单个资源
    /// </summary>
    /// <param name="obj">Object.</param>
    public void UnLoadResource(UnityEngine .Object obj)
    {

        Resources.UnloadAsset(obj);
    }
    public void Dispose()
    {

        if (this .assetBundle ==null )
        {
            return;
        }
        assetBundle.Unload(false );
    }
    /// <summary>
    /// 调试打印所有资源名字
    /// </summary>
    public void DebugAllRes()
    {

        string[] tempAsName = assetBundle.GetAllAssetNames();
        for (int i = 0; i < tempAsName.Length ; i++)
        {
            Debug.Log("ABRes contain asset name:"+tempAsName [i ]);
        }
    }
}
