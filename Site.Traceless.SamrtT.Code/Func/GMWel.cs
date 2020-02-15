using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.Gmanger.Datas;
using Site.Traceless.Gmanger.Enum;
using Site.Traceless.SamrtT.Code.Utils;

namespace Site.Traceless.SamrtT.Code.Func
{
    public class GMWel
    {
        public static void goWel(GroupData groupData,CQGroupMemberIncreaseEventArgs args)
        {
           string template = groupData.GetTemplate(SwitchEnum.welopen);
           template = template.ReplaceTrimAndLine().ReplaceAtQQ()
               .ReplaceGroupMemberInfo(args.FromGroup, args.FromQQ, args.BeingOperateQQ);
           args.CQApi.SendGroupMessage(args.FromGroup, template);
        }
    }
}
