using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public enum CanvasStatus
{
    Default,
    CanvasEnable,
    CanvasDisable
}
public class CameraController : MonoBehaviour
{
    public static CameraController instance;


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
    List<GraphicRaycaster> graphicRaycasterList = new List<GraphicRaycaster>();
    Scene scene;
    public CanvasStatus canvasStatus;
    public bool screenRotaEnale=true;//表示屏幕滑动旋转效果是否可用


    void Awake()
    {
        instance = this;
    
    } 
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
            graphicRaycasterList.Add(item.transform .GetComponent <GraphicRaycaster>());

        }
        screenRotaEnale = true;
      
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
                    graphicRaycasterList[i].enabled = true;
                }
                canvasStatus = CanvasStatus.Default;
                break;
            case CanvasStatus.CanvasDisable:
                for (int i = 0; i < canvasList.Count; i++)
                {
                    graphicRaycasterList[i].enabled = false ;


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
        if (scene.name =="Player2D"
            || scene.name == "Player2DVR")
        {
            if (screenRotaEnale==true )
            {
              //  Debug.Log("screenRotaEnale");
                CameraRotate();
            }
            else if (screenRotaEnale == false)
           
            {
               // Debug.Log("CanvasEnable");
                canvasStatus = CanvasStatus.CanvasEnable;
            }
        }
        else
        {
            CameraRotate();
        }
        
       
        SetCanvasEventTrue();

    }
    void CameraRotate()
    {
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

    }
}
