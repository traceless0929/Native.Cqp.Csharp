using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Native.Csharp.Tool.Utils
{
    public class FileUtil
    {
        public static string[] GetFileContent(string path, Encoding encoding)
        {
            if (!File.Exists(path)) return null;
            return File.ReadAllLines(path, encoding);
        }

        public static string GetFileText(string path, Encoding encoding)
        {
            if (!File.Exists(path)) return null;
            return File.ReadAllText(path, encoding);
        }

        public static void WriteFileText(String path,Encoding encoding,String content)
        {
            String pathStr = Path.GetDirectoryName(path);
            Directory.CreateDirectory(pathStr);
            File.WriteAllText(path, content, encoding);
        }

        ///// <summary>
        ///// 下载图片到本地
        ///// </summary>
        ///// <param name="url">HTML</param>
        ///// <param name="path">路径</param>
        //public string SaveUrlPics(string url, string path,string name)
        //{
        //    String pathStr = Path.GetDirectoryName(path);
        //    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        //    try
        //    {
        //        WebClient wc = new WebClient();
        //        wc.DownloadFile(url, path + "/" + name + url.Substring(url.LastIndexOf("/") + 1));
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    return strHTML;
        //}
    }

}
