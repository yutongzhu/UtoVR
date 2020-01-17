using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottomManager : UIBase
{

    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowBottomPart:
                {

                    BottomButtonContent.gameObject.SetActive(true );
                }
                break;
            case (ushort)UIEvent.HideBottomPart:

                {
                    BottomButtonContent.gameObject.SetActive(false);
                }
                break;
        }
    }
    public Transform head;
    Transform BottomButtonContent;
    Transform VideoButton;
   

   // Transform PersonButton;
    Transform ResetButton;
    Transform VRPartButton;
    private void Awake()
    {
        msgids = new ushort[]
{
            (ushort )UIEvent .HideBottomPart  ,
            (ushort )UIEvent .ShowBottomPart

};
        RegistSelf(this, msgids);

    }
    bool isVrEnable;
    void Start()
    {
        BottomButtonContent = UIManager.instance.GetGameObject("BottomButtonContent").transform;
        VideoButton = UIManager.instance.GetGameObject("VideoButton").transform;
      //  GameButton = UIManager.instance.GetGameObject("GameButton").transform;
     //   PersonButton = UIManager.instance.GetGameObject("PersonButton").transform;
        ResetButton = UIManager.instance.GetGameObject("ResetButton").transform;
        VRPartButton = UIManager.instance.GetGameObject("VRPartButton").transform;
     //  UISettingManager.AddButtonClickListener("BackGameButton",);
        VideoButton.GetComponent<Button>().onClick.AddListener(delegate() { ButtonOnClick(VideoButton); });
        //GameButton.GetComponent<Button>().onClick.AddListener(delegate () { ButtonOnClick(GameButton); });
       // PersonButton.GetComponent<Button>().onClick.AddListener(delegate () { ButtonOnClick(PersonButton); });
        ResetButton.GetComponent<Button>().onClick.AddListener(delegate () { ButtonOnClick(ResetButton); });
        VRPartButton.GetComponent<Button>().onClick.AddListener(delegate () { ButtonOnClick(VRPartButton); });
    }

    void ButtonOnClick(Transform button)
    {
        //Debug.Log("itemq");
        if (button.name == "VideoButton")
        {
            Debug.Log("item" );
            LauncherUIManager.instance.LauncherView.gameObject.SetActive(true );
            LauncherUIManager.instance.IniLauncer();
            SendMsg(new MsgBase((ushort)UIEvent.ShowLauncher  ));
            SendMsg(new MsgBase((ushort)UIEvent.HideGamePart));
            SendMsg(new MsgBase((ushort)UIEvent.HideUserCenter));
            SendMsg(new MsgBase((ushort)UIEvent.HideLivePart ));
        }
        else if (button.name == "GameButton")
        {
            Debug.Log("GameButton");
            MsgBase msg = new MsgBase((ushort)UIEvent.ShowGamePart );
            SendMsg(msg);
      
            SendMsg(new MsgBase((ushort)UIEvent.HideLauncher));
            SendMsg(new MsgBase((ushort)UIEvent.HideUserCenter ));
        }
        else if (button.name == "PersonButton")
        {
            SendMsg(new MsgBase((ushort)UIEvent.ShowUserCenter ));
            SendMsg(new MsgBase((ushort)UIEvent.HideGamePart ));
            SendMsg(new MsgBase((ushort)UIEvent.HideLauncher ));
            SendMsg(new MsgBase((ushort)UIEvent.HideLivePart));
            //  SendMsg(new MsgBase((ushort)UIEvent.ShowUserCenter));
        }
        else if (button.name == "ResetButton")
        {
            head.transform.rotation = Quaternion.identity;
        }
        else if (button.name == "VRPartButton")
        {
            if (SceneManager.GetActiveScene().name == "MainVR")
            {
                SceneManager.LoadScene("Main", LoadSceneMode.Single);

            }
            else if (SceneManager.GetActiveScene().name == "Main")
            {
                SceneManager.LoadScene("MainVR", LoadSceneMode.Single);
            }
           
        }

    }
}
