using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Csharp.App.Event
{
    public class Event_DiscussMessage : IReceiveDiscussMessage
    {
        public void ReceiveDiscussMessage(object sender, CqDiscussMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }


}
