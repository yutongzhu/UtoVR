using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassifyViteoRoot
{
    /// <summary>
    /// 
    /// </summary>
    public int code { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string msg { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public ClassifyInfo info { get; set; }
}

public class ClassifyInfo
{
    /// <summary>
    /// 
    /// </summary>
    public int total { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ClassifyVideoItem> data { get; set; }
}
public class ClassifyVideoItem
{
    /// <summary>
    /// 
    /// </summary>
    public string categoryId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string contentId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string contentType { get; set; }
    /// <summary>
    /// 中国大陆
    /// </summary>
    public string countryName { get; set; }
    /// <summary>
    /// 香港探员陈港生（成龙饰）跟踪黑道老大维克多·王（赵文瑄饰）长达十数年，为搜集他犯罪证据而被卷入一场“计中计”中，还将自己的侄女白舒（范冰冰饰）牵扯进来。在这场被中国特警、维克多·王、俄罗斯黑帮三面夹攻的“绝地逃亡”中，班尼结识了共患难的最佳拍档康纳（约翰尼·诺克斯维尔饰），一位正被维克多·王和俄国杀手追捕逃命的美国赌博高手，三人最终将怎样结束这场冒险变得扑朔迷离。
    /// </summary>
    public string description { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string himgM0 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string himgM7 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string himgM8 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string host { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string imgM0 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string imgM7 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string imgM8 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int isTVCopyRight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int length { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int limitFree { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int nowseriescount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string orderTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int plats { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int ppvStyle { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string productId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string publishTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string releasyear { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string screenType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int seriesId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int seriescount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int siteFolderId { get; set; }
    /// <summary>
    /// 绝地逃亡
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int vipSignal { get; set; }
}