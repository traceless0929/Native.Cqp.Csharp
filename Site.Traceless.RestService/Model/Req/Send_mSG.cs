using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.RestService.Model.Req
{
    [DataContract]
    public class Send_Msg
    {
        [DataMember]
        public long target { get; set; }
        [DataMember]
        public string msg { get; set; }
    }
}
