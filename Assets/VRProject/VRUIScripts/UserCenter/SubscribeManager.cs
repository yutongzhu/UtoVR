using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscribeManager : UIBase
{
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowMySubcribe:
                {
                    SubscribeRoot.gameObject.SetActive(true);

                }
                break;
            case (ushort)UIEvent.ShowLauncher:

                {

                }
                break;
        }
    }

    Transform SubscribeRoot;
   
    Transform ExitSubscribeButton;
    Transform LeftFavoritePage;
    Transform RightFavoritePage;
    private void Awake()
    {
        msgids = new ushort[]
{
            (ushort )UIEvent .ShowMySubcribe ,
            (ushort )UIEvent .ShowLauncher

};
        RegistSelf(this, msgids);

    }
    void Start()
    {
        SubscribeRoot = UISettingManager.GetUITransform("SubscribeRoot");
        ExitSubscribeButton = UISettingManager.GetUITransform("ExitSubscribeButton");
        LeftFavoritePage = UISettingManager.GetUITransform("LeftFavoritePage");
        RightFavoritePage = UISettingManager.GetUITransform("RightFavoritePage");
        SubscribeRoot.gameObject.SetActive(false );
        AddButtonLister(ExitSubscribeButton);
        AddButtonLister(LeftFavoritePage);
        AddButtonLister(RightFavoritePage);
    }
    void AddButtonLister(Transform button)
    {
        button.GetComponent<Button>().onClick.
           AddListener(delegate () { SubscribeButtonClick(button); });
    }
    void SubscribeButtonClick(Transform button)
    {
        if (button.name == "LeftFavoritePage")
        {
         
        }
        else if (button.name == "RightFavoritePage")
        {

        }
        else if (button.name == "ExitSubscribeButton")
        {
            SendMsg(new MsgBase((ushort)UIEvent.ShowUserCenter));
            SubscribeRoot.gameObject.SetActive(false);
            SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart ));
        }
       
    }
}
