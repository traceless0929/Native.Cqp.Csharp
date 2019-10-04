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
            Common.CqApi.SendPrivateMessage(Common.getSetting<long>("master"), $"来自个人{args.FromQQ}:{msg.Who} {msg.How}");
        }

        public static void menu(CqPrivateMessageEventArgs args, AnalysisMsg msg)
        {
            Common.sendResult(args, Common.menuStr);
        }

        public static void feedback(CqPrivateMessageEventArgs args, AnalysisMsg msg)
        {
            if (args.FromQQ != Common.getSetting<long>("master"))
            {
                return;
            }
            Common.CqApi.SendGroupMessage(long.Parse(msg.Who), msg.How + Environment.NewLine + "[来自作者的反馈]");

        }
        public static void trashsort(CqPrivateMessageEventArgs args, AnalysisMsg msg)
        {
            if (string.IsNullOrEmpty(msg.Who))
            {
                return;
            }
            Common.sendResult(args, Extend.TrashSort.goSort(msg.Who));
        }

        public static void pfeedback(CqPrivateMessageEventArgs args, AnalysisMsg msg)
        {
            if (args.FromQQ != Common.getSetting<long>("master"))
            {
                return;
            }
            Common.CqApi.SendPrivateMessage(long.Parse(msg.Who), msg.How + Environment.NewLine + "[来自作者的反馈]");
        }
    }
}
