using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;

/// <summary>
/// 控制重置按钮脚本
/// </summary>
public class VRMainControllScript : MonoBehaviour
{
    public Camera m_Camera; //主相机
    public GameObject resetButton; //重置按钮


    private void Awake()
    {
    }
    void Update()
    {
        if (m_Camera != null)
        {
            Vector3 v = m_Camera.transform.eulerAngles;
            //大于180度，转换一下角度，变成0到-90度范围内
            if (v.x > 180)
            {
                v.x = v.x - 360.0f;
            }
            //Main场景内
            if (SceneManager.GetActiveScene().name == "Main")
            {
                this.gameObject.transform.position = m_Camera.transform.position;
                this.gameObject.transform.eulerAngles = new Vector3(0, v.y, 0);
                // 低头 20至60角度范围内显示重置按钮。
                if (v.x > 20 && v.x < 60)
                {
                    resetButton.SetActive(true);
                }
                else
                {
                    resetButton.SetActive(false);
                }
            }
        }

    }
}

