using Native.Csharp.Sdk.Cqp.Interface;
using Site.Traceless.R6.Code.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Native.Csharp.App
{
	/// <summary>
	/// 酷Q应用主入口类
	/// </summary>
	public static class CQMain
	{
		/// <summary>
		/// 在应用被加载时将调用此方法进行事件注册, 请在此方法里向 <see cref="IUnityContainer"/> 容器中注册需要使用的事件
		/// </summary>
		/// <param name="container">用于注册的 IOC 容器 </param>
		public static void Register (IUnityContainer container)
		{
			container.RegisterType<IGroupMessage, Event_GroupMsg>("群消息处理");
            container.RegisterType<ICQStartup, Event_CQStartup>("酷Q启动事件");
			container.RegisterType<ICQExit, Event_CQExit>("酷Q关闭事件");
			container.RegisterType<IAppEnable, Event_AppEnable>("应用已被启用");
			container.RegisterType<IAppDisable, Event_AppDisable>("应用将被停用");
		}
	}
}
