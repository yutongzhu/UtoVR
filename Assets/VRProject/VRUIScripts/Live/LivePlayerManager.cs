using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum LiveVideoStatus
{
    Standard,
    HD,
    VeryHD,
    _1080P
}
public class LivePlayerManager : UIBase {
    public MediaPlayerCtrl mediaPlayerCtrl;
    Slider LiveVolumeSlider;
    public LiveVideoStatus liveVideoStatus;
    Transform LiveLevelControl;
    public   Transform LiveVideoRoot;
    public Transform LiveVideoCanvasRoot;
    public static LivePlayerManager instance;
    public List<Transform> liveStatusList = new List<Transform>();
    public Text LiveStatusText;
    void Awake()
    {
        instance = this;
        msgids = new ushort[]
{
     (ushort )UIEvent .ShowLivePart ,
        (ushort )UIEvent .HideLivePart
};
        RegistSelf(this, msgids);
    }
    void Start () {

        LiveVideoCanvasRoot = UISettingManager.GetUITransform("LiveVideoCanvasRoot");
        LiveVideoRoot = UISettingManager.GetUITransform("LiveVideoRoot");
        LiveStatusText= UISettingManager.GetUITransform("LiveStatusText").GetComponent <Text >();
        UISettingManager.AddButtonClickListener("LiveBackBtn", BackMainClick);
       // UISettingManager.AddButtonClickListener("ShowBtn", LiveAdjustPanel);
        UISettingManager.AddButtonClickListener("LivePlayButton", LivePlayButtonClick);
        LiveVolumeSlider = UISettingManager.GetUITransform("LiveVolumeSlider").GetComponent<Slider>();
        UISettingManager.AddButtonClickListener("LiveVolumeButton", SetVolumnPanel);
        UISettingManager.AddButtonClickListener("LiveVideoStatusBt", LiveAdjustPanel);
        LiveVolumeSlider.gameObject.SetActive(false );
        LiveLevelControl = UISettingManager.GetUITransform("LiveLevelControl");
        foreach (Button item in LiveLevelControl.GetComponentsInChildren <Button >())
        {
            liveStatusList.Add(item.transform );
            item.onClick.AddListener(delegate() { ChangeLiveStatus(item.transform); });
        }
        LiveLevelControl.gameObject.SetActive(false );

        LiveVideoRoot.gameObject.SetActive(false);
        LiveVideoCanvasRoot.gameObject.SetActive(false );

        if (Application.platform==RuntimePlatform.Android )
        {
            for (int i = 0; i < liveStatusList.Count; i++)
            {
                if (i < TV189MsgReciver.liveDataList.Count)
                {
                    liveStatusList[i].Find("Text").
                         GetComponent<Text>().text = TV189MsgReciver.liveDataList[i].qualityName;
                    liveStatusList[i].name = TV189MsgReciver.liveDataList[i].qualityid;
                }
                else
                {
                    liveStatusList[i].gameObject.SetActive(false);
                }
            }
            LiveStatusText.text = TV189MsgReciver.liveDataList[0].qualityName;
        }
            
        //if (Application.platform==RuntimePlatform.Android )
        //{
         
        //}
        
    }
    //显示码率切换界面
    bool adjustPanelEnable;
    void LiveAdjustPanel()
    {
        if (adjustPanelEnable==false)
        {
            LiveLevelControl.gameObject.SetActive(true);
            adjustPanelEnable = true;
        }
        else
        {
            LiveLevelControl.gameObject.SetActive(false);
            adjustPanelEnable = false ;
        }

    }
    bool volumnEnable;
    void SetVolumnPanel()
    {
        if (volumnEnable == false)
        {

            LiveVolumeSlider.gameObject.SetActive(true);
            volumnEnable = true;
        }
        else
        {
            LiveVolumeSlider.gameObject.SetActive(false);
            volumnEnable = false;
        }
    }
    /// <summary>
    /// 返回主页面
    /// </summary>
 public  void BackMainClick()
    {
        Debug.Log("BackMainClick");
        Transform VideoPlayFather = GameObject.Find("VideoPlayFather").transform;
        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "MainVR")
        {
            MediaPlayerCtrl mediaPlayerCtrl =
           VideoPlayFather.Find("Cinema/VideoScreen").GetComponent<MediaPlayerCtrl>();
            mediaPlayerCtrl.Stop();

        }

        LiveVideoCanvasRoot.gameObject.SetActive(false);
       LiveVideoRoot.gameObject.SetActive(false);
        LightManger.instance.VideoScreenVR.SetActive(false);
        LightManger.instance.VideoScreen.SetActive(false);
        LauncherUIManager.instance.LauncherView.gameObject.SetActive(true);
        SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart));
        LiveVolumeSlider.gameObject.SetActive(false);
        LiveUImanager.instance.LiveRoot.gameObject.SetActive(true );
    }
    
   
   
 
    bool isPlay=true;
    void LivePlayButtonClick()
    {
        if (isPlay)
        {
            mediaPlayerCtrl.Pause();
            isPlay = false;
        }
        else
        {
            mediaPlayerCtrl.UnLoad ();
            mediaPlayerCtrl.Load(MediaPlayerCtrl.m_videoID);
            isPlay = true;
        }
    }
    void ChangeLiveStatus(Transform button)
    {
        Debug.Log("ChangeLiveStatus");
        LiveLevelControl.gameObject.SetActive(false);
        if (Application.platform ==RuntimePlatform.Android)
        {
            //获取当前url
            string url = TV189MsgReciver.liveUrlDic[button.name].playUrl;
            button.Find("Text").GetComponent<Text>().text = TV189MsgReciver.liveUrlDic[button.name].qualityName;

            LiveStatusText.text = TV189MsgReciver.liveUrlDic[button.name].qualityName; ;
            MediaPlayerCtrl.m_videoID = url;
            MediaPlayerCtrl.is3dUD = false;
            mediaPlayerCtrl.UnLoad();
            mediaPlayerCtrl.Load(MediaPlayerCtrl.m_videoID);
        }
        else
        {

            string currentUrl = TV189MsgReciver.liveUrlDic[button.name].playUrl;
            button.Find("Text").GetComponent<Text>().text = TV189MsgReciver.liveUrlDic[button.name].qualityName;
            LiveStatusText.text = TV189MsgReciver.liveUrlDic[button.name].qualityName; ;


            MediaPlayerCtrl.m_videoID = "http://public.vod5g.nty.tv189.com/vod/demo/ar/hello5g-1-1.mp4.m3u8"; ;
            MediaPlayerCtrl.is3dUD = false;
            mediaPlayerCtrl.UnLoad();
            mediaPlayerCtrl.Load(MediaPlayerCtrl.m_videoID);

        }
      
      
        //MediaPlayerCtrl.infoName = "No";

    }
}
