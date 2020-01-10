using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using System.IO;
public class BuileAssetbundleEditor
{
    //[MenuItem("Itool/BuildAssetBumndle")]
    //public static void BuildAssetBumndle()
    //{
    //    //   string outPath = Application.dataPath + "/AssetBundle";
    //    string outPath = IPathTools.GetAssetBundlePath();

    //    if (Directory.Exists(outPath) == false)
    //    {
    //        Directory.CreateDirectory(outPath);//在工程下创建AssetBundles目录
    //    }
    //    BuildPipeline .BuildAssetBundles (outPath ,0,EditorUserBuildSettings .activeBuildTarget );    
   
    //    AssetDatabase.Refresh();//刷新
    
    //}


    //private   void Setname()
    //{

    //    string fullPath = "Assets/Art/Scences" + "/";

    //    //获取指定路径下面的所有资源文件  
    //    if (Directory.Exists(fullPath))
    //    {
    //        DirectoryInfo direction = new DirectoryInfo(fullPath);
    //        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

    //    //    Debug.Log(files.Length);

    //        for (int i = 0; i < files.Length; i++)
    //        {
    //            if (files[i].Extension==".meta")
    //            {
    //                continue;
    //            }
    //            if (files[i].Name==".DS_Store")
    //            {
    //                continue;
    //            }
    //            else 
    //            {

    //               Debug.Log("Name:" + files[i].Name);
    //              string path = fullPath + files[i].Name;

    //                Debug.Log("path:" + path);
    //             //   AssetImporter importer = AssetImporter.GetAtPath(path);

    //                //改变标记
    //              //  importer.assetBundleName = "hahah";
    //              //  Debug.Log("assetBundleName:" + importer.assetBundleName);

    //            }
               
    //            //Debug.Log( "FullName:" + files[i].FullName );  
    //            //Debug.Log( "DirectoryName:" + files[i].DirectoryName );  
    //        }
    //    }
    //}


   // [MenuItem("Itool/MarkAssetBundle")]
    //public static void MarkAssetBumndle()
    //{
    //    AssetDatabase.RemoveUnusedAssetBundleNames();
    //    string path = Application.dataPath + "/Art/Scences";//获取设定好的文件夹路径
    //    DirectoryInfo directory = new DirectoryInfo(path);
    //    //获取所有子文件夹
     
    //    FileSystemInfo[] fileSystemInfos = directory.GetFileSystemInfos();

    //   // Debug.Log("fileSystemInfos:"+fileSystemInfos.Length );
    //    for (int i = 0; i < fileSystemInfos.Length ; i++)
    //    {
    //        FileSystemInfo temfile = fileSystemInfos[i];
    //        if (temfile  is DirectoryInfo )
    //        {
    //            string tempPath = Path.Combine(path ,temfile .Name);//找到了sceneone这一层级文件夹


    //            //开始遍历每个场景文件夹里的文件
    //           SceneOverView(tempPath);


    //        }
    //    }
    //    string outPath = IPathTools.GetAssetBundlePath();
    //    Debug.Log("outPath:"+outPath );
    //    CopyRecord(path ,outPath);
    //    AssetDatabase.Refresh();//刷新
    //}

    //public static void CopyRecord(string sourcePath,string disPath)
    //{

    //    DirectoryInfo directory = new DirectoryInfo(sourcePath );
    //    if (!directory .Exists )
    //    {
    //        Debug.Log("is not exit");
    //        return;
    //    }
    //    if (!Directory .Exists (disPath ))
    //    Directory.CreateDirectory(disPath );
    //    FileSystemInfo[] files = directory.GetFileSystemInfos();
    //    for (int i = 0; i < files .Length ; i++)
    //    {
    //        FileInfo file = files[i] as FileInfo;
    //        //对于文件的操作
    //        if (file !=null &&file .Extension ==".txt")
    //        {
    //            string sourFile = sourcePath +"/"+ file.Name;
    //            string disFile = disPath + "/" + file.Name;

