using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.RestService.Model.Req
{
    [DataContract]
    public class Set_Info
    {
        [DataMember]
        public long targetFst { get; set; }
        [DataMember]
        public long targetSec { get; set; }
        [DataMember]
        public string para { get; set; }
        [DataMember]
        public long tSapnMs { get; set; }
    }
}
