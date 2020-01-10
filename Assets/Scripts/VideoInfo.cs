using System;

/// <summary>
/// 视频信息
/// </summary>
public class VideoInfo
{
    public int ID
    {
        get;
        set;
    }
    public string cover
    {
        get;
        set;
    }
    public string title
    {
        get;
        set;
    }
    public int clickType
    {
        get;
        set;
    }
    public string subscript
    {
        get;
        set;
    }
    public string contentId
    {
        get;
        set;
    }

    public string clickParam
    {
        get;
        set;
    }

    //分类, 0：4K，1：VR
    public int category
    {
        get;
        set;
    }
}