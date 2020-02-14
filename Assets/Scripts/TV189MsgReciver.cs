using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 同android端通讯类，供android端调用Unity的方法
/// </summary>
public class TV189MsgReciver : MonoBehaviour {

    public static TV189MsgReciver instance = null;
    public MediaPlayerCtrl mediaPlayerCtrl;
    
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 获取到视频信息json的回调方法
    /// </summary>
    /// <param name="json"></param>
    public static List<ClassifyVideoItem> classifyVideoItems = new List<ClassifyVideoItem>();
    public static string selectContentId;//筛选列表当前选定的id 左边栏的
    //不同的分类id 对应不同的列表
    public static Dictionary<string, List<ClassifyVideoItem>> itemsListDic = new Dictionary<string, List<ClassifyVideoItem>>();
    // 每个item 根据contentId 区分
    public static Dictionary<string, ClassifyVideoItem> classifyVideosDic = new Dictionary<string, ClassifyVideoItem>();
    public  int GetTotalPagesCount(int totalcount)
    {
        int count;

        if (totalcount<4)//当只有一个页面切数量小于一次显示的数量时候
        {
            count = 1;
        }
        else
        {
            if (totalcount / 4 == 0)
            {
                count = totalcount / 4;
            }
            else
            {
                count = totalcount / 4 + 1;
            }
        }
       
        return count;

    }
    public static int  totalPages;
    public void  GetVideosJson(string json )
    {
      //  print(json);

        ClassifyViteoRoot classifyViteoRoot = JsonConvert.DeserializeObject<ClassifyViteoRoot>(json);
        ClassifyInfo classifyInfo = classifyViteoRoot.info;
        totalPages = GetTotalPagesCount(classifyInfo.total);//总的页面数
        if (classifyVideoItems.Count >0)//
        {
            classifyVideoItems.Clear();
        }
        if (classifyInfo.data.Count==0)//表示此时的二级界面没有数据
        {
            LauncherUIManager.instance.NoDataReturned();
        }
        else//二级界面有数据返回的时候
        {
            LauncherUIManager.instance.HaveDataReturned();
            //赋值
            for (int i = 0; i < classifyInfo.data.Count; i++)
            {
                classifyVideoItems.Add(classifyInfo.data[i]);
                VideoItem item = new VideoItem();
                item.title = classifyInfo.data[i].title;
                item.contentId = classifyInfo.data[i].contentId;
                item.subscript = classifyInfo.data[i].description;
                //  item.title = classifyInfo.data[i].title;
                if (!classifyVideosDic.ContainsKey(classifyVideoItems[i].contentId))
                {
                    classifyVideosDic.Add(classifyVideoItems[i].contentId, classifyVideoItems[i]);

                }
                if (!JsonDataManager.VideosDic.ContainsKey(item.contentId))
                {
                    JsonDataManager.VideosDic.Add(item.contentId, item);

                }
            }
            //正常情况下都是小于等于4的测试时候会走这一步
            if (classifyInfo.data.Count > LauncherUIManager.instance.videoButtonList.Count)
            {
                for (int i = 0; i < 4; i++)
                {
                    LauncherUIManager.instance.videoButtonList[i].name = classifyVideoItems[i].contentId;
                    RawImage im = LauncherUIManager.instance.videoButtonList[i].transform.
                          Find("poster").GetComponent<RawImage>();
                    Text title = LauncherUIManager.instance.videoButtonList[i].transform.
                        Find("Text").GetComponent<Text>();
                    title.text = classifyVideoItems[i].title;
                    SetImage(classifyVideoItems[i].imgM0, im);
                }
            }
            else
            {
                for (int i = 0; i < LauncherUIManager.instance.videoButtonList.Count; i++)
                {
                    if (i < classifyVideoItems.Count)
                    {
                        LauncherUIManager.instance.videoButtonList[i].name = classifyVideoItems[i].contentId;
                        RawImage im = LauncherUIManager.instance.videoButtonList[i].transform.
                            Find("poster").GetComponent<RawImage>();
                        Text title = LauncherUIManager.instance.videoButtonList[i].transform.
                            Find("Text").GetComponent<Text>();
                        title.text = classifyVideoItems[i].title;
                        SetImage(classifyVideoItems[i].imgM0, im);


                    }
                    else
                    {
                        LauncherUIManager.instance.videoButtonList[i].name = "";

                    }

                }
            }

            LauncherUIManager.instance.pageShowText.text = LauncherUIManager.instance.currentPageIndex + "/" + totalPages.ToString();
        }
       
    }
    public void SetImage(string path, RawImage image)
    {
        StartCoroutine(GetTexture(path, image));
    }
    IEnumerator GetTexture(string path, RawImage image)
    {
        // Debug.Log(path);
        UnityWebRequest www = new UnityWebRequest(path);
        DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
        www.downloadHandler = texDl;
        yield return www.Send();

        int width = 400;

        int height = 200;

        // Texture2D texture = new Texture2D(width, height);

        if (!www.isError)
        {
            Texture2D tex = new Texture2D(width, height);
            tex = texDl.texture;
            image.texture = tex;
            
        }

    }
    public static int getClickParam(int categroyId, int contentType)
    {
        int clickParam = 0;
        switch (contentType)
        {
            case 103:
                clickParam = 3;
                break;
            case 104:
                clickParam = 4;
                break;
            case 102:
                clickParam = 5;
                break;
            case 100:
                if (categroyId == 1 || categroyId == 2)
                {
                    clickParam = 0;
                }
                else
                {
                    clickParam = 6;
                }
                break;
            case 200:
            default:
                clickParam = 6;
                break;
        }
        return clickParam;

    }

