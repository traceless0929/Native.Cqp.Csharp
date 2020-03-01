﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.SamrtT.Code.Utils
{
    public class MenuUitls
    {
        public static string getMenuStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[群功能目录]");
            int i = 1;
            foreach (var item in Common.GCommandDic)
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
            sb.AppendLine("获取更多：https://traceless.site/");
            return sb.ToString();
        }
    }
}
