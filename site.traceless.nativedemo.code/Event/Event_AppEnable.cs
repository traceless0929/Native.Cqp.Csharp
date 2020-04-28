using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;
using System.IO;
using Native.Tool.IniConfig.Linq;
using Native.Tool.IniConfig;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class Event_AppEnable : IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            Common.CqApi = e.CQApi;
            string commandPath = Common.CqApi.AppDirectory + "command.ini";
            IniConfig rootConfig=null;
            if (!File.Exists(commandPath))
            {
                rootConfig = new IniConfig(commandPath);
                rootConfig.Object["gcommands"]["功能1"] = "funcOne";
                rootConfig.Object["gcommands"]["功能2"] = "funcTwo";
                rootConfig.Object["pcommands"]["功能1"] = "funcOne";
                rootConfig.Object["pcommands"]["功能2"] = "funcTwo";

                rootConfig.Save();
            };
            rootConfig.Load();

            ISection pCommand = rootConfig.Object["pcommands"];
            Common.PCommandDic = pCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            ISection gCommand = rootConfig.Object["gcommands"];
            Common.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());

            commandPath = Common.CqApi.AppDirectory + "setting.ini";
            IniConfig settingConfig = null;
            if (!File.Exists(commandPath))
            {
                settingConfig = new IniConfig(commandPath);
                settingConfig.Object["setting"]["master"] = 415206409;

                settingConfig.Save();
            };
            settingConfig.Load();

            ISection settings = settingConfig.Object["setting"];
            Common.settingDic = settings.ToDictionary(p => p.Key, p => p.Value.ToString());

            e.CQApi.SendPrivateMessage(415206409, "[测试-应用启动]", sender, e);
        }
    }
}
