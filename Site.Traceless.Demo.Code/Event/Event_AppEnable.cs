using System.Linq;
using System.Text;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;
using System.IO;
using Native.Tool.IniConfig.Linq;
using Site.Traceless.Demo.DB;
using Native.Tool.IniConfig;

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
            IniConfig rootIni = new IniConfig (commandPath);
            IObject iObject;
            if (!File.Exists(commandPath))
            {
                rootIni.Object["gcommands"]["功能1"] = "funcOne";
                rootIni.Object["gcommands"]["功能2"] = "funcTwo";
                rootIni.Object["pcommands"]["功能1"] = "funcOne";
                rootIni.Object["pcommands"]["功能2"] = "funcTwo";
                rootIni.Object["gcommands"]["公告"] = "getGNotice";
                rootIni.Save();
            };
            ISection pCommand = rootIni.Object["pcommands"];
            Common.PCommandDic = pCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            ISection gCommand = rootIni.Object["gcommands"];
            Common.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());

            commandPath = Common.CqApi.AppDirectory + "setting.ini";
            IniConfig configIni = new IniConfig (commandPath);
            if (!File.Exists(commandPath))
            {
                configIni.Object["setting"]["master"] = 415206409;
                configIni.Save();
            };

            ISection settings = configIni.Object["setting"];
            Common.settingDic = settings.ToDictionary(p => p.Key, p => p.Value.ToString());

            e.CQApi.SendPrivateMessage(415206409, "[测试-应用启动]", sender, e);
        }
    }
}
