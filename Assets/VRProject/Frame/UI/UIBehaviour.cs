using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour 
{
   //所有的控件都可以挂
	// Use this for initialization
    //把控件的script注册到uimanager
    private void Awake()
    {
       // Debug.Log("gameObject .name:"+gameObject.name);
        //把该脚本注册进uinanager 里的容器，方便上层可以通过uimanager找到 该脚本的一些信息
        UIManager.instance.RegistGameObject(gameObject .name  ,gameObject );
      
    }
  
   
    /// <summary>
    /// 添加Button的点击事件
    /// </summary
    /// <param name="action"></param>
    public void AddButtonListener(UnityAction action)
    {
        Button temBu = transform.GetComponent<Button>();

        if (temBu != null)
        {
            temBu.onClick.AddListener(action);
        }
    }
    //移除监听
    public void RemoveButtonListener(UnityAction action)
    {
        Button temBu = transform.GetComponent<Button>();

        if (temBu != null)
        {
           
            temBu.onClick.RemoveListener (action);
        }
    }
    /// 更改图片
    /// </summary>
    /// <param name="action"></param>
    public void ChangeImage(Sprite sprite)
    {
        Image temBu = transform.GetComponent<Image>();

        if (temBu != null)
        {
            temBu.sprite = sprite;
        }
    }
    public void SetText(string  str)
    {
        Text text = transform.GetComponent<Text>();

        if (text != null)
        {
            text.text = str;
        }
    }
    /// <summary>
    /// 添加Slider事件
    /// </summary>
    /// <param name="action"></param>
    public void AddSliderListener(UnityAction<float> action)
    {
        Slider slider  = transform.GetComponent<Slider>();

        if (slider != null)
        {
            slider.onValueChanged.AddListener(action);

        }
    }
    //移除监听
    public void RemoveSliderListener(UnityAction<float > action)
    {
        Slider slider = transform.GetComponent<Slider>();

        if (slider  != null)
        {

            slider.onValueChanged.RemoveListener(action );
        }
    }
    public void AddInputListener(UnityAction<string> action)
    {
        if (action != null)
        {
            InputField input = transform.GetComponent<InputField>();
            input .onEndEdit .AddListener(action);
        }
    }
    public void AddDropDpwnListener(UnityAction<int> action)
    {
        if (action != null)
        {
            Dropdown dropdown = transform.GetComponent<Dropdown>();
            dropdown.onValueChanged.AddListener(action);
        }
    }
}
