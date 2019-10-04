using Native.Csharp.Sdk.Cqp;
using Native.Csharp.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Native.Csharp.App.Model;
using Native.Csharp.App.Interface;
using Native.Csharp.App.Command;
using Native.Csharp.App.EventArgs;

namespace Native.Csharp.App.Event
{
    public class Event_GroupMessage : IReceiveGroupMessage
    {
        public void ReceiveGroupMessage(object sender, CqGroupMessageEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息
            //         if (e.FromAnonymous != null)    // 如果此属性不为null, 则消息来自于匿名成员
            //{
            //	e.Handled = true;
            //             return;     // 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
            //         }

            AnalysisMsg nowModel = new AnalysisMsg(e.Message);
            if (String.IsNullOrEmpty(nowModel.GCommand))
            {
                if (e.FromQQ == Common.getSetting<long>("master") && nowModel.What == "command")
                {
                    try
                    {
                        var adminApp = Activator.CreateInstance(typeof(AdminApp)) as AdminApp;
                        var adminMethod = adminApp.GetType().GetMethod(nowModel.Who).MakeGenericMethod(e.GetType());
                        object adminResult = adminMethod.Invoke(null, new object[] { e, nowModel });
                    }
                    catch (Exception ex)
                    {
                        Common.CqApi.SendPrivateMessage(Common.getSetting<long>("master"), ex + "");
                    }

                }
                e.Handler = false;
                return;     // 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
            }
            var gapp = Activator.CreateInstance(typeof(GroupApp)) as GroupApp;
            var method = gapp.GetType().GetMethod(nowModel.GCommand);
            object result = method.Invoke(null, new object[] { e, nowModel });

            e.Handler = false;   // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }
    }
}
