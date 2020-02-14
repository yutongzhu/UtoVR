using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightManger : MonoBehaviour {
    public static LightManger instance;
    void Awake()
    {
        instance = this;
    }
    public GameObject VideoCanvasRoot;
    public GameObject VideoScreenVR;
    public GameObject VideoScreen;
    public GameObject mainCamera;
   
    public GameObject VideoCanvasRootVR;
   // public GameObject VideoCanvasVR;
    void Start () {
        //  VideoCanvasRoot.SetActive(false);
        if (SceneManager .GetActiveScene ().name =="MainVR")
        {
            VideoCanvasRootVR =  GameObject.Find("VideoCanvasVR/VideoCanvasRoot");
          //  VideoCanvasRootVR.SetActive(false);
        }
        VideoCanvasRoot = GameObject.Find("UI/VideoCanvas/VideoCanvasRoot");
     
        VideoScreen.SetActive(false);
     
        if (SceneManager.GetActiveScene().name =="Main")
        {
            mainCamera = GameObject.Find("UI/Camera");
            mainCamera.SetActive(true);
        }
        else if(SceneManager.GetActiveScene().name == "MainVR")
        {
           // mainCamera = GameObject.Find("UI/Pvr_UnitySDK");
           //  mainCamera.GetComponent<Pvr_UnitySDKManager>().enabled = true;
          //  mainCamera.SetActive(true);
           
        }
        else if (SceneManager.GetActiveScene().name == "PlayerVR")
        {
            
        }
        // VideoCanvasRoot.SetActive(false);
        IniObject();
    }
    void IniObject()
    {
        VideoCanvasRoot.SetActive(false);
        if (SceneManager.GetActiveScene().name == "MainVR")
        {
            VideoCanvasRootVR.SetActive(false);
            VideoCanvasRootVR.transform.Find("VideoContolRoot").gameObject.SetActive(false);
            VideoScreenVR.SetActive(false);
        }
           
        VideoCanvasRoot.transform.Find("VideoContolRoot").gameObject.SetActive(false);
     
        VideoScreen.SetActive(false);
       

    }
}
