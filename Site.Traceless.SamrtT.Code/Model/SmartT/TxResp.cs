using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.SamrtT.Code.Model.SmartT
{
    public class TxResp<T> where T : class
    {
        public int code { get; set; }
        public string msg { get; set; }
        public List<T> newslist { get; set; }
    }
}
