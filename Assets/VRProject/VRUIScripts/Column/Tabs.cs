

using System.Collections.Generic;

public class Tabs 
{
  
        /// <summary>
        /// 
        /// </summary>
        public List<TabsItem> tabs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int areaCode { get; set; }
   
}
public class TabsItem
{
    /// <summary>
    /// 
    /// </summary>
    public int classID { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int areaType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int imgtype { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string path { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int styleType { get; set; }
    /// <summary>
    /// 推荐
    /// </summary>
    public string name { get; set; }
}
