using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Extend
{
    public class StringOrg
    {
        public static string getMenuStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[群功能目录]");
            int i = 1;
            foreach(var item in Common.GCommandDic)
            {
                sb.AppendLine(i + ". " + item.Key);
                i++;
            }
            sb.AppendLine("[私聊功能目录]");
            i = 1;
            foreach (var item in Common.PCommandDic)
            {
                if (item.Key.Contains("反馈"))
                {
                    continue;
                }
                sb.AppendLine(i + ". " + item.Key);
                i++;
            }
            sb.AppendLine("群辅助等强大功能请访问：https://traceless.site/index.php/archives/62/");
            return sb.ToString();
        }
    }
}
