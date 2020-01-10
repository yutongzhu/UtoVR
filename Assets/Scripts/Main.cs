using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using VRStandardAssets.Utils;
using UnityEngine.UI;
using System;
using System.Collections.Specialized;
using System.Net;
using UnityEngine.Networking.Match;
using System.IO;
using System.Text;
using System.Threading;
using Pvr_UnitySDKAPI;

public class Main : MonoBehaviour
{
    private Text m_Page; //显示 页面信息文本（显示当前页/总页）
    private Text m_WarningText; //显示 加载中.... 信息文本
    private Image m_BackgroundImage; // “加载中...”的背景图片，包含了m_WarningText
    private Transform m_TextTransform; // m_WarningText的位置

    private List<TypeInfo> listType = new List<TypeInfo>();//分类，目前就两个，即4K专区和VR专区
    public static int currentType = 0; //分类索引
    public static int toBeType = 0;//带切换的分类id

    private List<VideoInfo> list = new List<VideoInfo>(); //某个分类的视频内容
    public static int pageIndex = 0; //当前页的起始数
    private int onePageNumber = 6; //单页里显示的总数目
    private int onePageRowNumber = 3;//单页里单行显示的总数目
    private int totalPage; //总页数；
    private int currentPage; //当前页；

    private string path;
    
    private List<GameObject> pageGameObj = new List<GameObject>(); //三个按钮
    public static int pageSelectIndex = 0;  //默认选中的按钮（0、1、2）这三个数值中一个

    public Material m_OverMaterial; //分类、页 中选中图片
    public Material m_NorMaterial;  //分类、页 中未选中图片

