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
    Slider VolumeSlider;
    public LiveVideoStatus liveVideoStatus;
    Transform LevelControl;
    Transform LiveRoot;
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


        LiveRoot = UISettingManager.GetUITransform("VideoContolRoot");
        LiveStatusText= UISettingManager.GetUITransform("LiveStatusText").GetComponent <Text >();
        UISettingManager.AddButtonClickListener("BackMainBtn", BackMainClick);
        UISettingManager.AddButtonClickListener("ShowBtn", LiveAdjustPanel);
        UISettingManager.AddButtonClickListener("LivePlayButton", LivePlayButtonClick);
        VolumeSlider = UISettingManager.GetUITransform("VolumeSlider").GetComponent<Slider>();
        UISettingManager.AddButtonClickListener("VolumeButton", SetVolumnPanel);
        UISettingManager.AddButtonClickListener("LiveVideoStatusBt", ShowLiveControl);
        VolumeSlider.gameObject.SetActive(false );
        LevelControl = UISettingManager.GetUITransform("LevelControl");
        foreach (Button item in LevelControl.GetComponentsInChildren <Button >())
        {
            liveStatusList.Add(item.transform );
            item.onClick.AddListener(delegate() { ChangeLiveStatus(item.transform); });
        }
        LevelControl.gameObject.SetActive(false );

        LiveRoot.gameObject.SetActive(false);
      
       
            for (int i = 0; i < liveStatusList.Count; i++)
            {
                if (i < TV189MsgReciver.liveDataList.Count )
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
    bool adjustPanelEnable;
    void LiveAdjustPanel()
    {
        if (adjustPanelEnable==false)
        {
            LiveRoot.gameObject.SetActive(true);
            adjustPanelEnable = true;
        }
        else
        {
            LiveRoot.gameObject.SetActive(false);
            adjustPanelEnable = false ;
        }

    }
    bool volumnEnable;
    void SetVolumnPanel()
    {
        if (volumnEnable == false)
        {

            VolumeSlider.gameObject.SetActive(true);
            volumnEnable = true;
        }
        else
        {
            VolumeSlider.gameObject.SetActive(false);
            volumnEnable = false;
        }
    }
    /// <summary>
    /// 返回主页面
    /// </summary>
    void BackMainClick()
    {
        Debug.Log("BackMainClick");
        mediaPlayerCtrl.Stop();
        if (SceneStatus.sceneEnum==SceneEnum.Main)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            SceneManager.LoadScene("MainVR");
        }
      
        VolumeSlider.gameObject.SetActive(false);
       
    }
    //显示码率切换界面
    void ShowLiveControl()
    {
        LevelControl.gameObject.SetActive(true );
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
        LevelControl.gameObject.SetActive(false);
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
