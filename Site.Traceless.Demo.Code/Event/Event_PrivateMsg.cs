using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Site.Traceless.Demo.Code.Command;
using Site.Traceless.Demo.Code.Model;
using System;

namespace Site.Traceless.Demo.Code.Event
{
    public class Event_PrivateMsg : IPrivateMessage
    {
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
            if (String.IsNullOrEmpty(nowModel.PCommand))
            {
                e.Handler = false;
                return;
            }
            var papp = Activator.CreateInstance(typeof(FriendApp)) as FriendApp;
            var method = papp.GetType().GetMethod(nowModel.PCommand);
            object result = method.Invoke(null, new object[] { e, nowModel });

            e.Handler = false;
        }
    }
}