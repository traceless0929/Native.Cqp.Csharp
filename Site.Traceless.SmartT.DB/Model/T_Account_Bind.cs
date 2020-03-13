using Site.Traceless.Tools.Utils;

namespace Site.Traceless.SmartT.DB.Model
{
    public class T_Account_Bind
    {
        public int Id { get; set; }

        public long QqId { get; set; }

        public string WeChatId { get; set; }

        public bool Suc { get; set; } = false;

        public string CheckCode { get; set; } = RandomUtil.RandomGet(0, 99999).ToString().PadLeft(5, '0');
    }
}