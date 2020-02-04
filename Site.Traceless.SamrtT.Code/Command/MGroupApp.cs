using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Enum;
using Native.Csharp.Sdk.Cqp.EventArgs;
using Site.Traceless.Gmanger.Datas;
using Site.Traceless.Gmanger.Enum;
using Site.Traceless.SamrtT.Code.Model;
using Site.Traceless.SamrtT.Code.Utils;

namespace Site.Traceless.SamrtT.Code.Command
{
    public class MGroupApp
    {
        public static void gmAllOpen(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                groupData.ChangeSwitch(SwitchEnum.gmopen,true);
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]群管开启成功!");
            }
        }

        public static void gmAllClose(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                groupData.ChangeSwitch(SwitchEnum.gmopen, false);
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]群管关闭成功!");
            }
        }

        public static void gmWelOpen(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                groupData.ChangeSwitch(SwitchEnum.welopen, true);
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]群欢迎开启成功!");
            }
        }

        public static void gmWelClose(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                groupData.ChangeSwitch(SwitchEnum.welopen, false);
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]群欢迎开启成功!");
            }
        }

        public static void gmWelSet(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator, QQGroupMemberType.Manage);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                string template = groupData.SetTemplate(SwitchEnum.welopen, msg.OriginStr.Replace(msg.What, "")).Trim();
                template = template.ReplaceTrimAndLine();
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]群欢迎设置成功!"+Environment.NewLine+"模板如下:"+Environment.NewLine+ template);
            }
        }
    }
}
