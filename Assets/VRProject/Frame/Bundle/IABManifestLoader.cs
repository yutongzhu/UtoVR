using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABManifestLoader
{
    private static IABManifestLoader instance = null;

    public static IABManifestLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new IABManifestLoader();
            }
            return instance;
        }
    }
    public IABManifestLoader()
    {
        manifestBundle = null;
        assetManifest = null;

        isLoadFish = false;

        //fixed
        //file:///c:/user/lenovo/Desktop/Game/Assets/StreamingAssets/Windows/Windows（是windows的manifest文件）
        ManifestPath = IPathTools.GetWWWAssetBundlePath() + "/" + IPathTools.GetPlatformFolderName(Application .platform );
    }
    public AssetBundleManifest assetManifest;
    public AssetBundle manifestBundle;
    public bool isLoadFish;
    public string ManifestPath;
    public void SetManifestPath(string path)
    {

        ManifestPath = path;
    }
    public IEnumerator LoadManifest()
    {


        WWW manifestW = new WWW(ManifestPath);

        yield return manifestW;

        if (!string.IsNullOrEmpty(manifestW.error))
        {
            Debug.Log(manifestW.error);
        }
        else
        {
            if (manifestW.progress >= 1)
            {
                manifestBundle = manifestW.assetBundle;
                if (Application .platform ==RuntimePlatform .OSXEditor||Application .platform ==RuntimePlatform .OSXPlayer )
                {
                    assetManifest = manifestBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
                }
                else
                {
                    assetManifest = manifestBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
                }

                isLoadFish = true;
            }
        }

    }

    public string[] GetDepences(string name)
    {
        return assetManifest.GetAllDependencies(name);
    }

    public void UnLoadManifest()
    {
        manifestBundle.Unload(true);
    }
}
