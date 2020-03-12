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
    [ServiceContract(Name = "HookService")]
    public interface IHook
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "hook_github", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResp<object> GithubHook(object req);
    }
}
