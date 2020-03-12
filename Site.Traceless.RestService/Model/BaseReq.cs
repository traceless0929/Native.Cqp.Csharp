using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.RestService.Model
{
    [DataContract]
    public class BaseReq<T>
    {
        [DataMember]
        public string source { get; set; }
        [DataMember]
        public string token { get; set; }
        [DataMember]
        public T data { get; set; }
    }
}
