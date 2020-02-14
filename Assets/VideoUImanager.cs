using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static MediaPlayerCtrl;
public   enum VideoStatus
{
    Standard,
    HD,
    VeryHD,
    BlueRay,
    _4k
}
public class VideoUImanager : UIBase {
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowScreenPlay:
                {
                    VideoCanvasRoot.SetActive(true);
                    VideoContolRoot.gameObject.SetActive(false);
                    VolumeSlider.gameObject.SetActive(false);
                    LevelControl.gameObject.SetActive(false);
                }
                break;
            case (ushort)UIEvent.HideScreenPlay:
                {
                    VideoCanvasRoot.SetActive(false);
                }
                break;

        }
    }
    public static VideoUImanager instance;
    public bool seekBarLoad = false;//用来控制seekBar脚本的运行时查找视屏播放插件
    public bool volumnBarLoad = false;//用来控制volumnBar脚本的运行时查找视屏播放插件
    private void Awake()
    {
          instance = this;
        //if (instance == null)
        //{
        //    DontDestroyOnLoad(gameObject);

        //}
        //else if (instance != null)
        //{
        //    Destroy(gameObject);
        //}
    
        msgids = new ushort[]
{
            (ushort )UIEvent.ShowScreenPlay  ,
             (ushort )UIEvent .HideScreenPlay


};
        RegistSelf(this, msgids);

    }
    VideoStatus videoStatus;
   public   MediaPlayerCtrl mediaPlayerCtrl;
    public GameObject VideoCanvasRoot;
   // public GameObject VideoPlay;
    Slider VolumeSlider;
  
    Transform SelectImage;//用于切换不同状态时候
    Image VideoStatusImage;//用于显示此时的清晰度状态
    Transform LevelControl;
    Transform VideoContolRoot;
    Transform PlayButton;
    List<Button> buttonList = new List<Button>();
    void Start () {
     
      //  mediaPlayerCtrl = GameObject.Find("VideoPlayFather").transform.
           // Find("Cinema/VideoScreen").GetComponent<MediaPlayerCtrl>();
     
        VolumeSlider = UISettingManager.GetUITransform("VolumeSlider").GetComponent<Slider >();
   //     VideoCanvasRoot = UISettingManager.GetUIObject("VideoCanvasRoot");
           PlayButton = UISettingManager.GetUITransform("PlayButton");
        SelectImage= UISettingManager.GetUITransform("SelectImage");
        VideoStatusImage = UISettingManager.GetUITransform("VideoStatusBt").GetComponent <Image >();
        UISettingManager.AddButtonClickListener("PlayButton", PlayVideoClick);
        UISettingManager.AddButtonClickListener("VideoStatusBt", VideoStatusclick);
        UISettingManager.AddButtonClickListener("BackMainBtn", BackMainClick);
        UISettingManager.AddButtonClickListener("ShowBtn", ShowVideoAdjustPanel);
        UISettingManager.AddButtonClickListener("VolumeButton", SetVolumnPanel);
        UISettingManager.AddButtonClickListener("ReplayButton", ReplayClick);
        LevelControl = UISettingManager.GetUITransform("LevelControl");
     
        VideoContolRoot= UISettingManager.GetUITransform("VideoContolRoot");
        foreach (Button  item in LevelControl.GetComponentsInChildren <Button >())
        {

            item.onClick.AddListener(delegate() { VideoStatusChange(item); });
            buttonList.Add(item);
        }
        if (SceneManager .GetActiveScene() .name =="PlayerVR")
        {
            VideoCanvasRoot.SetActive(true);
            VideoCanvasRoot.transform.Find("VideoContolRoot").gameObject.SetActive(false);
        }
        else
        {
            VideoCanvasRoot.transform .Find ("VideoContolRoot").gameObject .SetActive(false);
           // VideoContolRoot.gameObject.SetActive(false);
        }
       
      
        VolumeSlider.gameObject.SetActive(false);
        LevelControl.gameObject.SetActive(false);
    }
    //重播
    void ReplayClick()
    {
        mediaPlayerCtrl.Replay();
    }
 
 
    bool playBo=true ;//默认开始就播放加载
    //视频播放和暂停按钮
    void PlayVideoClick()
    {
        if (playBo==true )
        {
            mediaPlayerCtrl.Pause();
            PlayButton.GetComponent<Image>().sprite = ResourceContine.Instance.NormralPlayVideoSpi;
            playBo = false;
        }
        else
        {
            mediaPlayerCtrl.Play();
            PlayButton.GetComponent<Image>().sprite = ResourceContine.Instance.normalPauseVideoSpi;
            playBo = true;
        }
    }
    //视频清晰度切换
    bool showVideoLevel; 
    void VideoStatusclick()
    {
        if (showVideoLevel==false )
        {
            LevelControl.gameObject.SetActive(true);
            showVideoLevel = true;
        }
        else
        {
            LevelControl.gameObject.SetActive(false);
            showVideoLevel = false;
        }

       
    }
    bool volumnEnable;
    void SetVolumnPanel()
    {
        if (volumnEnable == false )
        {

            VolumeSlider.gameObject.SetActive(true );
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
  public   void BackMainClick()
    {
        Debug.Log("BackMainClick");
        Transform VideoPlayFather = GameObject.Find("VideoPlayFather").transform;
        if (SceneManager.GetActiveScene().name == "Main"|| SceneManager.GetActiveScene().name == "MainVR")
        {
            MediaPlayerCtrl mediaPlayerCtrl =
           VideoPlayFather.Find("Cinema/VideoScreen").GetComponent<MediaPlayerCtrl>();
            mediaPlayerCtrl.Stop();

        }
        else
        {
            mediaPlayerCtrl.Stop();
        }
       
     
        

        if (SceneStatus.sceneEnum == SceneEnum.MainVR)
        {
            LightManger.instance.VideoCanvasRoot.SetActive(false );
            LightManger.instance.VideoCanvasRootVR.SetActive(false);
            LightManger.instance.VideoCanvasRoot.transform.Find("VideoContolRoot").gameObject.SetActive(false);
            LightManger.instance.VideoCanvasRootVR.transform.Find("VideoContolRoot").gameObject.SetActive(false );
            LightManger.instance.VideoScreen.SetActive(false );
            LightManger.instance.VideoScreenVR.SetActive(false);

        
        }
        else
        {
           SceneManager.LoadScene("Main");

        }


        VideoControl.instance.VideoDetailRoot.gameObject.SetActive(true);
        VideoCanvasRoot.SetActive(false);
        //LightManger.instance.VideoPlay.SetActive(false);
    }
    bool VideoAdjusEnable;
    /// <summary>
    /// 显示时评调节的界面
    /// </summary>
  public   void ShowVideoAdjustPanel()
    {
        seekBarLoad = false;
        volumnBarLoad = false;

        if (VideoAdjusEnable == false)
        {
            if (SceneManager.GetActiveScene().name =="Main")
            {
            
                //此时ui控件的相应优先于滑动屏幕旋转
                CameraController.instance.screenRotaEnale = false;
            }

           
            
           
            VideoContolRoot .gameObject.SetActive(true);
            
            switch ( videoStatus)

            {
                case VideoStatus.Standard:
                    SelectImage.transform.position = buttonList[0].transform.position;
                    SelectImage.GetComponent<Image>().sprite = ResourceContine.Instance.highLightStandard;
                    break;
                case VideoStatus.HD :
                    SelectImage.transform.position = buttonList[1].transform.position;
                    SelectImage.GetComponent<Image>().sprite = ResourceContine.Instance.highLightHD;
                    break;
                case VideoStatus.VeryHD:
                    SelectImage.transform.position = buttonList[2].transform.position;
                    SelectImage.GetComponent<Image>().sprite = ResourceContine.Instance.highLightlVeryHD;
                    break;
                case VideoStatus.BlueRay :
                    SelectImage.transform.position = buttonList[3].transform.position;
                    SelectImage.GetComponent<Image>().sprite = ResourceContine.Instance.hightLightBlueRay;
                    break;
                case VideoStatus._4k:
                    SelectImage.transform.position = buttonList[4].transform.position;
                    SelectImage.GetComponent<Image>().sprite = ResourceContine.Instance.hightLight_4K;
                    break;
                default:
             break;
            }
            VideoAdjusEnable = true;
        }
        else
        {

            VideoContolRoot.gameObject.SetActive(false);
            VideoAdjusEnable = false;
            //滑动屏幕旋转
            CameraController.instance.screenRotaEnale = true;
        }
    }
    //重置位置的方法
    void ResetPosClick()
    {
       
    }
  
    void VideoStatusChange(Button button)
    {
        SelectImage.transform.position = button.transform.position;
    
        if (button.name == "StandardBt")
        {
             SelectImage.GetComponent <Image >().sprite = ResourceContine.Instance.highLightStandard;
            VideoStatusImage.sprite = ResourceContine.Instance.normalStandard;
             mediaPlayerCtrl.ChangeLevelWithIndex(0);
         
        }
        else if (button.name == "HDButton")
        {
            SelectImage.GetComponent<Image>().sprite = ResourceContine.Instance.highLightHD;
            VideoStatusImage.sprite = ResourceContine.Instance.normalHD ;
            mediaPlayerCtrl.ChangeLevelWithIndex(1);
            
        }
        else if (button.name == "VeryHighBt")
        {
             SelectImage.GetComponent<Image>().sprite = ResourceContine.Instance.highLightlVeryHD;
            VideoStatusImage.sprite = ResourceContine.Instance.normalVeryHD;
            mediaPlayerCtrl.ChangeLevelWithIndex(2);
           
        }
        else if (button.name == "BluRayBt")
        {
            SelectImage.GetComponent<Image>().sprite = ResourceContine.Instance.hightLightBlueRay;
            VideoStatusImage.sprite = ResourceContine.Instance.normalBlueRay ;
            mediaPlayerCtrl.ChangeLevelWithIndex(3);
          // 
        }
        else if (button.name == "_4KBt")
        {
            SelectImage.GetComponent<Image>().sprite = ResourceContine.Instance.hightLight_4K;
            VideoStatusImage.sprite = ResourceContine.Instance.normal_4k;
            mediaPlayerCtrl.ChangeLevelWithIndex(4);
         //   
        }
       
    }

  


  
 
}
