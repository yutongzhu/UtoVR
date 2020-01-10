using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//管理关系的依赖和被依赖的关系
public class IABRelastion

{
    void HandleLoadProgess(string bundlename, float progress)
    {


    }


    IABLoader iABLoader;
    LoadProgess loadProgess;
    string theBundleName;
    //记载依赖关系
    List<string> depedenceBundle = null;
    //记载被依赖关系
    List<string> referDepedence = null;
   
   
    public IABRelastion()
    {
        depedenceBundle = new List<string>();
        referDepedence = new List<string>();
    }
    //添加ref关系
    public void AddReferDepedence(string bundleName)
    {
        referDepedence.Add(bundleName);

    }
    //获取
    public List<string> GetReference()
    {
        return referDepedence;
    }

    public bool RemoveReference(string bundleName)
    {

        for (int i = 0; i < referDepedence.Count; i++)
        {
            if (bundleName.Equals(referDepedence[i]))
            {
                referDepedence.RemoveAt(i);
            }
        }

        if (referDepedence.Count <= 0)//表示没有任何相关联
        {
            Dispose();
            return true;
        }
        return false;
    }
    public void SetDependences(string [] depence)
    {

        if (depence.Length >0)
        {
            depedenceBundle.AddRange(depence );
        }
    }

    public List<string> GetDepence()
    {
        return depedenceBundle;
    }
    public void  RemoveDependence(string bundleName)
    {

        for (int i = 0; i < depedenceBundle.Count; i++)
        {
            if (bundleName.Equals(depedenceBundle[i]))
            {
                depedenceBundle.RemoveAt(i);
            }
        }


    }
    bool isLoadFished;
    public void BundleLoadFinish(string bundlename)
    {
       // Debug.Log("isLoadFished:" + isLoadFished);
        isLoadFished = true ;
      //  Debug.Log("BundleLoadFinish..:" + isLoadFished);
    }

    //为上层提供是否加载完
    public bool  IsBudleLoadFish()
    {
       
       
        return isLoadFished;
    }
    public string GetBundleName()
    {
        return theBundleName;
    }

    public LoadProgess GetProgess()
    {

        return loadProgess;
    }

    public void Inital(string bundlename, LoadProgess progess )
    {
        isLoadFished = false;
        theBundleName = bundlename;
        loadProgess = progess;

        iABLoader = new IABLoader(progess,BundleLoadFinish);

    
        iABLoader.SetBundleName(bundlename );

        string bundlePath = IPathTools.GetWWWAssetBundlePath()+"/"+bundlename ;
        iABLoader.LoadResources(bundlePath );
    }
    #region  //由下层提供api

    public void DebugAssets()
    {
        if (iABLoader!=null )
        {
            iABLoader.DebugLoader();
        }
        else
        {
            Debug.Log("aaet load is null ");
        }

    }

    public IEnumerator LoadAssetBundle()
    {
        yield return iABLoader.CommonLaod();
    }
    public void Dispose()
    {

        iABLoader.Dispose();
    }


    public Object GetSingleResource(string bundlename)
    {

        return iABLoader.GetResource(bundlename);
    }
    public UnityEngine.Object[] GetMutiResources(string bundlename)
    {

        return iABLoader.GetmutiResources (bundlename);
    }
    #endregion 

}
