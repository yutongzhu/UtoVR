using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILoaderManager : MonoBehaviour {

    public static ILoaderManager Instance;
	private void Awake()
	{
        Instance = this;
        //加载IAbmanifest

        StartCoroutine(IABManifestLoader.Instance.LoadManifest());
        //第二部读取配置文件
	}
    //IABScenceManager scenceManager;

    //scenceName   manager 可能会有多个场景
    private Dictionary<string, IABScenceManager> loadManager =new Dictionary<string, IABScenceManager>();



   /// <summary>
   /// Reads the configer.读取配置文件
   /// </summary>
   /// <param name="scencename">Scencename.</param>
    public void ReadConfiger(string scencename)
    {
       // Debug.Log("111");
        
        if (!loadManager.ContainsKey (scencename ))
        {
           // Debug.Log("000");
            IABScenceManager tempManager = new IABScenceManager(scencename);

            tempManager.ReadConfiger(scencename);

            loadManager.Add(scencename , tempManager );
        }
        else
        {
           Debug.Log("loadManager is null  ");
        }
    }
    public void LoadCallBack(string sceneceName,string bundleName)
    {
        if (loadManager .ContainsKey (sceneceName ))
        {
            IABScenceManager tempManager = loadManager[sceneceName ];
            StartCoroutine(tempManager .LoadAssetSys(bundleName ));
        }
        else
        {
            Debug.Log("bundle name is not contain: "+bundleName );
        }
    }
    //提供加载功能
    public void LoadAsset(string scencename,string bundleName,LoadProgess progess )
    {
        if (!loadManager .ContainsKey (scencename ))//如果没有包含就去读取配置文件
        {
            ReadConfiger(scencename );
        }

        IABScenceManager tempManager = loadManager[scencename ];
       // Debug.Log("000:--"+bundleName );
        tempManager.LoadAsset(bundleName ,progess ,LoadCallBack);
    }
    #region //由下层提供的功能


    public string GetBundleretateName(string scenceName,string bundleName)
    {

        IABScenceManager tmpManger = loadManager[scenceName ];
        if (tmpManger !=null )
        {
            return tmpManger.GetBundleretateName(bundleName );
        }
        return null;
    }
    public Object  GetSingleResource(string scenceName,  string bundlename, string resname)
    {
        if (loadManager.ContainsKey(scenceName))
        {
           // Debug.Log("aaaa");
            IABScenceManager tempManager = loadManager[scenceName];

            //string fullBundelName=
            return tempManager.GetSingleResource(bundlename ,resname );
        }
        else
        {
            Debug.Log("scenceName== " + scenceName +" bundlename=="+bundlename +"is not load");

            return null;
        }
    }
    //获取多个资源
    public Object[] GetMutiResources( string scenceName,    string bundlename, string resname)
    {

        if (loadManager.ContainsKey(scenceName))
        {
            IABScenceManager tempManager = loadManager[scenceName];
            return tempManager.GetMutiResources (bundlename, resname);
        }
        else
        {
            Debug.Log("scenceName== " + scenceName + " bundlename==" + bundlename + "is not load");

            return null;
        }
    }
    //释放资源
    //释放某一个资源
    public void UnLoadResobj(string scenceName, string bundlename, string res)
    {
        if (loadManager.ContainsKey(scenceName))
        {
            IABScenceManager tempManager = loadManager[scenceName];
            tempManager.DisposeResobj(bundlename ,res );
        }
    }

    //释放某一个bundle里的所有资源
    public void UnLoadBundleRes(string scenceName, string bundlename)
    {
        if (loadManager.ContainsKey(scenceName))
        {
            IABScenceManager tempManager = loadManager[scenceName];
            tempManager.DisposeBundleRes(bundlename);
        }
    }
    //释放一个场景里的所有加载的obj文件
    public void UnLoadAllRes(string scenceName)
    {
        if (loadManager.ContainsKey(scenceName))
        {
            IABScenceManager tempManager = loadManager[scenceName];
            tempManager.DisposeAllRes();
        }
    }
    //释放一个bundle文件
    public void UnLoadBundle(string scenceName,string bundlename)
    {
        if (loadManager.ContainsKey(scenceName))
        {
            IABScenceManager tempManager = loadManager[scenceName];
            tempManager.DisposeBundle (bundlename );
        }
    }
    //释放一个场景里的所有bundle文件
    public void UnLoadAllBundles(string scenceName)
    {
        if (loadManager.ContainsKey(scenceName))
        {
            IABScenceManager tempManager = loadManager[scenceName];
            tempManager.DisposeAllBundle ();
            System.GC.Collect();
        }
    }
    //释放一个场景里的所有bundle和obj文件
    public void UnLoadAllBundlesAndRes(string scenceName)
    {
        if (loadManager.ContainsKey(scenceName))
        {
            IABScenceManager tempManager = loadManager[scenceName];
            tempManager.DisposeAllBundleAndRes ();
            System.GC.Collect();
        }
    }


    public void DebugAllAssetBUndles(string sceneName)
    {
        if (loadManager .ContainsKey(sceneName ))
        {
            IABScenceManager temMannger = loadManager[sceneName ];
            temMannger.DebugAllAssets();
        }
    }
	#endregion
    public bool IsLoadingBundleFinish(string scenceName,string bundleName)
    {
        bool tmpBool = loadManager.ContainsKey(scenceName );

        if (tmpBool )
        {
            //Debug.Log("0000");
            IABScenceManager tmpManager = loadManager[scenceName];
            return tmpManager.IsLoadingFinish(bundleName );
        }
        return false;
    }
    public bool IsLoadingAssetBundle(string scenceName, string bundleName)
    {
        bool tmpBool = loadManager.ContainsKey(scenceName);

        if (tmpBool)
        {
            IABScenceManager tmpManager = loadManager[scenceName];
            return tmpManager.IsLoadingAssetBundle (bundleName);
        }
        return false;
    }
	private void OnDestroy()
	{
        loadManager.Clear();
        System.GC.Collect();
	}
	
}
