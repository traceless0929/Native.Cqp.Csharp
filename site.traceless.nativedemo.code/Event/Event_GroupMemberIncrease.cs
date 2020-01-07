using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.EventArgs;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class Event_GroupMemberIncrease : IGroupMemberIncrease
    {

        public void GroupMemberIncrease(object sender, CQGroupMemberIncreaseEventArgs e)
        {
            e.CQApi.SendPrivateMessage(415206409, "[测试-群成员增加]", sender, e);
        }
    }
}
