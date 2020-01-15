using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.R6.Code.Model.R6
{
    public class UserSeasonResp
    {
        public string username { get; set; }
        public string platform { get; set; }
        public string ubisoft_id { get; set; }
        public string uplay_id { get; set; }
        public DateTime last_updated { get; set; }
        public Seasons seasons { get; set; }
    }

    public class Seasons
    {
        public SeasonItem shifting_tides { get; set; }
        public SeasonItem ember_rise { get; set; }
        public SeasonItem phantom_sight { get; set; }
        public SeasonItem burnt_horizon { get; set; }
        public SeasonItem wind_bastion { get; set; }
        public SeasonItem grim_sky { get; set; }
        public SeasonItem para_bellum { get; set; }
        public SeasonItem chimera { get; set; }
        public SeasonItem white_noise { get; set; }
        public SeasonItem blood_orchid { get; set; }
        public SeasonItem health { get; set; }
        public List<SeasonItem> Getinfos()
        {
            return new List<SeasonItem>() { ember_rise,shifting_tides,phantom_sight, burnt_horizon, wind_bastion, grim_sky, para_bellum, chimera, white_noise, blood_orchid, health };
        }
    }

    public class SeasonItem
    {
        /// <summary>
        /// 赛季ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 赛季名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 赛季键值（和赛季名一样）
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime start_date { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public object end_date { get; set; }
        /// <summary>
        /// 排名（只有当前赛季有数据）
        /// </summary>
        public Rankings rankings { get; set; }
        /// <summary>
        /// 排位具体数据
        /// </summary>
        public Regions regions { get; set; }
    }

    public class Rankings
    {
        /// <summary>
        /// 美服
        /// </summary>
        public int? ncsa { get; set; }
        /// <summary>
        /// 欧服
        /// </summary>
        public int? emea { get; set; }
        /// <summary>
        /// 亚服
        /// </summary>
        public int? apac { get; set; }
        /// <summary>
        /// 全球
        /// </summary>
        public int? global { get; set; }
    }

    public class Regions
    {
        public RegionsItem[] ncsa { get; set; }
        public RegionsItem[] emea { get; set; }
        public RegionsItem[] apac { get; set; }

        public RegionsItem getBest()
        {
            RegionsItem nc = ncsa.FirstOrDefault();
            RegionsItem em = emea.FirstOrDefault();
            RegionsItem ac = apac.FirstOrDefault();
            List<RegionsItem> list = new List<RegionsItem>()
            {
                nc,em,ac
            };
            float max = list.Max(c => c.max_mmr);
            return list.FirstOrDefault(p => p.max_mmr == max);
        }
    }

    public class RegionsItem
    {
        /// <summary>
        /// 排位ID（没啥用）
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 赛季ID
        /// </summary>
        public int season_id { get; set; }
        /// <summary>
        /// 所属地区
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// 弃赛
        /// </summary>
        public int abandons { get; set; }
        /// <summary>
        /// 负场
        /// </summary>
        public int losses { get; set; }
        /// <summary>
        /// 最高分数
        /// </summary>
        public float max_mmr { get; set; }
        /// <summary>
        /// 最高段位
        /// </summary>
        public int max_rank { get; set; }
        /// <summary>
        /// 当前分值
        /// </summary>
        public float mmr { get; set; }
        /// <summary>
        /// 下一级分值
        /// </summary>
        public float next_rank_mmr { get; set; }
        /// <summary>
        /// 上一级分值
        /// </summary>
        public float prev_rank_mmr { get; set; }
        /// <summary>
        /// 当前段位
        /// </summary>
        public int rank { get; set; }
        /// <summary>
        /// 能力评分
        /// </summary>
        public float skill_mean { get; set; }
        /// <summary>
        /// 能力修正
        /// </summary>
        public float skill_standard_deviation { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime created_for_date { get; set; }
        /// <summary>
        /// 胜场
        /// </summary>
        public int wins { get; set; }
    }
}
