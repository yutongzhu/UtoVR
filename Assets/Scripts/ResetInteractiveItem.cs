using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

/// <summary>
/// 重置按钮脚本
/// </summary>
public class ResetInteractiveItem : MonoBehaviour
{
    [SerializeField]
    private Material m_NormalMaterial;
    [SerializeField]
    private Material m_OverMaterial;

    private VRInteractiveItem m_InteractiveItem;
    private SelectionRadial m_SelectionRadial;
    private bool m_GazeOver;

    private void Awake()
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
        gameObject.GetComponent<Renderer>().material = m_OverMaterial;
        m_SelectionRadial.Show();
        m_SelectionRadial.HandleDown();
        m_GazeOver = true;
    }

    private void HandleOut()
    {
        gameObject.GetComponent<Renderer>().material = m_NormalMaterial;
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

    private int iiiii = 0;

    private void EventCallBack()
    {
        #if !UNITY_EDITOR
            if (Application.platform == RuntimePlatform.Android)
            {
                Pvr_UnitySDKAPI.Sensor.UPvr_ResetSensor(0);
            }
        #endif
        
    }
}
