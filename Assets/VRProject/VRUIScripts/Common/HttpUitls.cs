using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpUitls 
{
    public static string GetStr(string Url)
    {
        System.GC.Collect();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        request.Proxy = null;
        request.KeepAlive = false;
        request.Method = "GET";
        request.ContentType = "application/json; charset=UTF-8";
        request.AutomaticDecompression = DecompressionMethods.GZip;


        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        Stream myResponseStream = response.GetResponseStream();
        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
        string retString = myStreamReader.ReadToEnd();

        myStreamReader.Close();
        myResponseStream.Close();

        if (response != null)
        {
            response.Close();
        }
        if (request != null)
        {
            request.Abort();
            }



            return retString;

        }




    public static string  ReplaceString(string path)
    {
        string newPath = path.Replace("/","_");
      

        return newPath;
    }
    /// <summary>
    /// 截取到_之前的字符串
    /// </summary>
    /// <param name="oldUrl"></param>
    /// <returns></returns>
    public static string SplitPath(string oldUrl)
    {
        string[] sArray = oldUrl.Split(new char[] { '_' });
       
        return sArray[0];
    }

    public static string   GetJsonPath(string RootPath, string path)
    {
        // string rePlacedPath = ReplaceString(oldPath);
        string newPath = SplitPath(RootPath) + ReplaceString(path) + "/0/115020310073.htm?bzv=11";

        return newPath;
    }
 
  public static    IEnumerator   GetImage(string path)

    {

        UnityWebRequest www = new UnityWebRequest(path);
     
        yield return www.Send();

        int width = 400;

        int height = 200;

        byte[] results = www.downloadHandler.data;

        Texture2D texture = new Texture2D(width, height);

        texture.LoadImage(results);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        
        Resources.UnloadUnusedAssets();
        yield return sprite;
    }

  
} 
