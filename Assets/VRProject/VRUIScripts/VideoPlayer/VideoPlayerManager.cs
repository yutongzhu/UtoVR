using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayerManager : UIBase 
{

    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowVideoPlay:
                {
                    VideoPlayerRoot.SetActive(true );
                    scrMedia.Play();
                    m_bFinish = false;
                    //if (isStoped ==true )//表示是否调用过结束方法。因为插件结束后必须执行两次play才能播放
                    //{
                    //    scrMedia.Play();
                    //    m_bFinish = false;
                    //    isStoped = false;
                    //}
               
                }
                break;
            case (ushort)UIEvent.HideVideoPlay:
                {
                    VideoPlayerRoot.SetActive(false);

                }
                break;

        }
    }
    bool isStoped=false ;
    public static VideoPlayerManager instance;
    public MediaPlayerCtrl scrMedia;
    public GameObject VideoPlayerRoot;
    Transform VideoScreenBack;
    public bool m_bFinish = false;
    private void Awake()
    {
        instance = this;
        msgids = new ushort[]
{
     (ushort )UIEvent .ShowVideoPlay ,
        (ushort )UIEvent .HideVideoPlay
};
        RegistSelf(this, msgids);
    }
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayerRoot.SetActive(false );
        //   VideoScreenBack = UISettingManager.GetUITransform("VideoScreenBack");
        //VideoPlayerRoot = transform.Find("VideoPlayerRoot").gameObject ;
        UISettingManager.AddButtonClickListener("VideoScreenBack", ScreenBack);
        scrMedia.OnEnd += OnEnd;
        Debug.Log("dddd");
    }
    void OnEnd()
    {
        m_bFinish = true;
    }
    void ScreenBack()
    {
        SendMsg(new MsgBase ((ushort )UIEvent .ShowVideoDetailRoot ));
        VideoPlayerRoot.SetActive(false );
        scrMedia.Stop();
        isStoped = true;
       
    }
}
