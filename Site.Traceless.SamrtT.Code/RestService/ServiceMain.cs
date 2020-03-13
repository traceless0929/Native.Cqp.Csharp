using Native.Sdk.Cqp;
using Site.Traceless.RestService.Service;
using Site.Traceless.SmartT.Code;
using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Site.Traceless.RestService
{
    public class ServiceMain
    {
        public static ServiceHost Start(CQApi CqApi, CQLog CQLog,string ip,int port,string sKey)
        {
            try
            {
                Uri baseAddress = new Uri($"http://{ip}:{port}/");
                WebServiceHost _serviceHost = new WebServiceHost(typeof(MainService), baseAddress);
                //如果不设置MaxBufferSize,当传输的数据特别大的时候，很容易出现“提示:413 Request Entity Too Large”错误信息,最大设置为20M
                WebHttpBinding binding = new WebHttpBinding
                {
                    TransferMode = TransferMode.Buffered,
                    MaxBufferSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647,
                    MaxBufferPoolSize = 2147483647,
                    ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max,
                    Security = { Mode = WebHttpSecurityMode.None }
                };
                Assembly ass = Assembly.GetExecutingAssembly();
                Type[] types = ass.GetTypes();
                //反射扫描指定命名空间下的所有接口并注册
                foreach(Type item in types.Where(p=>p.IsInterface&&p.Namespace=="Site.Traceless.RestService.Interface")){
                    _serviceHost.AddServiceEndpoint(item, binding, baseAddress+item.Name+"/"+sKey+"/");
                    CQLog.Info("初始化", $"服务{item.Name}完成");
                }
                // 把自定义的IEndPointBehavior插入到终结点中
                foreach (var endpont in _serviceHost.Description.Endpoints)
                {
                    endpont.EndpointBehaviors.Add(new MessageBehavior());
                }
                _serviceHost.Opened += delegate
                {
                    Common.token = sKey;
                    CQLog.Info("初始化", "Web服务已开启...");
                };
                _serviceHost.Open();
                
                return _serviceHost;
            }
            catch (Exception ex)
            {
                CQLog.Error("初始化", $"Web服务开启失败：{ex.Message}\r\n{ex.StackTrace}");
                return null;
            }
        }
    }
}