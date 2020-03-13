using Native.Sdk.Cqp;
using System.Collections.Generic;

namespace Site.Traceless.SmartT.DB
{
    public static class Common
    {
        public static CQApi CqApi { get; set; }
        public static CQLog CqLog { get; set; }
        public static string ConnectStr { get; set; }
    }
}