using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRStandardAssets.Utils;

/// <summary>
/// 多码率切换按钮的脚本
/// </summary>
public class LevelChangeBtnInteractiveItem : MonoBehaviour
{
    [SerializeField]
    private Material m_Level2Btn;
    [SerializeField]
    private Material m_Level3Btn;
    [SerializeField]
    private Material m_Level4Btn;
    [SerializeField]
    private Material m_Level5Btn;
    [SerializeField]
    private Material m_Level6Btn;

    public GameObject levelObject;         

    
    private VRInteractiveItem m_InteractiveItem;
    private SelectionRadial m_SelectionRadial;
    private bool m_GazeOver;
	private void Awake ()
	{
		m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();
        m_SelectionRadial = GameObject.FindWithTag("MainCamera2").GetComponent<SelectionRadial>();
	}

    public void changeLevel(int lvl)
    {
        if (lvl == 0)
        {
            gameObject.GetComponent<Renderer>().material = m_Level2Btn;
        }
        else if (lvl == 1)
        {
            gameObject.GetComponent<Renderer>().material = m_Level3Btn;
        }
        else if (lvl == 2)
        {
            gameObject.GetComponent<Renderer>().material = m_Level4Btn;
        }
        else if (lvl == 3)
        {
            gameObject.GetComponent<Renderer>().material = m_Level5Btn;
        }
        else if (lvl == 4)
        {
            gameObject.GetComponent<Renderer>().material = m_Level6Btn;
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
        m_SelectionRadial.Show();
        m_SelectionRadial.HandleDown();
        m_GazeOver = true;
	}

	private void HandleOut()
	{
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
        levelObject.SetActive(!levelObject.activeSelf);
    }
}

