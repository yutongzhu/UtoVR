using UnityEngine;

/// <summary>
/// 自动旋转脚本，用于Home中Loading图片动画
/// </summary>
public class AutoRotate:MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0,0,-Time.deltaTime *300f));
    }
}