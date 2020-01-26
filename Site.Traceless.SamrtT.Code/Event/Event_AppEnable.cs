using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Tool.IniConfig.Linq;
using System.IO;

namespace Site.Traceless.SamrtT.Code.Event
{
    public class Event_AppEnable : IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            Common.CqApi = e.CQApi;
            string commandPath = Common.CqApi.AppDirectory + "command.ini";
            IniObject iObject;
            if (!File.Exists(commandPath))
            {
                iObject = new IniObject
                {
                    new IniSection("gcommands")
                    {
                        { "功能","menu"},
                        { "查日常","dayTask"},
                        { "抽锦鲤","chose"},
                        { "开服监控","serverRemind"},
                        { "开服查询","serverQuery"},
                        { "/roll","roll" },
                        { "建议","advise"},
                        { "垃圾分类","trashsort"}
                    },
                    new IniSection("pcommands")
                    {
                        { "功能","menu"},
                        { "建议","advise"},
                        { "反馈","feedback"},
                        { "个人反馈","pfeedback"},
                        { "垃圾分类","trashsort"}
                    }
                };
                iObject.Save(commandPath);
            };
            iObject = IniObject.Load(commandPath, Encoding.Default);
            IniSection pCommand = iObject["pcommands"];
            Common.PCommandDic = pCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            IniSection gCommand = iObject["gcommands"];
            Common.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());

            commandPath = Common.CqApi.AppDirectory + "setting.ini";
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
