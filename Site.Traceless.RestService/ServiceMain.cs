using Native.Sdk.Cqp;
using Site.Traceless.RestService.Interface;
using Site.Traceless.RestService.Service;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Site.Traceless.RestService
{
    public class ServiceMain
    {
        public static ServiceHost Start(CQApi CqApi, CQLog CQLog)
        {
            try
            {
                Uri baseAddress = new Uri("http://127.0.0.1:7789/");
                ServiceHost _serviceHost = new WebServiceHost(typeof(TestService), baseAddress);
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
                _serviceHost.AddServiceEndpoint(typeof(ITest), binding, baseAddress);
                _serviceHost.Opened += delegate
                {
                    Common.CqApi = CqApi;
                    Common.CQLog = CQLog;
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