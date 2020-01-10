using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IProtoTools  {
    /// <summary>
    /// 序列化出来给socket发送
    /// </summary>
    /// <returns>The serialize.</returns>
    /// <param name="msg">Message.</param>
	//public static byte [] Serialize( IExtensible msg)
    //{

    //    byte[] result;
    //    using (var stream= new MemoryStream())
    //    {
    //        Serializer.Serlize(stream ,msg);
    //        result = stream.ToArray();
    //    }
    //    return result;
    //}

    /// <summary>
    /// 主要从socket接收数据。反序列化成类
    /// </summary>
    /// <returns>The deserliazer.</returns>
    /// <param name="message">Message.</param>
    /// <typeparam name="IExtensible">The 1st type parameter.</typeparam>
    //public static IExtensible Deserliazer<IExtensible>(byte[] message)
    //{

    //    IExtensible result;
    //    using (var stream = new MemoryStream())
    //    {

    //        result = Serializer.Deserlize<IExtensible>(stream);
    //    }
    //    return result;
    //}
}
