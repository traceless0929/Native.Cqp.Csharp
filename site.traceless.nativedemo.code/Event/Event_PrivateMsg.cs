using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.EventArgs;
using Site.Traceless.SamrtT.Code.Model;
using Site.Traceless.SamrtT.Code.Command;

namespace Site.Traceless.SamrtT.Code.Event
{
    public class Event_PrivateMsg : IPrivateMessage
    {
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
            if (String.IsNullOrEmpty(nowModel.PCommand))
            {
                e.Handler = false;
                return;     // 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
            }
            var papp = Activator.CreateInstance(typeof(FriendApp)) as FriendApp;
            var method = papp.GetType().GetMethod(nowModel.PCommand);
            object result = method.Invoke(null, new object[] { e, nowModel });

            e.Handler = false;
        }
    }
}
