using Native.Csharp.Sdk.Cqp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.R6.Code
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
			get;set;
		}
}
}
