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
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]群欢迎关闭成功!");
            }
        }

        public static void gmWelSet(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            if (!groupData.GetSwitch(SwitchEnum.welopen))
            {
                return;
            }
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator, QQGroupMemberType.Manage);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                string template = groupData.SetTemplate(SwitchEnum.welopen, msg.OriginStr.Replace(msg.What, "")).Trim();
                template = template.ReplaceTrimAndLine();
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]群欢迎设置成功!"+Environment.NewLine+"模板如下:"+Environment.NewLine+ template);
            }
        }
        public static void gmThesureOpen(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                groupData.ChangeSwitch(SwitchEnum.thesureopen, true);
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]词库开启成功!");
            }
        }
        public static void gmThesureClose(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                groupData.ChangeSwitch(SwitchEnum.thesureopen, false);
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]词库关闭成功!");
            }
        }
        public static void gmThesureAdd(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            if (!groupData.GetSwitch(SwitchEnum.thesureopen))
            {
                return;
            }
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator, QQGroupMemberType.Manage);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                int startIndex = msg.Who.IndexOf("问");
                int endIndex = msg.Who.IndexOf("答");
                string q = msg.Who.Substring(startIndex+1, endIndex - startIndex-1);
                string a = msg.Who.Substring(endIndex + 1);
                bool islike = msg.How.Contains("模糊");
                KeyValuePair<string, string> res = groupData.AddThesure(q, a,islike);
                string template = res.Value.ReplaceTrimAndLine().ReplaceAtQQ();
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]添加词库成功!" + Environment.NewLine + "如下:" + Environment.NewLine + "问:"+Environment.NewLine+q + Environment.NewLine+"答:" + Environment.NewLine+a+Environment.NewLine+"模式:"+msg.How);
            }
        }
        public static void gmThesureDel(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            if (!groupData.GetSwitch(SwitchEnum.thesureopen))
            {
                return;
            }
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator, QQGroupMemberType.Manage);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                bool islike = msg.How.Contains("模糊");
                groupData.DelThesure(msg.Who,islike);
                e.CQApi.SendGroupMessage(e.FromGroup.Id, "[小T群管]词库删除成功!" + Environment.NewLine + "删除如下:" + Environment.NewLine + "问:"+Environment.NewLine+msg.Who+ Environment.NewLine + "模式:" + msg.How);
            }
        }
        public static void gmThesureRead(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            if (!groupData.GetSwitch(SwitchEnum.thesureopen))
            {
                return;
            }
            List<long> rightIds = groupData.GetManager(QQGroupMemberType.Creator, QQGroupMemberType.Manage);
            if (rightIds.Contains(e.FromQQ.Id))
            {
                bool islike = msg.How.Contains("模糊");
                bool parseRes = int.TryParse(msg.Who,out int nowpage);
                int pageSize = 15;
                if (!parseRes)
                {
                    return;
                }
                Dictionary<string, string> res= groupData.ReadThesure(islike);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("[小T群管] "+(islike?"模糊":"精确")+"词库总数:" + res.Count + " 当前:" + msg.Who + "页 共:" + ((res.Count+ pageSize - 1)/ pageSize) + "页");
                int i = 1;
                foreach (var keyValuePair in res.Skip((nowpage-1)* pageSize).Take(pageSize).AsEnumerable())
                {
                    sb.AppendLine((i + (nowpage - 1) * pageSize) + ". " + keyValuePair.Key);
                    sb.AppendLine("答:" + keyValuePair.Value);
                    i++;
                }
                e.CQApi.SendGroupMessage(e.FromGroup.Id, sb.ToString());
            }
        }

        public static void gmThesureScan(CQGroupMessageEventArgs e, AnalysisMsg msg, GroupData groupData)
        {
            if (!groupData.GetSwitch(SwitchEnum.thesureopen))
            {
                return;
            }

            string value = groupData.ScanThesure(msg.OriginStr);
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            e.CQApi.SendGroupMessage(e.FromGroup.Id, value.ReplaceTrimAndLine().ReplaceAtQQ());
        }
    }
}
