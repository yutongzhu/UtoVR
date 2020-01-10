using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NativeResCallBack(NativeResCallBackNode tmpNode);
public class NativeResCallBackNode
{

    public string scenceName;
    public string bundleName;
    public string resName;
    public ushort backMsgId;
    public bool isSingle;

  public   NativeResCallBackNode nextValue;
    public NativeResCallBack callBack;
    public NativeResCallBackNode(bool tempSingle, string tempScenceName, string tempBundle, 
                                 string tempResName, ushort tempBackid,NativeResCallBack tmpBack,
                                 NativeResCallBackNode tmpNode   )
    {

        this.isSingle = tempSingle;
        this.scenceName = tempScenceName;
        this.bundleName = tempBundle;
        this.resName = tempResName;
        this.backMsgId = tempBackid;
        this.nextValue = tmpNode;
        this.callBack  = tmpBack ;

    }
    public void Dispose()
    {
        callBack = null;
        nextValue = null;
        this.scenceName = null;
        this.bundleName = null;
        this.resName = null;
      

    }
}
public class NativeResCallBackManager
{
    Dictionary<string, NativeResCallBackNode> manager = null;
    public NativeResCallBackManager()
    {
        manager = new Dictionary<string, NativeResCallBackNode>();

    }
    //来了请求添加的过程
    public void AddBundle(string bundle,NativeResCallBackNode curNode)
    {
        if (manager .ContainsKey(bundle ))
        {
            NativeResCallBackNode tmpNode = manager[bundle ];
            while (tmpNode.nextValue !=null  )
            {
                tmpNode = tmpNode.nextValue;
            }
            tmpNode = curNode;
        }
        else
        {
            manager.Add(bundle ,curNode );
        }

    }
    //加载完了，消息向上层传递完成了，就把这些缓存的命令删除
    public void Dispose(string bundle)
    {

        if (manager.ContainsKey(bundle))
        {
            NativeResCallBackNode tmpNode = manager[bundle ];
            //挨个释放node
            while (tmpNode.nextValue != null)
            {
                NativeResCallBackNode curNode = tmpNode;
                tmpNode = tmpNode.nextValue;
                curNode.Dispose();
            }
            tmpNode.Dispose();
            manager.Remove(bundle );

        }
    }

    public void CallBackRes(string bundles)
    {

        if (manager .ContainsKey (bundles ))
        {
            NativeResCallBackNode tmpNode = manager[bundles];
            do
            {
                tmpNode.callBack(tmpNode );
                tmpNode = tmpNode.nextValue;
            } while (tmpNode !=null );
        }
    }
}



