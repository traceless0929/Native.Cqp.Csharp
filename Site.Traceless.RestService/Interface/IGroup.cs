using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using Site.Traceless.RestService.Model;
using Site.Traceless.RestService.Model.Req;

namespace Site.Traceless.RestService.Interface
{
    [ServiceContract(Name = "GroupService")]
    public interface IGroup
    {
        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "send_group_msg", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<int> sendGroupMsg(BaseReq<Send_Msg> content);
    }
}
