using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Site.Traceless.RestService.Interface
{
    [ServiceContract(Name = "TestService")]
    public interface ITest
    {
        /// <summary>
        /// 说明：GET请求
        /// WebGet默认请求是GET方式
        /// UriTemplate(URL Routing)的参数名name必须要方法的参数名必须一致（不区分大小写）
        /// RequestFormat规定客户端必须是什么数据格式请求的（JSon或者XML），不设置默认为XML
        /// ResponseFormat规定服务端返回给客户端是以是什么数据格返回的（JSon或者XML）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "Test/{content}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void sendToMaster(string content);
    }
}
