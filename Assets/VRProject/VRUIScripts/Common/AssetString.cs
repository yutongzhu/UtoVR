using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetString : MonoBehaviour
{
    [SerializeField]
    private string _key;

    /**
     * 用于显示过程中动态修改词条
     */
    public string Key
    {
        get
        {
            return _key;
        }
        set
        {
            _key = value;
            UpdateTitle();
        }
    }

    private void UpdateTitle()
    {
        Text text = this.GetComponent<Text>();
        if (text != null)
        {
            string content = WhiteLabel.String.GetString(_key);
            if (content != null)
            {
                text.text = content;
            }
            else
            {
                Debug.LogError("词条不存在,key=" + _key);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        UpdateTitle();
    }
}
