using Native.Csharp.Sdk.Cqp.EventArgs;
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
                e.CQApi.SendGroupMessage(e.FromGroup, @"[R6排位]查无此人");
            }
            List<SeasonItem> infos = res.seasons.Getinfos().OrderByDescending(p => p.id).Take(3).ToList();
            StringBuilder sb = new StringBuilder();
            RegionsItem nowSeason = infos.FirstOrDefault().regions.getBest();
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
        public static void GetBattleStastic(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            UserDetailInfoResp res = Apis.GetUserDetailInfo(Apis.GetUserBaseInfo(msg.Who, "pc"));
            if (res != null)
            {
                var gen = res.stats.FirstOrDefault().general;
                var que = res.stats.FirstOrDefault().queue;
                string content = $"[{res.progression.level}]{res.username}-刷包概率{res.progression.lootbox_probability} 的战绩如下:" +
                             Environment.NewLine +
                             $"总计：" +
                             Environment.NewLine +
                             $"KD(击杀/死亡):{gen.kd}({gen.kills}/{gen.deaths})" +
                             Environment.NewLine +
                             $"近战/穿透/致盲:{gen.melee_kills}/{gen.penetration_kills}/{gen.blind_kills}" +
                             Environment.NewLine +
                             $"胜负比(胜/负):{gen.wl}/{gen.wins}/{gen.losses}" +
                             Environment.NewLine +
                             $"休闲：" +
                             Environment.NewLine +
                             $"KD(击杀/死亡):{que.casual.kd}({que.casual.kills}/{que.casual.deaths})" +
                             Environment.NewLine +
                             $"胜负比(胜/负):{que.casual.wl}/{que.casual.wins}/{que.casual.losses}" +
                             Environment.NewLine +
                             $"排位：" +
                             Environment.NewLine +
                             $"KD(击杀/死亡):{que.ranked.kd}({que.ranked.kills}/{que.ranked.deaths})" +
                             Environment.NewLine +
                             $"胜负比(胜/负):{que.ranked.wl}/{que.ranked.wins}/{que.ranked.losses}" +
                             Environment.NewLine +
                             @"详情:https://r6stats.com/zh/stats/" + res.uplay_id;
                e.CQApi.SendGroupMessage(e.FromGroup, content);
            }
            else
            {
                e.CQApi.SendGroupMessage(e.FromGroup, @"[R6战绩]查无此人");
            }
        }
    }
}
