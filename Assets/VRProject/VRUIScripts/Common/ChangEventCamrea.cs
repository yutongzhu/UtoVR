using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EventCamera
{
    SingleCamera,
    VrCamera
}
public class ChangEventCamrea : Singleton <ChangEventCamrea>
{
    public GameObject uiRoot;
    public Camera singleCamera;
    public Camera VrCamera;
    public GameObject Pvr_UnitySDK;
    public GameObject ControllerManager;
    public static List<Canvas> canvasList = new List<Canvas>();
    public EventCamera eventCamera;
    void Start () {
        foreach (Canvas item in uiRoot.transform.GetComponentsInChildren <Canvas >())
        {
            canvasList.Add(item);
            if (eventCamera==EventCamera.SingleCamera)
            {
                singleCamera.gameObject.SetActive(true);
                Pvr_UnitySDK.SetActive(false);
                ControllerManager.gameObject.SetActive(false );
                item.worldCamera = singleCamera;
            }
            else
            {
                singleCamera.gameObject.SetActive(false);
                Pvr_UnitySDK.SetActive(true);
                ControllerManager.gameObject.SetActive(true );
                item.worldCamera = VrCamera; 
            }
        }
	}
   

    public void ChangeCamera(EventCamera camera)
    {
        for (int i = 0; i < canvasList.Count ; i++)
        {
            if (eventCamera == EventCamera.SingleCamera)
            {
                singleCamera.gameObject.SetActive(true);
                Pvr_UnitySDK.SetActive(false);
                ControllerManager.gameObject.SetActive(false);
                canvasList[i].worldCamera = singleCamera;
            }
            else
            {
                singleCamera.gameObject.SetActive(false);
                Pvr_UnitySDK.SetActive(true);
                ControllerManager.gameObject.SetActive(true);
                canvasList[i].worldCamera = VrCamera;
            }
        }

    }
	
	
}
