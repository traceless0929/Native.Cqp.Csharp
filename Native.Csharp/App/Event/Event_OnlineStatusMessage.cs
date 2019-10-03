using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Csharp.App.Event
{
    public class Event_OnlineStatusMessage : IReceiveOnlineStatusMessage
    {
        public void ReceiveOnlineStatusMessage(object sender, CqPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
