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
    [ServiceContract(Name = "GroupService")]
    public interface IGroup
    {
        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "send_group_msg", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<int> SendGroupMsg(BaseReq<Send_Msg> content);

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "get_group_info", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<GroupInfo> GetGroupInfo(BaseReq<Get_Info> req);

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "get_group_member_info", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<GroupMemberInfo> GetGroupMemberInfo(BaseReq<Get_Info> req);

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "get_group_member_list", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<List<GroupMemberInfo>> GetGroupMemberList(BaseReq<Get_Info> req);

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "set_group_banspeak", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<bool> SetGroupBanSpeak(BaseReq<Set_Info> req);

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "set_group_member_banspeak", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<bool> SetGroupMemberBanSpeak(BaseReq<Set_Info> req);

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "set_group_member_vcard,", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<bool> SetGroupMemberVisitingCard(BaseReq<Set_Info> req);
    }
}
