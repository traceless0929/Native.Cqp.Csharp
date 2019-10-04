using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Model;
using Native.Csharp.Sdk.Cqp.Model;
using Native.Csharp.Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Csharp.App.Command
{
    public class AdminApp
    {
        public static void refreshGroups<T>(T args, AnalysisMsg msg) where T : CqEventArgsBase
        {
            doRefresh<T>(args,msg);
        }
        private static void doRefresh<T>(T args,AnalysisMsg msg) where T : CqEventArgsBase
        {
            Encoding encoding = Encoding.UTF8;
            List<GroupMember> memberInfos = null;
            int i = 1;

            List<Group> groups = Common.CqApi.GetGroupList().GroupBy(p => p.Id).Select(p => p.First()).ToList();
            Common.sendResult<T>(args, $"开始刷新群总计{groups.Count}个");
            groups.ForEach(p =>
            {
                memberInfos = Common.CqApi.GetMemberList(p.Id);
                String filePath = Common.groupMemberPath + p.Id + ".json";
                GroupMember zero = memberInfos.Where(c => c.GroupId == 0L || c.QQId == 0L).FirstOrDefault();
                if (null != zero)
                {
                    memberInfos.Remove(zero);
                }
                FileUtil.WriteFileText(filePath, encoding, Newtonsoft.Json.JsonConvert.SerializeObject(memberInfos));
                Tool.redis.GroupCache.SetGroup(p, memberInfos);
                Common.sendResult<T>(args,$"刷新第{i}个群列表，{p.Name}({p.Id})-{memberInfos.Count}人");
                i++;
            });
            Common.sendResult<T>(args, $"刷新完成{i - 1}/{groups.Count}个");
        }

       
    }
}
