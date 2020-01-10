using System;
using LitJson;
using UnityEngine;


namespace WhiteLabel
{
    public class String
    {
        private static JsonData jsonData;
        private static JsonData videoJsonData;
        public static void Init()
        {
            string stringResurce = "string/string_zh";
                //"string/string_" + EnvironmentVariables.lang;
            TextAsset json = Resources.Load<TextAsset>(stringResurce);
            jsonData = JsonMapper.ToObject(json.text);
            string videoPath = "Column/Column";
            //"string/string_" + EnvironmentVariables.lang;
         
        }

        public static string GetString(string key)
        {
            JsonData data = jsonData[key];
            if (data != null)
            {
                return (string)data["content"];
            }
            else
            {
                return null;
            }
        }
    }
}
