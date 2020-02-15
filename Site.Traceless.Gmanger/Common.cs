﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp;
using Site.Traceless.Gmanger.Datas;

namespace Site.Traceless.Gmanger
{
    public static class Common
    {
        public static Dictionary<long, GroupData> GroupDataDic = new Dictionary<long, GroupData>();
        public static CQApi CqApi { get; set; }

        public static GroupData GetGroupData(CQApi cqApi,long gid)
        {
            if (CqApi == null)
            {
                CqApi = cqApi;
            }

            if (gid < 1)
            {
                return null;
            }
            var exist = cqApi.GetGroupList().Exists(p => p.Group.Id == gid);
            if (!exist)
            {
                return null;
            }
            if (!GroupDataDic.TryGetValue(gid, out GroupData data))
            {
                data = new GroupData(gid);
            }
            return data;
        }
    }
}
