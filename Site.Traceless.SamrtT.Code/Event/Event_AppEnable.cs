using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Tool.IniConfig.Linq;
using Site.Traceless.SamrtT.Code.Func;
using Site.Traceless.SamrtT.Code.Model.SmartT;
using System.IO;
using System.Linq;
using System.Text;

namespace Site.Traceless.SamrtT.Code.Event
{
    public class Event_AppEnable : IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            Common.CqApi = e.CQApi;
            string commandPath = e.CQApi.AppDirectory + "command.ini";
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
                        { "垃圾","trashsort"}
                    },
                    new IniSection("pcommands")
                    {
                        { "功能","menu"},
                        { "查日常","dayTask"},
                        { "建议","advise"},
                        { "反馈","feedback"},
                        { "个人反馈","pfeedback"},
                        { "垃圾","trashsort"}
                    }
                };
                iObject.Save(commandPath);
            };
            iObject = IniObject.Load(commandPath, Encoding.Default);
            IniSection pCommand = iObject["pcommands"];
            Common.PCommandDic = pCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            IniSection gCommand = iObject["gcommands"];
            Common.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());

            e.CQLog.Info("初始化", "读取指令正常");
            commandPath = Common.CqApi.AppDirectory + "setting.ini";
            if (!File.Exists(commandPath))
            {
                iObject = new IniObject
                {
                    new IniSection("setting")
                    {
                        { "master",415206409},
                        { "taskAddr",@"https://nico.nicemoe.cn/dailylist.php"}
                    }
                };
                iObject.Save(commandPath);
            };
            iObject = IniObject.Load(commandPath, Encoding.Default);
            IniSection settings = iObject["setting"];
            Common.settingDic = settings.ToDictionary(p => p.Key, p => p.Value.ToString());
            e.CQLog.Info("初始化", "读取设置正常");
            string trashSortPath = Common.CqApi.AppDirectory + "trashSort.ini";
            if (!File.Exists(trashSortPath))
            {
                iObject = new IniObject
                {
                    new IniSection("sortData")
                    {
                    }
                };
                iObject.Save(trashSortPath);
            }
            iObject = IniObject.Load(trashSortPath, Encoding.Default);
            IniSection sortData = iObject["sortData"];
            var temp = sortData.ToDictionary(p => p.Key, p => p.Value.ToString());
            foreach (var item in temp)
            {
                TrashSortResp sortItem = Newtonsoft.Json.JsonConvert.DeserializeObject<TrashSortResp>(item.Value);
                Common.TrashDic.Add(sortItem.name, sortItem);
            }
            e.CQLog.Info("初始化", "读取垃圾分类正常");
            Common.SerList = JxServer.GetSerList();
            e.CQLog.Info("初始化", "读取服务器列表正常");
            Common.JxServer = new JxServer();
            e.CQLog.Info("初始化", "初始化开服监控正常");
            Common.menuStr = Utils.MenuUitls.getMenuStr();
            e.CQLog.Info("初始化", "初始化菜单正常");
        }
    }
}