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
    [ServiceContract(Name = "FriendService")]
    public interface IFriend
    {
        // 说明：GET请求
        // WebGet默认请求是GET方式
        // UriTemplate(URL Routing)的参数名name必须要方法的参数名必须一致（不区分大小写）
        // RequestFormat规定客户端必须是什么数据格式请求的（JSon或者XML），不设置默认为XML
        // ResponseFormat规定服务端返回给客户端是以是什么数据格返回的（JSon或者XML）
        //[OperationContract]
        //[WebGet(UriTemplate = "Test/{content}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //void sendToMasterGroup(string content);

        // 说明：POS请求
        // WebInvoke请求方式有POST、PUT、DELETE等，所以需要明确指定Method是哪种请求的，这里我们设置POST请求。
        // 注意：POST情况下，UriTemplate(URL Routing)一般是没有参数（和上面GET的UriTemplate不一样，因为POST参数都通过消息体传送）
        // RequestFormat规定客户端必须是什么数据格式请求的（JSon或者XML），不设置默认为XML
        // ResponseFormat规定服务端返回给客户端是以是什么数据格返回的（JSon或者XML）

        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "send_private_msg", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<int> sendPrivateMsg(BaseReq<Send_Msg> content);
    }
}
