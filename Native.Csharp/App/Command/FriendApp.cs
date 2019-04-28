using Native.Csharp.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Command
{
    public class FriendApp
    {
        public static void test(PrivateMessageEventArgs args, AnalysisMsg msg)
        {
            String res = "私聊消息" + args.FromQQ + " 发送:" + msg.OriginStr;
            Common.CqApi.SendPrivateMessage(args.FromQQ, res);
        }
    }
}
