using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.R6.Code.Http;
using Site.Traceless.R6.Code.Model;
using Site.Traceless.R6.Code.Model.R6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.R6.Code.Command
{
    public class GroupApp
    {
        public static void GetRankInfo(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            UserBaseInfoResp baseRes = Apis.GetUserBaseInfo(msg.Who, "pc");
            UserSeasonResp res = Apis.GetUserSeasonInfo(baseRes);
            if (res == null)
            {
                e.CQApi.SendGroupMessage(e.FromGroup, @"[R6排位]查无此人/BUG碧查询服务器维护中");
                return;
            }
            List<SeasonItem> infos = res.GetSeasons().OrderByDescending(p => p.id).Take(3).ToList();
            StringBuilder sb = new StringBuilder();
            RegionsItem nowSeason = infos.FirstOrDefault().regions.getBest();
            if (null == nowSeason)
            {
                nowSeason = new RegionsItem()
                {
                    mmr = null,
                    prev_rank_mmr = null,
                    next_rank_mmr = null,
                    max_rank=null
                };
            }
            var rankItem = infos.FirstOrDefault().rankings;
            sb.AppendLine($"[{baseRes.progressionStats.level}]{baseRes.username}-排名(全球/亚/美/欧):{rankItem.global}/{rankItem.apac}/{rankItem.ncsa}/{rankItem.emea}-MMR[{nowSeason.mmr}]-({nowSeason.prev_rank_mmr}/{nowSeason.next_rank_mmr})");
            infos.ForEach(p =>
            {
                var item = p.regions.getBest();
                sb.AppendLine($"[{p.name}]现/顶:{Utils.ConvertToRankDes(item.rank)}/{Utils.ConvertToRankDes(item.max_rank)}-能力:{item.skill_mean}(±{item.skill_standard_deviation})");
            });
            e.CQApi.SendGroupMessage(
                e.FromGroup,
                sb.ToString() +
                Environment.NewLine +
                @"详情:https://r6stats.com/zh/stats/" + res.uplay_id + "/seasons");
        }

        public static void GetWeapon(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            UserBaseInfoResp baseRes = Apis.GetUserBaseInfo(msg.Who, "pc");
            UserWeaponResp res = Apis.GetUserWeaponInfo(baseRes);
            if (res == null)
            {
                e.CQApi.SendGroupMessage(e.FromGroup, @"[R6排位]查无此人/BUG碧查询服务器维护中");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[{baseRes.progressionStats.level}]{res.username}-武器数据统计:");
                sb.AppendLine($"敬请期待");
                sb.AppendLine(@"详情:https://r6stats.com/zh/stats/" + res.uplay_id + "/weapons");
                e.CQApi.SendGroupMessage(
                e.FromGroup,
                sb.ToString()
                );
        }

        public static void GetBattleStastic(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            UserDetailInfoResp res = Apis.GetUserDetailInfo(Apis.GetUserBaseInfo(msg.Who, "pc"));
            if (res != null)
            {
                StringBuilder sb = new StringBuilder();
                var gen = res.stats.FirstOrDefault().general;
                var que = res.stats.FirstOrDefault().queue;
                List<Operator> operators = res.operators.OrderByDescending(p => p.experience).Take(20).ToList();
                Operator kdOpt = operators.OrderByDescending(p => p.kd).FirstOrDefault();
                Operator killOpt = operators.OrderByDescending(p => p.kills).FirstOrDefault();
                Operator winOpt = operators.OrderByDescending(p => p.wins).FirstOrDefault();
                Operator winRateOpt = operators.OrderByDescending(p => p.wl).FirstOrDefault();
                Operator timeOpt = operators.OrderByDescending(p => p.playtime).FirstOrDefault();
                Operator expOpt = operators.OrderByDescending(p => p.experience).FirstOrDefault();
                sb.AppendLine($"[{res.progression.level}]{res.username}-刷包概率{res.progression.lootbox_probability} 的战绩如下:");
                sb.AppendLine($"总计：");
                sb.AppendLine($"KD(击杀/死亡):{gen.kd}({gen.kills}/{gen.deaths})");
                sb.AppendLine($"近战/穿透/致盲:{gen.melee_kills}/{gen.penetration_kills}/{gen.blind_kills}");
                sb.AppendLine($"胜负比(胜/负):{gen.wl}({gen.wins}/{gen.losses})");
                sb.AppendLine($"休闲：");
                sb.AppendLine($"KD(击杀/死亡):{que.casual.kd}({que.casual.kills}/{que.casual.deaths})");
                sb.AppendLine($"胜负比(胜/负):{que.casual.wl}({que.casual.wins}/{que.casual.losses})");
                sb.AppendLine($"排位：");
                sb.AppendLine($"KD(击杀/死亡):{que.ranked.kd}({que.ranked.kills}/{que.ranked.deaths})");
                sb.AppendLine($"胜负比(胜/负):{que.ranked.wl}({que.ranked.wins}/{que.ranked.losses})");
                sb.AppendLine($"干员（为避免无意义数据，仅统计熟练度TOP20）：");
                sb.AppendLine($"本命(熟练度):" + expOpt.@operator.name+"/"+expOpt.experience);
                sb.AppendLine($"全场最佳(KD):" + kdOpt.@operator.name+"/"+kdOpt.kd);
                sb.AppendLine($"反恐杀神(击杀):" + killOpt.@operator.name+"/"+killOpt.kills);
                sb.AppendLine($"战功赫赫(胜场):" + winOpt.@operator.name+"/"+winOpt.wins);
                sb.AppendLine($"上分机器(胜负比):" + winRateOpt.@operator.name+"/"+winRateOpt.wl);
                sb.AppendLine($"情有独钟(使用时长):" + timeOpt.@operator.name+"/"+timeOpt.playtime+"秒");
                sb.AppendLine( @"详情:https://r6stats.com/zh/stats/" + res.uplay_id);
                e.CQApi.SendGroupMessage(e.FromGroup, sb.ToString());
                return;
            }
            e.CQApi.SendGroupMessage(e.FromGroup, @"[R6战绩]查无此人/BUG碧查询服务器维护中");
        }
    }
}
