using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using Site.Traceless.RestService.Model;
using Site.Traceless.RestService.Model.Req;
using Native.Sdk.Cqp.Model;

namespace Site.Traceless.RestService.Interface
{
    [ServiceContract(Name = "AuthService")]
    public interface IAuth
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "wechat_bind", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<string> Wechat_Bind(BaseReq<Send_Msg> req);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "wechat_bind_submit", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<string> Wechat_Bind_Submit(BaseReq<Set_Info> req);
    }
}
