﻿using System.Linq;
using System.Text;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;
using System.IO;
using Native.Tool.IniConfig.Linq;
using Site.Traceless.Demo.DB;

namespace Site.Traceless.Demo.Code.Event
{
    public class Event_AppEnable : IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            Common.CqApi = e.CQApi;

            DB.Common.CqApi = e.CQApi;
            DB.Common.CqLog = e.CQLog;
            DB.Common.DbPath = e.CQApi.AppDirectory + "yourname.db";//".db前面的文字可以修改为你想要的数据库文件名称，建议使用英文"
            DB.Common.sqliteHelper = new SQLiteHelper(DB.Common.DbPath);

            string commandPath = Common.CqApi.AppDirectory + "command.ini";
            IniObject iObject;
            if (!File.Exists(commandPath))
            {
                iObject = new IniObject
                {
                    new IniSection("gcommands")
                    {
                        { "功能1","funcOne"},
                        { "功能2","funcTwo"}
                    },
                    new IniSection("pcommands")
                    {
                        { "功能1","funcOne"},
                        { "功能2","funcTwo"},
                        { "公告","getGNotice"},
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

            e.CQApi.SendPrivateMessage(415206409, "[测试-应用启动]", sender, e);
        }
    }
}
