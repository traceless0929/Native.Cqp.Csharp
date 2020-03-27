using Native.Sdk.Cqp;

namespace Site.Traceless.Demo.DB
{
    public static class Common
    {
        public static CQApi CqApi { get; set; }
        public static CQLog CqLog { get; set; }
        public static string DbPath { get; set; }
        public static SQLiteHelper sqliteHelper { get; set; }
    }
}
