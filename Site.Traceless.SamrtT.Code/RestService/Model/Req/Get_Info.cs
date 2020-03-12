using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.RestService.Model.Req
{
    [DataContract]
    public class Get_Info
    {
        [DataMember]
        public long targetFst { get; set; }
        [DataMember]
        public long targetSec { get; set; }
        [DataMember]
        public bool isCache { get; set; } = false;
    }
}
