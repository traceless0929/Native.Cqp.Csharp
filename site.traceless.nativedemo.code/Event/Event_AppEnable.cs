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
using Site.Traceless.Common.Model;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class Event_AppEnable : IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            CommonData.CqApi = e.CQApi;
            CommonData.CqLog = e.CQLog;
            PluginStore.InitPlugIn();
            string commandPath = commandPath = CommonData.CqApi.AppDirectory + "setting.ini";
            IniConfig settingConfig = null;
            if (!File.Exists(commandPath))
            {
                settingConfig = new IniConfig(commandPath);
                settingConfig.Object["setting"]["master"] = 415206409;

                settingConfig.Save();
            }
            else
            {
                settingConfig = new IniConfig(commandPath);
                settingConfig.Load();
            }

            ISection settings = settingConfig.Object["setting"];
            CommonData.settingDic = settings.ToDictionary(p => p.Key, p => p.Value.ToString());

            e.CQApi.SendPrivateMessage(415206409, "[测试-应用启动]", sender, e);
        }
    }
}