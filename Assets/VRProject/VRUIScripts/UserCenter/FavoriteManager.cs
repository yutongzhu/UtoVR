using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FavoriteManager : UIBase 
{
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowFavorite:
                {
                  FavoriteRoot.gameObject.SetActive(true );
                    collectedCount = JsonDataManager.collectedVideos.Count;
                    // JsonDataManager .collectedVideos 
                    IniCollectedList();

                }
                break;
            case (ushort)UIEvent.ShowLauncher:

                {

                }
                break;
        }
    }

    Transform FavoriteRoot;
    int collectedCount;//收藏的视频数量
    Transform CollectlistContent;
    List<Button> collectedItemsList = new List<Button>();//显示收藏视频的items
    private void Awake()
    {
        msgids = new ushort[]
{
            (ushort )UIEvent .ShowFavorite ,
           

};
        RegistSelf(this, msgids);

    }
    void Start()
    {
        FavoriteRoot = UISettingManager.GetUITransform("FavoriteRoot");
        FavoriteRoot.gameObject.SetActive(false );
    
        UISettingManager.AddButtonClickListener("ExitCollectionButton", ExitMyCollectionClick);
        UISettingManager.AddButtonClickListener("LeftFavoritePage", ExitMyCollectionClick);
        UISettingManager.AddButtonClickListener("RightFavoritePage", ExitMyCollectionClick);
        CollectlistContent= UISettingManager.GetUITransform("CollectlistContent");
        foreach (Button item in CollectlistContent.GetComponentsInChildren<Button>())
        {
            collectedItemsList.Add(item);
            item.onClick.AddListener(delegate() { CollectItemClick(item.transform); });
        }
    }
    void CollectItemClick(Transform item)
    {
        JsonDataManager.currentId = item.name;//当前的点击视频的索引
            SendMsg(new MsgBase ((ushort )UIEvent.ShowVideoDetailRoot));
        FavoriteRoot.gameObject.SetActive(false );

    }
    void IniCollectedList()
    {

       
       

        if (collectedCount==0)
        {
            for (int i = 0; i < collectedItemsList.Count ; i++)
            {
                collectedItemsList[i].gameObject.SetActive(false );
            }
        }
        else
        {
            if (collectedCount< collectedItemsList.Count)//收藏的数量少于一页显示的数量
            {
                int index = 0;
                for (int i = 0; i < collectedItemsList.Count; i++)
                {
                    if (index< collectedCount)
                    {
                        collectedItemsList[i].gameObject.SetActive(true);
                        collectedItemsList[i].transform.Find("name").GetComponent<Text>().text =
                            JsonDataManager.collectedVideos[i].title; 
                        collectedItemsList[i].GetComponent<RawImage>().texture =
                        JsonDataManager.collectedVideos[i].coverTexture;

                        collectedItemsList[i].name = JsonDataManager.collectedVideos[i].contentId;
                    }
                    else
                    {
                        collectedItemsList[i].gameObject.SetActive(false);
                    }
                    index++;
                }
            }
            else//说明此时数量》||=显示的数量
            {
                for (int i = 0; i < collectedItemsList.Count; i++)
                {
                   
                  collectedItemsList[i].gameObject.SetActive(true);
                    collectedItemsList[i].transform.Find("name").GetComponent<Text>().text =
                             JsonDataManager.collectedVideos[i].title;
                    collectedItemsList[i].GetComponent<RawImage>().texture =
                    JsonDataManager.collectedVideos[i].coverTexture;

                    collectedItemsList[i].name = JsonDataManager.collectedVideos[i].contentId;


                }
            }
           
        }

    }
    void ExitMyCollectionClick()
    {
        FavoriteRoot.gameObject.SetActive(false );
        SendMsg(new MsgBase((ushort)UIEvent.ShowUserCenter));
        SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart));
    }
    void LeftPageClick()
    {
        FavoriteRoot.gameObject.SetActive(false);
        SendMsg(new MsgBase((ushort)UIEvent.ShowUserCenter));

    }
    void RightPageClick()
    {
        FavoriteRoot.gameObject.SetActive(false);
        SendMsg(new MsgBase((ushort)UIEvent.ShowUserCenter));

    }
}
