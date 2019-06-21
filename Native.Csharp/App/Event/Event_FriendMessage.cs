using Native.Csharp.App.Command;
using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Interface;
using Native.Csharp.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Event
{
    public class Event_FriendMessage : IReceiveFriendMessage
    {
        #region --公开方法--

        #endregion
        public void ReceiveFriendMessage(object sender, CqPrivateMessageEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息

            AnalysisMsg nowModel = new AnalysisMsg(e.Message);
            if (String.IsNullOrEmpty(nowModel.GCommand))
            {
                e.Handler = false;
                return;     // 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
            }
            var gapp = Activator.CreateInstance(typeof(FriendApp)) as FriendApp;
            var method = gapp.GetType().GetMethod(nowModel.PCommand);
            object result = method.Invoke(null, new Object[] { e, nowModel });
            e.Handler = false;
            // e.Handled 相当于 原酷Q事件的返回值
            // 如果要回复消息，请调用api发送，并且置 true - 截断本条消息，不再继续处理 //注意：应用优先级设置为"最高"(10000)时，不得置 true
            // 如果不回复消息，交由之后的应用/过滤器处理，这里置 false  - 忽略本条消息
        }
    }
}
