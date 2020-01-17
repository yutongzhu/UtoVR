using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    #region
    //float firstPosX = 0f;
    //float currentRotY = 0f;
    //public GameObject roleObj = null;
    //private void RoleRotation(float detalTime)
    //{

    //        if (Input.touchCount == 1)
    //        {
    //            if (Input.GetTouch(0).phase == TouchPhase.Began)
    //            {
    //                firstPosX = Input.GetTouch(0).position.x;
    //                currentRotY = roleObj.transform.localRotation.eulerAngles.y;
    //            }
    //            if (Input.GetTouch(0).phase == TouchPhase.Moved)
    //            {
    //                float currentPosX = Input.GetTouch(0).position.x;
    //                float XAxis = firstPosX - currentPosX;
    //                if (Mathf.Abs(XAxis) < 5)
    //                { return; }
    //                roleObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, currentRotY + XAxis, 0) * 0.1f);
    //            }
    //            if (Input.GetTouch(0).phase == TouchPhase.Ended)
    //            {
    //                 firstPosX = 0f;
    //                currentRotY = roleObj.transform.localRotation.eulerAngles.y;

    //            }
    //        }





    //}
    #endregion


   // public Transform target;
    float distance = 4;
    float xSpeed = 6.0F;
    private float x = 0.0F;
    private float y = 0.0F;
    public static float camerY;
    //  private float y = 0.0F;
    Vector2 a;
    Quaternion rotation;
    Vector3 position;
    private Transform UI;
    List<Canvas> canvasList = new List<Canvas>();
    Scene scene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Main")
        {
            Debug.Log("Main");
            UI = GameObject.Find("UI").transform;

        }
        else if (scene.name == "Player2D" || scene.name == "Player2D"
            || scene.name == "PlayerLive"  )
        {
           // Debug.Log("Player2D");
            UI = GameObject.Find("Canvas").transform;
        }

        foreach (Canvas item in UI.GetComponentsInChildren<Canvas>())
        {
            canvasList.Add(item);
        }

    }
    CanvasStatus canvasStatus;
    public void click() {

        Debug.Log("hlakgjlkjsd;lgj;lhgieha;ro");
    }
    enum CanvasStatus
    {
        Default,
        CanvasEnable,
       CanvasDisable
    }
    void SetCanvasEventTrue()
    {
        switch (canvasStatus)
        {
            case CanvasStatus.Default:
                break;
            case CanvasStatus.CanvasEnable:
                for (int i = 0; i < canvasList.Count; i++)
                {
                    canvasList[i].transform.GetComponent<GraphicRaycaster>().enabled = true;
                }
                canvasStatus = CanvasStatus.Default;
                break;
            case CanvasStatus.CanvasDisable:
                for (int i = 0; i < canvasList.Count; i++)
                {
                    canvasList[i].transform.GetComponent<GraphicRaycaster>().enabled = false ;
                }
                canvasStatus = CanvasStatus.Default;
                break;
            default:
                break;
        }
      
    }

   
    private void LateUpdate()
    {
        //  position = rotation * new Vector3(0.0F, 0.5f, -distance) + target.transform.position;
        //   transform.position = Vector3.Lerp(transform.position, position, 0.8f);
     
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                canvasStatus = CanvasStatus.CanvasDisable;

                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    a = Input.GetTouch(0).deltaPosition;
                    x += a.x * xSpeed * Time.deltaTime;
                    y += a.y * xSpeed * Time.deltaTime;
                    if (y > 5)
                        y = 5;
                    if (y < -35)
                        y = -35;
                    rotation = Quaternion.Euler(y, x, 0);
                    transform.rotation = rotation;
                    // position = rotation * new Vector3(0.0F, 0.5f, -distance) + target.transform.position;
                    //transform.position = Vector3.Lerp(transform.position, position, 0.8f);
                   
                }
            }
            else
            {
                canvasStatus = CanvasStatus.CanvasEnable;
            }
        }
        SetCanvasEventTrue();

    }
}
