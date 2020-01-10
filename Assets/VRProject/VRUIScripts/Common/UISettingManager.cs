using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISettingManager  
{
    //public static UISettingManager instance;
  
    //private void Awake()
    //{
    //    instance = this;
    //}
    /// <summary>
    /// 根据resource路径获取
    /// </summary>
    /// <param name="iconPath"></param>
    /// <returns></returns>
  public static   Sprite GetImageSprite(string iconPath)
    {
        Sprite sp = Resources.Load<Sprite>(iconPath);
        return sp;
    }
    public static GameObject GetUIObject(string name)
    {

        return  UIManager.instance.GetGameObject(name );
    }
    public static Transform GetUITransform(string name)
    {
        return UIManager.instance.GetGameObject(name).transform;
    }
    public static void AddButtonClickListener(string name, UnityAction action)
    {

        GameObject game = UIManager.instance.GetGameObject(name);
        game.GetComponent<UIBehaviour>().AddButtonListener(action);
    }
    public static void AddButtonClickListener(GameObject game, UnityAction action)
    {
        game.GetComponent<UIBehaviour>().AddButtonListener(action);
    }
    public static void SetUIText(Transform text,string content)
    {
        text.GetComponent<UIBehaviour>().SetText(content);
    }
    public static void  IniFindTransform(Transform obj,string name)
    {
        obj = GetUITransform(name);
    }
    /// <summary>
    /// 点击按钮背景图片会自动到按钮下面当背景
    /// </summary>
    /// <param name="image"></param>
    /// <param name="button"></param>
    public static void   ButtonClickEvent(Transform imageName, Transform  button)
    {
        //Transform image = GetUITransform(imageName);
        imageName.position = button.position;
    }
    public static void AddButonListener(UnityAction <string >action,string  name)
    {
        GameObject game = UIManager.instance.GetGameObject(name);
        game.GetComponent<Button >().onClick .AddListener (
            delegate () {

                action(name);
            }
            
            
            );
    }
    //实现ui抖动效果
    public static void ShakeUI(Transform game,Vector3 pos,float timeLength)
    {
        game.DOShakePosition(timeLength, pos);
      
    }
    public static void ShakeUI(string  gameName, Vector3 pos, float timeLength)
    {
        Transform go = GetUITransform("gameName");
        go.DOShakePosition(timeLength, pos);

    }
}
