using Native.Sdk.Cqp.Enum;
using Native.Sdk.Cqp.EventArgs;
using Newtonsoft.Json;
using Site.Traceless.Gmanger.Datas;
using Site.Traceless.SmartT.Code.Model;
using Site.Traceless.SmartT.Code.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Site.Traceless.SmartT.Code.Command
{
    public class MFriendApp
    {
        public static void getToken(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(long.Parse(Common.settingDic["master"]), e.CQApi.GetCsrfToken());
            e.CQApi.SendPrivateMessage(long.Parse(Common.settingDic["master"]), e.CQApi.GetCookies("qun.qq.com"));
        }

        public static void addGroupMenu(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] raw = msg.Who.Split(',');
            int max = raw.Length / 2;
            for (int i = 0; i < max; i++)
            {
                dic.Add(raw[i], raw[i + 1]);
            }

            if (string.IsNullOrEmpty(msg.How))
            {
                e.CQApi.GetGroupList().ForEach(p =>
                {
                    GroupData groupData = Gmanger.Common.GetGroupData(e.CQApi, p.Group.Id);
                    groupData?.upsertMenu(dic);
                });
            }
            else
            {
                foreach (var s in msg.How.Split(','))
                {
                    long gid = long.Parse(s);
                    GroupData groupData = Gmanger.Common.GetGroupData(e.CQApi, gid);
                    groupData?.upsertMenu(dic);
                }
            }
        }

        public static void addManager(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            QQGroupMemberType type = QQGroupMemberType.Manage;
            if (msg.What.Contains("超级管理员"))
            {
                type = QQGroupMemberType.Creator;
            }
            long gid = long.Parse(msg.Who);
            GroupData groupData = Gmanger.Common.GetGroupData(e.CQApi, gid);
            long[] qqs = msg.How.Split(',').Select(long.Parse).ToArray();
            List<long> res = groupData?.AddManager(type, qqs);
            if (res != null && res.Any())
            {
                e.CQApi.SendPrivateMessage(long.Parse(Common.settingDic["master"]),
                    $"[小T群管] 添加群{gid}{QQGroupMemberType.Manage.GetDescription()}成功当前{JsonConvert.SerializeObject(res)}");
            }
        }
    }
}