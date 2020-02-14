using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveUImanager : UIBase
{
    public static LiveUImanager instance;
    List<Button> liveItems = new List<Button>();
    //创建list存储不用每次都get了
    List<RawImage> liveRawImages = new List<RawImage>();
    List<Text> liveTitles = new List<Text>();
    Text livePageShowText;
    int currentPage = 1;//当前的页面
    int totalPages ;//当前的页面
    public Transform LiveRoot;
    Transform LiveItemCotent;
    Transform LiveNextPage;
    Transform LivePreviosPage;
    public void SetLiveItemDate()
    {
        totalPages = JsonDataManager.instance.GetPages(LauncherUIManager.instance.columnType);
        //字典存储每个item的信息
     
        LiveItemDisplay(0);
      }
    //给显示的item赋值的方法
   
     
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowLivePart :
                {

                    LiveRoot.gameObject.SetActive(true );
                    if (isLoad ==false )//加载过 后的不能重复赋值
                    {
                        int num = 0;
                        foreach (Button item in LiveItemCotent.GetComponentsInChildren<Button>())
                        {
                            liveItems.Add(item);
                            RawImage raw = item.GetComponent<RawImage>();
                            Text title = item.GetComponentInChildren<Text>();
                            liveRawImages.Add(raw);
                            liveTitles.Add(title);
                            item.onClick.AddListener(delegate() 
                            {
                                LiveItemClick(item .transform );
                            });
                             num++;
                        }

                        if (Application.platform==RuntimePlatform.Android )
                        {
                            JsonDataManager.instance.LoadLivePart();
                        }
                       
                    
                        isLoad = true;
                    }
                    
                }
                break;
            case (ushort)UIEvent.HideLivePart :

                {

                    LiveRoot.gameObject.SetActive(false);
                }
                break;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        msgids = new ushort[]
{
     (ushort )UIEvent .ShowLivePart ,
        (ushort )UIEvent .HideLivePart 
};
        RegistSelf(this, msgids);

    }

   
    //    Transform 
    // Transform LiveExitButton;
    bool isLoad=false;//表示是否初始化加载过了
    string jsonTxt;
    void Start()
    {
        LiveRoot = UISettingManager.GetUITransform("LiveRoot");
        LiveItemCotent = UISettingManager.GetUITransform("LiveItemCotent");
        livePageShowText = UISettingManager.GetUITransform("LivePageShowText").GetComponent<Text>();
        LiveNextPage = UISettingManager.GetUITransform("LiveNextPage");
        LivePreviosPage = UISettingManager.GetUITransform("LivePreviosPage");
     //   UISettingManager.AddButtonClickListener("LiveExitButton", ExitLive);
        UISettingManager.AddButtonClickListener("LiveNextPage", NextLivePage);
        UISettingManager.AddButtonClickListener("LivePreviosPage", PriviousLivePage);

        LiveRoot.gameObject.SetActive(false );
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader read = new StreamReader(Application.dataPath + "/Resources/select.json");

            jsonTxt = read.ReadToEnd();
        }
       
    }
  
    void LiveItemDisplay(int startIndex)//表示items从直播列表的哪一个索引值开始显示
    {
        Debug.Log("startIndext:" + startIndex);
        for (int i = 0; i < liveItems.Count; i++)
        {
            if (i < JsonDataManager.liveTotalCount)
            {
                liveTitles[i].text = JsonDataManager.liveItems[startIndex + i].title;
                liveItems[i].name = JsonDataManager.liveItems[startIndex + i].liveId;
                LiveItemData Items = new LiveItemData();
                Items.contentId = JsonDataManager.liveItems[startIndex+i].liveId;
                Items.title = JsonDataManager.liveItems[startIndex + i].title;
                Items.clickType = JsonDataManager.liveItems[startIndex + i].clickType;
                Items.clickParam = JsonDataManager.liveItems[startIndex + i].clickParam;

                if (!JsonDataManager.liceItemDic.ContainsKey(Items.contentId))
                {
                   // Debug.Log("Items.contentId:" + Items.contentId);
                    JsonDataManager.liceItemDic.Add(Items.contentId, Items);//加载图片完成后，加入字典。根据id添加
                }
                JsonDataManager.instance.SetImage(JsonDataManager.liveItems[startIndex+i].cover, Items, liveRawImages[i]);
            }
            else
            {
                liveTitles[i].text = "";
                liveItems[i].gameObject.SetActive(false);
            }

        }

        livePageShowText.text = currentPage + "/" + totalPages;

    }
    void ExitLive()
    {
        LiveRoot.gameObject.SetActive(false );
        SendMsg(new MsgBase ((ushort )UIEvent .ShowLauncher ));
        LauncherUIManager.instance.LauncherView.gameObject.SetActive(true );
    }
    void NextLivePage()
    {
        currentPage++;
        if (currentPage< totalPages|| currentPage == totalPages)
        {
              LiveItemDisplay((currentPage-1)*5);
         
        }
        else
        {
            UISettingManager.ShakeUI(LiveNextPage.transform, new Vector3(5f, 5f, 5f), 1);
            Debug.Log("已经不能向后翻页！");
           // currentPageIndex = TV189MsgReciver.totalPages;
            livePageShowText .text = currentPage + "/" + totalPages ;
            currentPage = totalPages;
        }
    }
    void PriviousLivePage()
    {
        currentPage--;

        if (currentPage >0)
        {
             LiveItemDisplay((currentPage - 1) * 5);
        }
        else
        {
            UISettingManager.ShakeUI(LivePreviosPage.transform, new Vector3(5f, 5f, 5f), 1);
            Debug.Log("已经不能向前翻页！");
            // currentPageIndex = TV189MsgReciver.totalPages;
            currentPage = 1;
            livePageShowText.text = currentPage + "/" + totalPages;
          
        }
    }
    bool isLiveLoad=false ;
    void LiveItemClick(Transform button)
    {

        LauncherUIManager.instance.panelBackStatus = PanelBackStatus.LivePlayer;
        //  LivePlayerManager.instance.LiveVideoCanvasRoot.gameObject.SetActive(true );
        if (Application.platform == RuntimePlatform.Android)
        {
            VideoItem data = JsonDataManager.liceItemDic[button.name];
            AndroidAPI.StartActivityForUnityTV189(data.contentId, data.clickType, data.clickParam, data.title, 0);
           // SceneManager.LoadScene("PlayerLive");


        }
        else
        {
            LivePlayDisplay();
            // SceneManager.LoadScene("PlayerLive", LoadSceneMode.Single);
            // mediaPlayerCtrl.Load(MediaPlayerCtrl.m_videoID);
        }
        

    }
    //点击播放直播按钮后的一些UI界面控制
  public void LivePlayDisplay()
    {

        LiveRoot.gameObject.SetActive(false);
        LivePlayerManager.instance.LiveVideoCanvasRoot.gameObject.SetActive(true);
        LivePlayerManager.instance.LiveVideoRoot.gameObject.SetActive(true);
        LightManger.instance.VideoScreenVR.SetActive(false);
        LightManger.instance.VideoScreen.SetActive(true);
        LauncherUIManager.instance.LauncherView.gameObject.SetActive(false);
        SendMsg(new MsgBase((ushort)UIEvent.HideBottomPart));

    }
}
