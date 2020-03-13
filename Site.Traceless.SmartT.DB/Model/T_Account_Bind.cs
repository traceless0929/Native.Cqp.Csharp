using Site.Traceless.Tools.Utils;
using SQLite;

namespace Site.Traceless.SmartT.DB.Model
{
    public class T_Account_Bind
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed("idx_qqid",1)] 
        public long QqId { get; set; }
        [Indexed("idx_wechatId",2)] 
        public string WeChatId { get; set; }

        public bool Suc { get; set; } = false;

        public string CheckCode { get; set; } = RandomUtil.RandomGet(0, 99999).ToString().PadLeft(5, '0');
    }
}