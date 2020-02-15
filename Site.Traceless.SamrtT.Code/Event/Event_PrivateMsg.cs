using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.SamrtT.Code.Model;
using Site.Traceless.SamrtT.Code.Command;

namespace Site.Traceless.SamrtT.Code.Event
{
    public class Event_PrivateMsg : IPrivateMessage
    {
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
            if (!String.IsNullOrEmpty(nowModel.MPCommand) && e.FromQQ == long.Parse(Common.settingDic["master"]))
            {
                var mpapp = Activator.CreateInstance(typeof(MFriendApp)) as MFriendApp;
                //有管理命令，且是主人发的
                var mmethod = mpapp.GetType().GetMethod(nowModel.MPCommand);
                object mresult = mmethod.Invoke(null, new object[] { e, nowModel });
                e.Handler = false;
                return;     // 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
            }

            if (!String.IsNullOrEmpty(nowModel.PCommand))
            {
                var papp = Activator.CreateInstance(typeof(FriendApp)) as FriendApp;
                var method = papp.GetType().GetMethod(nowModel.PCommand);
                object result = method.Invoke(null, new object[] { e, nowModel });
            }
            else
            {
                //私聊命令为空
                FriendApp.changeCode(e, nowModel);
            }
            e.Handler = false;
        }

    }
}
