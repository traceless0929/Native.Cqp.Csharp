using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.App.EventArgs;
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
            // 当应用被启用后，本方法将被调用。
            // 如果酷Q载入时应用已被启用，则在 ICqStartup 接口的实现方法被调用后，本方法也将被调用一次。
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
                        { "测试","test"}
                    },
                    new IniSection("pcommands")
                    {
                        { "测试","test"}
                    }
                };
                iObject.Save(commandPath);
            };
            iObject = IniObject.Load(commandPath, Encoding.Default);
            IniSection pCommand = iObject["pcommands"];
            Common.PCommandDic = pCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            IniSection gCommand = iObject["gcommands"];
            Common.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());

            commandPath = Common.CqApi.GetAppDirectory() + "setting.ini";
            if (!File.Exists(commandPath))
            {
                iObject = new IniObject
                {
                    new IniSection("setting")
                    {
                        { "master",415206409}
                    }
                };
                iObject.Save(commandPath);
            };
            iObject = IniObject.Load(commandPath, Encoding.Default);
            IniSection settings = iObject["setting"];
            Common.settingDic = settings.ToDictionary(p => p.Key, p => p.Value.ToString());
        }
    }
}
