using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveUImanager : UIBase
{
    public static LiveUImanager instance;
    List<Button> liveItems = new List<Button>();
    List<RawImage> liveRawImages = new List<RawImage>();
    List<Text> liveTitles = new List<Text>();
    public void SetLiveItemDate()
    {
            for (int i = 0; i < liveItems.Count; i++)
            {
                if (i<JsonDataManager.liveItems.Count )
                {
                    liveTitles[i].text = JsonDataManager.liveItems[i].title;
                    liveItems[i].name = JsonDataManager.liveItems[i].liveId;
                    LiveItemData Items = new LiveItemData();
                    Items.contentId = JsonDataManager.liveItems[i].liveId;
                    Items.title = JsonDataManager.liveItems[i].title;
                    
                    if (!JsonDataManager.liceItemDic.ContainsKey(Items.title))
                    {
                        JsonDataManager.liceItemDic.Add(Items.contentId, Items);//加载图片完成后，加入字典。根据id添加
                    }
                    JsonDataManager.instance.SetImage(JsonDataManager.liveItems[i].cover, Items, liveRawImages[i]);
                }
                else
                {
                    liveTitles[i].text = "";
                   liveItems[i].gameObject.SetActive(false );
                }
         
            }
           
         
      }
      
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowLivePart :
                {

                    LiveRoot.gameObject.SetActive(true );
                    if (isLoad ==false )//加载过 后的不能重复赋值
                    {
                        int num = 0;
                        foreach (Button item in LiveItemCotent.GetComponentsInChildren<Button>())
                        {
                            liveItems.Add(item);
                            RawImage raw = item.GetComponent<RawImage>();
                            Text title = item.GetComponentInChildren<Text>();
                            liveRawImages.Add(raw);
                            liveTitles.Add(title);
                            //print(num);
                           // print(item.name );
                            num++;
                        }


                        JsonDataManager.instance .LoadLivePart ();
                    
                        //int index = 0;
                        //foreach (Button item in LiveItemCotent.GetComponentsInChildren<Button>())
                        //{
                        //    RawImage raw = item.GetComponent<RawImage>();
                        //    Text title = item.transform.Find("name").GetComponent<Text>();
                           
                        //    title.text = JsonDataManager.liveItems[index].title;
                        //    item.name = JsonDataManager.liveItems[index].liveId;
                        //    LiveItemData  Items = new LiveItemData();
                        //    Items.contentId = JsonDataManager.liveItems[index].liveId ;
                        //    Items.title = JsonDataManager.liveItems[index].title;
                        //   // Items.subscript = JsonDataManager.liveItems[index].subscript;
                        //    if (!JsonDataManager.liceItemDic.ContainsKey(Items.title))
                        //    {
                        //        JsonDataManager.liceItemDic .Add(Items.contentId , Items);//加载图片完成后，加入字典。根据id添加
                        //    }
                        //    JsonDataManager.instance.SetImage(JsonDataManager.liveItems[index ].cover, Items, raw);
                        //    index++;
                        //}

                        isLoad = true;
                    }
                    
                }
                break;
            case (ushort)UIEvent.HideLivePart :

                {

                    LiveRoot.gameObject.SetActive(false);
                }
                break;
        }
    }
    private void Awake()
    {
        instance = this;
        msgids = new ushort[]
{
     (ushort )UIEvent .ShowLivePart ,
        (ushort )UIEvent .HideLivePart 
};
        RegistSelf(this, msgids);

    }

    Transform LiveRoot;
    Transform LiveItemCotent;
    // Transform LiveExitButton;
    bool isLoad=false;//表示是否初始化加载过了
    void Start()
    {
        LiveRoot = UISettingManager.GetUITransform("LiveRoot");
        LiveItemCotent = UISettingManager.GetUITransform("LiveItemCotent");
       
        UISettingManager.AddButtonClickListener("LiveExitButton", ExitLive);
      
        LiveRoot.gameObject.SetActive(false );
       
        }

    void ExitLive()
    {
        LiveRoot.gameObject.SetActive(false );
        SendMsg(new MsgBase ((ushort )UIEvent .ShowLauncher ));
        LauncherUIManager.instance.LauncherView.gameObject.SetActive(true );
    }
}
