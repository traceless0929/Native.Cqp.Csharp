using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Tool.IniConfig.Linq;
using Native.Tool.IniConfig;

namespace Site.Traceless.R6.Code.Event
{
    public class Event_AppEnable : IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            Common.CqApi = e.CQApi;
            string commandPath = Common.CqApi.AppDirectory + "command.ini";
            IniConfig rootIni = null;
            if (!File.Exists(commandPath))
            {
                rootIni = new IniConfig (commandPath);
                rootIni.Object["gcommands"]["R6排位"] = "GetRankInfo";
                rootIni.Object["gcommands"]["R6战绩"] = "GetBattleStastic";

                rootIni.Save();
            }
            else
            {
                rootIni = new IniConfig (commandPath);
                rootIni.Load();
            }
            
            ISection pCommand = rootIni.Object["pcommands"];
            Common.PCommandDic = pCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            ISection gCommand = rootIni.Object["gcommands"];
            Common.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
        }
    }
}
