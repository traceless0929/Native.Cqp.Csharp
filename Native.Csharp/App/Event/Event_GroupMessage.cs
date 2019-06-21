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
        #region --公开方法--
        /// <summary>
        /// Type=2 群消息<para/>
        /// 处理收到的群消息
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMessage (object sender, CqGroupMessageEventArgs e)
		{
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息

            AnalysisMsg nowModel = new AnalysisMsg(e.Message);
            if (String.IsNullOrEmpty(nowModel.GCommand))
            {
                e.Handler = false;
                return;     // 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
            }
            var gapp = Activator.CreateInstance(typeof(GroupApp)) as GroupApp;
            var method = gapp.GetType().GetMethod(nowModel.PCommand);
            object result = method.Invoke(null, new Object[] { e, nowModel });

			e.Handler = false;   // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
            
        }
        #endregion
    }
}
