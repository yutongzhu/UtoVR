using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using VRStandardAssets.Utils;

/// <summary>
/// 缩略图组件的脚本
/// </summary>
public class ThumbnailInteractiveItem : MonoBehaviour
{
    public VideoInfo vInfo = null;
    public string videoID = "4292";
    public bool is3dUD;
    private VRInteractiveItem m_InteractiveItem;
    public event Action<ThumbnailInteractiveItem> OnButtonSelected;     
    private SelectionRadial m_SelectionRadial;         					
    private bool m_GazeOver;                                            


    //TV189视频地址
    private static int indexXX = 8;
    private string h264_m3u8 = "http://vod5g.nty.tv189.com/vod/C42102172/S1/index.m3u8";
    private string h265_m3u8 = "http://vod5g.nty.tv189.com/vod/C42102172/S2/index.m3u8";
    

    private string p50_vid_9023176146 = "http://vod3.nty.tv189.cn/6/ol/st02/2019/08/23/Q200_dffb2a9a-e7a3-459b-8d17-c5b853f5748f.mp4.m3u8?sign=ff7c33e05a151d3573be2c5d38eeef6e&qualityCode=294&qualityid=2&version=1&guid=55dd837b-00d8-6402-e6fe-84a85b9a4547&app=115020310221&cookie=52e4755ea11d905d1b49a1340a2c439d&session=52e4755ea11d905d1b49a1340a2c439d&uid=104318916908268130204&uname=18916908268&time=20190907192814&videotype=2&cid=C42162367&cname=&cateid=&dev=000001&ep=1&os=30&ps=0099&clienttype=LYA-AL00&appver=5.3.45.1&res=1080%2A2145&channelid=059999&pid=1000000432&orderid=&nid=&cp=00000368&sp=00000014&ip=114.86.207.71&ipSign=d60d296515f3fc47b1d09eaaffe33cab&cdntoken=api_5d73944e7bc20&a=7t8vRiyFpzv5z2RvVNizqCqLH2FoSrzpUa%2Fn%2Ba2clQ%2FrD916eef7EQ%3D%3D&pvv=1.2.0&t=5d73cc8e&cf=tx&s2=b53b3a3d6ab90ce0268229151c9bde11";
    private string p150_vid_9023176148 = "http://vod3.nty.tv189.cn/6/ol/st02/2019/08/23/Q350_874707b1-f002-4681-820f-12ec62493706.mp4.m3u8?sign=2de687f5d06a0614ea7b8b65925afa99&qualityCode=662&qualityid=8&version=1&guid=55dd837b-00d8-6402-e6fe-84a85b9a4547&app=115020310221&cookie=52e4755ea11d905d1b49a1340a2c439d&session=52e4755ea11d905d1b49a1340a2c439d&uid=104318916908268130204&uname=18916908268&time=20190907192814&videotype=2&cid=C42162367&cname=&cateid=&dev=000001&ep=1&os=30&ps=0099&clienttype=LYA-AL00&appver=5.3.45.1&res=1080%2A2145&channelid=059999&pid=1000000432&orderid=&nid=&cp=00000368&sp=00000014&ip=114.86.207.71&ipSign=d60d296515f3fc47b1d09eaaffe33cab&cdntoken=api_5d73944e7bc20&a=7t8vRiyFpzv5z2RvVNizqCqLH2FoSrzpUa%2Fn%2Ba2clQ%2FrD916eef7EQ%3D%3D&pvv=1.2.0&t=5d73cc8e&cf=tx&s2=b53b3a3d6ab90ce0268229151c9bde11";
    private string p300_vid_9023176149 = "http://vod3.nty.tv189.cn/6/ol/st02/2019/08/23/Q600_cf0cbd52-9ec5-4f68-8688-0e691fee361e.mp4.m3u8?sign=bc8272da6bd11005e94c3c0b882b816f&qualityCode=1007&qualityid=16&version=1&guid=55dd837b-00d8-6402-e6fe-84a85b9a4547&app=115020310221&cookie=52e4755ea11d905d1b49a1340a2c439d&session=52e4755ea11d905d1b49a1340a2c439d&uid=104318916908268130204&uname=18916908268&time=20190907192814&videotype=2&cid=C42162367&cname=&cateid=&dev=000001&ep=1&os=30&ps=0099&clienttype=LYA-AL00&appver=5.3.45.1&res=1080%2A2145&channelid=059999&pid=1000000432&orderid=&nid=&cp=00000368&sp=00000014&ip=114.86.207.71&ipSign=d60d296515f3fc47b1d09eaaffe33cab&cdntoken=api_5d73944e7bc20&a=7t8vRiyFpzv5z2RvVNizqCqLH2FoSrzpUa%2Fn%2Ba2clQ%2FrD916eef7EQ%3D%3D&pvv=1.2.0&t=5d73cc8e&cf=tx&s2=b53b3a3d6ab90ce0268229151c9bde11";
    private string h264_4M_vid_9023176150 = "http://vod3.nty.tv189.cn/6/ol/st02/2019/08/23/Q4M_89fd7755-e1c0-4742-826e-0dc15b4f34f2.mp4.m3u8?sign=9843317b4e3a37672ddc3e415899af7c&qualityCode=2135&qualityid=1024&version=1&guid=55dd837b-00d8-6402-e6fe-84a85b9a4547&app=115020310221&cookie=52e4755ea11d905d1b49a1340a2c439d&session=52e4755ea11d905d1b49a1340a2c439d&uid=104318916908268130204&uname=18916908268&time=20190907192814&videotype=2&cid=C42162367&cname=&cateid=&dev=000001&ep=1&os=30&ps=0099&clienttype=LYA-AL00&appver=5.3.45.1&res=1080%2A2145&channelid=059999&pid=1000000432&orderid=&nid=&cp=00000368&sp=00000014&ip=114.86.207.71&ipSign=d60d296515f3fc47b1d09eaaffe33cab&cdntoken=api_5d73944e7bc20&a=7t8vRiyFpzv5z2RvVNizqCqLH2FoSrzpUa%2Fn%2Ba2clQ%2FrD916eef7EQ%3D%3D&pvv=1.2.0&t=5d73cc8e&cf=tx&s2=b53b3a3d6ab90ce0268229151c9bde11";
    private string h264_1M_vid_9023176151 = "http://vod3.nty.tv189.cn/6/ol/st02/2019/08/23/Q1M_deb2eecc-8e0e-49d1-9722-ca861d2ff80e.mp4.m3u8?sign=e9994db52f39d3f525419b27c0c52060&qualityCode=1514&qualityid=4096&version=1&guid=55dd837b-00d8-6402-e6fe-84a85b9a4547&app=115020310221&cookie=52e4755ea11d905d1b49a1340a2c439d&session=52e4755ea11d905d1b49a1340a2c439d&uid=104318916908268130204&uname=18916908268&time=20190907192814&videotype=2&cid=C42162367&cname=&cateid=&dev=000001&ep=1&os=30&ps=0099&clienttype=LYA-AL00&appver=5.3.45.1&res=1080%2A2145&channelid=059999&pid=1000000432&orderid=&nid=&cp=00000368&sp=00000014&ip=114.86.207.71&ipSign=d60d296515f3fc47b1d09eaaffe33cab&cdntoken=api_5d73944e7bc20&a=7t8vRiyFpzv5z2RvVNizqCqLH2FoSrzpUa%2Fn%2Ba2clQ%2FrD916eef7EQ%3D%3D&pvv=1.2.0&t=5d73cc8e&cf=tx&s2=b53b3a3d6ab90ce0268229151c9bde11";
    private string h264_8M_vid_416 = "http://vod5gct.nty.tv189.com/ceph_201907/42162367_v1/8000/playlist.m3u8?sign=c6280acf7fb8150f19e6043beafeaca9&qualityCode=8000&qualityid=8192&version=1&guid=55dd837b-00d8-6402-e6fe-84a85b9a4547&app=115020310221&cookie=52e4755ea11d905d1b49a1340a2c439d&session=52e4755ea11d905d1b49a1340a2c439d&uid=104318916908268130204&uname=18916908268&time=20190907192814&videotype=2&cid=C42162367&cname=&cateid=&dev=000001&ep=1&os=30&ps=0099&clienttype=LYA-AL00&appver=5.3.45.1&res=1080%2A2145&channelid=059999&pid=1000000432&orderid=&nid=&cp=00000368&sp=00000014&ip=114.86.207.71&ipSign=d60d296515f3fc47b1d09eaaffe33cab&cdntoken=api_5d73944e7bc20&a=7t8vRiyFpzv5z2RvVNizqCqLH2FoSrzpUa%2Fn%2Ba2clQ%2FrD916eef7EQ%3D%3D&pvv=1.2.0&t=5d73cc8e";
    private string h264_20M_vid_417 = "http://vod5gct.nty.tv189.com/ceph_201907/42162367_v1/20000/playlist.m3u8?sign=57c45b167a2778449c3a446782689935&qualityCode=20000&qualityid=16384&version=1&guid=55dd837b-00d8-6402-e6fe-84a85b9a4547&app=115020310221&cookie=52e4755ea11d905d1b49a1340a2c439d&session=52e4755ea11d905d1b49a1340a2c439d&uid=104318916908268130204&uname=18916908268&time=20190907192814&videotype=2&cid=C42162367&cname=&cateid=&dev=000001&ep=1&os=30&ps=0099&clienttype=LYA-AL00&appver=5.3.45.1&res=1080%2A2145&channelid=059999&pid=1000000432&orderid=&nid=&cp=00000368&sp=00000014&ip=114.86.207.71&ipSign=d60d296515f3fc47b1d09eaaffe33cab&cdntoken=api_5d73944e7bc20&a=7t8vRiyFpzv5z2RvVNizqCqLH2FoSrzpUa%2Fn%2Ba2clQ%2FrD916eef7EQ%3D%3D&pvv=1.2.0&t=5d73cc8e";
    private string h264_4M_vid_418 = "http://vod5gct.nty.tv189.com/ceph_201907/42162367_v1/4000/playlist.m3u8?sign=3dffc64699424b2f2be1bced04608393&qualityCode=4000&qualityid=2048&version=1&guid=55dd837b-00d8-6402-e6fe-84a85b9a4547&app=115020310221&cookie=52e4755ea11d905d1b49a1340a2c439d&session=52e4755ea11d905d1b49a1340a2c439d&uid=104318916908268130204&uname=18916908268&time=20190907192814&videotype=2&cid=C42162367&cname=&cateid=&dev=000001&ep=1&os=30&ps=0099&clienttype=LYA-AL00&appver=5.3.45.1&res=1080%2A2145&channelid=059999&pid=1000000432&orderid=&nid=&cp=00000368&sp=00000014&ip=114.86.207.71&ipSign=d60d296515f3fc47b1d09eaaffe33cab&cdntoken=api_5d73944e7bc20&a=7t8vRiyFpzv5z2RvVNizqCqLH2FoSrzpUa%2Fn%2Ba2clQ%2FrD916eef7EQ%3D%3D&pvv=1.2.0&t=5d73cc8e";

