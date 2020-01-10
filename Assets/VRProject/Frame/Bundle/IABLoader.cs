using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 每一帧的回调
/// </summary>
public delegate void LoadProgess(string bundlename, float progress);

//加载结束时候的回调
public delegate void LoadFinished(string bundlename);
public class IABLoader

{

    private string bundleName;
    private string commonBundlePath = "";
    WWW comWWW;
    float progress;

    private LoadProgess loadProgess;
    private LoadFinished loadFinished;


    private IABResLoad iABResLoad;
    public IABLoader(LoadProgess prog,LoadFinished loadFinish )
    {

        commonBundlePath = "";
        bundleName = "";
        loadProgess = prog;
        loadFinished = loadFinish;
        iABResLoad = null;

    }
    //设置包名 //scenceOne/test.prefab
    public void SetBundleName(string bundle)
    {
        this.bundleName = bundle;
    }

    /// <summary>
    /// 要求上层传递完整的路径
    /// </summary>
    /// <param name="path">Path.</param>
    public void LoadResources(string path)
    {

        commonBundlePath = path;


    }

    /// <summary>
    /// 协程加载
    /// </summary>
    /// <returns>The laod.</returns>
    public IEnumerator CommonLaod()
    {

        comWWW = new WWW(commonBundlePath);

        while (!comWWW.isDone)
        {
            progress = comWWW.progress;

            if (loadProgess != null)
            {
                loadProgess(bundleName, progress);
            }

            yield return comWWW.progress;

            progress = comWWW.progress;
        }
        if (progress >= 1.0)//表示加载完成
        {

            iABResLoad = new IABResLoad(comWWW.assetBundle);
            if (loadProgess != null)
            {
                loadProgess(bundleName, progress);
            }
            if (loadFinished != null)
            {
                
                this.loadFinished(bundleName);
               // Debug.Log("loadFinished");
            }

           // Debug.Log("CommonLaod");
        }
        else
        {
            Debug.LogError("load bundle  error " + bundleName);
        }
        comWWW = null;
    }
    /// <summary>
    /// 调试用
    /// </summary>
    public void DebugLoader()
    {
        if (comWWW != null)
        {
            iABResLoad.DebugAllRes();

        }

    }


    #region  //下层提供功能

   
  

    /// <summary>
    /// 获取单个资源
    /// </summary>
    /// <returns>The resource.</returns>
    /// <param name="name">Name.</param>
    public UnityEngine .Object GetResource(string name)
    {
        if (iABResLoad != null)
        {
            return iABResLoad.LoadResoure(name, iABResLoad.assetBundle);
        }
        else return null;
    }

    //获取多个资源
    public UnityEngine.Object[] GetmutiResources(string name)
    {
        if (iABResLoad != null)
        {
            return iABResLoad.LoadResoures (name, iABResLoad.assetBundle);
        }
        else return null;
    }

    //释放功能
    public void Dispose()
    {
        if (iABResLoad != null)
        {
            iABResLoad.Dispose();
            iABResLoad = null;
        }
    }
    /// <summary>
    /// 卸载多个资源
    /// </summary>
    /// <param name="obj">Object.</param>
    public void UnLoadAssetRes(UnityEngine.Object obj)
    {
        if (iABResLoad != null)
        {
            iABResLoad.UnLoadResource(obj);
        }
    }
    #endregion
}
