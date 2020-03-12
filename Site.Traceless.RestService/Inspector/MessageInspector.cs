using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.RestService.Inspector
{
    public class MessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Common.CQLog.Debug("http api req",request.ToString());
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
             
        }
    }
}
