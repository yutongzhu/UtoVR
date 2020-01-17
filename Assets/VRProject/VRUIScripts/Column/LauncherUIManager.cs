using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum ColumnType
{
    RecommendVideo,
    VR,
    _4K,
    GiantScreen,
    Live
}
public class LauncherUIManager : UIBase
{

    public static LauncherUIManager instance;
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.HideLauncher:
                {
                    // Debug.Log("das");
                    LauncherView.gameObject.SetActive(false);
                }
                break;
            case (ushort)UIEvent.ShowLauncher:

                {
                    // Debug.Log("rrrr");


                    IniLauncer();
                }
                break;
        }
    }
    public ColumnType columnType;
    public Transform LauncherView;
    Transform recommendPart;
    Transform VideoListPartPanel;
    Transform recommendButton;

    private Transform backImage;
 
    Transform listContent;

    Transform downButton;
    Transform upButton;
    Transform arrowImage;
    Text seletedText;
    Transform leftListContent;
    int LeftPageIndex = 0;//表示左边显示区域当前的页面索引

    Transform topTabs;
    Transform RecommendConntent;
    Transform dataItem;
    //左边列表种的buttons
    List<Button> leftButtonItems = new List<Button>();
    public GameObject loadCanvas;
    private void Awake()
    {
        instance = this;
        msgids = new ushort[]
{

            (ushort )UIEvent .HideLauncher ,
             (ushort )UIEvent .ShowLauncher
};
        RegistSelf(this, msgids);

    }
    string jsonTxt="";
  //  string paths = Application.dataPath + "/Resources/select.json";
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main" )
        {
            SceneStatus.sceneEnum = SceneEnum.Main;
        }
        else if (SceneManager.GetActiveScene().name == "MainVR")
        {
            SceneStatus.sceneEnum = SceneEnum.MainVR;
        }
        this .Invoke("HideLoadCanvas",2);
        recommendPart = UIManager.instance.GetGameObject("RecommendPart").transform;

        IniLeftButtons();
        IniVideoItemPart();
        foreach (Button item in recommendPart.GetComponentsInChildren<Button>())
        {
            item.onClick.AddListener(delegate () { RecommendVideoClick(item); });
        }

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StreamReader read = new StreamReader(Application.dataPath + "/Resources/select.json");

            jsonTxt = read.ReadToEnd();
        }

    }
    void HideLoadCanvas()
    {

        loadCanvas.SetActive(false );
    }
    public void IniRecommendContent()
    {
        RecommendConntent = UISettingManager.GetUITransform("RecommendConntent");
        dataItem = UISettingManager.GetUITransform("DataItem");
        dataItem.Find("name").GetComponent<Text>().text = JsonDataManager.leftRecoItem.title;
        dataItem.name = JsonDataManager.leftRecoItem.contentId;
        ////把该item添加进去字典里
        VideoItem leftItems = new VideoItem();
        leftItems.contentId = JsonDataManager.leftRecoItem.contentId;
        leftItems.title = JsonDataManager.leftRecoItem.title;
        leftItems.subscript = JsonDataManager.leftRecoItem.subscript;

        if (!JsonDataManager.VideosDic.ContainsKey(leftItems.contentId))
        {
            JsonDataManager.VideosDic.Add(leftItems.contentId, leftItems);//加载图片完成后，加入字典。根据id添加
        }
        RawImage raw = dataItem.Find("poster").GetComponent<RawImage>();
        JsonDataManager.instance.SetImage(JsonDataManager.leftRecoItem.cover, leftItems, raw);


        int num = 0;
        foreach (Button item in RecommendConntent.GetComponentsInChildren<Button>())
        {
            item.transform.Find("name").GetComponent<Text>().text = JsonDataManager.childrenItems[num].title;
            RawImage rawIm = item.transform.Find("poster").GetComponent<RawImage>();
            item.name = JsonDataManager.childrenItems[num].contentId;//把每个item控件名称为id
            VideoItem Items = new VideoItem();
            Items.contentId = JsonDataManager.childrenItems[num].contentId;
            Items.title = JsonDataManager.childrenItems[num].title;
            Items.subscript = JsonDataManager.childrenItems[num].subscript;
            Items.clickType = JsonDataManager.childrenItems[num].clickType;
            Items.clickParam = JsonDataManager.childrenItems[num].clickParam;
            //Items.coverTexture = JsonDataManager.childrenItems[num].cover;
            if (!JsonDataManager.VideosDic.ContainsKey(Items.contentId))
            {
                //
              //  Debug.Log(Items.name);
                JsonDataManager.VideosDic.Add(Items.contentId, Items);//加载图片完成后，加入字典。根据id添加
            }
            JsonDataManager.instance.SetImage(JsonDataManager.childrenItems[num].cover, Items, rawIm);
            num++;
        }


    }
    void RecommendVideoClick(Button button)
    {
        JsonDataManager.currentId = button.name;
        SendMsg(new MsgBase((ushort)UIEvent.ShowVideoDetailRoot));
        SendMsg(new MsgBase((ushort)UIEvent.HideBottomPart));
        LauncherView.gameObject.SetActive(false);
    }
    public void IniLauncer()
    {
        LauncherView.gameObject.SetActive(true);
        VideoListPartPanel.gameObject.SetActive(false);
        recommendPart.gameObject.SetActive(true);
        backImage.position = recommendButton.position;
        columnType = ColumnType.RecommendVideo;

    }
    /// <summary>
    /// 上面不同栏目时候的初始化
    /// </summary>
    public void IniTopCulumn()
    {
        backImage = UIManager.instance.GetGameObject("BackImage").transform;
        topTabs = UIManager.instance.GetGameObject("TopTabs").transform;
        int index = 0;
        foreach (Button item in topTabs.GetComponentsInChildren<Button>())
        {
            item.onClick.AddListener(delegate () { ColumuOnClick(item.transform); });
            item.transform.Find("Text").GetComponent<Text>().text = JsonDataManager.tabsItems[index].name;
            index++;
        }
        LauncherView = UIManager.instance.GetGameObject("LauncherView").transform;
        recommendPart = UIManager.instance.GetGameObject("RecommendPart").transform;
        VideoListPartPanel = UIManager.instance.GetGameObject("VideoListPartPanel").transform;
        recommendButton = UIManager.instance.GetGameObject("RecommendButton").transform;

        VideoListPartPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 左边的按钮ui的初始化
    /// </summary>
    void IniLeftButtons()
    {
        leftListContent = UIManager.instance.GetGameObject("SecondListContent").transform;
        foreach (Button item in leftListContent.GetComponentsInChildren<Button>())
        {
            leftButtonItems.Add(item);
            item.onClick.AddListener(delegate () { LeftButtonOnClick(item.transform); });
        }

        downButton = UIManager.instance.GetGameObject("DownButton").transform;
        upButton = UIManager.instance.GetGameObject("UpButton").transform;
        arrowImage = UIManager.instance.GetGameObject("ArrowImage").transform;
        seletedText = UIManager.instance.GetGameObject("SelectedText").GetComponent<Text>();


        downButton.GetComponent<Button>().onClick.AddListener(DownButtonClick);
        upButton.GetComponent<Button>().onClick.AddListener(UpButtonClick);
    }
    /// <summary>
    /// 通过tag获取索引值
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    int GetTagIndex(string tag)
    {
        if (tag == "Zero")
        {
            return 0;
        }
        else if (tag == "One")
        {
            return 1;
        }
        else if (tag == "Two")
        {
            return 2;
        }
        else if (tag == "Three")
        {
            return 3;
        }
        return 0;
    }
    /// <summary>
    /// 左边list种列表button事件
    /// </summary>
    /// <param name="button"></param>
    string currentContentID;//当前点击筛选的id 防止重复调用
    void LeftButtonOnClick(Transform button)
    {
        Debug.Log("LeftButtonOnClick");
        //currentContentID
        Transform Image = button.Find("Image");
        Transform Text = button.Find("Text");
        arrowImage.parent = Image;
        arrowImage.localPosition = new Vector3(-133, 0, 0);
        seletedText.transform.parent = Text;
        seletedText.transform.position = Text.position;
        seletedText.text = Text.GetComponent<Text>().text;

        currentContentID = button.name;
        currentPageIndex = 1;//把当前的页面索引置为1
                           

        if (Application.platform == RuntimePlatform.Android)
        {
          
            Debug .Log("clickParam:" + button.name);
          
            AndroidAPI.GetCurrentPagesVideos(button.name, currentPageIndex, 4);
          
        }
        else
        {
            TV189MsgReciver.instance.GetVideosJson(jsonTxt);
        }

      
    }
  
    bool is4KLoaded = false;
    bool isVrLoaded = false;
    bool isGiantLoaded = false;
    /// <summary>
    /// 上面不同的栏目点击事件
    /// </summary>
    /// <param name="columnButton"></param>
    void ColumuOnClick(Transform columnButton)
    {
        backImage.position = columnButton.position;
        currentPageIndex = 1;
        if (columnButton.name == "RecommendButton")
        {
            VideoListPartPanel.gameObject.SetActive(false);
            recommendPart.gameObject.SetActive(true);
            SendMsg(new MsgBase((ushort)UIEvent.HideLivePart));
            columnType = ColumnType.RecommendVideo;
        }
        else if (columnButton.name == "LiveButton")
        {

            columnType = ColumnType.Live;
            SendMsg(new MsgBase((ushort)UIEvent.ShowLivePart));
            SendMsg(new MsgBase((ushort)UIEvent.HideVideoDetailRoot));
            //LauncherView.gameObject.SetActive(false);
            recommendPart.gameObject.SetActive(false );
            VideoListPartPanel.gameObject.SetActive(false);
        }
        else
        {
            SendMsg(new MsgBase((ushort)UIEvent.HideLivePart));
            //if (Application.platform == RuntimePlatform.WindowsEditor)
            //{
            //    TV189MsgReciver.instance.GetVideosJson(jsonTxt);
            //}
         
           

            LeftPageIndex = 0;
            VideoListPartPanel.gameObject.SetActive(true);
            recommendPart.gameObject.SetActive(false);
            if (columnButton.name == "VRButton")
            {
                columnType = ColumnType.VR;
             
                if (isVrLoaded == false)
                {
                    JsonDataManager.instance .LoadVRPart();
                    isVrLoaded = true;
                }
                else
                {
                    SetVRItemDate();
                }
              

            }
            else if (columnButton.name == "4KButton")
            {
                columnType = ColumnType._4K;
                if (is4KLoaded == false)
                {
                    JsonDataManager.instance .Load_4K();
                    is4KLoaded = true;
                }
                else
                {
                    Set_4kItemDate();
                }


            }
            else if (columnButton.name == "GiantscreenButton")
            {
                columnType = ColumnType.GiantScreen;
               
                if (isGiantLoaded == false)
                {
                    JsonDataManager .instance .LoadGiantScreen();
                    isGiantLoaded = true;
                }
                else
                {
                    SetScreenItemDate();
                }

            }
          
        }

    }
    void IniSelectItem()
    {
        Transform Image = leftButtonItems[0].transform.Find("Image");
        arrowImage.SetParent(Image);// = Image;
        arrowImage.localPosition = new Vector3(-133, 0, 0);
        Text titles = leftButtonItems[0].transform.Find("Text").GetComponent<Text>();
        seletedText.transform.SetParent(titles.transform);// = titles.transform;
        seletedText.transform.localPosition = new Vector3(0, 0, 0); ;
        seletedText.text = titles.text;
    }
    public void Set_4kItemDate()
    {
    //    seletedText.
      
        for (int i = 0; i < leftButtonItems.Count; i++)
        {

            Text title = leftButtonItems[i].transform.Find("Text").GetComponent<Text>();
            title.text = JsonDataManager._4kDataItems[i].title;
            leftButtonItems[i].name = JsonDataManager._4kDataItems[i].clickParam;

        }
        IniSelectItem();
        currentContentID = leftButtonItems[0].name;
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidAPI.GetCurrentPagesVideos(leftButtonItems[0].name, 1, 4);
        }
    }

    public void SetVRItemDate()
    {
    //    Debug.Log(JsonDataManager.vrDataItems.Count );
        for (int i = 0; i < leftButtonItems.Count; i++)
        {
            Text title = leftButtonItems[i].transform.Find("Text").GetComponent<Text>();
            title.text = JsonDataManager.vrDataItems[i].title;
            leftButtonItems[i].name = JsonDataManager.vrDataItems[i].clickParam;//后面会根据名子区别每个item
        }
        IniSelectItem();//用于选中时候，选中UI的显示位置方法
        currentContentID = leftButtonItems[0].name;
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidAPI.GetCurrentPagesVideos(leftButtonItems[0].name, 1, 4);
        }
        else
        {
            TV189MsgReciver.instance.GetVideosJson(jsonTxt);
        }

    }
    public void SetScreenItemDate()
    {
        for (int i = 0; i < leftButtonItems.Count; i++)
        {
            Text title = leftButtonItems[i].transform.Find("Text").GetComponent<Text>();
            title.text = JsonDataManager.screenDataItems[i].title;
            leftButtonItems[i].name = JsonDataManager.screenDataItems[i].clickParam;
        }
        IniSelectItem();
        currentContentID = leftButtonItems[0].name;
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidAPI.GetCurrentPagesVideos(leftButtonItems[0].name, 1, 4);
        }
    }


    int vrPageIndex = 1;//当前的页面索引
    int gsPageIndex = 1;//当前的页面索引
    int _4kPageIndex = 1;//当前的页面索引
    void UpButtonClick()
    {


        switch (columnType)
        {
            case ColumnType.RecommendVideo:
                break;
            case ColumnType.VR:
               
                int vrTatalCount = JsonDataManager.vrDataItems.Count;//总共数量
                if (vrPageIndex>1)
                {
                    vrPageIndex--;
                    int previousCount = (vrPageIndex-1 )* 4;//此时已经显示过的数量


                    for (int i = 0; i < 4; i++)
                    {
                        leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                           = JsonDataManager.vrDataItems[previousCount+i].title;
                    }
                    seletedText.text = leftButtonItems[0].transform.Find("Text").GetComponent<Text>().text;
                }
                else
                {
                    UISettingManager.ShakeUI(upButton.transform, new Vector3(5f, 5f, 5f), 1);
                    Debug.Log("已经不能向前翻页！");
                    vrPageIndex = 1;
                }
            
                break;
            case ColumnType.GiantScreen:
                int gsTatalCount = JsonDataManager.screenDataItems .Count;//总共数量
                if (gsPageIndex > 1)
                {
                    gsPageIndex--;
                    int previousCount = (gsPageIndex - 1) * 4;//此时已经显示过的数量


                    for (int i = 0; i < 4; i++)
                    {
                        leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                           = JsonDataManager.screenDataItems[previousCount + i].title;
                    }
                    seletedText.text = leftButtonItems[0].transform.Find("Text").GetComponent<Text>().text;
                }
                else
                {
                    UISettingManager.ShakeUI(upButton.transform, new Vector3(5f, 5f, 5f), 1);
                    Debug.Log("已经不能向前翻页！");
                    gsPageIndex = 1;
                }
                break;
            case ColumnType._4K :
                int _4kTatalCount = JsonDataManager._4kDataItems.Count;//总共数量
                if (_4kPageIndex > 1)
                {
                    _4kPageIndex--;
                    int previousCount = (_4kPageIndex - 1) * 4;//此时已经显示过的数量


                    for (int i = 0; i < 4; i++)
                    {
                        leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                           = JsonDataManager._4kDataItems[previousCount + i].title;
                    }
                }
                else
                {
                    UISettingManager.ShakeUI(upButton.transform, new Vector3(5f, 5f, 5f), 1);
                    Debug.Log("已经不能向前翻页！");
                    _4kPageIndex = 1;
                }

                break;
            default:
                break;
        }



    }
    void DownButtonClick()
    {
        switch (columnType)
        {
            case ColumnType.RecommendVideo:
                break;
            case ColumnType.VR:
                 int pages=    JsonDataManager.instance.GetPages(columnType);
                int tatalCount = JsonDataManager.vrDataItems.Count;//总共数量
                int previousCount = vrPageIndex * 4;//此时已经显示过的数量

                int leftCount = tatalCount - previousCount;//还剩余的数量

                if (previousCount < tatalCount)//保证显示的数量不能超过总的数量
                {
                    if (leftCount < 4)//当node数量小于显示的行数
                    {


                        for (int i = 0; i < 4; i++)
                        {
                            if (i < leftCount)
                            {
                                leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                               = JsonDataManager.vrDataItems[i].title;
                            }
                            else
                            {
                                leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                             = "";

                            }

                        }
                       // seletedText.text = leftButtonItems[0].transform.Find("Text").GetComponent<Text>().text;
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            
                            leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                               = JsonDataManager.vrDataItems[vrPageIndex*4+i].title;
                        }
                       
                    }
                    
                    vrPageIndex++;
                }

                else
                {

                    UISettingManager.ShakeUI(downButton.transform, new Vector3(5f, 5f, 5f), 1);
                    vrPageIndex = JsonDataManager.instance.GetPages(columnType);
                    Debug.Log("已经不能向后翻页！");
                }

                IniSelectItem();

                break;
            case ColumnType.GiantScreen:
                int gsTatalCount = JsonDataManager.screenDataItems.Count;//总共数量
             //   Debug.Log("gsTatalCount" + gsTatalCount);
                int gsPreviousCount = gsPageIndex * 4;//此时已经显示过的数量

                int gsLeftCount = gsTatalCount - gsPreviousCount;//还剩余的数量

                if (gsPreviousCount < gsTatalCount)//保证显示的数量不能超过总的数量
                {
                    if (gsLeftCount < 4)//当node数量小于显示的行数
                    {


                        for (int i = 0; i < 4; i++)
                        {
                            if (i < gsLeftCount)
                            {
                                leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                               = JsonDataManager.screenDataItems[i].title;
                            }
                            else
                            {
                                leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                             = "";

                            }

                        }

                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                               = JsonDataManager.screenDataItems[gsPageIndex*4+i].title;
                        }
                    }
                    gsPageIndex++;
                }
                else
                {
                    UISettingManager.ShakeUI(downButton.transform, new Vector3(5f, 5f, 5f), 1);
                    gsPageIndex = JsonDataManager.instance.GetPages(columnType);
                    Debug.Log("已经不能向后翻页！");
                }

                IniSelectItem();
                break;
            case ColumnType._4K:
                int _4kTatalCount = JsonDataManager._4kDataItems.Count;//总共数量
                int _4kPreviousCount = _4kPageIndex * 4;//此时已经显示过的数量

                int _4kLeftCount = _4kTatalCount - _4kPreviousCount;//还剩余的数量

                if (_4kPreviousCount < _4kTatalCount)//保证显示的数量不能超过总的数量
                {
                    if (_4kLeftCount < 4)//当node数量小于显示的行数
                    {


                        for (int i = 0; i < 4; i++)
                        {
                            if (i < _4kLeftCount)
                            {
                                leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                               = JsonDataManager._4kDataItems[i].title;
                            }
                            else
                            {
                                leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                             = "";

                            }

                        }

                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            leftButtonItems[i].transform.Find("Text").GetComponent<Text>().text
                               = JsonDataManager._4kDataItems[_4kPageIndex * 4 + i].title;
                        }
                    }
                    _4kPageIndex++;
                }
                else
                {
                    UISettingManager.ShakeUI(downButton.transform, new Vector3(5f, 5f, 5f), 1);
                    _4kPageIndex = JsonDataManager.instance.GetPages(columnType);
                    Debug.Log("已经不能向后翻页！");
                }

                IniSelectItem();
                break;
            default:
                break;
        }

    }
   
    

    #region 中间具体视频显示的一些操作
    Button PreviosPageButton;
    Button NextPagButton;
   public  Text pageShowText;
    public List<Button> videoButtonList = new List<Button>();

    Transform videoItemCotent;
 public    int currentPageIndex =1;//具体视频列表的页面索引
   
    void IniVideoItemPart()
    {
        PreviosPageButton = UIManager.instance.GetGameObject("PreviosPageButton").GetComponent<Button>();
        NextPagButton = UIManager.instance.GetGameObject("NextPagButton").GetComponent<Button>();
        pageShowText = UIManager.instance.GetGameObject("PageShowText").GetComponent<Text>();
        NextPagButton.onClick.AddListener(NextPageClick);
        PreviosPageButton.onClick.AddListener(PriviousPageClick);
        videoItemCotent = UIManager.instance.GetGameObject("PanelVideoItemCotent").transform;

        foreach (Button item in videoItemCotent.GetComponentsInChildren<Button>())
        {
            item.onClick.AddListener( delegate() { ItemOnClick(item.transform ); }    );
            videoButtonList.Add(item );
        }
    
    }

    void PriviousPageClick()
    {
        currentPageIndex--;
        if (currentPageIndex>0)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                Debug.Log("currentContentID:" + currentContentID);
                //调用安卓那边的方法，json会回调返回
                AndroidAPI.GetCurrentPagesVideos(currentContentID, currentPageIndex, 4);
             
            }
            else
            {
              
                Debug.Log("currentContentID:" + currentContentID);
                TV189MsgReciver.instance.GetVideosJson(jsonTxt);
            }

        }
        else
        {
            UISettingManager.ShakeUI(PreviosPageButton .transform, new Vector3(5f, 5f, 5f), 1);
            Debug.Log("已经不能向前翻页！");
            // vrPageIndex = 1;
            currentPageIndex = 1;
            pageShowText.text = currentPageIndex + "/" + TV189MsgReciver.totalPages;
        }
        
    }
    void NextPageClick()
    {
        currentPageIndex++;
        if (Application.platform == RuntimePlatform.Android)
        {
            if (currentPageIndex<TV189MsgReciver .totalPages || currentPageIndex == TV189MsgReciver.totalPages)
            {
                Debug.Log("currentContentID:"+ currentContentID);
                //调用安卓那边的方法，json会回调返回
                AndroidAPI.GetCurrentPagesVideos(currentContentID, currentPageIndex, 4);
            }
            else
            {
                UISettingManager.ShakeUI(NextPagButton.transform, new Vector3(5f, 5f, 5f), 1);
                     Debug.Log("已经不能向后翻页！");
                currentPageIndex = TV189MsgReciver.totalPages;
                pageShowText.text = currentPageIndex + "/" + TV189MsgReciver.totalPages;
            }
          
        }
        else
        {

            Debug.Log("currentContentID:" + currentContentID);
            TV189MsgReciver.instance.GetVideosJson(jsonTxt);
        }
       
    
    }
    void ItemOnClick(Transform itemButton)
    {
       
        LauncherView.gameObject.SetActive(false);

    
      JsonDataManager .currentId = itemButton.name;
        MsgBase msg = new MsgBase((ushort)UIEvent.ShowVideoDetailRoot);
        SendMsg(msg);
        SendMsg(new MsgBase((ushort)UIEvent.HideBottomPart ));
    }
   
  
    #endregion
}
