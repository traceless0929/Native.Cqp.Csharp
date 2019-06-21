using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Command
{
    public class FriendApp
    {
        public static void advise(CqPrivateMessageEventArgs args, AnalysisMsg msg)
        {
            Common.CqApi.SendPrivateMessage(Common.masterQQ, $"来自个人{args.FromQQ}:{msg.Who} {msg.How}");
        }

        public static void menu(CqPrivateMessageEventArgs args, AnalysisMsg msg)
        {
            Common.CqApi.SendPrivateMessage(args.FromQQ, Common.menuStr);
        }

        public static void feedback(CqPrivateMessageEventArgs args, AnalysisMsg msg)
        {
            if (args.FromQQ != Common.masterQQ)
            {
                return;
            }
            Common.CqApi.SendGroupMessage(long.Parse(msg.Who), msg.How + Environment.NewLine + "[来自作者的反馈]");

        }

        public static void pfeedback(CqPrivateMessageEventArgs args, AnalysisMsg msg)
        {
            if (args.FromQQ != Common.masterQQ)
            {
                return;
            }
            Common.CqApi.SendPrivateMessage(long.Parse(msg.Who), msg.How + Environment.NewLine + "[来自作者的反馈]");
        }
    }
}
