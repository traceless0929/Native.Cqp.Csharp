using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.EventArgs;

namespace Site.Traceless.SamrtT.Code.Event
{
    public class Event_GroupBanSpeak : IGroupBanSpeak
    {
        public void GroupBanSpeak(object sender, CQGroupBanSpeakEventArgs e)
        {
            e.CQApi.SendPrivateMessage(415206409, "[测试-群禁言]", sender, e);
        }
    }
}
