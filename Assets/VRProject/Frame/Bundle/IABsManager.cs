using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//单个物体有可能多个存取
public class AssetSingleObj
{

    public List<Object> objs;

    public AssetSingleObj(params Object[] temobj)
    {
        objs = new List<Object>();
        objs.AddRange(temobj);
    }


    public void ReleaseObj()
    {

        for (int i = 0; i < objs.Count; i++)
        {
            Resources.UnloadAsset(objs[i]);
        }
    }
}
//多个存取 存的是一个bundle里的objs
public class AssetObjs
{
    public Dictionary<string, AssetSingleObj> resObjs;
    public AssetObjs(string name, AssetSingleObj tempobj)
    {
        resObjs = new Dictionary<string, AssetSingleObj>();
        resObjs.Add(name, tempobj);
    }

    public void AddResobj(string name, AssetSingleObj temp)
    {
        resObjs.Add(name, temp);
    }
    //释放整个资源文件
    public void ReleaseAllResObj()
    {
        List<string> keys = new List<string>();

        keys.AddRange(resObjs.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            ReleaseSingleObj(keys[i]);
        }
    }
    //释放单个资源
    public void ReleaseSingleObj(string name)
    {
        if (resObjs.ContainsKey(name))
        {
            AssetSingleObj tempobj = resObjs[name];
            tempobj.ReleaseObj();
        }
        else
        {

            Debug.Log("release object is not exit:" + name);
        }
    }


    public List<Object> GetResObj(string name)
    {
        if (resObjs.ContainsKey(name))
        {
            AssetSingleObj temo = resObjs[name];
            return temo.objs;
        }
        else
        {
            return null;
        }
    }
}



public delegate void LoadAssetBundleCallBack(string scenceName,string bundlename);
public class IABsManager  {

    //把每一个包都存起来
    Dictionary<string, IABRelastion> loadHelper = new Dictionary<string, IABRelastion>();

    Dictionary<string, AssetObjs> loadObj = new Dictionary<string, AssetObjs>();
    string scenceName;
    string thebundleName;
   
    public string GetBndleName()
    {
        return thebundleName;
    }
    public IABsManager (string tempScencename)
    {
        scenceName = tempScencename;
    }
    //表示是否加载bundle
    public bool IsLoadingAssetBundle(string bundleName)
    {

        if (!loadHelper.ContainsKey(bundleName))
        {
            return false;

        }
        else
        {
            return true;
        }
    }


    #region //释放 缓存
    //释放特定包的特定资源
    public void DisposeResObj(string bundlename, string resname)
    {
        if (loadObj.ContainsKey(bundlename))
        {
            AssetObjs temobj = loadObj[bundlename];
            temobj.ReleaseSingleObj(resname);
        }


    }

    //释放特定整个包的资源
    public void DisposeBundleResObj(string bundlename)
    {
        if (loadObj.ContainsKey(bundlename))
        {
            AssetObjs temobj = loadObj[bundlename];
            temobj.ReleaseAllResObj();
        }
        Resources.UnloadUnusedAssets();

    }
    //释放所有包 的资源
    public void DisposeAllResObjs()
    {
        List<string> keys = new List<string>();
        keys.AddRange(loadObj.Keys);
        for (int i = 0; i < loadObj.Count; i++)
        {
            DisposeBundleResObj(keys[i]);
        }
        loadObj.Clear();
    }
    //卸载bundle文件 循环处理依赖关系
    public void DisposeBundle(string bundle)
    {

        if (loadHelper.ContainsKey(bundle))
        {
            IABRelastion loader = new IABRelastion();
            List<string> depences = loader.GetDepence();
            for (int i = 0; i < depences .Count ; i++)
            {
                if (loadHelper.ContainsKey(depences[i]))
                {
                    IABRelastion depence = loadHelper[depences[i]];
                    if (depence.RemoveReference (bundle ))
                    {
                        DisposeBundle(depence .GetBundleName ());
                    }


                }
            }
            if (loader .GetReference ().Count <=0)
            {
                loader.Dispose();
                loadHelper.Remove(bundle );
            }
        }
    }
    //删除所有bundle文件
    public void DisposeAllBundle()
    {
       

        List<string> keys = new List<string>();
        keys.AddRange(loadObj.Keys);
        for (int i = 0; i < loadObj.Count; i++)
        {
            IABRelastion loader = loadHelper[keys[i]];
            loader.Dispose();
        }
        loadHelper.Clear();
    }
    public void DisposeAllBundleAndRes()
    {
        DisposeAllResObjs ();

        DisposeAllBundle();
    }
    string [] GetDepedences(string bundleName)
    {

      return   IABManifestLoader.Instance.GetDepences(bundleName );
    }

