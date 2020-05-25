using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.R6.Code.Model.R6
{
    public class UserBaseInfoResp
    {

        /// <summary>
        /// id
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 用户的UplayId
        /// </summary>
        public string uplay_id { get; set; }
        /// <summary>
        /// 用户的育碧id(目前是一样的)
        /// </summary>
        public string ubisoft_id { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 平台pc/ps4/xbox
        /// </summary>
        public string platform { get; set; }
        /// <summary>
        /// ？？？大概是是不是被封号了？
        /// </summary>
        public bool avatar_banned { get; set; }
        /// <summary>
        /// 等级信息
        /// </summary>
        public Progressionstats progressionStats { get; set; }
        /// <summary>
        /// 对局数据概况
        /// </summary>
        public Genericstats genericStats { get; set; }

        public class Progressionstats
        {
            /// <summary>
            /// 等级
            /// </summary>
            public long level { get; set; }
        }

        public class Genericstats
        {
            /// <summary>
            /// 击杀
            /// </summary>
            public long kills { get; set; }
            /// <summary>
            /// 死亡
            /// </summary>
            public long deaths { get; set; }
            /// <summary>
            /// 胜场
            /// </summary>
            public long wins { get; set; }
            /// <summary>
            /// 负场
            /// </summary>
            public long losses { get; set; }

        }

    }
}
