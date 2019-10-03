using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Interface;

namespace Native.Csharp.App.Event
{
    public class Event_DiscussPrivateMessage : IReceiveDiscussPrivateMessage
    {
        public void ReceiveDiscussPrivateMessage(object sender, CqPrivateMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
