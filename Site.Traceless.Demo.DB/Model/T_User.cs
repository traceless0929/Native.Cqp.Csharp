using SQLite;

namespace Site.Traceless.Demo.DB.Model
{
    public class T_User
    {
        /// <summary>
        /// 自增主键，用作用户ID
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nick { get; set; } = "我是默认昵称";
        /// <summary>
        /// 性别 0未知 1男 2女
        /// </summary>
        public int Gender { get; set; } = 0;
    }
}
