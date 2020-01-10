using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IABScenceManager
{
    Dictionary<string, string> allAssets;
    IABsManager aBsManager;
    public IABScenceManager (string scencename)
    {

        aBsManager = new IABsManager(scencename );
    }
    public void ReadConfiger(string scenceName)
    {

      //  string path="";
       // string path = Application.persistentDataPath + "/AssetBundle/" + scenceName + "Record.txt";

       
        string   path =IPathTools.GetAssetBundlePath()+ "/"+scenceName + "Record.txt";

      

        //Debug.Log("path:"+path );
        allAssets = new Dictionary<string, string>();

        aBsManager = new IABsManager(scenceName);


        ReadConfig(path);


    }
    private void ReadConfig(string path)
    {
       
        FileStream fileStream = new FileStream(path, FileMode.Open);


        StreamReader bw = new StreamReader(fileStream);

        string temcout = bw.ReadLine();
      //  Debug.Log("temcout"+temcout);
        int allcount = int.Parse(temcout);
       
     //   Debug.Log("allcount"+allcount.ToString ());
        for (int i = 0; i < allcount; i++)
        {
            string line = bw.ReadLine();
            string[] allstr = line.Split(" ".ToCharArray());
            allAssets.Add(allstr[0], allstr[1]);

        }
        bw.Close();
        fileStream.Close();
    }
    public void LoadAsset(string bundleName, LoadProgess progres, LoadAssetBundleCallBack callback)
    {
        //scenceOne/laod.ld
        if (allAssets.ContainsKey(bundleName))
        {
           // Debug.Log("111--:"+bundleName );
            string temvalue = allAssets[bundleName];
            aBsManager.LoadAssetBundle(temvalue, progres, callback);
        }
        else
        {
            Debug.Log("Dont contain the bundle name:" + bundleName);
        }
    }
    public string GetBundleretateName( string bundleName)
    {

       
        if (allAssets .ContainsKey (bundleName ))
        {
            return allAssets[bundleName ];
        }
        else
        {
            return null;
        }
    }
    #region //由下层提供接口

    public IEnumerator LoadAssetSys(string bundlename)
    {

        yield return aBsManager.LoadAssetBundles(bundlename );
    }


    public Object  GetSingleResource(string bundlename,string resname)
    {
        if (allAssets .ContainsKey (bundlename ))
        {


            string fuBundleName = allAssets[bundlename ];
            //Debug.Log("fuBundleName"+fuBundleName);
            return aBsManager.GetSingleResource(fuBundleName ,resname );
        }
        else
        {
            Debug.Log("Dont contain the bundle name:" + bundlename);
            return null;
        }
    }
    public Object[] GetMutiResources(string bundlename,string resname)
    {

        if (allAssets.ContainsKey(bundlename))
        {
            return aBsManager.GetMutiResource (bundlename, resname);
        }
        else
        {
            Debug.Log("Dont contain the bundle name:" + bundlename);
            return null;
        }
    }

    //释放单个资源
    public void DisposeResobj(string bundlename,string res)
    {
        if (allAssets.ContainsKey(bundlename))
        {
             aBsManager.DisposeResObj(allAssets [bundlename ],res);

        }
        else
        {
            Debug.Log("Dont contain the bundle name:" + bundlename);
           
        }
    }
    //卸载整个包
    public void DisposeBundleRes(string bundlename)
    {
        if (allAssets.ContainsKey(bundlename))
        {
            aBsManager.DisposeBundleResObj (bundlename );

        }
        else
        {
            Debug.Log("Dont contain the bundle name:" + bundlename);

        }
    }
    //卸载所有的资源
    public void DisposeAllRes()
    {
        aBsManager.DisposeAllResObjs();
    }
    //卸载bundle文件
    public void DisposeBundle(string bundlename)
    {
        if (allAssets.ContainsKey(bundlename))
        {
            aBsManager.DisposeBundle(bundlename);

        }
    }
    public void DisposeAllBundle()
    {

        aBsManager.DisposeAllBundle();
        allAssets.Clear();
    }
    public void DisposeAllBundleAndRes()
    {
        aBsManager.DisposeAllResObjs();
        allAssets.Clear();
    }
    public void DebugAllAssets()
    {

        List<string> keys = new List<string>();
        keys.AddRange(allAssets .Keys );
        for (int i = 0; i < keys .Count ; i++)
        {
            aBsManager.DebugAssets(allAssets [keys [i]]);
        }
    }
    //bundlename==test  (scenceone/test.id)
    public bool IsLoadingFinish(string bundleName)
    {

        if (allAssets .ContainsKey (bundleName ))
        {
           // Debug.Log("test");
            return aBsManager.IsLoadingFish(allAssets[bundleName]);
        }
        else
        {

            Debug.Log("is not contain bundle:"+bundleName );
        }
       // Debug.Log("test....");
        return false;

    }


    public bool IsLoadingAssetBundle(string bundleName)
    {

        if (allAssets.ContainsKey(bundleName))
        {
            return aBsManager.IsLoadingAssetBundle (allAssets[bundleName]);
        }
        else
        {

            Debug.Log("is not contain bundle:" + bundleName);
        }
        return false;

    }
    #endregion 

}
