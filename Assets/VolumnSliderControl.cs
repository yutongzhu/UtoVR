using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumnSliderControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{

    public MediaPlayerCtrl m_srcVideo;
    public Slider volumnSlider;

  
    bool volumnUpdate = true;
    // Use this for initialization
    void Start()
    {
        if ( Application.platform == RuntimePlatform.Android)
            
        {
            AndroidAPI.Init();
        }
        
    }


    bool volumnBo = true;
   // bool get
    // Update is called once per frame
    void Update()
    {

        if (volumnBo == false)//表示正常状态没有对slider进行操作
            return;
        
        if (m_srcVideo.GetCurrentState() == MEDIAPLAYER_STATE.PLAYING)//播放状态显示视频时长
        {


            if (volumnSlider != null)
            {
               //Debug.Log("m_srcVideo.GetVolume:" + AndroidAPI.getVolume());
               volumnSlider.value = AndroidAPI.getVolume();//  m_srcVideo.GetVolume();
            }

        }
       


    }

    private PointerEventData pEventData;
    public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log("OnPointerEnter:");
        //记录位置

        volumnUpdate = false;


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit:");

        volumnUpdate = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
     

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //   Debug.Log("OnPointerUp:" + m_srcSlider.value);
        //   m_srcVideo.SetVolume(volumnSlider .value );
        //  m_srcVideo.SetSeekBarValue(volumnSlider.value);
        AndroidAPI.setVolume(volumnSlider.value);
          volumnUpdate = true;

    }


    public void OnDrag(PointerEventData eventData)
    {
        //	 Debug.Log("OnDrag:"+ eventData);

        volumnUpdate = false;


    }

}