    //          //  Debug.Log("sourfile:"+sourFile );

    //        //    Debug.Log("disFile:" + disFile );

    //            File.Copy(sourFile ,disFile ,true );
    //        }
    //    }



    //}

    ////对整个场景遍历
    //public static void SceneOverView(string scenePath)
    //{
       

    //    string textFileName = "Record.txt";
    //    string tempath = scenePath + textFileName;
    //    FileStream fileStream = new FileStream(tempath ,FileMode .OpenOrCreate );
    //    StreamWriter streamWriter = new StreamWriter(fileStream);
    //    //存储对应关系
    //    Dictionary<string, string> recDir=new Dictionary<string ,string >();

    //    InterceptHead(scenePath,recDir);
    //    streamWriter.WriteLine(recDir.Count );
    //    foreach (string  key in recDir.Keys )
    //    {
    //        streamWriter.Write(key);
    //        streamWriter.Write(" ");
    //        streamWriter.Write(recDir[key ]);
    //        streamWriter.Write("\n");
    //    }

    //    streamWriter.Close();
    //    fileStream.Close();

    //}
    ////截取相对路径//D:/Tolue/Assets/Art/Scenes/    SceneOne /Laod
    ////想要得到SceneOne/Load 这段字符串
    //public static void  InterceptHead(string fullPath,Dictionary  <string,string>tempRec)
    //{
    //    //得到的是D:/Tolue/总长度
    //    int tempCount = fullPath.IndexOf("Assets");

    //    int tempLength = fullPath.Length;
    //  //  //路径截取成功Assets/Art/Scenes/SCeneOne
    //    string repalecePath = fullPath.Substring(tempCount,tempLength -tempCount );
    //    Debug.Log("repalecePath:"+repalecePath);
    //    DirectoryInfo directory = new DirectoryInfo(fullPath );
    //    if (directory !=null )
    //    {
    //        ListFiles(directory,repalecePath,tempRec);//遍历场景里的每个功能文件夹
    //    }
    //    else
    //    {
    //        Debug.Log("this path is not exit");
    //    }
    //}
    ////遍历场景里的每个功能文件夹

    //public static void  ListFiles(FileSystemInfo  info,string repalcepath,Dictionary <string ,string >tempdir)
    //{

       
    //    if (!info.Exists )
    //    {
    //        Debug.Log(" is not extt");
    //        return;
    //    }
    //    DirectoryInfo directory=info as DirectoryInfo;
    //    FileSystemInfo[] files = directory.GetFileSystemInfos();
    //  //  Debug.Log("issss"+files .Length );
    //    for (int i = 0; i < files .Length ; i++)
    //    {
    //        DirectoryInfo dir = files[i] as DirectoryInfo ;//判断是否为文件夹
    //        FileInfo file = files[i] as FileInfo;
    //       // Debug.Log("i--------" + i);
           
    //        if (dir !=null)     //判断是否为文件夹
    //        {
    //           // Debug.Log("文件夹："+dir .Name );
    //            ListFiles(files[i], repalcepath, tempdir);
    //        }
    //        else//为文件
    //        {
                
