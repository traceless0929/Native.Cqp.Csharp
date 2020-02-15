using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class Event_GroupManageChange : IGroupManageChange
    {
        public void GroupManageChange(object sender, CQGroupManageChangeEventArgs e)
        {
            e.CQApi.SendPrivateMessage(415206409, "[测试-群管理变动]", sender, e);
        }
    }
}
