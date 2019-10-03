using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Csharp.App.Event
{
    public class ReceiveGroupManageDecrease : IReceiveGroupManageDecrease
    {
        void IReceiveGroupManageDecrease.ReceiveGroupManageDecrease(object sender, CqGroupManageChangeEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class ReceiveGroupManageIncrease : IReceiveGroupManageIncrease
    {
        void IReceiveGroupManageIncrease.ReceiveGroupManageIncrease(object sender, CqGroupManageChangeEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class ReceiveGroupMemberLeave : IReceiveGroupMemberLeave
    {
        void IReceiveGroupMemberLeave.ReceiveGroupMemberLeave(object sender, CqGroupMemberDecreaseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class ReceiveGroupMemberRemove : IReceiveGroupMemberRemove
    {
        void IReceiveGroupMemberRemove.ReceiveGroupMemberRemove(object sender, CqGroupMemberDecreaseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class ReceiveGroupMemberPass : IReceiveGroupMemberPass
    {
        void IReceiveGroupMemberPass.ReceiveGroupMemberPass(object sender, CqGroupMemberIncreaseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class ReceiveAddGroupBeInvitee : IReceiveAddGroupBeInvitee
    {
        void IReceiveAddGroupBeInvitee.ReceiveAddGroupBeInvitee(object sender, CqAddGroupRequestEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class ReceiveAddGroupRequest : IReceiveAddGroupRequest
    {
        void IReceiveAddGroupRequest.ReceiveAddGroupRequest(object sender, CqAddGroupRequestEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