    // 获取内置SD卡路径  
    public static string GetStoragePath()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return new AndroidJavaClass("android.os.Environment").CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getPath");
        }
        return null;
    }

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidAPI.Init();
        }
        //UnityEngine.VR.VRSettings.renderScale = 1f;
        QualitySettings.masterTextureLimit = 0;
        path = Application.persistentDataPath + "/";

        //获取各组件
        m_Page = GameObject.FindWithTag("Page").GetComponent<Text>();
        m_WarningText = GameObject.FindWithTag("WarningText").GetComponent<Text>();
        m_BackgroundImage = GameObject.FindWithTag("WarningTextBg").GetComponent<Image>();
        m_TextTransform = GameObject.FindWithTag("WarningTextTransform").GetComponent<Transform>();
        
        //三个页面组件
        GameObject p1 = GameObject.Find("Page1");
        GameObject p2 = GameObject.Find("Page2");
        GameObject p3 = GameObject.Find("Page3");
        pageGameObj.Clear();
        pageGameObj.Add(p1);
        pageGameObj.Add(p2);
        pageGameObj.Add(p3);

        //初始化分类
        initType();

        //加载分类的内容
        init();

    }

    private void init()
    {
        m_WarningDisplayTime = 100;
        m_WarningCoroutine = StartCoroutine(DisplayWarning("加载中..."));

        string index = listType[toBeType].Index;
        string indexKey = index.Replace("/", "_");
        string api = indexKey + NetConstant.TV189_CPMSURL_SUFFIX;
        string key = api.Replace("/", "");
        string apiData = PlayerPrefs.GetString(key);
        if (!string.IsNullOrEmpty(apiData))
        {
            parseData(apiData);
        }
        else
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                toast("网络无连接");
                //告知不要isLoading了，否则再点击Type，不会加载数据了
                isLoading = false;
            }
            else
            {
                StopAllCoroutines();

                StartCoroutine(RequestAPI(api));
            }
        }
    }

    public static bool isLoading = true;
    IEnumerator RequestAPI(string api)
    {
        string key = api.Replace("/", "");
        WWW ret = new WWW(NetConstant.TV189_BASEURL + api);
        yield return ret;

        if (ret.error != null || string.IsNullOrEmpty(ret.text))
        {
            Debug.LogError("error:" + ret.error);
            isLoading = false;
            yield break;
        }
        string result = ret.text;
        PlayerPrefs.SetString(key, result);

        if (isLoading)
        {
            parseData(result);
        }
    }

    private void parseData(string jsonStr)
    {
        JsonData jsdArray = JsonMapper.ToObject(jsonStr);//转换成json格式;需要引入LitJson
        JsonData jsdList = jsdArray["data"];
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(GameObject.Find("ID" + list[i].ID));
        }
        list.Clear();
        int id = 0;
        if (jsdList.IsArray)
        {
            for (int i = 0; i < jsdList.Count; i++)
            {
                //直接data部分
                JsonData jsd = jsdList[i]["data"];
                if (jsd.Keys.Contains("data"))
                {
                    JsonData j_data_data = jsd["data"];
                    if (j_data_data.IsArray)
                    {
                        for (int j = 0; j < j_data_data.Count; j++)
                        {
                            JsonData jsd_data = j_data_data[j];
                            VideoInfo vi = createVideoInfo(jsd_data, id);

                            list.Add(vi);
                            id = id + 1;
                        }
                    }
                }

                //children中data部分
                if (jsd.Keys.Contains("children"))
                {
                    JsonData j_data_children = jsd["children"];
                    if (j_data_children.IsArray)
                    {
                        for (int j = 0; j < j_data_children.Count; j++)
                        {
                            JsonData jsd_children = j_data_children[j];
                            JsonData j_data_children_data = jsd_children["data"];
                            if (j_data_children_data.IsArray)
                            {
                                for (int k = 0; k < j_data_children_data.Count; k++)
                                {
                                    JsonData jsd_data = j_data_children_data[k];
                                    VideoInfo vi = createVideoInfo(jsd_data, id);

                                    list.Add(vi);
                                    id = id + 1;
                                }
                            }
                        }
                    }
                }
            }

        }

        //选中状态
        currentType = toBeType;
        upateTypeMaterial();

        //总页数
        totalPage = list.Count % onePageNumber == 0 ? list.Count / onePageNumber : list.Count / onePageNumber + 1;

        showUI(true, 0);

        isLoading = false;
        m_WarningText.text = string.Empty;
        m_BackgroundImage.enabled = false;
        m_DisplayingWarning = false;

    }

    private VideoInfo createVideoInfo(JsonData jsd_data, int id)
    {
        VideoInfo vi = new VideoInfo();
        vi.ID = id;
        vi.cover = (string)jsd_data["cover"];
        vi.title = (string)jsd_data["title"];
        vi.clickType = (int)jsd_data["clickType"];
        vi.subscript = jsd_data.Keys.Contains("subscript") ? (string)jsd_data["subscript"] : "";
        vi.contentId = (string)jsd_data["contentId"];
        vi.clickParam = (string)jsd_data["clickParam"];
        vi.category = toBeType;

        return vi;
    }


    public void updateUI(object[] objs)
    {
        pageSelectIndex = (int)(objs[1]);

        showUI((bool)(objs[0]), 0);
    }

    public void showUI(bool updatePage, int startIndex)
    {
        //当前页
        currentPage = pageIndex / onePageNumber + 1;
        m_Page.text = (pageIndex / onePageNumber + 1) + " / " + totalPage;

        for (int i = 0; i < list.Count; i++)
        {
            Destroy(GameObject.Find("ID" + list[i].ID));
        }

        for (int i = pageIndex; i < list.Count && i < pageIndex + onePageNumber; i++)
        {
            Vector3 rotation = new Vector3(0, 0, 0);
            Vector3 position = new Vector3(-9.2f + (i % onePageRowNumber) * 9.0f, 4.0f, 8.0f);

            if (i < pageIndex + onePageRowNumber)
            {
                position.y = 4.0f;
            }
            else
            {
                position.y = -2.0f;
            }
            addThumbnail(i, position, rotation);
        }

        if (updatePage)
        {
            for (int i = 0; i < pageGameObj.Count; i++)
            {
                GameObject pageObj = pageGameObj[i];
                if (pageObj != null)
                {
                    TextMesh pageObjText = pageObj.GetComponentInChildren<TextMesh>();
                    pageObjText.text = (pageIndex / onePageNumber + 1 + i + startIndex).ToString();
                
                    TextInteractiveItem pageTI = pageObj.GetComponentInChildren<TextInteractiveItem>();
                    pageTI.name = "page_" + i + "_" + (int.Parse(pageObjText.text) - 1) * onePageNumber;
                }
            }
        }

        //默认是第一个
        upatePageMaterial(pageSelectIndex);
    }

    private void upatePageMaterial(int selectId)
    {
        for (int i = 0; i < pageGameObj.Count; i++)
        {
            GameObject pageObj = pageGameObj[i];
            if (pageObj != null)
            {
                TextInteractiveItem gameTI = pageObj.GetComponentInChildren<TextInteractiveItem>();
                if (i == selectId)
                {
                    gameTI.GetComponent<Renderer>().material = m_OverMaterial;
                    gameTI.isSelected = true;
                }
                else
                {
                    gameTI.GetComponent<Renderer>().material = m_NorMaterial;
                    gameTI.isSelected = false;
                }
            }
        }
    }

    public void Last()
    {
        int startIndex = -2;
        bool needUpate = false;
        pageSelectIndex -= 1;
        if (pageSelectIndex < 0)
        {
            pageSelectIndex = pageGameObj.Count - 1;
            needUpate = true;
        }

        if (pageSelectIndex == pageGameObj.Count - 1)
        {
            int yu = currentPage - pageGameObj.Count;
            if (yu < 1)
            {
                startIndex = 1+yu;
                pageSelectIndex = -(1+yu);
            }
        }

        pageIndex -= onePageNumber;
        if (pageIndex < 0)
        {
            pageIndex += onePageNumber;
            pageSelectIndex = 0;
            toast("已是第一页");
            return;
        }

        showUI(needUpate, startIndex);

    }

    public void Next()
    {
        int startIndex = 0;
        bool needUpate = false;
        pageSelectIndex += 1;
        if (pageSelectIndex > (pageGameObj.Count-1))
        {
            pageSelectIndex = 0;
            needUpate = true;
        }

        if (pageSelectIndex == 0)
        {
            int yu = totalPage - currentPage - pageGameObj.Count;
            if (yu < 1)           
            {
                startIndex = yu;
                pageSelectIndex = -yu;
            }
        }

        pageIndex += onePageNumber;
        if (pageIndex > list.Count - 1)
        {
            pageIndex -= onePageNumber;
            pageSelectIndex = 2;
            toast("已是最后一页");
            return;
        }
        showUI(needUpate, startIndex);
    }

    private void addThumbnail(int index, Vector3 position, Vector3 rotation)
    {
        VideoInfo vi = list[index];
        GameObject cube = Instantiate(Resources.Load("Prefabs/Thumbnail00"), Vector3.zero, Quaternion.identity) as GameObject;
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.Euler(rotation);
        cube.name = "ID" + vi.ID;
        cube.AddComponent<VRInteractiveItem>();
        ThumbnailInteractiveItem eii = cube.AddComponent<ThumbnailInteractiveItem>();
        eii.videoID = vi.ID + ""; //一开始测试用
        eii.vInfo = vi;
        string name = vi.title;
        if (name.Length > 10)
        {
            name = name.Substring(0, 10) + "...";
        }
        if (!vi.title.StartsWith("Name"))
        {
            cube.GetComponentInChildren<TextMesh>().text = name;
        }

        // 判断是否是第一次加载这张图片
        if (vi.cover != null && !File.Exists(path + vi.cover.GetHashCode()))
        {
            //如果之前不存在缓存文件
            StartCoroutine(LoadTexture(cube, vi.cover));
        }
        else
        {
            StartCoroutine(LoadLocalImage(cube, vi.cover));
        }
    }

    IEnumerator LoadTexture(GameObject gameObject, string Thumbnail)
    {
        WWW www = new WWW(Thumbnail);
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Texture texture = www.texture;
            Texture2D image = www.texture;
            if (gameObject != null)
            {
                image.filterMode = FilterMode.Trilinear;
                gameObject.GetComponent<Renderer>().material.mainTexture = image;
            }
            byte[] pngData = image.EncodeToPNG();
            File.WriteAllBytes(path + Thumbnail.GetHashCode(), pngData);
        }
    }

    IEnumerator LoadLocalImage(GameObject gameObject, string Thumbnail)
    {
        string filePath = "file:///" + path + Thumbnail.GetHashCode();

        WWW www = new WWW(filePath);
        yield return www;
        //直接贴图
        if (gameObject != null)
        {
            Texture2D image = www.texture;
            image.filterMode = FilterMode.Trilinear;
            gameObject.GetComponent<Renderer>().material.mainTexture = image;
        }
    }

    private void destoryVideoThumbnail()
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject gObject = GameObject.Find("ID" + list[i].ID);
            if (gObject != null)
            {
                Destroy(gObject);
            }
        }
    }

    private void destoryType()
    {
        for (int i = 0; i < listType.Count; i++)
        {
            GameObject gObject = GameObject.Find("paratype" + listType[i].ID);
            if (gObject != null)
            {
                Destroy(gObject);
            }
        }
    }

    // 分类
    private void initType()
    {
        for (int i = 0; i < 2; i++)
        {
            TypeInfo ti = new TypeInfo();
            ti.ID = i;
            if(i==0) {
                ti.Name = NetConstant.TYPE_4K_NAME;
                ti.Index = NetConstant.CPMS_4K;
            }
            else
            {
                ti.Name = NetConstant.TYPE_VR_NAME;
                ti.Index = NetConstant.CPMS_VR;
            }
            listType.Add(ti);
        }


        for (int i = 0; i < listType.Count; i++)
        {
            //左边
            addType(listType[i], new Vector3(-14.6f, 5.62f - i * 1.1f, -2), new Vector3(0, 330, 0));
        }

        //选中状态
        upateTypeMaterial();
        
    }

    private void upateTypeMaterial()
    {
        for (int i = 0; i < listType.Count; i++)
        {
            GameObject gameObj = GameObject.Find("paratype" + i + "/type" + i);
            if (gameObj != null)
            {
                TextInteractiveItem gameTI = gameObj.GetComponentInChildren<TextInteractiveItem>();
                if (i == Main.currentType)
                {
                    gameObj.GetComponent<Renderer>().material = m_OverMaterial;
                    gameTI.isSelected = true;
                }
                else
                {
                    gameObj.GetComponent<Renderer>().material = m_NorMaterial;
                    gameTI.isSelected = false;
                }
            }
        }
    }

    private void addType(TypeInfo ti, Vector3 position, Vector3 rotation)
    {
        GameObject objPrefab = Instantiate(Resources.Load("Prefabs/Type"), Vector3.zero, Quaternion.identity) as GameObject;
        objPrefab.name = "paratype" + ti.ID;
        GameObject.Find(objPrefab.name + "/type").name = "type" + ti.ID;
        objPrefab.GetComponentInChildren<TextMesh>().text = ti.Name;
        objPrefab.transform.position = position;
        objPrefab.transform.rotation = Quaternion.Euler(rotation);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.APP))
        {
            toast("再按一次退出");
            escapeTimes++;
            StartCoroutine(resetTimes());
            if (escapeTimes > 1)
            {
                Application.Quit();
            }
        }

        if (Input.GetKeyDown(KeyCode.Home) || Pvr_UnitySDKAPI.Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.HOME))
        {
            //Application.Quit();
        }

    }

    int escapeTimes = 0;
    IEnumerator resetTimes()
    {
        yield return new WaitForSeconds(1);
        escapeTimes = 0;

        m_WarningText.text = string.Empty;
        m_BackgroundImage.enabled = false;
        m_DisplayingWarning = false;
    }

    private bool m_DisplayingWarning;                                               // Whether the warning is currently being displayed.
    private float m_WarningDisplayTime = 9f;                       					// How long the warning is displayed for.
    private Coroutine m_WarningCoroutine;                                           // Reference to the coroutine that displays the warning message so it can be stopped prematurely.

    private IEnumerator DisplayWarning(string message)
    {
        // If a warning is already being displayed, quit.
        if (m_DisplayingWarning)
            yield break;

        m_WarningText.text = message;
        m_BackgroundImage.enabled = true;

        if (true || Input.GetKeyDown(KeyCode.Escape))
        {
            m_TextTransform.position = new Vector3(0, 0, -5);
            m_TextTransform.rotation = Quaternion.identity;
        }

        yield return new WaitForSeconds(m_WarningDisplayTime);

        m_WarningText.text = string.Empty;
        m_BackgroundImage.enabled = false;

        m_DisplayingWarning = false;
    }

    public void toast(string msg)
    {
        m_WarningDisplayTime = 3;
        StartCoroutine(DisplayWarning(msg));
    }
}
