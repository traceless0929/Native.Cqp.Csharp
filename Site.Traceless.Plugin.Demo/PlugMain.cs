using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.Common.Model;
using Site.Traceless.Common.Model.Base;
using Site.Traceless.Plugin.Base;
using System;
using System.Collections.Generic;

namespace Site.Traceless.Plugin.Demo
{
    public class PlugMain : IBasePlugin
    {
        public string GCommand { get; set; } = "demog";
        public string PCommand { get; set; } = "demop";
        public string Name { get; set; } = "Demo";
        public string Author { get; set; } = "Traceless";

        public bool DoGroup(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(Convert.ToInt64(CommonData.settingDic["master"]), $"{e.ToString()}", Newtonsoft.Json.JsonConvert.SerializeObject(msg));
            return false;
        }

        public bool DoPrivate(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            PCommand = "demop" + new Random().Next();
            e.CQApi.SendPrivateMessage(Convert.ToInt64(CommonData.settingDic["master"]), $"{e.ToString()}", Newtonsoft.Json.JsonConvert.SerializeObject(msg), PCommand);
            return false;
        }
    }
}