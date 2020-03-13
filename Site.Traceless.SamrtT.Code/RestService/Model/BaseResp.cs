using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.RestService.Model
{
    [DataContract]
    public class BaseResp<T>
    {
        [DataMember]
        public long ts { get; set; } = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        [DataMember]
        public T data { get; set; }
        [DataMember]
        public int code { get; set; } = 1;
        [DataMember]
        public string msg { get; set; } = "";

        public BaseResp(T data) {
            this.data = data;
        }
        public static BaseResp<T> respSuc(T data)
        {
            return new BaseResp<T>(data);
        }

        public static BaseResp<T> respFail(string msg)
        {
            BaseResp<T> baseResp = new BaseResp<T>(default(T));
            baseResp.code = 0;
            baseResp.msg = msg;
            return baseResp;
        }
    }
}
