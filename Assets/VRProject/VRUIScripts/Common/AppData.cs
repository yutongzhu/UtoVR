

using System;
using System.Collections.Generic;


public class ChildrenItem
{
    /// <summary>
    /// 
    /// </summary>
    public string cover { get; set; }
    /// <summary>
    /// 感受这超强的视觉盛宴
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int clickType { get; set; }
    /// <summary>
    /// 4K精彩瞬间集锦
    /// </summary>
    public string subscript { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string contentId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string clickParam { get; set; }
}

public class DataItem
{
    /// <summary>
    /// 
    /// </summary>
    public string cover { get; set; }
    /// <summary>
    /// 感受冲浪的魅力
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int clickType { get; set; }
    /// <summary>
    /// 板上人生
    /// </summary>
    public string subscript { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string contentId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string clickParam { get; set; }
}

public class ColumnRoot
{
    /// <summary>
    /// 
    /// </summary>
    public int areaCode { get; set; }
    /// <summary>
    /// VR推荐
    /// </summary>
    public string areaName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<ChildrenItem> children { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<DataItem> data { get; set; }
}

