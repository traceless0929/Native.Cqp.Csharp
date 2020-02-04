using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.EventArgs;
using Site.Traceless.Gmanger.Datas;
using Site.Traceless.Gmanger.Enum;
using Site.Traceless.SamrtT.Code.Command;
using Site.Traceless.SamrtT.Code.Func;

namespace Site.Traceless.SamrtT.Code.Event
{
    public class Event_GroupMemberIncrease : IGroupMemberIncrease
    {

        public void GroupMemberIncrease(object sender, CQGroupMemberIncreaseEventArgs e)
        {
            GroupData groupData = Gmanger.Common.GetGroupData(e.CQApi,e.FromGroup.Id);
            if (null != groupData)
            {
                // 群管命令,群欢迎
                if (groupData.GetSwitch(SwitchEnum.gmopen)&&groupData.GetSwitch(SwitchEnum.welopen))
                {
                    GMWel.goWel(groupData,e);
                }
            }
        }
    }
}
