﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace Site.Traceless.Demo.Code.Event
{
    public class Event_DiscussMsg : IDiscussMessage
    {
        public void DiscussMessage(object sender, CQDiscussMessageEventArgs e)
        {
            
        }
    }
}