using Newtonsoft.Json;
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

    private static TV189MsgReciver _instance = null;

    public static TV189MsgReciver instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("TV189MsgReciver");
                _instance = go.AddComponent<TV189MsgReciver>();
            }
            return _instance;
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
        if (totalcount / 4==0)
        {
            count = totalcount / 4;
        }
        else
        {
            count = totalcount / 4+1;
        }
        return count;

    }
    public static int  totalPages;
    public void  GetVideosJson(string json )
    {
       // print(json);

        ClassifyViteoRoot classifyViteoRoot = JsonConvert.DeserializeObject<ClassifyViteoRoot>(json);
        ClassifyInfo classifyInfo = classifyViteoRoot.info;
        totalPages = GetTotalPagesCount(classifyInfo.total);//总的页面数
        if (classifyVideoItems.Count >0)//
        {
            classifyVideoItems.Clear();
        }
       
        for (int i = 0; i < classifyInfo.data.Count ; i++)
        {
            classifyVideoItems.Add(classifyInfo.data[i]);
            VideoItem item = new VideoItem();
            item.title = classifyInfo.data[i].title;
            item.contentId  = classifyInfo.data[i].contentId;
            item.subscript  = classifyInfo.data[i].description;
          //  item.title = classifyInfo.data[i].title;
            if (!classifyVideosDic.ContainsKey (classifyVideoItems[i].contentId))
            {
                classifyVideosDic.Add(classifyVideoItems[i].contentId, classifyVideoItems[i]);

            }
            if (!JsonDataManager .VideosDic .ContainsKey(item.contentId))
            {
                JsonDataManager.VideosDic.Add(item.contentId, item );

            }
        }
        //正常情况下都是小于等于4的测试时候会走这一步
        if (classifyInfo.data.Count> LauncherUIManager.instance.videoButtonList.Count)
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
                if (i< classifyVideoItems.Count )
                {
                    LauncherUIManager.instance.videoButtonList[i].name = classifyVideoItems[i].contentId;
                    RawImage im = LauncherUIManager.instance.videoButtonList[i].transform.
                        Find("poster").GetComponent<RawImage>();
                    Text title= LauncherUIManager.instance.videoButtonList[i].transform.
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
       
       LauncherUIManager.instance.pageShowText.text = LauncherUIManager.instance.currentPageIndex  + totalPages.ToString ();
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
        Debug.Log("PlayerVR000");
        if (SceneManager.GetActiveScene().name == "Main")
        {
            Debug.Log("PlayerVR111");

            MediaPlayerCtrl.m_videoID = url;
            MediaPlayerCtrl.is3dUD = false;
      //      MediaPlayerCtrl.infoName = "No";
            SceneManager.LoadScene("Player", LoadSceneMode.Single);
        }
     else  if (SceneManager.GetActiveScene().name == "MainVR")
        {
            Debug.Log("PlayerVR111");

            MediaPlayerCtrl.m_videoID = url;
            MediaPlayerCtrl.is3dUD = false;
            //      MediaPlayerCtrl.infoName = "No";
            SceneManager.LoadScene("PlayerVR", LoadSceneMode.Single);
        }
    }

    //播放2D视频
    public void Player2D(string url)
    {
        Debug.Log("Player2D000");
        if (SceneManager.GetActiveScene().name == "Main")
        {
            Debug.Log("Player2D111");
            MediaPlayerCtrl.m_videoID = url;
            MediaPlayerCtrl.is3dUD = false;
           // MediaPlayerCtrl.infoName = "No";
            SceneManager.LoadScene("Player2D", LoadSceneMode.Single);
        }
       else if (SceneManager.GetActiveScene().name == "MainVR")
        {
            Debug.Log("Player2D111");
            MediaPlayerCtrl.m_videoID = url;
            MediaPlayerCtrl.is3dUD = false;
            // MediaPlayerCtrl.infoName = "No";
            SceneManager.LoadScene("Player2DVR", LoadSceneMode.Single);
        }

    }

    //多码率切换
    public void ChangeLevelWithPath(string path)
    {
        if (SceneManager.GetActiveScene().name == "Player" || SceneManager.GetActiveScene().name == "Player2D")
        {
            GameObject.Find("VideoScreen").SendMessage("ChangeLevelWithPath", path);
        }
    }
    
  
	
	
}
