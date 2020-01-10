using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;

/// <summary>
/// 控制 工具条面板（内含返回、显示视频工具条、定位重置按钮）脚本
/// </summary>
public class VRPlayerControllScript : MonoBehaviour
{
    public Camera m_Camera; //主相机
    public GameObject resetButton; //工具条面板（内含返回、显示视频工具条、定位重置按钮）
    private Reticle m_Reticle; //圈圈
    private bool isShowed; //显示状态
    private MediaPlayerCtrl mpc; //播放器脚本
    

    private void Awake()
    {
        m_Reticle = GameObject.FindWithTag("MainCamera2").GetComponent<Reticle>();
        mpc = GameObject.Find("sphere").GetComponent<MediaPlayerCtrl>();
        isShowed = false;
    }
    void Update()
    {
        if (m_Camera != null && m_Reticle != null)
        {
            Vector3 v = m_Camera.transform.eulerAngles;
            //大于180度，转换一下角度，变成0到-90度范围内
            if (v.x > 180)
            {
                v.x = v.x - 360.0f;
            }

            // 低头 20至60角度范围内显示重置按钮。
            if (v.x > 20 && v.x < 60)
            {
                if (!resetButton.activeSelf && !isShowed)
                {
                    isShowed = true;
                    resetButton.SetActive(true);
                    m_Reticle.Show();

                    this.gameObject.transform.position = m_Camera.transform.position;
                    this.gameObject.transform.eulerAngles = new Vector3(0, v.y, 0); 
                }                    
            }
            else
            {
                isShowed = false;
              //  m_Reticle.Hide();
                resetButton.SetActive(false);
              //  if (mpc.m_VideoControl.activeSelf)
              //  {
              ////      mpc.HideVideoCtrl();
              //  }
            }                             


        }

    }
}


