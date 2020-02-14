using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoControl : UIBase
{
    public static VideoControl instance;
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.HideVideoDetailRoot:
                {

                    VideoDetailRoot.gameObject.SetActive(false);
                }
                break;
            case (ushort)UIEvent.ShowVideoDetailRoot:

                {
                    Debug.Log("ShowVideoDetailRoot");
                    if (LauncherUIManager.instance.columnType == ColumnType.RecommendVideo)
                    {

                        VideoPoster.texture =
                        JsonDataManager.VideosDic[JsonDataManager.currentId].coverTexture;
                        VideoDetailName.text =
                            JsonDataManager.VideosDic[JsonDataManager.currentId].title;
                        IntroduceText.text = JsonDataManager.VideosDic[JsonDataManager.currentId].subscript;

                        CountryName.text = "";
                        //表示该视频是否被收藏了

                    }
                    else
                    {

                        ClassifyVideoItem item = TV189MsgReciver.classifyVideosDic[JsonDataManager.currentId];
                        TV189MsgReciver.instance.SetImage(item.imgM0, VideoPoster);

                        VideoDetailName.text = item.title;
                        IntroduceText.text = item.description;
                        CountryName.text = item.countryName;

                    }
                    VideoDetailRoot.gameObject.SetActive(true);

                    if (JsonDataManager.VideosDic[JsonDataManager.currentId].isCollected == false)
                    {
                        CollectionButton.GetComponent<Image>().sprite = NormalCollectedSprite;
                    }
                    else
                    {
                        CollectionButton.GetComponent<Image>().sprite = CollectedSprite;
                    }


                }
                break;
        }
    }
    public void ShowDetailFun()
    {
        if (LauncherUIManager.instance.columnType == ColumnType.RecommendVideo)
        {

            VideoPoster.texture =
            JsonDataManager.VideosDic[JsonDataManager.currentId].coverTexture;
            VideoDetailName.text =
                JsonDataManager.VideosDic[JsonDataManager.currentId].title;
            IntroduceText.text = JsonDataManager.VideosDic[JsonDataManager.currentId].subscript;

            CountryName.text = "";
            //表示该视频是否被收藏了

        }
        else
        {

            ClassifyVideoItem item = TV189MsgReciver.classifyVideosDic[JsonDataManager.currentId];
            TV189MsgReciver.instance.SetImage(item.imgM0, VideoPoster);

            VideoDetailName.text = item.title;
            IntroduceText.text = item.description;
            CountryName.text = item.countryName;

        }
        VideoDetailRoot.gameObject.SetActive(true);

        if (JsonDataManager.VideosDic[JsonDataManager.currentId].isCollected == false)
        {
            CollectionButton.GetComponent<Image>().sprite = NormalCollectedSprite;
        }
        else
        {
            CollectionButton.GetComponent<Image>().sprite = CollectedSprite;
        }

    }
   // public GameObject videoPlay;
    //public MediaPlayerCtrl mediaPlayerCtrl;
    public Sprite CollectedSprite;
    public Sprite NormalCollectedSprite;
   public  Transform VideoDetailRoot;
    Transform CollectionButton;
    Transform BackLauncherButton;
    Transform PlayVidButton;
    Text CountryName;
    Text VideoDetailName;
    Transform DateInfo;
    Transform TypeInfo;
    Text IntroduceText;
    RawImage VideoPoster;
    Image collectImage;
    string collectNormPath = "Collection/collection_normal";
    string collectedPath = "Collection/collection_complete";
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
     (ushort )UIEvent .HideVideoDetailRoot,
        (ushort )UIEvent .ShowVideoDetailRoot
};
        RegistSelf(this, msgids);

    }
    // Start is called before the first frame update
    void Start()
    {
        Initialized();
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidAPI.Init();
        }
   
    }
    private void Initialized()
    {
        VideoDetailRoot = UIManager.instance.GetGameObject("VideoDetailRoot").transform;
        VideoPoster= UISettingManager.GetUITransform("VideoPoster").GetComponent <RawImage >();
        CollectionButton = UISettingManager.GetUITransform("CollectionButton");
        BackLauncherButton = UISettingManager.GetUITransform("BackLauncherButton");
        PlayVidButton = UISettingManager.GetUITransform("PlayVidButton");
        VideoDetailName = UISettingManager.GetUITransform("VideoDetailName").GetComponent <Text >();
        CountryName= UISettingManager.GetUITransform("CountryName").GetComponent<Text>(); ;
        IntroduceText = UISettingManager.GetUITransform("IntroduceText").GetComponent <Text >();
        UISettingManager.AddButtonClickListener(PlayVidButton.gameObject, PlayVideoClick);
        UISettingManager.AddButtonClickListener(CollectionButton.gameObject, CollectButtonClick);
        UISettingManager.AddButtonClickListener(BackLauncherButton.gameObject, BackLauncherClick);
        collectImage = CollectionButton.GetComponent<Image>();
    
          VideoDetailRoot.gameObject.SetActive(false);
      
       
    }
    bool isScreenLoaded=false;
    private void PlayVideoClick()
    {
      //  if (Application.platform == RuntimePlatform.WindowsEditor)
      //  {
            Transform VideoPlayFather = GameObject.Find("VideoPlayFather").transform;
            //处于哪种场景状态也更新
            if (SceneManager.GetActiveScene().name == "Main")
            {
                SceneStatus.sceneEnum = SceneEnum.Main;

                TV189MsgReciver.instance.mediaPlayerCtrl =
                    VideoPlayFather.Find("Cinema/VideoScreen").GetComponent<MediaPlayerCtrl>();
            }
            else
            {
                SceneStatus.sceneEnum = SceneEnum.MainVR;
                if (LauncherUIManager.instance.columnType == ColumnType.VR)//vr播放情况
                {
                    TV189MsgReciver.instance.mediaPlayerCtrl =
                 VideoPlayFather.Find("VideoScreenVR").GetComponent<MediaPlayerCtrl>();
                }
                else
                {
                    TV189MsgReciver.instance.mediaPlayerCtrl =
                 VideoPlayFather.Find("Cinema/VideoScreen").GetComponent<MediaPlayerCtrl>();
                }

           }


       // }
       
        if (Application.platform == RuntimePlatform.WindowsEditor )
        {
            TV189MsgReciver.instance.mediaPlayerCtrl.Load(MediaPlayerCtrl.m_videoID);

        }
           
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("no intent content");
            //GameObject.Find("Directional Light").GetComponent<Main>().toast("网络无连接");
            return;
        }
        if (LauncherUIManager.instance.columnType == ColumnType.RecommendVideo)
        {
                VideoItem data = JsonDataManager.VideosDic[JsonDataManager.currentId];
                Debug.Log("data.clickParam:"+ data.clickParam);
                if (Application.platform == RuntimePlatform.Android)
                {
                   AndroidAPI.StartActivityForUnityTV189(data.contentId, data.clickType, data.clickParam, data.title, 0);
                  
                }
            VideoDetailRoot.gameObject.SetActive(false);
            

            if (SceneManager.GetActiveScene().name == "MainVR")
            {
                LightManger.instance.VideoCanvasRootVR.SetActive(false);
                LightManger.instance.VideoScreen.SetActive(true);
                LightManger.instance.VideoScreenVR.SetActive(false);
                LightManger.instance.VideoCanvasRoot.SetActive(true);
            }
            else
            {
                Transform videoCanvas = GameObject.Find("UI/VideoCanvas").transform;
                videoCanvas.Find("VideoCanvasRoot").gameObject.SetActive(true );
              //  VideoUImanager.instance.VideoCanvasRoot.SetActive(true);
                LightManger.instance.VideoScreenVR.SetActive(false);
                LightManger.instance.VideoScreen.SetActive(true);
            }

        }
        else if (LauncherUIManager.instance.columnType == ColumnType.VR)
        {
            Debug.Log(" ColumnType.VR");
            VideoDetailRoot.gameObject.SetActive(false);
            ClassifyVideoItem item = TV189MsgReciver.classifyVideosDic[JsonDataManager.currentId];

            if (Application.platform == RuntimePlatform.Android)
            {
                int ClickParam = TV189MsgReciver.getClickParam(int.Parse(item.categoryId), int.Parse(item.contentType));

                AndroidAPI.StartActivityForUnityTV189(item.contentId, 0, "7", item.title, 0);
            }
            else//在window平台
            {
                //在vr界面
                if (SceneManager .GetActiveScene ().name =="MainVR")
                {
                  
                        LightManger.instance.VideoCanvasRootVR.SetActive(true);
                        LightManger.instance.VideoScreen.SetActive(false);
                        LightManger.instance.VideoScreenVR.SetActive(true);
                        LightManger.instance.VideoCanvasRoot.SetActive(false);
                       
                   
                }
                else
                {
                    SceneManager.LoadScene("PlayerVR");
                }
              

               
              
            }

           // VideoDetailRoot.gameObject.SetActive(false);

        }
       
        else
        {
          
               
                if (Application.platform == RuntimePlatform.Android)
                {
                   
                    ClassifyVideoItem item = TV189MsgReciver.classifyVideosDic[JsonDataManager.currentId];
                    int ClickParam = TV189MsgReciver.getClickParam(int .Parse (item.categoryId), int.Parse(item.contentType));
                    
                    AndroidAPI.StartActivityForUnityTV189(item.contentId, 0, ClickParam.ToString (), item.title, 0);
                 
                    //return;
                }
          

            VideoDetailRoot.gameObject.SetActive(false);
            GameObject.Find("VideoCanvas").transform.Find("VideoCanvasRoot").gameObject.SetActive(true);

            LightManger.instance.VideoScreenVR.SetActive(false);
            LightManger.instance.VideoScreen.SetActive(true);
        }
    //    VideoDetailRoot.gameObject.SetActive(false);
    }
    bool isCollected = false;//表示此时是否收藏
    /// <summary>
    /// 点击收藏按钮的事件
    /// </summary>
    private void CollectButtonClick()
    {
        if (JsonDataManager.VideosDic[JsonDataManager.currentId].isCollected == false)
        {
            collectImage.sprite = CollectedSprite;
            JsonDataManager.VideosDic[JsonDataManager.currentId].isCollected = true;
            //把当前的海报也存在字典里
            JsonDataManager.VideosDic[JsonDataManager.currentId].coverTexture = VideoPoster.texture;


            //添加进去收藏列表
            JsonDataManager.collectedVideos.Add(JsonDataManager.VideosDic[JsonDataManager.currentId]);
        }
        else
        {
            collectImage.sprite = NormalCollectedSprite;
            JsonDataManager.VideosDic[JsonDataManager.currentId].isCollected = false;
           
        }
    }
    /// <summary>
    /// 返回主界面
    /// </summary>
    public  void BackLauncherClick()
    {

    
        LauncherUIManager.instance.LauncherView.gameObject .SetActive (true );
        VideoDetailRoot.gameObject.SetActive(false );
       // SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart ));
      //  BottomManager.instance.BottomButtonContent.gameObject.SetActive(false);
        //  SendMsg(new MsgBase((ushort)UIEvent.ShowLauncher));
        BottomManager.instance.BottomButtonContent.gameObject.SetActive(true );
    }
}
