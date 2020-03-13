using System.Collections.Generic;

namespace Site.Traceless.SmartT.Code.Model.SmartT
{
    public class TxResp<T> where T : class
    {
        public int code { get; set; }
        public string msg { get; set; }
        public List<T> newslist { get; set; }
    }
}