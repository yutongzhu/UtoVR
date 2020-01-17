using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveUrlRoot
{
    /// <summary>
    /// Item列表
    /// </summary>
    public List<LiveUrlData> Items { get; set; }

}
 public class LiveUrlData
{
   
    public string audioUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string playUrl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int protocol { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string qualityid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string vid { get; set; }
    /// <summary>
    /// 标清
    /// </summary>
    public string qualityName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string quality { get; set; }

}
