using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.Nativedemo.Code.Model;
using Site.Traceless.Nativedemo.Code.Command;

namespace Site.Traceless.Nativedemo.Code.Event
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
