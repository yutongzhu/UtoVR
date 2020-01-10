using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using VRStandardAssets.Utils;



/// <summary>
/// 音量进度条控制脚本，调节音量
/// </summary>
#if !UNITY_WEBGL

public class VolumeSeekBarCtrl : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler{

	public MediaPlayerCtrl m_srcVideo;
	public Slider m_srcSlider;

    //凝视圈圈
    private SelectionRadial m_SelectionRadial;
    private bool m_GazeOver;

	// Use this for initialization
    void Awake()
    {
        m_SelectionRadial = GameObject.FindWithTag("MainCamera2").GetComponent<SelectionRadial>();
	}

    private void OnEnable()
    {
        m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
    }


    private void OnDisable()
    {
        m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
    }

    private void HandleSelectionComplete()
    {
        if (m_GazeOver)
        {
            EventCallBack();
        }
    }

    private void EventCallBack()
    {
        if (pEventData != null)
        {
            //执行pointerDownHandler事件
            ExecuteEvents.Execute(this.gameObject, pEventData, ExecuteEvents.pointerDownHandler);
        }
    }

    private PointerEventData pEventData;
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("OnPointerEnter:");
        //记录位置
        pEventData = eventData;
        pEventData.pressPosition = eventData.position;
        pEventData.pointerPressRaycast = eventData.pointerCurrentRaycast;

        //显示圈圈
        m_SelectionRadial.Show();
        m_SelectionRadial.HandleDown();
        m_GazeOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("OnPointerExit:");
        //隐藏圈圈
        m_SelectionRadial.HandleUp();
        m_SelectionRadial.Hide();
        m_GazeOver = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
        Debug.Log("OnPointerDown:" + m_srcSlider.value);
        //更新进度条位置
        if (m_GazeOver)
        {
            m_srcVideo.SetVolume(m_srcSlider.value);
        }

        //隐藏圈圈
        m_SelectionRadial.HandleUp();
        m_SelectionRadial.Hide();
        m_GazeOver = false;
	}

	public void OnPointerUp(PointerEventData eventData)
	{	
        m_srcVideo.SetVolume(m_srcSlider.value);
	}
}
#endif