    private void Awake()
    {
        m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();
        m_SelectionRadial = GameObject.FindWithTag("MainCamera2").GetComponent<SelectionRadial>();
    }

    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
        m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
    }


    private void OnDisable()
    {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_InteractiveItem.OnClick -= HandleClick;
        m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
    }

    private void HandleOver()
    {
        Debug.Log("--name:" + gameObject.name + "is HandleOver!");
        gameObject.transform.localScale = new Vector3(8.64f, 5.2f, 0.01f);
        m_SelectionRadial.Show();
        m_SelectionRadial.HandleDown();
        m_GazeOver = true;
    }

    private void HandleOut()
    {
        Debug.Log("==name:" + gameObject.name + " is HandleOut!");
        gameObject.transform.localScale = new Vector3(8.0f, 4.82f, 0.01f);
        m_SelectionRadial.Hide();
        m_SelectionRadial.HandleUp();
        m_GazeOver = false;
    }

    private void HandleClick()
    {
       // eventCallBack();
    }

    private void HandleSelectionComplete()
    {
        Debug.Log("==name:" + gameObject.name + " is HandleSelectionComplete!");
        if (m_GazeOver)
        {
            Debug.Log("==name:" + gameObject.name + " is HandleSelectionComplete!!!!!!!!!!!!!!!!!!!!!!!!!OK");
            eventCallBack();
        }
    }

    private void eventCallBack()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            GameObject.Find("Directional Light").GetComponent<Main>().toast("网络无连接");

        }
        else
        {
            if (vInfo != null)
            {
                Debug.Log("contentId:" + vInfo.contentId + ",clickType:" + vInfo.clickType + ",clickParam:" + vInfo.clickParam + ",title:" + vInfo.title + ",category:" + vInfo.category);
                if (Application.platform == RuntimePlatform.Android)
                {

                    AndroidAPI.StartActivityForUnityTV189(vInfo.contentId, vInfo.clickType, vInfo.clickParam, vInfo.title, vInfo.category);
                  //  UntiyPlayer
                    return;
                }
            }

            if (!string.IsNullOrEmpty(videoID))
            {
                //MediaPlayerCtrl.m_videoID = "5473";
                string url = h264_m3u8;
                string infoName = "h264_m3u8";
                switch (indexXX)
                {
                    case 0:
                        url = p50_vid_9023176146;
                        infoName = "p50_vid_9023176146";
                        break;
                    case 1:
                        url = p150_vid_9023176148;
                        infoName = "p150_vid_9023176148";
                        break;
                    case 2:
                        url = p300_vid_9023176149;
                        infoName = "p300_vid_9023176149";
                        break;
                    case 3:
                        url = h264_4M_vid_9023176150;
                        infoName = "h264_4M_vid_9023176150";
                        break;
                    case 4:
                        url = h264_1M_vid_9023176151;
                        infoName = "h264_1M_vid_9023176151";
                        break;
                    case 5:
                        url = h264_8M_vid_416;
                        infoName = "h264_8M_vid_416";
                        break;
                    case 6:
                        url = h264_20M_vid_417;
                        infoName = "h264_20M_vid_417";
                        break;
                    case 7:
                        url = h264_4M_vid_418;
                        infoName = "h264_4M_vid_418";
                        break;
                    case 8:
                        url = h264_m3u8;
                        infoName = "h264_m3u8";
                        break;
                    case 9:
                        url = h265_m3u8;
                        infoName = "h265_m3u8";
                        break;
                    default:
                        url = h264_4M_vid_418;
                        infoName = "h264_4M_vid_418";
                        break;
                }
                indexXX = indexXX + 1;
                if (indexXX > 9) indexXX = 8;
                MediaPlayerCtrl.m_videoID = url;
                MediaPlayerCtrl.is3dUD = is3dUD;
              //  MediaPlayerCtrl.infoName = infoName;
                SceneManager.LoadScene("Player", LoadSceneMode.Single);
            }
        }

    }
}
