using Native.Csharp.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Command
{
    public class GroupApp
    {
        public static void test(GroupMessageEventArgs args, AnalysisMsg msg)
        {
            String res = args.FromQQ + " 发送:" + msg.OriginStr;
            Common.CqApi.SendGroupMessage(args.FromGroup, res);
        }
    }
}
