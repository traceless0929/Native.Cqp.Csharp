using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Site.Traceless.Demo.Code.Command;
using Site.Traceless.Demo.Code.Model;
using System;

namespace Site.Traceless.Demo.Code.Event
{
    public class Event_GroupMsg : IGroupMessage
    {
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
            if (String.IsNullOrEmpty(nowModel.GCommand))
            {
                e.Handler = false;
                return;
            }
            var gapp = Activator.CreateInstance(typeof(GroupApp)) as GroupApp;
            var method = gapp.GetType().GetMethod(nowModel.GCommand);
            object result = method.Invoke(null, new object[] { e, nowModel });

            e.Handler = false;
        }
    }
}