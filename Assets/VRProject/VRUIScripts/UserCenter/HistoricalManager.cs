using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoricalManager : UIBase
{
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowHistoricalRecord:
                {

                        HistorialRoot.gameObject.SetActive(true);
                }
                break;
            case (ushort)UIEvent.HideHistoryRecord :

                {
                    HistorialRoot.gameObject.SetActive(false);
                }
                break;
        }
    }
    Transform HistorialRoot;
    Transform HistoryExitButton;
    private void Awake()
    {
        msgids = new ushort[]
{
            (ushort )UIEvent .ShowHistoricalRecord ,
            (ushort )UIEvent .HideHistoryRecord 



};
        RegistSelf(this, msgids);

    }
    void Start()
    {
        HistorialRoot = UISettingManager.GetUITransform("HistorialRoot");
        HistorialRoot.gameObject.SetActive(false);
        HistoryExitButton = UISettingManager.GetUITransform("HistoryExitButton");
        UISettingManager.AddButtonClickListener("HistoryExitButton", ExitButtonClick);
    }

    void ExitButtonClick()
    {

        HistorialRoot.gameObject.SetActive(false );
        SendMsg(new MsgBase((ushort)UIEvent.ShowUserCenter ));
        SendMsg(new MsgBase((ushort)UIEvent.ShowBottomPart));
    }
}
