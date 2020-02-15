using Native.Sdk.Cqp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Traceless.SamrtT.Code.Func;
using Site.Traceless.SamrtT.Code.Model.SmartT;

namespace Site.Traceless.SamrtT.Code
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
        /// 命令映射路由(主人私聊)
        /// </summary>
        public static Dictionary<string, string> MPCommandDic { get; set; } = new Dictionary<string, string>()
        {
            {"添加超级管理员","addManager" },
            {"添加管理员","addManager" },
            {"更新菜单","addGroupMenu" },
            {"获取Token","getToken" }
        };
		/// <summary>
		/// 设置
		/// </summary>
		public static Dictionary<string, string> settingDic { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 垃圾分类缓存
        /// </summary>
        public static Dictionary<string, TrashSortResp> TrashDic { get; set; } = new Dictionary<string, TrashSortResp>();

        /// <summary>
        /// 菜单
        /// </summary>
        public static string menuStr = "";
        /// <summary>
        /// 开服监控
        /// </summary>
        public static JxServer JxServer;
		/// <summary>
		/// 服务器列表
		/// </summary>
		public static string[,] SerList;
		public static CQApi CqApi
		{
			get;set;
		}
}
}
