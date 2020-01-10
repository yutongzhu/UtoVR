using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoControl : UIBase
{
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
                    if (LauncherUIManager.instance .columnType==ColumnType.RecommendVideo)
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
                        TV189MsgReciver.instance.SetImage (item.imgM0 , VideoPoster);

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
    public Sprite CollectedSprite;
    public Sprite NormalCollectedSprite;
    Transform VideoDetailRoot;
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
    private void PlayVideoClick()
    {
        VideoDetailRoot.gameObject.SetActive(false);
        if (LauncherUIManager.instance.columnType == ColumnType.RecommendVideo)
        {
          
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("no intent content");
                //GameObject.Find("Directional Light").GetComponent<Main>().toast("网络无连接");

            }
            else
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    VideoItem data = JsonDataManager.VideosDic[JsonDataManager.currentId];

                    AndroidAPI.StartActivityForUnityTV189(data.contentId, data.clickType, data.clickParam, data.title, 0);
                    //  UntiyPlayer
                    return;
                }
                else
                {
                    if (SceneManager.GetActiveScene().name == "MainVR")
                    {
                        SceneManager.LoadScene("Player2DVR", LoadSceneMode.Single);

                    }
                    else if (SceneManager.GetActiveScene().name == "Main")
                    {
                        SceneManager.LoadScene("Player2D", LoadSceneMode.Single);
                    }
                   // SceneManager.LoadScene("Player2D", LoadSceneMode.Single);
                }
            }
        }
        else
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("no intent content");
              

            }
            else
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                   
                    ClassifyVideoItem item = TV189MsgReciver.classifyVideosDic[JsonDataManager.currentId];
                    int ClickParam = TV189MsgReciver.getClickParam(int .Parse (item.categoryId), int.Parse(item.contentType));
                    
                    AndroidAPI.StartActivityForUnityTV189(item.contentId, 0, ClickParam.ToString (), item.title, 0);
                 
                    return;
                }
                else
                {
                    if (SceneManager.GetActiveScene().name == "MainVR")
                    {
                        SceneManager.LoadScene("Player2DVR", LoadSceneMode.Single);

                    }
                    else if (SceneManager.GetActiveScene().name == "Main")
                    {
                        SceneManager.LoadScene("Player2D", LoadSceneMode.Single);
                    }
                }
            }
        }
      
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
    private void BackLauncherClick()
    {

        MsgBase msg = new MsgBase((ushort)UIEvent.ShowLauncher );
        SendMsg(msg);
       // LauncherUIManager.instance.IniLauncer();
        VideoDetailRoot.gameObject.SetActive(false );
        SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart ));
        SendMsg(new MsgBase((ushort)UIEvent.ShowLauncher));
    }
}
