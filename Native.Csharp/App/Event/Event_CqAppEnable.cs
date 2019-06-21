using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Extend;
using Native.Csharp.App.Interface;
using Native.Csharp.Tool.IniConfig.Linq;

namespace Native.Csharp.App.Event
{
    /// <summary>
	/// Type=1003 应用被启用, 事件实现
	/// </summary>
    public class Event_CqAppEnable : ICqAppEnable
    {
        /// <summary>
		/// 处理 酷Q 的插件启动事件回调
		/// </summary>
		/// <param name="sender">事件的触发对象</param>
		/// <param name="e">事件的附加参数</param>
        public void CqAppEnable (object sender, CqAppEnableEventArgs e)
        {
            // 当应用被启用后，将收到此事件。
            // 如果酷Q载入时应用已被启用，则在_eventStartup(Type=1001,酷Q启动)被调用后，本函数也将被调用一次。
            // 如非必要，不建议在这里加载窗口。（可以添加菜单，让用户手动打开窗口）
            Common.IsRunning = true;
            string commandPath = Common.CqApi.GetAppDirectory() + "command.ini";
            IniObject iObject;
            if (!File.Exists(commandPath))
            {
                iObject = new IniObject
                {
                    new IniSection("gcommands")
                    {
                        { "功能","menu"},
                        { "查日常","dayTask"},
                        //{ "抽锦鲤","chose"},
                        { "开服监控","serverRemind"},
                        { "开服查询","serverQuery"},
                        { "/roll","roll" },
                        { "建议","advise"}
                    },
                    new IniSection("pcommands")
                    {
                        { "功能","menu"},
                        { "建议","advise"},
                        { "反馈","feedback"},
                        { "个人反馈","pfeedback"},
                    }
                };
                iObject.Save(commandPath);
            };
            iObject = IniObject.Load(commandPath, Encoding.Default);
            IniSection pCommand = iObject["pcommands"];
            Common.PCommandDic = pCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            IniSection gCommand = iObject["gcommands"];
            Common.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            Common.SerList = Jx3OpenTell.GetSerList();
            Common.ServerRemind = new ServerRemind();
            Common.menuStr = StringOrg.getMenuStr();
            Common.masterQQ = 415206409L;
        }
    }
}