    //播放VR视频
    public void PlayerVR(string url)
    {
        LauncherUIManager.instance.panelBackStatus = PanelBackStatus.VideoPlayPanel;
        Debug.Log("PlayerVR111");

            MediaPlayerCtrl.m_videoID = url;
            MediaPlayerCtrl.is3dUD = false;
    


        if (SceneManager.GetActiveScene().name == "MainVR")
        {
          
            LightManger.instance.VideoCanvasRootVR.SetActive(true);
            LightManger.instance.VideoScreen.SetActive(false);
            LightManger.instance.VideoScreenVR.SetActive(true);
            LightManger.instance.VideoCanvasRoot.SetActive(false);


        }
        else
        {
          //LauncherUIManager .instance  .  panelBackStatus = PanelBackStatus.PlayerVR;
            SceneManager.LoadScene("PlayerVR");
        }

        //if (isScreenLoaded == true)
        //{

        //    mediaPlayerCtrl.Load(MediaPlayerCtrl.m_videoID);
        //}
        //else
        //{
        //    isScreenLoaded = true;
        //}
    }
    bool isScreenLoaded = false;
    //播放2D视频
    public void Player2D(string url)
    {
        LauncherUIManager.instance.panelBackStatus = PanelBackStatus.VideoPlayPanel;
        MediaPlayerCtrl.m_videoID = url;
            MediaPlayerCtrl.is3dUD = false;
            mediaPlayerCtrl.Load(MediaPlayerCtrl.m_videoID);
          
     

    }

    public static List<LiveUrlData> liveDataList = new List<LiveUrlData>();
    public static Dictionary<string , LiveUrlData> liveUrlDic = new Dictionary<string , LiveUrlData>();
    //播放直播视频
    public void PlayerLive(string urlJson)
    {
        Debug.Log("urlJson" + urlJson);
        
        JArray urlArry = (JArray)JsonConvert.DeserializeObject(urlJson);
        int count = urlArry.Count;
        if (liveDataList.Count >0)//，每次加载需要重新清理之前数据
        {
            liveDataList.Clear();
        }
        for (int i = 0; i < count; i++)
        {
            LiveUrlData livedata = new LiveUrlData();
            livedata.playUrl= urlArry[i]["playUrl"].ToString();
            livedata.audioUrl = urlArry[i]["audioUrl"].ToString();
            livedata.qualityName = urlArry[i]["qualityName"].ToString();
            livedata.qualityid = urlArry[i]["qualityid"].ToString();
            livedata.vid = urlArry[i]["vid"].ToString();
            liveDataList.Add(livedata);
            if (!liveUrlDic.ContainsKey(livedata.qualityid))
            {
                liveUrlDic.Add(livedata.qualityid, livedata);
            }
        }
        MediaPlayerCtrl.m_videoID = urlArry[0]["playUrl"].ToString(); 
        MediaPlayerCtrl.is3dUD = false;
        //LivePlayerManager.instance.LiveVideoCanvasRoot.gameObject.SetActive(true);
        //LivePlayerManager.instance.LiveVideoRoot.gameObject.SetActive(true);
       
        LiveUImanager.instance.LivePlayDisplay();
     



        mediaPlayerCtrl.Load(MediaPlayerCtrl.m_videoID);
    }

    //多码率切换
    public void ChangeLevelWithPath(string path)
    {

        // if (SceneManager.GetActiveScene().name == "MainVR")
//{
        if (LauncherUIManager.instance.columnType == ColumnType.VR)
        {
                GameObject.Find("VideoPlayFather/VideoScreenVR").SendMessage("ChangeLevelWithPath", path);
         }
        else
        {
            GameObject.Find("VideoPlayFather/Cinema/VideoScreen").SendMessage("ChangeLevelWithPath", path);
        }
       // }
       // {
          //  GameObject.Find("VideoPlayFather/VideoScreen").SendMessage("ChangeLevelWithPath", path);
        //}
    }


   public void AndroidBackClick()
    {

        if (LauncherUIManager.instance.panelBackStatus == PanelBackStatus.VideoPlayPanel)
        {
            VideoUImanager.instance.BackMainClick();
        }
        else if (LauncherUIManager.instance.panelBackStatus == PanelBackStatus.VideoDetail)
        {
            VideoControl.instance.BackLauncherClick();
        }
        else if (LauncherUIManager.instance.panelBackStatus == PanelBackStatus.LivePlayer)
        {
            LivePlayerManager .instance.BackMainClick();
        }
        else if(LauncherUIManager.instance.panelBackStatus == PanelBackStatus.Default)
        {
            Application.Quit();
        }
        LauncherUIManager.instance.panelBackStatus = PanelBackStatus.Default;
    }
	
}
