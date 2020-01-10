using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountSetMamager : UIBase
{
    public static AccountSetMamager instance;
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowAccountSetting:
                {
                    accountRoot.gameObject.SetActive(true);

                }
                break;
            case (ushort)UIEvent.ShowLauncher:

                {

                }
                break;
        }
    }
    Transform accountRoot;
    Transform AccountView;
    Transform PortraitView;
    Transform HeadPortraitButton;
    Transform ExitAccountButton;
    Transform CancellationButton;
    Transform SaveHeadPortrait;
    Transform BackUserCenter;
    Transform HeadPortraitList;
    public Sprite portraitSprite;
    private void Awake()
    {
        instance = this;
        msgids = new ushort[]
{
            (ushort )UIEvent .ShowAccountSetting 
         

};
        RegistSelf(this, msgids);

    }
    void Start()
    {
        accountRoot = UISettingManager.GetUITransform("AccountSettingRoot");
        AccountView = UISettingManager.GetUITransform("AccountView");
        HeadPortraitButton = UISettingManager.GetUITransform("HeadPortraitButton");
        ExitAccountButton = UISettingManager.GetUITransform("ExitAccountButton");
        CancellationButton = UISettingManager.GetUITransform("CancellationButton");
        SaveHeadPortrait = UISettingManager.GetUITransform("SaveHeadPortrait");
        HeadPortraitButton = UISettingManager.GetUITransform("HeadPortraitButton");
        BackUserCenter = UISettingManager.GetUITransform("BackUserCenter");
        PortraitView = UISettingManager.GetUITransform("PortraitView");
       
        HeadPortraitList = UISettingManager.GetUITransform("HeadPortraitList");
        foreach (Button item in HeadPortraitList.GetComponentsInChildren <Button >() )
        {

        }
        PortraitView.gameObject.SetActive(false );
        accountRoot.gameObject.SetActive(false);
        AddButtonLister(HeadPortraitButton);
        AddButtonLister(ExitAccountButton);
        AddButtonLister(CancellationButton);
        AddButtonLister(SaveHeadPortrait);
        AddButtonLister(BackUserCenter);

    }
    void AddButtonLister(Transform button)
    {
        button.GetComponent<Button>().onClick.
           AddListener(delegate () { AccountButtonClick(button); });
    }
    void AccountButtonClick(Transform button)
    {
        if (button.name == "HeadPortraitButton")
        {
            PortraitView.gameObject.SetActive(true );
            AccountView.gameObject.SetActive(false );
        }
        else if (button.name == "ExitAccountButton")
        {
         
        }
        else if (button.name == "CancellationButton")
        {

        }
        else if(button.name == "SaveHeadPortrait")
        {
            PortraitView.gameObject.SetActive(false );
            AccountView.gameObject.SetActive(true);
           HeadPortraitButton.GetComponent<Image>().sprite = portraitSprite;
        }
   
        else if (button.name == "BackUserCenter")
        {
         //   Debug.Log("fjkajflkds");
            accountRoot.gameObject.SetActive(false);

            SendMsg(new MsgBase((ushort)UIEvent.ShowUserCenter));
            SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart));
        }
    }
    void HeadPortraitButtonClick(Transform button)
    {
      //  button.Find("Select").gameObject.SetActive(true );
    }

}
