using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class Event_CQExit : ICQExit
    {
        public void CQExit(object sender, CQExitEventArgs e)
        {
            e.CQApi.SendPrivateMessage(415206409, "[测试-酷Q退出]", sender, e);
        }

    }
}
