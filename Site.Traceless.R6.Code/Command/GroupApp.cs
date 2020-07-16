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
            Datum baseRes = Apis.GetUserBaseInfo(msg.Who, "pc")?.Data.FirstOrDefault();
            SeasonData res = Apis.GetUserSeasonInfo(baseRes);
            if (res == null)
            {
                e.CQApi.SendGroupMessage(e.FromGroup, @"[R6排位]查无此人/BUG碧查询服务器维护中");
                return;
            }
            List<Season> infos = res.Seasons.OrderByDescending(p => p.Id.Value).Take(3).ToList();
            StringBuilder sb = new StringBuilder();
            Apac nowSeason = infos.FirstOrDefault().Regions.getBest();
            if (null == nowSeason)
            {
                nowSeason = new Apac()
                {
                    Mmr = null,
                    PrevRankMmr = null,
                    NextRankMmr = null,
                    MaxRank = null
                };
            }
            var rankItem = infos.FirstOrDefault().Rankings;
            sb.AppendLine($"[{baseRes.ProgressionStats.Level}]{baseRes.Username}-排名(全球/亚/美/欧):{rankItem.Global}/{rankItem.Apac}/{rankItem.Ncsa}/{rankItem.Emea}-MMR[{nowSeason.Mmr}]-({nowSeason.PrevRankMmr}/{nowSeason.NextRankMmr})");
            infos.ForEach(p =>
            {
                var item = p.Regions.getBest();
                sb.AppendLine($"[{p.Name}]现/顶:{Utils.ConvertToRankDes(item.Rank)}/{Utils.ConvertToRankDes(item.MaxRank)}-能力:{item.SkillMean}(±{item.SkillStandardDeviation})");
            });
            e.CQApi.SendGroupMessage(
                e.FromGroup,
                sb.ToString() +
                Environment.NewLine +
                @"详情:https://r6stats.com/zh/stats/" + res.UplayId?.ToString("D") + "/seasons");
        }

        public static void GetWeapon(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            Datum baseRes = Apis.GetUserBaseInfo(msg.Who, "pc")?.Data.FirstOrDefault();
            WeaponData res = Apis.GetUserWeaponInfo(baseRes);
            if (res == null)
            {
                e.CQApi.SendGroupMessage(e.FromGroup, @"[R6排位]查无此人/BUG碧查询服务器维护中");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[{baseRes.ProgressionStats.Level}]{res.Username}-武器数据统计:");
            sb.AppendLine($"敬请期待");
            sb.AppendLine(@"详情:https://r6stats.com/zh/stats/" + res.UplayId?.ToString("D") + "/weapons");
            e.CQApi.SendGroupMessage(
            e.FromGroup,
            sb.ToString()
            );
        }

        public static void GetBattleStastic(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            DetailData res = Apis.GetUserDetailInfo(Apis.GetUserBaseInfo(msg.Who, "pc").Data.FirstOrDefault());
            if (res != null)
            {
                StringBuilder sb = new StringBuilder();
                var gen = res.Stats.FirstOrDefault().General;
                var que = res.Stats.FirstOrDefault().Queue;
                List<OperatorElement> operators = res.Operators.OrderByDescending(p => p.Experience).Take(20).ToList();
                OperatorElement kdOpt = operators.OrderByDescending(p => p.Kd).FirstOrDefault();
                OperatorElement killOpt = operators.OrderByDescending(p => p.Kills).FirstOrDefault();
                OperatorElement winOpt = operators.OrderByDescending(p => p.Wins).FirstOrDefault();
                OperatorElement winRateOpt = operators.OrderByDescending(p => p.Wl).FirstOrDefault();
                OperatorElement timeOpt = operators.OrderByDescending(p => p.Playtime).FirstOrDefault();
                OperatorElement expOpt = operators.OrderByDescending(p => p.Experience).FirstOrDefault();
                sb.AppendLine($"[{res.Progression.Level}]{res.Username}-刷包概率{res.Progression.LootboxProbability} 的战绩如下:");
                sb.AppendLine($"总计：");
                sb.AppendLine($"KD(击杀/死亡):{gen.Kd}({gen.Kills}/{gen.Deaths})");
                sb.AppendLine($"近战/穿透/致盲:{gen.MeleeKills}/{gen.PenetrationKills}/{gen.BlindKills}");
                sb.AppendLine($"胜负比(胜/负):{gen.Wl}({gen.Wins}/{gen.Losses})");
                sb.AppendLine($"休闲：");
                sb.AppendLine($"KD(击杀/死亡):{que.Casual.Kd}({que.Casual.Kills}/{que.Casual.Deaths})");
                sb.AppendLine($"胜负比(胜/负):{que.Casual.Wl}({que.Casual.Wins}/{que.Casual.Losses})");
                sb.AppendLine($"排位：");
                sb.AppendLine($"KD(击杀/死亡):{que.Ranked.Kd}({que.Ranked.Kills}/{que.Ranked.Deaths})");
                sb.AppendLine($"胜负比(胜/负):{que.Ranked.Wl}({que.Ranked.Wins}/{que.Ranked.Losses})");
                sb.AppendLine($"干员（为避免无意义数据，仅统计熟练度TOP20）：");
                sb.AppendLine($"本命(熟练度):" + expOpt.@Operator.Name + "/" + expOpt.Experience);
                sb.AppendLine($"全场最佳(KD):" + kdOpt.@Operator.Name + "/" + kdOpt.Kd);
                sb.AppendLine($"反恐杀神(击杀):" + killOpt.@Operator.Name + "/" + killOpt.Kills);
                sb.AppendLine($"战功赫赫(胜场):" + winOpt.@Operator.Name + "/" + winOpt.Wins);
                sb.AppendLine($"上分机器(胜负比):" + winRateOpt.@Operator.Name + "/" + winRateOpt.Wl);
                sb.AppendLine($"情有独钟(使用时长):" + timeOpt.@Operator.Name + "/" + timeOpt.Playtime + "秒");
                sb.AppendLine(@"详情:https://r6stats.com/zh/stats/" + res.UplayId?.ToString("D"));
                e.CQApi.SendGroupMessage(e.FromGroup, sb.ToString());
                return;
            }
            e.CQApi.SendGroupMessage(e.FromGroup, @"[R6战绩]查无此人/BUG碧查询服务器维护中");
        }
    }
}