using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            File.WriteAllText(path, content, encoding);
        }
    }
}
