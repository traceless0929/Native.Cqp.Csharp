﻿using Native.Csharp.Sdk.Cqp.EventArgs;
using Site.Traceless.SamrtT.Code.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Traceless.SamrtT.Code.Func;

namespace Site.Traceless.SamrtT.Code.Command
{
    public class FriendApp
    {
        public static void menu(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(e.FromQQ, Common.menuStr);
        }
        public static void feedback(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            if (e.FromQQ != Convert.ToInt64(Common.settingDic["master"]))
            {
                return;
            }
            e.CQApi.SendGroupMessage(long.Parse(msg.Who), msg.How + Environment.NewLine + "[来自作者的反馈]");
        }
        public static void trashsort(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            if (string.IsNullOrEmpty(msg.Who))
            {
                return;
            }
            e.CQApi.SendPrivateMessage(e.FromQQ, TrashSort.goSort(msg.Who));
        }
        public static void advise(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(Convert.ToInt64(Common.settingDic["master"]), $"来自个人{e.FromQQ}:{msg.Who} {msg.How}");
        }
        public static void pfeedback(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            if (e.FromQQ != Convert.ToInt64(Common.settingDic["master"]))
            {
                return;
            }
            e.CQApi.SendPrivateMessage(long.Parse(msg.Who), msg.How + Environment.NewLine + "[来自作者的反馈]");
        }
        public static void dayTask(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(e.FromQQ, JxTask.getTask());
        }
    }
}