using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;

namespace Site.Traceless.SmartT.Code.Event
{
    public class Event_FriendAddRequest : IFriendAddRequest
    {
        public void FriendAddRequest(object sender, CQFriendAddRequestEventArgs e)
        {
        }
    }
}