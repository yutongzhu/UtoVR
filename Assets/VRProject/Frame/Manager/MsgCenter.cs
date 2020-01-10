using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgCenter : MonoBehaviour   {
    public static MsgCenter instance;
	private void Awake()
	{
        instance = this;
       gameObject.AddComponent<UIManager>();
       gameObject.AddComponent<AssetManager>();
        gameObject.AddComponent<ILoaderManager >();
        WhiteLabel.String.Init();
    }
	// Use this for initialization
	void Start () {
		
	}
	public void SendToMsg(MsgBase tmpMsg)
    {
        AnasysisMsg(tmpMsg);
    }
    private void AnasysisMsg(MsgBase tmpMsg)
    {
        ManagerID tmpId = tmpMsg.GetManager();
        switch (tmpId )
        {
         
            case ManagerID .AssetManager:
                Debug.Log("AssetManager");
                AssetManager.instance.SendMsg(tmpMsg);
                break;
            case ManagerID.UIManager:
                Debug.Log("UIManager");

                break;
            case ManagerID.NPCManager:
                break;
            case ManagerID.GameManager:
                break;
            case ManagerID.AudioManager:
                break;
            default:
                break;
        }
    }
	
}
