using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameManager : UIBase
{
    public override void ProcessEvent(MsgBase tmpMag)
    {

        switch (tmpMag.msgid)
        {

            case (ushort)UIEvent.ShowGamePart:
                {
                    gameRoot.SetActive(true );
                    GameDetail.SetActive(false );
                    GameTop.SetActive(true);
                    GameLauncher.SetActive(true);
                    GameItemsList.SetActive(false );
                }
                break;
            case (ushort)UIEvent.HideGamePart :
                {
                    gameRoot.SetActive(false );
                  
                }
                break;

        }
    }
    GameObject gameRoot;
    GameObject GameDetail;
    GameObject GameTop;
    GameObject GameLauncher;
    GameObject GameItemsList;
    GameObject GameItemsRoot;
    Transform backImage;
    Transform GameRecommendButton;
    Transform AllGameButton;
    Transform BackGameButton;
    Text GamePageText;
    Text GameDetailName;
    private void Awake()
    {
        msgids = new ushort[]
{
            (ushort )UIEvent .ShowGamePart ,
             (ushort )UIEvent .HideGamePart 


};
        RegistSelf(this, msgids);

    }
    void Start()
    {
        gameRoot = UISettingManager.GetUIObject("GameRoot");
        GameDetail = UISettingManager.GetUIObject("GameDetail");
        GameTop = UISettingManager.GetUIObject("GameTop");
        GameLauncher = UISettingManager.GetUIObject("GameLauncher");
        GameItemsList = UISettingManager.GetUIObject("GameItemsList");
        GameItemsRoot = UISettingManager.GetUIObject("GameItemsRoot");
        backImage = UISettingManager.GetUIObject("BackImage").transform ;
        GameRecommendButton = UISettingManager.GetUIObject("GameRecommendButton").transform;
        AllGameButton = UISettingManager.GetUIObject("AllGameButton").transform;
        BackGameButton= UISettingManager.GetUIObject("BackGameButton").transform;
        GamePageText = UISettingManager.GetUIObject("GamePageText").GetComponent<Text>();
        GameDetailName = UISettingManager.GetUIObject("GameDetailName").GetComponent<Text>();
        GamePageText.text = GameData.CurrentPages+1 + "/" + GameData.gameTotalPages.ToString();


        IniButtonItemEvent();

        gameRoot.SetActive(false);
        GameDetail.SetActive(false);
        GameTop.SetActive(true);
        GameLauncher.SetActive(true);
        GameItemsList.SetActive(false);
         

        BackGameButton.GetComponent<Button>().onClick.AddListener(delegate () { TopGameButtonEvent(BackGameButton); });
        GameRecommendButton.GetComponent<Button>().onClick.AddListener(delegate () { TopGameButtonEvent(GameRecommendButton); });
        AllGameButton.GetComponent<Button>().onClick.AddListener(delegate () { TopGameButtonEvent(AllGameButton); });
        
    }
    Transform NextButton;
    Transform PriviosButton;
    void IniButtonItemEvent()
    {
        NextButton= UISettingManager.GetUIObject("NextButton").transform;
        PriviosButton = UISettingManager.GetUIObject("PreviousButton").transform;
        int index = 0;
        foreach (Button item in GameItemsRoot.GetComponentsInChildren<Button>())
        {
              Text nameText = item.transform.Find("name").GetComponent<Text >();
           //  nameText.text = jsonTestData.instance.gameItemList[index].name;

            item.onClick.AddListener(delegate () { GameItemClick(item.transform); });
            index++;
        }
        int num = 0;
        foreach (Button item in GameLauncher.GetComponentsInChildren<Button>())
        {
            Text nameText = item.transform.Find("name").GetComponent<Text>();
           // nameText.text = jsonTestData.instance.gameItemList[num].name;
            item.onClick.AddListener(delegate () { GameItemClick(item.transform ); });
            num++;
        }

        UISettingManager.AddButtonClickListener("NextButton", NextGamePage);
        UISettingManager.AddButtonClickListener("PreviousButton", PriviousGamePage);
    }
    void   TopGameButtonEvent(Transform  button)
    {
        backImage.position = button.position;
        if (button.name == "GameRecommendButton")
        {
            GameLauncher.SetActive(true);
            GameItemsList.SetActive(false);
        }
        else if (button.name == "AllGameButton")
        {
            GameLauncher.SetActive(false );
            GameItemsList.SetActive(true);
        }
        else if (button.name == "BackGameButton")
        {
            GameTop.SetActive(true);
            GameDetail.SetActive(false );
            GameLauncher.SetActive(true );
            backImage.position = GameRecommendButton.position;
            MsgBase msg = new MsgBase((ushort)UIEvent.ShowBottomPart);
            SendMsg(msg);
        }
    }
    void GameItemClick(Transform gameItem)
    {
        GameLauncher.SetActive(false );
        GameItemsList.SetActive(false);
        GameDetail.SetActive(true );
        GameTop.SetActive(false );
        MsgBase msg = new MsgBase((ushort)UIEvent.HideBottomPart);
        SendMsg(msg);
       // GameData.GameID = GetItemIndexByTag(gameItem.tag );//获取此时的gameid
       // GameData.GameName = jsonTestData.instance.gameItemList[GameData.GameID].name;
       
       // GameDetailName.text = GameData.GameName;
    }

    //获取当前点击item的gameData的索引值
    int  GetItemIndexByTag(string tag)
    {
        if (tag=="Zero")
        {
            return GameData.CurrentPages * 4 + 0;
        }
        else if (tag == "One")
        {
            return GameData.CurrentPages * 4 + 1;
        }
        else if (tag == "Two")
        {
            return GameData.CurrentPages * 4 + 2;
        }
        else if (tag == "Three")
        {
            return GameData.CurrentPages * 4 + 3;
        }
        else if (tag == "Four")
        {
            return GameData.CurrentPages * 4 + 4;
        }
        else if (tag == "Five")
        {
            return GameData.CurrentPages * 4 + 5;
        }
        else if (tag == "Six")
        {
            return GameData.CurrentPages * 4 + 6;

        }
        return 0;
    }

    int gameIndex;//每一页初始时候的索引
    void NextGamePage()
    {

       
       
        if (GameData.CurrentPages+1 < GameData.gameTotalPages)
        {
            GameData.CurrentPages++;
             Debug.Log("CurrentPages" + GameData.CurrentPages);
            gameIndex = GameData.CurrentPages * 4;
            GamePageText.text = GameData.CurrentPages + 1 + "/" + GameData.gameTotalPages.ToString();
            foreach (Button item in GameItemsRoot.GetComponentsInChildren<Button>())
            {
                Text nameText = item.transform.Find("Name").GetComponent<Text>();
                //if (gameIndex< jsonTestData.instance.gameItemList.Count )
                //{
                //    nameText.text = jsonTestData.instance.gameItemList[gameIndex].name;
                //}
                //else
                //{
                //    nameText.text = "";
                //}
             
               gameIndex++;
            }
            
        }
        else
        {
            UISettingManager.ShakeUI(NextButton, new Vector3(0.05f, 0.05f, 0.05f), 1);
        }
       
    }
    void PriviousGamePage()
    {
     
        if (GameData.CurrentPages > 0)
        {
            GameData.CurrentPages--;
            Debug.Log("CurrentPages" + GameData.CurrentPages);
            GamePageText.text = GameData.CurrentPages + 1 + "/" + GameData.gameTotalPages.ToString();
            gameIndex = GameData.CurrentPages * 4;
            foreach (Button item in GameItemsRoot.GetComponentsInChildren<Button>())
            {
                Text nameText = item.transform.Find("Name").GetComponent<Text>();
            //    nameText.text = jsonTestData.instance.gameItemList[gameIndex].name;

                item.onClick.AddListener(delegate () { GameItemClick(item.transform); });
                gameIndex++;
            }
           
        }
        else
        {
            UISettingManager.ShakeUI(PriviosButton, new Vector3(0.05f, 0.05f, 0.05f), 1);
        }
    }


}
