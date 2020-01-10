using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    string jsonStr;
    string url = "https://nhapi02.tv189.com/cpms/index/template/_clt4_xtysxkhd_zt_yszt_20194Kzq_zssy_index.json/0/0/308020210491.htm?bzv=10";

    void Start()
    {
        jsonStr = HttpUitls.GetStr(url);
       // Root root = JsonConvert.DeserializeObject<Root>(jsonStr);
   ///     Debug.Log( "json data size " + root.data.Count);
      //  DataRoot dataRoot = root.data[1];//获取首页的最外的父级

       

       // Data data = dataRoot.data;
        ///获取到首页的6个item
          
       // Debug.Log(" dataRoot.labelName  " + data.data  .Count    );


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
