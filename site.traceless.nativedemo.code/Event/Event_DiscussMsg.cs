using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.EventArgs;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class Event_DiscussMsg : IDiscussMessage
    {
        public void DiscussMessage(object sender, CQDiscussMessageEventArgs e)
        {
            e.CQApi.SendPrivateMessage(415206409, "[测试-讨论组]", sender, e);
        }
    }
}
