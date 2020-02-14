using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JsonDataManager : MonoBehaviour
{
    public static JsonDataManager instance;
   
    //用来存储被收藏的视频列表
    public static List<VideoItem> collectedVideos = new List<VideoItem>();
    //用来根据索引来判断具体要点击哪个视频,把视频信息都存在字典里便于查询
    public static Dictionary<string, VideoItem> VideosDic = new Dictionary<string, VideoItem>();
  //  RecoVideoItemData
    // public static Dictionary<string, RecoVideoItemData> RecoVideoItemDic = new Dictionary<string, RecoVideoItemData>();
    static string tabUrl = "https://nhapi01.tv189.com/cpms/index/_clt4_xtysxkhd_sygb_ceshi_vrjm_sdh_index.json/0/115020310073.htm?bzv=11";
    static public List<TabsItem> tabsItems = new List<TabsItem>();
  public static DataItem leftRecoItem = new DataItem();//首页左边的item
                                                         // public static RecoVideoItemData recoVideoData = new RecoVideoItemData();
    public static string currentId;//当前所选items的id
    public static int liveTotalCount;//直播item总的数量
    //右边的items
    public static List<ChildrenItem> childrenItems = new List<ChildrenItem>();

    public static List<LiveDataItem> liveItems = new List<LiveDataItem>();
    //存储直播
    public static Dictionary<string, VideoItem> liceItemDic = new Dictionary<string, VideoItem>();
    private void Awake()
    {
        // instance = this;
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        StartCoroutine(Getjson(tabUrl, LoadTabs));


    }
 public    delegate void  JsonCallBack();
 
  public static   string retString;
 public  IEnumerator  Getjson(string url, JsonCallBack call)
    {
        UnityWebRequest unityWeb = new UnityWebRequest(url, "GET");

        unityWeb.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        unityWeb.downloadHandler = new DownloadHandlerBuffer();
        yield return unityWeb.Send();
        if (unityWeb.isDone)
        {
             retString = unityWeb.downloadHandler.text;

           // Debug.Log("retString" + retString);
            call();
         
        }
        else
        {
            Debug.Log("Http 请求失败");
            Debug.Log(unityWeb.error);
        }

    }
    void LoadTabs()
    {
        Tabs tabs = JsonConvert.DeserializeObject<Tabs>(retString);
    
        for (int i = 0; i < tabs.tabs.Count; i++)
        {
        
            tabsItems.Add(tabs.tabs[i]);
        }
        TabsItem recommendRoot = tabsItems[0];//推荐专区的
        string jsonPath = HttpUitls.GetJsonPath(tabUrl, recommendRoot.path);
        LauncherUIManager.instance.IniTopCulumn();
        StartCoroutine(Getjson(jsonPath, LoadRecommendPart));
    }

    void LoadRecommendPart()
    {
        ColumnRoot columnRoot = JsonConvert.DeserializeObject<ColumnRoot>(retString);

        leftRecoItem.contentId = columnRoot.data[0].contentId;
        leftRecoItem.title = columnRoot.data[0].title;
        leftRecoItem.subscript = columnRoot.data[0].subscript;
        leftRecoItem.cover = columnRoot.data[0].cover;
        leftRecoItem.clickType = columnRoot.data[0].clickType;
        leftRecoItem.clickParam = columnRoot.data[0].clickParam;
        
        childrenItems = columnRoot.children;
        LauncherUIManager.instance.IniRecommendContent();
    }
    public static List<CategorysItem> categorysItem = new List<CategorysItem>();
    public static List<ListDataItem> _4kDataItems = new List<ListDataItem>();
    /// <summary>
    /// 4k专区的数据加载
    /// </summary>
  public  void Load_4K()
    {
        Debug.Log("Load_4K" );
        TabsItem _4kRoot = tabsItems[1];
        string _4kPath = HttpUitls.GetJsonPath(tabUrl, _4kRoot.path);
      
        StartCoroutine(Getjson(_4kPath, Load4KPart));
    }
     void  Load4KPart()
    {
       // string _4kJson = HttpUitls.GetStr(_4kPath);//获取4K专区的内容
        AllRoot _4K = JsonConvert.DeserializeObject<AllRoot>(retString);
    //    Debug.Log("Count:" + _4K.categorys.Count );
        categorysItem = _4K.categorys;
        _4kDataItems = categorysItem[0].data;
        LauncherUIManager.instance.Set_4kItemDate();
    }

    public static List<ListDataItem> vrDataItems = new List<ListDataItem>();
    /// <summary>
    /// 巨幕的数据加载
    /// </summary>
    ///   public static List<ListDataItem> vrDataItems = new List<ListDataItem>();
    /// <summary>
    /// VR专区的数据加载
    /// </summary>
    /// 
    public void LoadVRPart()
    {

        TabsItem vrRoot = tabsItems[2];
         string vrPath = HttpUitls.GetJsonPath(tabUrl, vrRoot.path);
        StartCoroutine(Getjson(vrPath, LoadVR));
    }

    void LoadVR()
    {
        AllRoot vr = JsonConvert.DeserializeObject<AllRoot>(retString);
       
        CategorysItem vrCategorysItem = vr.categorys[0];
        vrDataItems = vrCategorysItem.data;
        LauncherUIManager.instance.SetVRItemDate();
    }

  
    public static List<ListDataItem> screenDataItems = new List<ListDataItem>();
    /// <summary>
    /// 巨幕的数据加载
    /// </summary>
    ///   public static List<ListDataItem> vrDataItems = new List<ListDataItem>();
    /// <summary>
    /// VR专区的数据加载
    /// </summary>
    /// 

    public void LoadGiantScreen()
    {

        TabsItem gsRoot = tabsItems[3];

        string gsPath = HttpUitls.GetJsonPath(tabUrl, gsRoot.path);
        StartCoroutine(Getjson(gsPath, LoadScreen));
    }

    void LoadScreen()
    {
        AllRoot giantScreen = JsonConvert.DeserializeObject<AllRoot>(retString);
        CategorysItem gsCategorysItem = giantScreen.categorys[0];
        screenDataItems = gsCategorysItem.data;
        LauncherUIManager.instance.SetScreenItemDate ();
    }

  


    public void LoadLivePart()
    {

        TabsItem liveRoot = tabsItems[4];//直播专区的
        string jsonLivePath = HttpUitls.GetJsonPath(tabUrl, liveRoot.path);
        StartCoroutine(Getjson(jsonLivePath, LoadLive));
    }

    void LoadLive()
    {
        Debug.Log("LoadLive");
       LiveRoot columnRoot = JsonConvert.DeserializeObject<LiveRoot>(retString);
        liveItems = columnRoot.data;
        Debug.Log("liveItems" + liveItems.Count );
       // Debug.Log("liveItems" + liveItems[14].title);
        liveTotalCount = liveItems.Count;
     
        LiveUImanager.instance.SetLiveItemDate();
        Debug.Log("LoadLive Over");
    }

    public void SetImage(string path, VideoItem item, RawImage image)
    {
        StartCoroutine(GetTexture(path, item, image));
    }
    IEnumerator GetTexture(string path, VideoItem item, RawImage image)
    {
       // Debug.Log(path);
        UnityWebRequest www = new UnityWebRequest(path);
        DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
        www.downloadHandler = texDl;
        yield return www.Send ();

        int width = 400;

        int height = 200;

       // Texture2D texture = new Texture2D(width, height);
       
        if (!www.isError) 
        {
            Texture2D tex = new Texture2D(width, height);
            tex = texDl.texture;
            image.texture = tex;
            item.coverTexture = tex;
        }

    }

//    int displayCount = 4;//一行显示的数量
     int pagesCount;//当前应当用到的页数
    //计算页数
 public int GetPages(ColumnType type )
    {
        switch (type)
        {
            case ColumnType.VR:
                pagesCount= GetPageCount(vrDataItems.Count,4 );
                break;
            case ColumnType._4K:
                pagesCount = GetPageCount(_4kDataItems.Count,4);

                break;
            case ColumnType.GiantScreen:
                pagesCount = GetPageCount(screenDataItems.Count,4);
                break;
            case ColumnType.Live:
                pagesCount = GetPageCount(liveItems.Count, 5);
            
                break;

        }
        return pagesCount;
    }
    int GetPageCount(int count,int displayCount)
    {
        int pages=0;
        if (count % displayCount == 0)
        {
            pages = count / displayCount;
            return pages;
        }
        else
        {
            pages = count / displayCount + 1;
            return pages;
        }

    }
   
      
 
}
