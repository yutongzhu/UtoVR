using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum AboutStatus
{
    AboutDefault,
    userAgreement,
    PrivacyPolicy
}
public class AboultManager : UIBase
{
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowAboult:
                {

                    aboultRoot.gameObject.SetActive(true);
                }
                break;
            case (ushort)UIEvent.ShowLauncher:

                {

                }
                break;
        }
    }
    AboutStatus aboutStatus;
    Transform aboultRoot;
    Transform ExitAboutButton;
    Transform CheckVersonButton;
    Transform userAgreementButton;
    Transform PrivacyButton;
    
    private void Awake()
    {
        msgids = new ushort[]
{
            (ushort )UIEvent .ShowAboult 
       

};
        RegistSelf(this, msgids);

    }
    void Start()
    {
        aboultRoot = UISettingManager.GetUITransform("AboultPartRoot");
        aboultRoot.gameObject.SetActive(false );
        ExitAboutButton = UISettingManager.GetUITransform("ExitAboutButton");
        CheckVersonButton = UISettingManager.GetUITransform("CheckVersonButton");
        userAgreementButton = UISettingManager.GetUITransform("userAgreementButton");
        PrivacyButton = UISettingManager.GetUITransform("PrivacyButton");

        AddButtonLister(ExitAboutButton);
    }
    void AddButtonLister(Transform button)
    {
        button.GetComponent<Button>().onClick.
           AddListener(delegate () { SubscribeButtonClick(button); });
     //   SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart));
    }
    void SubscribeButtonClick(Transform button)
    {
        if (button.name == "CheckVersonButton")
        {

        }
        else if (button.name == "userAgreementButton")
        {
            aboutStatus = AboutStatus.userAgreement;
        }
        else if (button.name == "PrivacyButton")
        {
            aboutStatus = AboutStatus.PrivacyPolicy;
        }
        else if (button.name == "ExitAboutButton")
        {
          //  Debug.Log("jfkajlfk");
            SendMsg(new MsgBase((ushort)UIEvent.ShowUserCenter));
            aboultRoot.gameObject.SetActive(false);
            SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart));
            switch (aboutStatus)
            {
                case AboutStatus.AboutDefault:

                    break;
                case AboutStatus.userAgreement:
                    break;
                case AboutStatus.PrivacyPolicy:
                    break;
                default:
                    break;
            }
            aboutStatus = AboutStatus.AboutDefault;
        }

    }
}
