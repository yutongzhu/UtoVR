using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoItem 
{
    public string title;//视频名称

    public  ColumnType columnType;
    public bool isCollected;//表示是否收藏了;//表示是否收藏
    public string subscript;//内容介绍
    public int dataInfo;//时长
    public Texture coverTexture;
    public string contentId;
    public string clickParam;
    public int clickType;
}
