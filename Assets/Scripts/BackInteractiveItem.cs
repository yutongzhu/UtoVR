using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRStandardAssets.Utils;

/// <summary>
/// 返回按钮脚本，退回到Main
/// </summary>
public class BackInteractiveItem : MonoBehaviour
{
    [SerializeField]
    private Material m_NormalMaterial;
    [SerializeField]
    private Material m_OverMaterial;
    
    private VRInteractiveItem m_InteractiveItem;
    private SelectionRadial m_SelectionRadial;
    private bool m_GazeOver;

	private void Awake ()
	{
		m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();

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
        m_SelectionRadial.Show();
        m_SelectionRadial.HandleDown();
        m_GazeOver = true;
        gameObject.GetComponent<Renderer>().material = m_OverMaterial;
    }

	private void HandleOut()
	{
        m_SelectionRadial.HandleUp();
        m_SelectionRadial.Hide();
        m_GazeOver = false;
        gameObject.GetComponent<Renderer>().material = m_NormalMaterial;
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
        SceneManager.LoadScene("Main", LoadSceneMode.Single);        
    }
}
