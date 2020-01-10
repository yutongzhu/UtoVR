using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRStandardAssets.Utils;

/// <summary>
/// 控制 工具条面板（内含返回、显示视频工具条、定位重置按钮）的脚本
/// </summary>
public class ShowInteractiveItem : MonoBehaviour
{
    public Material m_NormalMaterialPlay;
    public Material m_OverMaterialPlay;
    public Material m_NormalMaterialPause;
    public Material m_OverMaterialPause;                  

    
    private VRInteractiveItem m_InteractiveItem;
    private MediaPlayerCtrl mpc;
    private SelectionRadial m_SelectionRadial;
    private bool m_GazeOver;

	private void Awake ()
	{
		m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();
        mpc = GameObject.Find("sphere").GetComponent<MediaPlayerCtrl>();

        m_SelectionRadial = GameObject.FindWithTag("MainCamera2").GetComponent<SelectionRadial>();
	}

	private void OnEnable()
	{
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
		m_InteractiveItem.OnClick += HandleClick;
        m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
	}


	private void OnDisable()
	{
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;
		m_InteractiveItem.OnClick -= HandleClick;
        m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
	}

	private void HandleOver()
	{
        //if (mpc.m_VideoControl.activeSelf)
        //{
        //    gameObject.GetComponent<Renderer>().material = m_OverMaterialPause;
        //}
        //else
        //{
        //    gameObject.GetComponent<Renderer>().material = m_OverMaterialPlay;
        //}
        m_SelectionRadial.Show();
        m_SelectionRadial.HandleDown();
        m_GazeOver = true;
	}

	private void HandleOut()
	{
        //if (mpc.m_VideoControl.activeSelf)
        //{
        //    gameObject.GetComponent<Renderer>().material = m_NormalMaterialPause;
        //}
        //else
        //{
        //    gameObject.GetComponent<Renderer>().material = m_NormalMaterialPlay;
        //}
        m_SelectionRadial.HandleUp();
        m_SelectionRadial.Hide();
        m_GazeOver = false;
	}

	private void HandleClick()
	{
        EventCallBack();
	}

    private void HandleSelectionComplete()
    {
        if (m_GazeOver)
        {
            EventCallBack();
        }
    }

    private void EventCallBack() {
        //if(mpc.m_VideoControl.activeSelf)
        //{
        //   // mpc.HideVideoCtrl();
        //} else {
        //  //  mpc.ShowVideoCtrl();
        //}
    }
}
