using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VRStandardAssets.Utils;
using System.Collections.Generic;



#if !UNITY_WEBGL

public class SeekBarCtrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{

    public MediaPlayerCtrl m_srcVideo;
    public Slider m_srcSlider;

    public Text currentTime;
    public Text totalTime;
    bool videoUpdate = true;
    // Use this for initialization
    void Start()
    {
    }

    bool getTotalBo;
    //获取视频总时间
    string GetTatalTime()
    {
        int time = m_srcVideo.GetDuration();
        int durationM = time / 1000 / 60;
        int durationS = time / 1000 % 60;
        string durM = durationM < 10 ? "0" + durationM : durationM + "";
        string durS = durationS < 10 ? "0" + durationS : durationS + "";
        string total = durM + ":" + durS;
        return total;

    }
    //获取当前视频进程时间
    string GetCurrentTime()
    {
        int time = m_srcVideo.GetSeekPosition();
        int currentM = time / 1000 / 60;
        int currentS = time / 1000 % 60;//秒
        string curM = currentM < 10 ? "0" + currentM : currentM + "";
        string curS = currentS < 10 ? "0" + currentS : currentS + "";
        string progress = curM + ":" + curS;
        return progress;

    }
    // Update is called once per frame
    void Update()
    {

        if (videoUpdate == false)//表示正常状态没有对slider进行操作
            return;

        if (m_srcVideo != null)
        {

            if (m_srcSlider != null)
            {
                m_srcSlider.value = m_srcVideo.GetSeekBarValue();
            }

        }
        if (m_srcVideo.GetCurrentState() == MEDIAPLAYER_STATE.PLAYING)//播放状态显示视频时长
        {

            currentTime.text = GetCurrentTime();
            if (getTotalBo == false)
            {
                totalTime.text = GetTatalTime();
                getTotalBo = true;

            }

        }
        //   }


    }

    private PointerEventData pEventData;
    public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log("OnPointerEnter:");
        //记录位置

        videoUpdate = false;


    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // Debug.Log("OnPointerExit:");

        videoUpdate = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_srcVideo.Pause();
        int seekPos = (int)(m_srcSlider .value * m_srcVideo.GetDuration());
        m_srcVideo.SeekTo(seekPos);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp:" + m_srcSlider.value);
        m_srcVideo.Play();
       // m_srcVideo.SetSeekBarValue(m_srcSlider.value);
        videoUpdate = true;

    }


    public void OnDrag(PointerEventData eventData)
    {
        //	 Debug.Log("OnDrag:"+ eventData);

       
        int current = (int)((float)m_srcVideo.GetDuration() * m_srcSlider.value);
        int currentM = current / 1000 / 60;
        int currentS = current / 1000 % 60;
        string curM = currentM < 10 ? "0" + currentM : currentM + "";
        string curS = currentS < 10 ? "0" + currentS : currentS + "";
        currentTime.text = curM + ":" + curS;
        videoUpdate = false;
    }

}
#endif