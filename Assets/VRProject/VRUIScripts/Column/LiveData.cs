using System.Collections.Generic;

public class LiveRoot
{
    /// <summary>
    /// 
    /// </summary>
    public int areaCode { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string replace { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string refresh_background_img { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> topPreviews { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<LiveDataItem> data { get; set; }
}
public class LiveDataItem
{
    /// <summary>
    /// 
    /// </summary>
    public string liveId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string freeLiveId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string productId { get; set; }
    /// <summary>
    /// 江苏卫视
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int clickType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string clickParam { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cover { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string recommendid { get; set; }
}
