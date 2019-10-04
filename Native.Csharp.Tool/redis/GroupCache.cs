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
        public static Dictionary<Group, List<GroupMember>> groupData = new Dictionary<Group, List<GroupMember>>();

        /// <summary>
        /// 设置群成员内容至redis
        /// </summary>
        /// <param name="group">群信息</param>
        /// <param name="members">群成员</param>
        /// <returns></returns>
        public static List<GroupMember> SetGroup(Group group,List<GroupMember> members) 
        {
            BaseRedis.getRedis().SetAsync($"Groups:{group.Id}:info", group);
            BaseRedis.getRedis().HSetAsync($"Groups:list", group.Id + "", group);
            //加载自定义权限
            Dictionary<string, PermitType> resDic = BaseRedis.getRedis().HGetAll<PermitType>($"Groups:{group.Id}:customPermit");
            members.ForEach(p =>
            {
                //是否存在自定义
                bool exist = resDic.TryGetValue(p.QQId + "", out PermitType premit);
                if (exist)
                {
                    //更改权限
                    p.PermitType = premit;
                }
                BaseRedis.getRedis().Del($"Groups:{group.Id}:members");
                BaseRedis.getRedis().HSetAsync($"Groups:{group.Id}:members", p.QQId+"", p);
            });

            //群主
            GroupMember master = members.Where(c => c.PermitType == PermitType.Holder).FirstOrDefault();
            BaseRedis.getRedis().Del($"Groups:{group.Id}:master");
            BaseRedis.getRedis().HSetAsync($"Groups:{group.Id}:master", master.QQId + "", master);
            //管理
            List<GroupMember> manages = members.Where(c => c.PermitType == PermitType.Manage).ToList();
            BaseRedis.getRedis().Del($"Groups:{group.Id}:manage");
            manages.ForEach(c =>
            {
                BaseRedis.getRedis().HSetAsync($"Groups:{group.Id}:manage", c.QQId + "", c);
            });

            return members;
        }
        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="gid">群号</param>
        /// <param name="redis">是否从redis中读取</param>
        /// <returns></returns>
        public static List<GroupMember> GetGroupMember(long gid,bool redis=true) {
            if (redis)
            {
                Dictionary<string, GroupMember> resDic = BaseRedis.getRedis().HGetAll<GroupMember>($"Groups:{gid}");
                return resDic.Values.ToList();
            }
            else
            {
                return groupData.Where(p => p.Key.Id == gid).Select(p => p.Value).FirstOrDefault();
            }
            
        }

        /// <summary>
        /// 初始化群信息到内存
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Group, List<GroupMember>> initGroupCache()
        {
            //清理
            groupData.Clear();
            Dictionary<string, Group> resDic = BaseRedis.getRedis().HGetAll<Group>($"Groups:list");
            //填充群缓存
            foreach (KeyValuePair<string, Group> kvp in resDic)
            {
                groupData.Add(kvp.Value, GetGroupMember(kvp.Value.Id));
            }
            return groupData;
        }

        /// <summary>
        /// 获取成员身份
        /// </summary>
        /// <param name="gid">群号</param>
        /// <param name="qq">QQ</param>
        /// <param name="permitType">身份枚举</param>
        /// <returns></returns>
        public static bool IsGroupType(long gid,long qq, PermitType permitType)
        {
            int count = GetGroupMember(gid, false).Where(p => p.PermitType == permitType && p.QQId == qq).Count();
            return count > 0;
        }
        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="gid">群号</param>
        /// <param name="permitType">权限类型</param>
        /// <returns></returns>
        public static List<GroupMember> GetMember(long gid, PermitType permitType)
        {
            return GetGroupMember(gid, false).Where(p => p.PermitType == permitType).ToList();
        }
        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="gid">群号</param>
        /// <param name="qq">QQ</param>
        /// <returns></returns>
        public static GroupMember GetMember(long gid, long qq)
        {
            return GetGroupMember(gid, false).Where(p => p.QQId == qq).FirstOrDefault();
        }
    }
}
