using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;

namespace Site.Traceless.SmartT.Code.Event
{
    public class Event_GroupMemberDecrease : IGroupMemberDecrease
    {
        public void GroupMemberDecrease(object sender, CQGroupMemberDecreaseEventArgs e)
        {
        }
    }
}