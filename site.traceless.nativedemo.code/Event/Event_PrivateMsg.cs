﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.Nativedemo.Code.Model;
using Site.Traceless.Nativedemo.Code.Command;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class Event_PrivateMsg : IPrivateMessage
    {
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
            if (String.IsNullOrEmpty(nowModel.PCommand))
            {
                e.Handler = false;
                return;
            }
            var papp = Activator.CreateInstance(typeof(FriendApp)) as FriendApp;
            var method = papp.GetType().GetMethod(nowModel.PCommand);
            object result = method.Invoke(null, new object[] { e, nowModel });

            e.Handler = false;
        }
    }
}
