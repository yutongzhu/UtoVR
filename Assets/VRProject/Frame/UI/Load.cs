using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MsgTransform:MsgBase 
{

    public Transform value;
    public MsgTransform ():base (0)
    {}
    public MsgTransform (ushort tmpid,Transform tmptrans):base (tmpid )
    {
        this.msgid = tmpid;
        this.value = tmptrans;
    }

}
//收集面板下所有的控件的事件处理
//对外通信
public class Load : UIBase  {

    public override void ProcessEvent(MsgBase tmpMsg)
    {
        switch (tmpMsg.msgid)
        {
            case (ushort)UIEvent.UPDownSlider :
                {


                }
                break;
            case (ushort)UIEvent .ForBackSlider:

                {
                    Debug.Log("ForBackSlider");
                }
                break;
        }

    }
	private void Awake()
	{
        msgids = new ushort[]
      {
            (ushort )UIEvent .ForBackSlider,
            (ushort )UIEvent .UPDownSlider 
      };
        RegistSelf(this, msgids);

	}
	void Start () {
        UIManager.instance.GetGameObject("Button").
                 GetComponent <UIBehaviour >().AddButtonListener(ButtonClick);
	}
	void ButtonClick()
    {
        MsgBase msg = new MsgBase((ushort )AssetEvent .LoadAssect );
         SendMsg(msg );
       // MsgTransform tmptran = new MsgTransform((ushort)AssetEvent.ReleaseAll,transform  );
      //  SendMsg(tmptran);
        Debug.Log("ButtonClick");

    }


	// Update is called once per frame
	void Update () {
		
	}
}