    //            if (files[i] .Extension !=".meta"&&files[i] .Name !=".DS_Store")//(ios会有该文件)
    //            {
    //               // Debug.Log("文件名：" + files[i].Name);
    //                ChangeMark(file, repalcepath, tempdir);
    //            }
    //        }
          
               
          
           
    //    }
    //}
//    public static void ChangeAssetMark(FileInfo tempfile, string markstr, Dictionary<string, string> theWriter)
//    {

//        string fullpath = tempfile.FullName;
//        int assetcount = fullpath.IndexOf("Assets");
//        string assetpath = fullpath.Substring(assetcount ,fullpath .Length -assetcount );

//        //assets/scenceone/laod/test.prefab
//        AssetImporter importer = AssetImporter.GetAtPath(assetpath);

//        //改变标记
//        importer.assetBundleName = markstr;
//        if (tempfile.Extension ==".unity")
//        {
//            importer.assetBundleVariant = "u3d";//更改后缀名称
//        }
//        else
//        {
//            importer.assetBundleVariant = "ld";//更改后缀名称表示低质量的文件
//        }


//        //Load----ScenceOne/load

//        string modleName = "";
//        string[] subMark = markstr.Split("/".ToCharArray());

//        if (subMark .Length >1)
//        {
//            modleName = subMark[1];
//        }
//        else
//        {

//            //ScenceOne---sceneOne
//            modleName = markstr;
//        }

//        // scenceOne/load.ld
//        string modlepath = markstr.ToLower()+"."+importer .assetBundleVariant ;
//        if (!theWriter .ContainsKey (modleName ))
//        {
//            theWriter.Add(modleName ,modlepath);
//        }

//    }   
//    //改变物体的tag
//    public static void ChangeMark(FileInfo tempfile, string repalcepath, Dictionary<string, string> tempdir)
//    {
//       // Debug.Log("....0000");
//        if (tempfile!=null )
//        {
            
//            if (tempfile.Name  != ".DS_Store"&&tempfile.Extension != ".meta")
//            {
//                //Debug.Log("!=.DS_Store");
//                string marst = GetBundlePath(tempfile, repalcepath);
////                Debug.Log("marst" + marst);
    //            ChangeAssetMark(tempfile, marst, tempdir);
    //        }
         

           
    //    }
      
      
       
    //}
    ////计算mark标记值是多少
    //public static string  GetBundlePath(FileInfo file, string repalcepath)
    //{
    //    //e:\\temp\\sss.text是这种形式的，和u3d的左斜线不一样，所以需要修正一下对应正确

    //    string tempath = file.FullName;
    //  //  Debug.Log(" file.FullName"+tempath);
    //   // Debug.Log(" repalcepath" + repalcepath);
    //    //windows系统：c:\Users\if\Desktop\yutongzhu\test\
    //    //projectTest\Assets\Art\Scences/ScenceOne\Load\cube.prefab
    //    // mac:   /Users/if/Desktop/yutongzhu/test/projectTest/Assets/Art/Scences/ScenceOne/.DS_Store
    //    if (Application .platform==    RuntimePlatform .WindowsEditor )
    //    {
    //        tempath = FixedWindowsPath(tempath);
    //    }

       
    //    //Assets/Art/Scenes/    SceneOne ==repalcepath  //Load
    //    int assetCount = tempath.IndexOf(repalcepath );//得到Assect出现的开始索引值
    //    assetCount += repalcepath.Length + 1; //得到是d:/ss/Assets/Art/Scenes/    SceneOne / 修正后的长度值
    //    Debug.Log(" file .Name" + file.Name);
    //    //得到文件名字出现的索引值，只不过查找的时候从后开始查找
    //    int namecount = tempath.LastIndexOf(file .Name);
    //    int tempcount = repalcepath.LastIndexOf("/");//最后一次出现/的索引

     
    //    //得到sceneOne的名字
    //    string scenehead = repalcepath.Substring(tempcount +1,repalcepath .Length -tempcount -1);
    //   // Debug.Log("scenehead： " + scenehead);
    //    int templength = namecount - assetCount;

    //    if (templength >0)
    //    {
    //        //windows:Load/cube.prafab
    //        string substring = tempath.Substring(assetCount ,tempath .Length-assetCount );
    //        string[] results = substring.Split("/".ToCharArray ());
    //        return scenehead + "/" + results[0];


    //    }
    //    else
    //    {
    //        //场景文件正好放在scenceone文件下
    //      return   scenehead;
    //    }
    //}
    //public static string FixedWindowsPath(string path)
    //{
    //    path = path.Replace("\\","/");

    //    return path;
    //}
   
}
