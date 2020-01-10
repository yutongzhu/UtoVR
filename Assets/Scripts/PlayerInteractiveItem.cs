using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRStandardAssets.Utils;

/// <summary>
/// 播放或暂停 按钮脚本
/// </summary>
public class PlayerInteractiveItem : MonoBehaviour
{
    [SerializeField]
    private Material m_NormalMaterialPlay;
    [SerializeField]
    private Material m_OverMaterialPlay;
    [SerializeField]
    private Material m_NormalMaterialPause;
    [SerializeField]
    private Material m_OverMaterialPause;                  

    
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

        if (mpc.m_CurrentState ==MEDIAPLAYER_STATE.PLAYING)
        {
            gameObject.GetComponent<Renderer>().material = m_OverMaterialPause;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = m_OverMaterialPlay;
        }
	}

	private void HandleOut()
	{
        m_SelectionRadial.HandleUp();
        m_SelectionRadial.Hide();
        m_GazeOver = false;

        if (mpc.m_CurrentState == MEDIAPLAYER_STATE.PLAYING)
        {
            gameObject.GetComponent<Renderer>().material = m_NormalMaterialPause;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = m_NormalMaterialPlay;
        }
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
            if (mpc.m_CurrentState == MEDIAPLAYER_STATE.PLAYING)
            {
                gameObject.GetComponent<Renderer>().material = m_NormalMaterialPlay;
                mpc.Pause();
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = m_NormalMaterialPause;
                mpc.Play();
            }
        }
    }
}
