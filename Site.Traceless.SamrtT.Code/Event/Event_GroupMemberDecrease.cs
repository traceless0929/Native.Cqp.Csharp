﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.EventArgs;

namespace Site.Traceless.SamrtT.Code.Event
{
    public class Event_GroupMemberDecrease : IGroupMemberDecrease
    {

        public void GroupMemberDecrease(object sender, CQGroupMemberDecreaseEventArgs e)
        {
           
        }
    }
}
