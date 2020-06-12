using Native.Sdk.Cqp;
using System.Collections.Generic;

namespace Site.Traceless.Common.Model
{
    public static class CommonData
    {
        /// <summary>
        /// 设置
        /// </summary>
        public static Dictionary<string, string> settingDic { get; set; } = new Dictionary<string, string>();

        public static CQApi CqApi
        {
            get; set;
        }

        public static CQLog CqLog
        {
            get; set;
        }
    }
}