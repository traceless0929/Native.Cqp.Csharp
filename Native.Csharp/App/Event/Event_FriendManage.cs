using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Csharp.App.Event
{
    public class ReceiveFriendIncrease : IReceiveFriendIncrease
    {
        void IReceiveFriendIncrease.ReceiveFriendIncrease(object sender, CqFriendIncreaseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class ReceiveFriendAddRequest : IReceiveFriendAddRequest
    {
        void IReceiveFriendAddRequest.ReceiveFriendAddRequest(object sender, CqAddFriendRequestEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
