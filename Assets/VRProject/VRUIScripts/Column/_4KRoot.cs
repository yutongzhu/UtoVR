using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRoot 
{
    /// <summary>
    /// 
    /// </summary>
    public string areaCode { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int autoRecommend { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string recommendParam { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> data { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<CategorysItem> categorys { get; set; }
}
public class ListDataItem
{
    /// <summary>
    /// 
    /// </summary>
    public int imgtype { get; set; }
    /// <summary>
    /// contenttype=100&screenType=4K&tag=动作
    /// </summary>
    public string clickParam { get; set; }
    /// <summary>
    /// 动作
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int clickType { get; set; }
}

public class CategorysItem
{
    /// <summary>
    /// 
    /// </summary>
    public List<ListDataItem> data { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int categoryType { get; set; }
    /// <summary>
    /// 分类1
    /// </summary>
    public string typeName { get; set; }
}