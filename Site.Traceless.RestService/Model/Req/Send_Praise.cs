using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.RestService.Model.Req
{
    [DataContract]
    public class Send_Praise
    {
        [DataMember]
        public long target { get; set; }
        [DataMember]
        public int count { get; set; } = 1;
    }
}
