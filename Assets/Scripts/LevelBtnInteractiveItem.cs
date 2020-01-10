using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRStandardAssets.Utils;

/// <summary>
/// 控制 显示当前码率、显示切换多码率面板的按钮 的脚本
/// </summary>

public class LevelBtnInteractiveItem : MonoBehaviour
{
    [SerializeField]
    private Material m_LevelBtn_Nor;
    [SerializeField]
    private Material m_LevelBtn_Sel;

    public int m_Index;
    
    private VRInteractiveItem m_InteractiveItem;
    private SelectionRadial m_SelectionRadial;
    private bool m_GazeOver;

    private bool isSelected;

    private MediaPlayerCtrl mpc;
	private void Awake ()
	{
		m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();
        m_SelectionRadial = GameObject.FindWithTag("MainCamera2").GetComponent<SelectionRadial>();

        mpc = GameObject.Find("sphere").GetComponent<MediaPlayerCtrl>();
	}

    public void setSelected(bool value)
    {
        isSelected = value;
        if (isSelected)
        {
            gameObject.GetComponent<Renderer>().material = m_LevelBtn_Sel;
        } else
        {
            gameObject.GetComponent<Renderer>().material = m_LevelBtn_Nor;
        }
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
        if (!isSelected)
        {
            m_SelectionRadial.Show();
            m_SelectionRadial.HandleDown();
            m_GazeOver = true;

            gameObject.GetComponent<Renderer>().material = m_LevelBtn_Sel;
        }
	}

	private void HandleOut()
	{
        if (!isSelected)
        {
            m_SelectionRadial.HandleUp();
            m_SelectionRadial.Hide();
            m_GazeOver = false;

            gameObject.GetComponent<Renderer>().material = m_LevelBtn_Nor;
        }

        
	}

	private void HandleClick()
	{
        EventCallBack();
	}

    private void HandleSelectionComplete()
    {
        if (m_GazeOver && !isSelected)
        {
            EventCallBack();
        }
    }

    private void EventCallBack() {
        m_SelectionRadial.HandleUp();
        m_SelectionRadial.Hide();
        m_GazeOver = false;

        mpc.ChangeLevelWithIndex(m_Index);
    }
}


