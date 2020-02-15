using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class Event_GroupUpload : IGroupUpload
    {
        public void GroupUpload(object sender, CQGroupUploadEventArgs e)
        {
            e.CQApi.SendPrivateMessage(415206409, "[测试-文件上传]", sender, e);
        }
    }
}
