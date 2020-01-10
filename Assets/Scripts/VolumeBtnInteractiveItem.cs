using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRStandardAssets.Utils;

/// <summary>
/// 音量按钮脚本，用于显示音量进度条
/// </summary>
public class VolumeBtnInteractiveItem : MonoBehaviour
{
    private VRInteractiveItem m_InteractiveItem;

    private MediaPlayerCtrl mpc;

	private void Awake ()
	{
		m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();

        mpc = GameObject.Find("sphere").GetComponent<MediaPlayerCtrl>();
	}

	private void OnEnable()
	{
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
	}


	private void OnDisable()
	{
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;
	}

	private void HandleOver()
	{
    //    mpc.m_VolumeControl.SetActive(true);
	}

	private void HandleOut()
	{
	}
}

