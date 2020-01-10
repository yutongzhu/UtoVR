using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRStandardAssets.Utils;

/// <summary>
/// 重播按钮脚本
/// </summary>
public class ReplayInteractiveItem : MonoBehaviour
{
    [SerializeField]
    private Material m_NormalMaterialReplay;
    [SerializeField]
    private Material m_OverMaterialReplay;
    
    private VRInteractiveItem m_InteractiveItem;
    private SelectionRadial m_SelectionRadial;
    private bool m_GazeOver;

    private MediaPlayerCtrl mpc;

	private void Awake ()
	{
		m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();
        m_SelectionRadial = GameObject.FindWithTag("MainCamera2").GetComponent<SelectionRadial>();

        mpc = GameObject.Find("sphere").GetComponent<MediaPlayerCtrl>();
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
        m_SelectionRadial.Show();
        m_SelectionRadial.HandleDown();
        m_GazeOver = true;
        gameObject.GetComponent<Renderer>().material = m_OverMaterialReplay;
    }

	private void HandleOut()
	{
        m_SelectionRadial.HandleUp();
        m_SelectionRadial.Hide();
        m_GazeOver = false;
        gameObject.GetComponent<Renderer>().material = m_NormalMaterialReplay;
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
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            GameObject.Find("Directional Light").GetComponent<Main>().toast("网络无连接");
        }
        else
        {
            mpc.Replay();
            gameObject.GetComponent<Renderer>().material = m_NormalMaterialReplay;
        }
    }
}
