using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 表示vr ，推荐， 巨幕等下面的视频分类
/// </summary>
public class VideoTypeNode 
{
    public int id;
    public string name;
    public VideoTypeNode()
    {
        VideoTypDic = new Dictionary<int, VideoItem[]>();
    }
    //存储注册消息 key：为string类型的ID    value;为包含的数据
    public  Dictionary<int, VideoItem[] > VideoTypDic;
    //添加子物体。可以根据不同索引添加
    public void AddChild(int ID, VideoItem[] item)
    {

        if (!VideoTypDic.ContainsKey(ID))
        {

            VideoTypDic.Add(ID, item);

        }
        else
        {
            Debug.Log("sonMembers had exited");
        }

    }
    public VideoItem SeleteItems(int ID,int itemId)//根据不同的videoType  id查找子集
    {
        return VideoTypDic[ID][itemId];
    }
    /// <summary>
    /// 返回特定视频类型里具体视频的数量
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public int ItemsCount(int ID)
    {
        return VideoTypDic[ID].Length;
    }

}
