using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.EventArgs;
using Newtonsoft.Json;
using Site.Traceless.SamrtT.Code.Model;
using Site.Traceless.SamrtT.Code.Command;
using Site.Traceless.Gmanger;
using Site.Traceless.Gmanger.Datas;
using Site.Traceless.Gmanger.Enum;

namespace Site.Traceless.SamrtT.Code.Event
{
    public class Event_GroupMsg : IGroupMessage
    {
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
            if (string.IsNullOrEmpty(nowModel.GCommand))
            {
                //没有普通功能,检查是否有群管功能
                GroupData groupData = Gmanger.Common.GetGroupData(e.CQApi,e.FromGroup.Id);
                if (null != groupData)
                {
                    string gmCmd = groupData.GetFuncName(nowModel.What);
                    if (!string.IsNullOrEmpty(gmCmd))
                    {
                        //群管命令
                        if (groupData.GetSwitch(SwitchEnum.gmopen)|| gmCmd == "gmAllOpen")
                        {
                            //群管总开关开启，或是开启群管的指令
                            var gmApp = Activator.CreateInstance(typeof(MGroupApp)) as MGroupApp;
                            var gmMethod = gmApp.GetType().GetMethod(gmCmd);
                            object gmResult = gmMethod.Invoke(null, new object[] { e, nowModel, groupData });
                        }
                    }
                }
                e.Handler = false;
                return;     // 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
            }
            var gapp = Activator.CreateInstance(typeof(GroupApp)) as GroupApp;
            var method = gapp.GetType().GetMethod(nowModel.GCommand);
            object result = method.Invoke(null, new object[] { e, nowModel });

            e.Handler = false;

        }

    }
}
