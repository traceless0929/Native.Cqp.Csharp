/*
 *	此代码由 T4 引擎根据 LibExport.tt 模板生成, 若您不了解以下代码的用处, 请勿修改!
 *	
 *	此文件包含项目 Json 文件的事件导出函数.
 */
using System;
using System.Runtime.InteropServices;
using System.Text;
using Native.Csharp.App.Event;
using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Interface;
using Native.Csharp.Sdk.Cqp;
using Native.Csharp.Sdk.Cqp.Model;
using Native.Csharp.Sdk.Cqp.Other;
using Unity;

namespace Native.Csharp.App.Core
{
    public class LibExport
    {
		#region --字段--
		private static Encoding _defaultEncoding = null;
		#endregion

		#region --构造函数--
		/// <summary>
		/// 静态构造函数, 注册依赖注入回调
		/// </summary>
		static LibExport ()
		{
			_defaultEncoding = Encoding.GetEncoding ("GB18030");
			
			// 初始化 Costura.Fody
			CosturaUtility.Initialize ();
			
			// 初始化依赖注入容器
			Common.UnityContainer = new UnityContainer ();

			// 程序开始调用方法进行注册
			Event_AppMain.Registbackcall (Common.UnityContainer);

			// 注册完毕调用方法进行分发
			Event_AppMain.Resolvebackcall (Common.UnityContainer);

			// 分发应用内事件
			ResolveAppbackcall ();
		}
		#endregion
		
		#region --核心方法--
		/// <summary>
		/// 返回 AppID 与 ApiVer, 本方法在模板运行后会根据项目名称自动填写 AppID 与 ApiVer
		/// </summary>
		/// <returns></returns>
		[DllExport (ExportName = "AppInfo", CallingConvention = CallingConvention.StdCall)]
		private static string AppInfo ()
		{
			// 请勿随意修改
			// 
			Common.AppName = "解析失败";
			Common.AppVersion = Version.Parse ("0.0.0");		

			//
			// 当前项目名称: Native.Csharp
			// Api版本: 9

			return string.Format ("{0},{1}", 9, "Native.Csharp");
		}

		/// <summary>
		/// 接收插件 AutoCode, 注册异常
		/// </summary>
		/// <param name="authCode"></param>
		/// <returns></returns>
		[DllExport (ExportName = "Initialize", CallingConvention = CallingConvention.StdCall)]
		private static int Initialize (int authCode)
		{
			// 酷Q获取应用信息后，如果接受该应用，将会调用这个函数并传递AuthCode。
			Common.CqApi = new CqApi (authCode);

			// AuthCode 传递完毕后将对象加入容器托管, 以便在其它项目中调用
			Common.UnityContainer.RegisterInstance<CqApi> (Common.CqApi);

			// 注册插件全局异常捕获回调, 用于捕获未处理的异常, 回弹给 酷Q 做处理
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			// 本函数【禁止】处理其他任何代码，以免发生异常情况。如需执行初始化代码请在Startup事件中执行（Type=1001）。
			return 0;
		}
		#endregion
		
		#region --私有方法--
		/// <summary>
		/// 全局异常捕获, 用于捕获开发者未处理的异常, 此异常将回弹至酷Q进行处理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void CurrentDomain_UnhandledException (object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex != null)
			{
				StringBuilder innerLog = new StringBuilder ();
				innerLog.AppendLine ("发现未处理的异常!");
				innerLog.AppendLine ("异常堆栈：");
				innerLog.AppendLine (ex.ToString ());
				Common.CqApi.AddFatalError (innerLog.ToString ());      //将未经处理的异常弹回酷Q做处理
			}
		}
		
		/// <summary>
		/// 获取所有的注入项, 分发到对应的事件
		/// </summary>
		private static void ResolveAppbackcall ()
		{
		}
		#endregion
		
		#region --导出方法--
		#endregion
    }
}

