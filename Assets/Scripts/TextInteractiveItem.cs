using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;

/// <summary>
/// 通用文本脚本，主要用于文字显示的组件上，比如上一页、下一页、页面数、分类页等。
/// </summary>
public class TextInteractiveItem : MonoBehaviour
{
    [SerializeField]
    private Material m_NormalMaterial;
    [SerializeField]
    private Material m_OverMaterial;
    [SerializeField]
    private Material m_ClickedMaterial;

    private VRInteractiveItem m_InteractiveItem;
    private SelectionRadial m_SelectionRadial;         
    private bool m_GazeOver;                           

    public bool isSelected;  //选中

    private void Awake()
    {
        isSelected = false;
        m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();
        m_SelectionRadial = GameObject.FindWithTag("MainCamera2").GetComponent<SelectionRadial>();
    }


    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
        m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
    }


    private void OnDisable()
    {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_InteractiveItem.OnClick -= HandleClick;
        m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
    }


    private void HandleOver()
    {
        gameObject.GetComponent<Renderer>().material = m_OverMaterial;
        m_SelectionRadial.Show();
        m_SelectionRadial.HandleDown();
        m_GazeOver = true;
    }


    private void HandleOut()
    {
        // 不是当前分类，材质恢复Normal
        if (!isSelected)
        {
            gameObject.GetComponent<Renderer>().material = m_NormalMaterial;
        }
        m_SelectionRadial.Hide();
        m_SelectionRadial.HandleUp();
        m_GazeOver = false;
    }


    private void HandleClick()
    {
        eventCallBack();
    }

    private void HandleSelectionComplete()
    {
        if (m_GazeOver)
        {
            eventCallBack();
        }
    }

    private void eventCallBack()
    {
        switch (gameObject.name)
        {
            case "Last": //上一页
                GameObject.Find("Directional Light").SendMessage("Last");					
                break;

            case "Next": //下一页
                GameObject.Find("Directional Light").SendMessage("Next");
                break;

            default:
                string typeName = gameObject.name;

                if (typeName.StartsWith("type") && !Main.isLoading) //分类
                {
                    Main.isLoading = true;
                    Main.pageIndex = 0;
                    Main.toBeType = int.Parse(typeName.Replace("type", ""));
                    GameObject.Find("Directional Light").SendMessage("init");
                }
                else if (typeName.StartsWith("page")) //翻页
                {
                    string[] tname = typeName.Split('_');
                    Main.pageIndex = int.Parse(tname[2]);
                    Main.pageSelectIndex = int.Parse(tname[1]);
                    object[] message = new object[2];
                    message[0] = false;
                    message[1] = int.Parse(tname[1]);
                    GameObject.Find("Directional Light").SendMessage("updateUI", message);
                }
                break;
        }
    }
}
