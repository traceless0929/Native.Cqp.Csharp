using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.Common.Model.Base;
using Site.Traceless.Nativedemo.Code.Command;
using Site.Traceless.Plugin.Base;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class Event_PrivateMsg : IPrivateMessage
    {
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
            IBasePlugin basePlugin = PluginStore.GetPluginPcmd(nowModel.What);
            if (basePlugin != null)
            {
                basePlugin.DoPrivate(e, nowModel);
            }
            e.Handler = false;
        }
    }
}