public class NativeResLoader : AssetBase
{
    public override void ProcessEvent(MsgBase RecMsg)
    {
        switch (RecMsg.msgid)
        {
            case (ushort)AssetEvent.ReleaseSingleObj:

                {


                    HunkAssetRes tmpMsg = (HunkAssetRes)RecMsg;
                    ILoaderManager.Instance.UnLoadResobj(tmpMsg.scenceName, tmpMsg.bundleName, tmpMsg.resName);

                }
                break;
            case (ushort)AssetEvent.ReleasebundleObj:

                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)RecMsg;
                    ILoaderManager.Instance.UnLoadBundleRes(tmpMsg.scenceName, tmpMsg.bundleName);

                }
                break;
            case (ushort)AssetEvent.ReleaseScenceObj:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)RecMsg;
                    ILoaderManager.Instance.UnLoadAllRes(tmpMsg.scenceName);

                }
                break;
            case (ushort)AssetEvent.ReleaseSingleBundle:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)RecMsg;
                    ILoaderManager.Instance.UnLoadBundle(tmpMsg.scenceName, tmpMsg.bundleName);

                }
                break;
            case (ushort)AssetEvent.ReleaseScenceBundle:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)RecMsg;
                    ILoaderManager.Instance.UnLoadAllBundles(tmpMsg.scenceName);

                }
                break;
            case (ushort)AssetEvent.ReleaseAll:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)RecMsg;
                    ILoaderManager.Instance.UnLoadAllBundlesAndRes(tmpMsg.scenceName);

                }
                break;
                //请求资源
            case (ushort)AssetEvent.HunkRes:
                {
                    HunkAssetRes tmpMsg = (HunkAssetRes)RecMsg;
                    GetResources(tmpMsg .scenceName ,tmpMsg .bundleName ,tmpMsg .resName ,tmpMsg .isSingle ,tmpMsg .backMsgId );
                }

                break;

        }


    }


    HunkAssetResBack resBackMsg = null;
    HunkAssetResBack ReleaseBack
    {

        get
        {

            if (resBackMsg == null)
            {
                resBackMsg = new HunkAssetResBack();
            }
            return ReleaseBack;
        }
    }
    NativeResCallBackManager   callBack  = null;
    NativeResCallBackManager CallBack
    {

        get
        {

            if (resBackMsg == null)
            {
                callBack = new NativeResCallBackManager();
            }
            return callBack;
        }
    }


    void Awake()
    {
        msgids = new ushort[]
    {

           (ushort ) AssetEvent. ReleaseSingleObj,
            (ushort )AssetEvent. ReleasebundleObj,
            (ushort ) AssetEvent.ReleaseScenceObj,
            (ushort ) AssetEvent.ReleaseSingleBundle,
            (ushort ) AssetEvent. ReleaseScenceBundle,
            (ushort )  AssetEvent. ReleaseAll,
            (ushort )  AssetEvent. HunkRes

    };


        RegistSelf(this, msgids);
    }

    //node回调
    public void SendToBackMsg(NativeResCallBackNode tmpNode)
    {
        if (tmpNode.isSingle)
        {
            Object tmpobj = ILoaderManager.Instance.GetSingleResource(tmpNode.scenceName, tmpNode.bundleName, tmpNode.resName);

            this.ReleaseBack.Changer(tmpNode.backMsgId, tmpobj);
            SendMsg(ReleaseBack);
        }
        else
        {
            Object[] tmpobj = ILoaderManager.Instance.GetMutiResources(tmpNode.scenceName, tmpNode.bundleName, tmpNode.resName);

            this.ReleaseBack.Changer(tmpNode.backMsgId, tmpobj);
            SendMsg(ReleaseBack);
        }
    }

    void LoadingProgress(string bundleName, float progress)
    {
        Debug.Log("bundle Progress "+ progress );
        //上层的回调
        if (progress >=1.0)
        {
            callBack.CallBackRes(bundleName );
            Debug.Log("bundle name: " + bundleName );
            callBack.Dispose(bundleName );
        }
    }
    //Load
    public void GetResources(string scencename, string bundleName, string resName, bool single, ushort backId)
    {
        //没有加载
       if (!ILoaderManager .Instance .IsLoadingAssetBundle (scencename ,bundleName ))
        {
            ILoaderManager.Instance.LoadAsset(scencename ,bundleName ,LoadingProgress);
            string bundleFullName = ILoaderManager.Instance.GetBundleretateName(scencename, bundleName );
            if (bundleFullName !=null )
            {
                NativeResCallBackNode tmpNode = new   NativeResCallBackNode( single , scencename ,bundleName ,resName ,backId ,SendToBackMsg,null );


                callBack.AddBundle(bundleFullName ,tmpNode );
            }
            else
            {
                Debug.Log("Do not cotain bundle:"+bundleFullName );
            }
        }
        if (ILoaderManager.Instance.IsLoadingBundleFinish (scencename, bundleName))//加载完成
        {
            if (single )
            {
                Object tmpobj = ILoaderManager.Instance.GetSingleResource(scencename ,bundleName ,resName );
                this.ReleaseBack.Changer (backId ,tmpobj );
                SendMsg(ReleaseBack );
            }
            else
            {
                Object[] tmpobj = ILoaderManager.Instance.GetMutiResources (scencename, bundleName, resName);
                this.ReleaseBack.Changer(backId, tmpobj);
                SendMsg(ReleaseBack);
            }
        }
        else//  已经加载但是没有加载完成
        {
            string bundleFullname = ILoaderManager.Instance.GetBundleretateName(scencename ,bundleName  );

            if (bundleFullname != null)
            {
                NativeResCallBackNode tmpNode = new NativeResCallBackNode(single, scencename, bundleName, resName, backId, SendToBackMsg, null);


                callBack.AddBundle(bundleName, tmpNode);
            }
            else
            {
                Debug.Log("Do not cotain bundle:" + bundleName);
            }
        }

    }
}