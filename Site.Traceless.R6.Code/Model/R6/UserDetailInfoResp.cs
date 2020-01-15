using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.R6.Code.Model.R6
{
    public class UserDetailInfoResp
    {
        public string username { get; set; }
        public string platform { get; set; }
        public string ubisoft_id { get; set; }
        public string uplay_id { get; set; }
        public bool avatar_banned { get; set; }
        public DateTime last_updated { get; set; }
        /// <summary>
        /// 曾用名(不太准确
        /// </summary>
        public Alias[] aliases { get; set; }
        public object[] profiles { get; set; }
        /// <summary>
        /// 等级进度
        /// </summary>
        public Progression progression { get; set; } = new Progression();
        public Stat[] stats { get; set; }
        public Operator[] operators { get; set; }
    }

    public class Progression
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 战利品概率
        /// </summary>
        public int lootbox_probability { get; set; }
        /// <summary>
        /// 总经验
        /// </summary>
        public int total_xp { get; set; }
    }
    /// <summary>
    /// 曾用名
    /// </summary>
    public class Alias
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime last_seen_at { get; set; }
    }
    /// <summary>
    /// 数据
    /// </summary>
    public class Stat
    {
        /// <summary>
        /// 概况
        /// </summary>
        public General general { get; set; }
        /// <summary>
        /// 排位、休闲数据
        /// </summary>
        public Queue queue { get; set; }
        /// <summary>
        /// 游戏模式数据
        /// </summary>
        public Gamemode gamemode { get; set; }
        /// <summary>
        /// 数据时间
        /// </summary>
        public Timestamps timestamps { get; set; }
    }

    public class General
    {
        /// <summary>
        /// 助攻
        /// </summary>
        public int assists { get; set; }
        /// <summary>
        /// 封阻次数
        /// </summary>
        public int barricades_deployed { get; set; }
        /// <summary>
        /// 致盲击杀
        /// </summary>
        public int blind_kills { get; set; }
        /// <summary>
        /// 子弹射击数
        /// </summary>
        public int bullets_fired { get; set; }
        /// <summary>
        /// 子弹击中数
        /// </summary>
        public int bullets_hit { get; set; }
        /// <summary>
        /// 被击倒但未死亡
        /// </summary>
        public int dbnos { get; set; }
        /// <summary>
        /// 死亡
        /// </summary>
        public int deaths { get; set; }
        public int distance_travelled { get; set; }
        /// <summary>
        /// 装备摧毁数
        /// </summary>
        public int gadgets_destroyed { get; set; }
        /// <summary>
        /// 爆头数
        /// </summary>
        public int headshots { get; set; }
        /// <summary>
        /// 击杀死亡比
        /// </summary>
        public float kd { get; set; }
        /// <summary>
        /// 击杀
        /// </summary>
        public int kills { get; set; }
        /// <summary>
        /// 负场
        /// </summary>
        public int losses { get; set; }
        /// <summary>
        /// 近战击杀
        /// </summary>
        public int melee_kills { get; set; }
        /// <summary>
        /// 穿透击杀
        /// </summary>
        public int penetration_kills { get; set; }
        /// <summary>
        /// 游玩时间（到底是什么单位还没研究出来）
        /// </summary>
        public int playtime { get; set; }
        /// <summary>
        /// 被举报数？
        /// </summary>
        public int rappel_breaches { get; set; }
        /// <summary>
        /// 强化墙面
        /// </summary>
        public int reinforcements_deployed { get; set; }
        /// <summary>
        /// 救助
        /// </summary>
        public int revives { get; set; }
        /// <summary>
        /// 自杀
        /// </summary>
        public int suicides { get; set; }
        /// <summary>
        /// 胜场
        /// </summary>
        public int wins { get; set; }
        /// <summary>
        /// 胜负比
        /// </summary>
        public float wl { get; set; }
    }

    public class Queue
    {
        /// <summary>
        /// 休闲
        /// </summary>
        public QueueItem casual { get; set; }
        /// <summary>
        /// 排位
        /// </summary>
        public QueueItem ranked { get; set; }
    }

    public class QueueItem
    {
        /// <summary>
        /// 死亡
        /// </summary>
        public int deaths { get; set; }
        /// <summary>
        /// 场次
        /// </summary>
        public int games_played { get; set; }
        /// <summary>
        /// 击杀死亡比
        /// </summary>
        public float kd { get; set; }
        /// <summary>
        /// 击杀
        /// </summary>
        public int kills { get; set; }
        /// <summary>
        /// 负场
        /// </summary>
        public int losses { get; set; }
        /// <summary>
        /// 游玩时间
        /// </summary>
        public int playtime { get; set; }
        /// <summary>
        /// 胜场
        /// </summary>
        public int wins { get; set; }
        /// <summary>
        /// 胜负比
        /// </summary>
        public float wl { get; set; }
    }

    public class Gamemode
    {
        /// <summary>
        /// 拆除炸弹
        /// </summary>
        public Bomb bomb { get; set; }
        /// <summary>
        /// 团队死斗
        /// </summary>
        public Secure_Area secure_area { get; set; }
        /// <summary>
        /// 人质营救
        /// </summary>
        public Hostage hostage { get; set; }
    }

    public class Bomb
    {
        /// <summary>
        /// 最高分
        /// </summary>
        public int best_score { get; set; }
        /// <summary>
        /// 场次
        /// </summary>
        public int games_played { get; set; }
        /// <summary>
        /// 负场
        /// </summary>
        public int losses { get; set; }
        /// <summary>
        /// 游玩时间
        /// </summary>
        public int playtime { get; set; }
        /// <summary>
        /// 胜场
        /// </summary>
        public int wins { get; set; }
        /// <summary>
        /// 胜负比
        /// </summary>
        public float wl { get; set; }
    }

    public class Secure_Area
    {
        /// <summary>
        /// 最高分
        /// </summary>
        public int best_score { get; set; }
        /// <summary>
        /// 场次
        /// </summary>
        public int games_played { get; set; }
        /// <summary>
        /// 作为进攻方击杀
        /// </summary>
        public int kills_as_attacker_in_objective { get; set; }
        /// <summary>
        /// 作为防守方击杀
        /// </summary>
        public int kills_as_defender_in_objective { get; set; }
        /// <summary>
        /// 负场
        /// </summary>
        public int losses { get; set; }
        /// <summary>
        /// 游玩时间
        /// </summary>
        public int playtime { get; set; }
        /// <summary>
        /// 肃清威胁成功次数
        /// </summary>
        public int times_objective_secured { get; set; }
        /// <summary>
        /// 胜场
        /// </summary>
        public int wins { get; set; }
        /// <summary>
        /// 胜负比
        /// </summary>
        public float wl { get; set; }
    }

    public class Hostage
    {
        /// <summary>
        /// 最高分
        /// </summary>
        public int best_score { get; set; }
        /// <summary>
        /// 场次
        /// </summary>
        public int games_played { get; set; }
        /// <summary>
        /// 负场
        /// </summary>
        public int losses { get; set; }
        /// <summary>
        /// 游玩时间
        /// </summary>
        public int playtime { get; set; }
        /// <summary>
        /// 阻止人质护送次数
        /// </summary>
        public int extractions_denied { get; set; }
        /// <summary>
        /// 胜场
        /// </summary>
        public int wins { get; set; }
        /// <summary>
        /// 胜负比
        /// </summary>
        public float wl { get; set; }
    }

    public class Timestamps
    {
        public DateTime created { get; set; }
        public DateTime last_updated { get; set; }
    }

    public class Operator
    {
        /// <summary>
        /// 击杀
        /// </summary>
        public int kills { get; set; }
        /// <summary>
        /// 死亡
        /// </summary>
        public int deaths { get; set; }
        /// <summary>
        /// 击杀死亡比
        /// </summary>
        public float kd { get; set; }
        /// <summary>
        /// 胜场
        /// </summary>
        public int wins { get; set; }
        /// <summary>
        /// 负场
        /// </summary>
        public int losses { get; set; }
        /// <summary>
        /// 胜负比
        /// </summary>
        public float wl { get; set; }
        /// <summary>
        /// 爆头数
        /// </summary>
        public int headshots { get; set; }
        /// <summary>
        /// 被击倒但未死亡
        /// </summary>
        public int dbnos { get; set; }
        /// <summary>
        /// 近战击杀
        /// </summary>
        public int melee_kills { get; set; }
        /// <summary>
        /// 经验值
        /// </summary>
        public int experience { get; set; }
        /// <summary>
        /// 使用时长
        /// </summary>
        public int playtime { get; set; }
        /// <summary>
        /// 技能
        /// </summary>
        public Ability[] abilities { get; set; }
        /// <summary>
        /// 干员信息
        /// </summary>
        public OperatorInfo _operator { get; set; }
    }

    public class OperatorInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 内部名
        /// </summary>
        public string internal_name { get; set; }
        /// <summary>
        /// 角色"defender"防守方 "recruit"进攻方
        /// </summary>
        public string role { get; set; }
        /// <summary>
        /// 所属组织
        /// </summary>
        public string ctu { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public Images images { get; set; }
    }

    public class Images
    {
        public string badge { get; set; }
        public string bust { get; set; }
        public string figure { get; set; }
    }

    public class Ability
    {
        /// <summary>
        /// 键
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public int value { get; set; }
    }
}
