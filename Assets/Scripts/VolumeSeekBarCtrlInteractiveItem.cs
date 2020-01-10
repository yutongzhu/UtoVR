using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRStandardAssets.Utils;

/// <summary>
/// ������������������壩�Ľű�����Ҫ�������� ������������������壩
/// </summary>
public class VolumeSeekBarCtrlInteractiveItem : MonoBehaviour
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
	}


	private void OnDisable()
	{
		m_InteractiveItem.OnOver -= HandleOver;
	}

	private void HandleOver()
	{
       // mpc.m_VolumeControl.SetActive(false);
	}
}
