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
    [ServiceContract(Name = "InfoService")]
    public interface IInfo
    {

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "get_csrf_token", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<int> GetCsrfToken(BaseReq<object> req);
        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "get_friend_list", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<List<FriendInfo>> GetFriendList(BaseReq<object> req);
        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "get_group_list", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<List<GroupInfo>> GetGroupList(BaseReq<object> req);
        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "get_login_nick", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<string> GetLoginNick(BaseReq<object> req);
        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "get_login_qqid", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<long> GetLoginQQId(BaseReq<object> req);
    }
}
