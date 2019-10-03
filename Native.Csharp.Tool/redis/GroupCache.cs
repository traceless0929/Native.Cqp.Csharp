using Native.Csharp.Sdk.Cqp.Enum;
using Native.Csharp.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Csharp.Tool.redis
{
    public class GroupCache
    {
       
        public static List<GroupMember> setGroupMember(long gid,List<GroupMember> members) 
        {
            members.ForEach(p =>
            {
                BaseRedis.getRedis().HSetAsync($"Groups:{gid}:members", p.QQId+"", p);
                //群主
                GroupMember master = members.Where(c => c.PermitType == PermitType.Holder).FirstOrDefault();
                BaseRedis.getRedis().Del($"Groups:{gid}:master");
                BaseRedis.getRedis().HSetAsync($"Groups:{gid}:master", master.QQId + "", master);
                //管理
                List<GroupMember> manages = members.Where(c => c.PermitType == PermitType.Manage).ToList();
                BaseRedis.getRedis().Del($"Groups:{gid}:manage");
                manages.ForEach(c =>
                {
                    BaseRedis.getRedis().HSetAsync($"Groups:{gid}:manage", c.QQId + "", c);
                });
            });
            return members;
        }

        public static List<GroupMember> getGroupMember(long gid) {
            Dictionary<string, GroupMember> resDic = BaseRedis.getRedis().HGetAll<GroupMember>($"Groups:{gid}");
            return resDic.Values.ToList();
        }
    }
}
