using Site.Traceless.RestService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Site.Traceless.RestService.Model;
using Site.Traceless.RestService.Model.Req;

namespace Site.Traceless.RestService.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MainService : IGroup,IFriend
    {
        public BaseResp<int> sendGroupMsg(BaseReq<Send_Msg> req)
        {
            Send_Msg p = req.data;
            return BaseResp<int>.respSuc(Common.CqApi.SendGroupMessage(p.target, p.msg));
        }

        public BaseResp<int> sendPrivateMsg(BaseReq<Send_Msg> req)
        {
            Send_Msg p = req.data;
            return BaseResp<int>.respSuc(Common.CqApi.SendPrivateMessage(p.target, p.msg));
        }
    }
}
