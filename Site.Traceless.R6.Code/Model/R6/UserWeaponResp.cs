using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.R6.Code.Model.R6
{

    public class UserWeaponResp
    {
        public string username { get; set; }
        public string platform { get; set; }
        public string ubisoft_id { get; set; }
        public string uplay_id { get; set; }
        public string avatar_url_146 { get; set; }
        public string avatar_url_256 { get; set; }
        public DateTime last_updated { get; set; }
        public Category[] categories { get; set; }
        public Weapon[] weapons { get; set; }
    }

    /// <summary>
    /// 分类统计
    /// </summary>
    public class Category
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
        /// KD
        /// </summary>
        public float kd { get; set; }
        /// <summary>
        /// 爆头
        /// </summary>
        public long headshots { get; set; }
        /// <summary>
        /// 爆头率
        /// </summary>
        public float headshot_percentage { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public long times_chosen { get; set; }
        public long bullets_fired { get; set; }
        public long bullets_hit { get; set; }
        public DateTime created { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime last_updated { get; set; }
        public Category1 category { get; set; }
    }

    /// <summary>
    /// 大类
    /// </summary>
    public class Category1
    {
        public string name { get; set; }
        public string longernal_name { get; set; }
    }

    /// <summary>
    /// 武器统计
    /// </summary>
    public class Weapon
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
        /// KD
        /// </summary>
        public float kd { get; set; }
        /// <summary>
        /// 爆头
        /// </summary>
        public long headshots { get; set; }
        /// <summary>
        /// 爆头率
        /// </summary>
        public float headshot_percentage { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public long times_chosen { get; set; }
        public long bullets_fired { get; set; }
        public long bullets_hit { get; set; }
        public DateTime created { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime last_updated { get; set; }
        /// <summary>
        /// 武器信息
        /// </summary>
        public Weapon1 weapon { get; set; }
    }

    /// <summary>
    /// 武器信息
    /// </summary>
    public class Weapon1
    {
        /// <summary>
        /// 武器名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 内部名
        /// </summary>
        public string longernal_name { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public Category2 category { get; set; }
    }

    public class Category2
    {
        public string name { get; set; }
        public string key { get; set; }
    }

}
