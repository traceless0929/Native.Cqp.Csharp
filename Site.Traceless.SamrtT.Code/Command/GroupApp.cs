using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.SamrtT.Code.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp;
using Native.Sdk.Cqp.Model;
using Site.Traceless.SamrtT.Code.Func;
using Site.Traceless.Tools.Utils;

namespace Site.Traceless.SamrtT.Code.Command
{
    public class GroupApp
    {
        public static void menu(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendGroupMessage(e.FromGroup, Common.menuStr);
        }
        public static void advise(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(Convert.ToInt64(Common.settingDic["master"]), $"来自群{e.FromGroup}的{e.FromQQ}:{msg.Who} {msg.How}");
        }
        public static void trashsort(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            if (string.IsNullOrEmpty(msg.Who))
            {
                return;
            }
            e.CQApi.SendGroupMessage(e.FromGroup, TrashSort.goSort(msg.Who));

        }
        public static void roll(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendGroupMessage(e.FromGroup, CQApi.CQCode_At(e.FromQQ) + $"Roll了 {RandomUtil.RandomGet(0, 101)} 点");
        }
        public static void chose(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            var memberInfos = e.FromGroup.GetGroupMemberList();
            var str = msg.Who;
            var orderid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Common.CqApi.SendGroupMessage(e.FromGroup,
                $"5S后,开始从{memberInfos.Count()}人中抽取幸运锦鲤{str}!" +
                Environment.NewLine +
                $"锦鲤编号:{orderid}");
            System.Threading.Thread.Sleep(5000);
            var choseQQIndex = RandomUtil.RandomGet(0, memberInfos.Count());
            if (memberInfos.Count - 1 < choseQQIndex)
            {
                choseQQIndex = memberInfos.Count - 1;
            }
            var choseQQ = memberInfos[choseQQIndex].QQ.Id;
            var choseQQStr = "我自己！没想到吧！";
            if (choseQQ != e.CQApi.GetLoginQQ())
            {
                choseQQStr = CQApi.CQCode_At(choseQQ) + Environment.NewLine + "ヽ(●-`Д´-)ノ！";
            }
            e.CQApi.SendGroupMessage(e.FromGroup,
                $"Boom!{orderid}号{str}的锦鲤为！" +
                Environment.NewLine +
                choseQQStr);
        }
        public static void dayTask(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendGroupMessage(e.FromGroup, JxTask.getTask());
        }
        public static void serverRemind(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            Common.JxServer.GoServerRemind(e.FromGroup.Id, msg.Who);
        }

        public static void serverQuery(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            Common.JxServer.GoServerQuery(e.FromGroup.Id, msg.Who);
        }
    }
}