    //对外接口
    public void LoadAssetBundle(string bundleName,LoadProgess progress,LoadAssetBundleCallBack callBack)
    {
        if (!loadHelper .ContainsKey (bundleName ))
        {
            //Debug.Log("loadHelper");
            IABRelastion loader = new IABRelastion();
            loader.Inital(bundleName ,progress );
            loadHelper.Add(bundleName , loader );
          //  Debug.Log("bundleName:"+bundleName);
            //回调给上层启动 IEnumerator LoadAssetBundles(string bundlename);
            callBack(scenceName ,bundleName );

        }
        else
        {
            Debug .Log("IAbmanager have contain name:" + bundleName);
        }

    }

    public IEnumerator LoadAssetundleDepences(string bundleName,string refname,LoadProgess progres)
    {
        //以前没有加载过这个包
        if (!loadHelper .ContainsKey (bundleName ))
        {
            IABRelastion loader = new IABRelastion();
            loader.Inital(bundleName, progres);
            if (refname != null)
            {
                loader.AddReferDepedence(refname );
            }
            loadHelper.Add(bundleName ,loader );
            yield return LoadAssetBundles(bundleName );
        }
        else//表示已经加载过了
        {
            if (refname != null)
            {
                IABRelastion loader = loadHelper[bundleName ];
                loader.AddReferDepedence(refname );
            }
        }

    }
    /// <summary>
    /// 加载assetbundle必须加载mainfest 
    /// </summary>callBack(scenceName ,bundleName );返回上层调用这api
    /// <returns>The asset bundles.</returns>
    /// <param name="bundlename">Bundlename.</param>
    public IEnumerator LoadAssetBundles(string bundlename)
    {

        while (!IABManifestLoader .Instance .isLoadFish)
        {
            yield return null;
        }
        IABRelastion loader = loadHelper[bundlename ];
        string[] depences = GetDepedences(bundlename );
        loader.SetDependences(depences );
        for (int i = 0; i < depences .Length; i++)
        {
            yield return LoadAssetundleDepences(depences [i],bundlename ,loader.GetProgess());
           // yield return null;
        }
        yield return loader.LoadAssetBundle();
    }
    #endregion



    #region 由下层提供的api
    public void DebugAssets(string bundelName)
    {

        if (loadHelper.ContainsKey(bundelName))
        {
            IABRelastion loader = loadHelper[bundelName];
            loader.DebugAssets();
        }
    }
    public bool IsLoadingFish(string bundleName)
    {
        if (loadHelper.ContainsKey(bundleName))
        {
            Debug.Log("test");
            IABRelastion loader = loadHelper[bundleName];
            return loader.IsBudleLoadFish();
        }
        else
        {
            Debug.Log(" no contain bundle:" + bundleName);
            return false;
        }
    }
    public Object GetSingleResource(string bundleName, string resName)
    {
        //是否已经缓存了物体
        if (loadObj.ContainsKey(bundleName))
        {
            Debug.Log("0000");
            AssetObjs temres = loadObj[bundleName];

            List<Object> temobj = temres.GetResObj(resName);

            if (temobj != null)
            {
                return temobj[0];
            }
        }
        //表示已经加载过bundle
        if (loadHelper.ContainsKey(bundleName))
        {
           // Debug.Log("表示已经加载过bundle");
           
            IABRelastion loader = loadHelper[bundleName];

            Object tempobj = loader.GetSingleResource(resName);
            AssetSingleObj tempsigleOb = new AssetSingleObj(tempobj);
            //缓存里是否有这个包
            if (loadObj.ContainsKey(bundleName))
            {
              //  Debug.Log("1111");
                AssetObjs temRes = loadObj[bundleName];
                temRes.AddResobj(resName, tempsigleOb);
            }
            else
            {
               // Debug.Log("2222");
                //没有加载过这个包
                AssetObjs temRes = new AssetObjs(resName, tempsigleOb);
                loadObj.Add(bundleName, temRes);
            }
            return tempobj;
        }
        else
        {
            Debug.Log("null");
            return null;
        }

    }


    public Object[] GetMutiResource(string bundleName, string resName)
    {
        //是否已经缓存
        if (loadObj.ContainsKey(bundleName))
        {
            AssetObjs temres = loadObj[bundleName];

            List<Object> temobj = temres.GetResObj(resName);

            if (temobj != null)
            {
                return temobj.ToArray();
            }
        }
        //表示已经加载过bundle
        if (loadHelper.ContainsKey(bundleName))
        {
            IABRelastion loader = loadHelper[bundleName];

            Object[] tempobj = loader.GetMutiResources(resName);
            AssetSingleObj tempsigleOb = new AssetSingleObj(tempobj);
            //缓存里是否有这个包
            if (loadObj.ContainsKey(bundleName))
            {
                AssetObjs temRes = loadObj[bundleName];
                temRes.AddResobj(resName, tempsigleOb);
            }
            else
            {
                //没有加载过这个包
                AssetObjs temRes = new AssetObjs(resName, tempsigleOb);
                loadObj.Add(bundleName, temRes);
            }
            return tempobj;
        }
        else
        {
            return null;
        }

    }
    #endregion

}
