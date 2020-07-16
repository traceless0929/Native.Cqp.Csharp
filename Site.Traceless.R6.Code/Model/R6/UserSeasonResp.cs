using System;
using System.Collections.Generic;

using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Site.Traceless.R6.Code.Model.R6
{
    public partial class UserSeasonResp
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public SeasonData Data { get; set; }
    }

    public partial class SeasonData
    {
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; set; }

        [JsonProperty("ubisoft_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? UbisoftId { get; set; }

        [JsonProperty("uplay_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? UplayId { get; set; }

        [JsonProperty("avatar_url_146", NullValueHandling = NullValueHandling.Ignore)]
        public Uri AvatarUrl146 { get; set; }

        [JsonProperty("avatar_url_256", NullValueHandling = NullValueHandling.Ignore)]
        public Uri AvatarUrl256 { get; set; }

        [JsonProperty("claimed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Claimed { get; set; }

        [JsonProperty("claimee", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Claimee { get; set; }

        [JsonProperty("profile")]
        public object Profile { get; set; }

        [JsonProperty("last_updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdated { get; set; }

        [JsonProperty("queue", NullValueHandling = NullValueHandling.Ignore)]
        public Queue Queue { get; set; }

        [JsonProperty("seasons", NullValueHandling = NullValueHandling.Ignore)]
        public List<Season> Seasons { get; set; }
    }

    public partial class Queue
    {
        [JsonProperty("queued", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Queued { get; set; }

        [JsonProperty("estimated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Estimated { get; set; }
    }

    public partial class Season
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("start_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTimeOffset? EndDate { get; set; }

        [JsonProperty("primary_color", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryColor { get; set; }

        [JsonProperty("rankings", NullValueHandling = NullValueHandling.Ignore)]
        public Rankings Rankings { get; set; }

        [JsonProperty("ranks", NullValueHandling = NullValueHandling.Ignore)]
        public List<Rank> Ranks { get; set; }

        [JsonProperty("regions", NullValueHandling = NullValueHandling.Ignore)]
        public Rankings Regions { get; set; }
    }

    public partial class Rankings
    {
        [JsonProperty("ncsa")]
        public List<Apac> Ncsa { get; set; }

        [JsonProperty("emea")]
        public List<Apac> Emea { get; set; }

        [JsonProperty("apac")]
        public List<Apac> Apac { get; set; }

        [JsonProperty("global")]
        public object Global { get; set; }

        public Apac getBest()
        {
            Apac nc = this.Ncsa.FirstOrDefault();
            Apac em = this.Emea.FirstOrDefault();
            Apac ac = this.Apac.FirstOrDefault();
            List<Apac> list = new List<Apac>()
            {
                nc,em,ac
            };
            long? max = list.Max(c => c.MaxMmr);
            return list.FirstOrDefault(p => p.MaxMmr == max);
        }
    }

    public partial class Apac
    {
        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public Region? Region { get; set; }

        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public long? Wins { get; set; }

        [JsonProperty("losses", NullValueHandling = NullValueHandling.Ignore)]
        public long? Losses { get; set; }

        [JsonProperty("abandons", NullValueHandling = NullValueHandling.Ignore)]
        public long? Abandons { get; set; }

        [JsonProperty("kills", NullValueHandling = NullValueHandling.Ignore)]
        public long? Kills { get; set; }

        [JsonProperty("deaths", NullValueHandling = NullValueHandling.Ignore)]
        public long? Deaths { get; set; }

        [JsonProperty("prev_rank_mmr", NullValueHandling = NullValueHandling.Ignore)]
        public long? PrevRankMmr { get; set; }

        [JsonProperty("mmr", NullValueHandling = NullValueHandling.Ignore)]
        public long? Mmr { get; set; }

        [JsonProperty("next_rank_mmr", NullValueHandling = NullValueHandling.Ignore)]
        public long? NextRankMmr { get; set; }

        [JsonProperty("max_mmr", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxMmr { get; set; }

        [JsonProperty("last_match_mmr_change", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastMatchMmrChange { get; set; }

        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public long? Rank { get; set; }

        [JsonProperty("max_rank", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxRank { get; set; }

        [JsonProperty("champions_rank_position", NullValueHandling = NullValueHandling.Ignore)]
        public long? ChampionsRankPosition { get; set; }

        [JsonProperty("skill_mean", NullValueHandling = NullValueHandling.Ignore)]
        public double? SkillMean { get; set; }

        [JsonProperty("last_match_skill_mean_change", NullValueHandling = NullValueHandling.Ignore)]
        public double? LastMatchSkillMeanChange { get; set; }

        [JsonProperty("skill_standard_deviation", NullValueHandling = NullValueHandling.Ignore)]
        public double? SkillStandardDeviation { get; set; }

        [JsonProperty("last_match_skill_standard_deviation_change", NullValueHandling = NullValueHandling.Ignore)]
        public double? LastMatchSkillStandardDeviationChange { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("created_for_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreatedForDate { get; set; }
    }

    public partial class Rank
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public long? RankRank { get; set; }

        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        [JsonProperty("champions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Champions { get; set; }
    }

    public enum Region { Apac, Emea, Ncsa };

    internal class RegionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Region) || t == typeof(Region?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "apac":
                    return Region.Apac;

                case "emea":
                    return Region.Emea;

                case "ncsa":
                    return Region.Ncsa;
            }
            throw new Exception("Cannot unmarshal type Region");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Region)untypedValue;
            switch (value)
            {
                case Region.Apac:
                    serializer.Serialize(writer, "apac");
                    return;

                case Region.Emea:
                    serializer.Serialize(writer, "emea");
                    return;

                case Region.Ncsa:
                    serializer.Serialize(writer, "ncsa");
                    return;
            }
            throw new Exception("Cannot marshal type Region");
        }

        public static readonly RegionConverter Singleton = new RegionConverter();
    }
}