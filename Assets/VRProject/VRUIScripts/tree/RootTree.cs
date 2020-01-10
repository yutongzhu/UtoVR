using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTree 
{
    // public string RootName;//
    //存储注册消息 key：为string类型的ID    value;为包含的数据
    //vr 巨幕，旅游等不同的rootree 下面包含着不同的视频类型的子集
    private Dictionary<string  , VideoTypeNode[]> rootTreeDic;  
    public RootTree()
    {

        rootTreeDic = new Dictionary<string, VideoTypeNode[]>();
    }

    public void   AddVideoTypeChild(string name, VideoTypeNode[] item)
    {
        if (!rootTreeDic.ContainsKey(name))
        {

            rootTreeDic.Add(name, item);

        }
        else
        {
            Debug.Log("sonMembers had exited");
        }

    }
    /// <summary>
    /// 查询元素
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public   VideoTypeNode SeleteVideoNode(string key,int videoID)
    {
       
         return rootTreeDic[key][videoID];//根据videoID返回特定
    }
    /// <summary>
    /// 返回特定的item
    /// </summary>
    /// <param name="key"></param>
    /// <param name="videoID"></param>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public VideoItem  SeleteVideoItems(string key,int videoID,int itemID)//key是root的名字
    {
        return     SeleteVideoNode(key, videoID).SeleteItems(videoID,itemID);
       
    }
    /// <summary>
    /// 返回根据不同的栏目名称下面视频种类的数量
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public int VideoNodesCount(string rootName )
    {
        return rootTreeDic[rootName].Length ;
    }
    /// <summary>
    /// 根据不同的栏目名称和视频种类的id返回具体视频的数量
    /// </summary>
    /// <param name="rootName"></param>
    /// <param name="videoID"></param>
    /// <returns></returns>
    public int ItemsCount(string rootName, int videoID)
    {
        return SeleteVideoNode(rootName, videoID).ItemsCount(videoID);
    }

}
