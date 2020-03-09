using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Site.Traceless.SamrtT.Code.Command;
using Site.Traceless.SamrtT.Code.Model;
using System;
using System.Linq;

namespace Site.Traceless.SamrtT.Code.Event
{
    public class Event_PrivateMsg : IPrivateMessage
    {
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            try
            {
                AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
                if ((e.FromQQ == 1016302195 || e.FromQQ == 415206409) && e.Message.CQCodes.Count() > 0)
                {
                    FriendApp.changeQr(e, nowModel);
                }
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
            catch (Exception ex)
            {
                e.CQApi.SendPrivateMessage(Convert.ToInt64(Common.settingDic["master"]), "[发生错误]" + ex.ToString());
            }
        }
    }
}