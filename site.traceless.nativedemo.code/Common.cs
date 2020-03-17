using Native.Sdk.Cqp;
using System.Collections.Generic;

namespace Site.Traceless.Nativedemo.Code
{
    public static class Common
    {
        /// <summary>
        /// 命令映射路由(群聊)
        /// </summary>
        public static Dictionary<string, string> GCommandDic { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 命令映射路由(私聊)
        /// </summary>
        public static Dictionary<string, string> PCommandDic { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 设置
        /// </summary>
        public static Dictionary<string, string> settingDic { get; set; } = new Dictionary<string, string>();

        public static CQApi CqApi
        {
            get; set;
        }
    }
}