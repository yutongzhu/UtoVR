using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserCenterManager : UIBase
{
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowUserCenter:
                {

                    userCenterRoot.gameObject.SetActive(true );
                }
                break;
            case (ushort)UIEvent.HideUserCenter:
                {

                    userCenterRoot.gameObject.SetActive(false );
                }
                break;

        }
    }

     
     Transform userCenterRoot;
    Transform CustomerService;
    
   // Transform SureConnectButon;
    private void Awake()
    {
        msgids = new ushort[]
{
            (ushort )UIEvent .ShowUserCenter ,
             (ushort )UIEvent .HideUserCenter

};
        RegistSelf(this, msgids);

    }
    void Start()
    {

        userCenterRoot=  UISettingManager.GetUITransform("UserCenterRoot");
       
        foreach (Button  item in userCenterRoot.GetComponentsInChildren <Button >())
        {
            item.onClick.AddListener(delegate () { ButtonOnClick(item.transform ); });
        }

        CustomerService = UISettingManager.GetUITransform("CustomerService");
     
        userCenterRoot.gameObject.SetActive(false );
        CustomerService.gameObject.SetActive(false );
     
        UISettingManager.AddButtonClickListener("SureConnectButon", SureConnectButonClick);
    }
    void AddButtonLister(Transform button)
    {
        button.GetComponent<Button>().onClick.
           AddListener(delegate () { ButtonOnClick(button); });
    }
    void ButtonOnClick(Transform button)
    {
       // Debug.Log("fasfdaf");
        userCenterRoot.gameObject.SetActive(false);
        SendMsg(new MsgBase((ushort)UIEvent.HideBottomPart));
        switch (button.name )
        {
            
            case "AccountSettingButton":
                MsgBase msg = new MsgBase((ushort)UIEvent.ShowAccountSetting );
                SendMsg(msg);
                break;
            case "FavoriteButton":
                MsgBase msg1 = new MsgBase((ushort)UIEvent.ShowFavorite);
                SendMsg(msg1);
                break;
            case "HistoricalRecordButton":

                MsgBase msg2 = new MsgBase((ushort)UIEvent.ShowHistoricalRecord);
                SendMsg(msg2);
                break;
            case "CustomerServiceButton":
                CustomerService.gameObject.SetActive(true);
                userCenterRoot.gameObject.SetActive(false );


                break;
            case "MySubscriptionButton":
                Debug.Log("MySubscriptionButton");
                MsgBase msg3 = new MsgBase((ushort)UIEvent.ShowMySubcribe);
                SendMsg(msg3);
              
                break;
            case "AboultButton":
                SendMsg(new MsgBase((ushort)UIEvent.ShowAboult ));
                break;

            default:
                break;
        }
    }
    void SureConnectButonClick()
    {
        CustomerService.gameObject.SetActive(false );
        userCenterRoot.gameObject.SetActive(true );
        SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart));
    }
}
