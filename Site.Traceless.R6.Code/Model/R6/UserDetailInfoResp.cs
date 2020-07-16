using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Site.Traceless.R6.Code.Model.R6
{
    public partial class UserDetailInfoResp
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public DetailData Data { get; set; }
    }

    public partial class DetailData
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
        public DataQueue Queue { get; set; }

        [JsonProperty("aliases", NullValueHandling = NullValueHandling.Ignore)]
        public List<Alias> Aliases { get; set; }

        [JsonProperty("profiles", NullValueHandling = NullValueHandling.Ignore)]
        public List<Profile> Profiles { get; set; }

        [JsonProperty("progression", NullValueHandling = NullValueHandling.Ignore)]
        public Progression Progression { get; set; }

        [JsonProperty("stats", NullValueHandling = NullValueHandling.Ignore)]
        public List<Stat> Stats { get; set; }

        [JsonProperty("operators", NullValueHandling = NullValueHandling.Ignore)]
        public List<OperatorElement> Operators { get; set; }
    }

    public partial class Alias
    {
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("last_seen_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastSeenAt { get; set; }
    }

    public partial class OperatorElement
    {
        [JsonProperty("kills", NullValueHandling = NullValueHandling.Ignore)]
        public long? Kills { get; set; }

        [JsonProperty("deaths", NullValueHandling = NullValueHandling.Ignore)]
        public long? Deaths { get; set; }

        [JsonProperty("kd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Kd { get; set; }

        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public long? Wins { get; set; }

        [JsonProperty("losses", NullValueHandling = NullValueHandling.Ignore)]
        public long? Losses { get; set; }

        [JsonProperty("wl", NullValueHandling = NullValueHandling.Ignore)]
        public double? Wl { get; set; }

        [JsonProperty("headshots", NullValueHandling = NullValueHandling.Ignore)]
        public long? Headshots { get; set; }

        [JsonProperty("dbnos", NullValueHandling = NullValueHandling.Ignore)]
        public long? Dbnos { get; set; }

        [JsonProperty("melee_kills", NullValueHandling = NullValueHandling.Ignore)]
        public long? MeleeKills { get; set; }

        [JsonProperty("experience", NullValueHandling = NullValueHandling.Ignore)]
        public long? Experience { get; set; }

        [JsonProperty("playtime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Playtime { get; set; }

        [JsonProperty("abilities", NullValueHandling = NullValueHandling.Ignore)]
        public List<Ability> Abilities { get; set; }

        [JsonProperty("operator", NullValueHandling = NullValueHandling.Ignore)]
        public OperatorOperator Operator { get; set; }
    }

    public partial class Ability
    {
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public long? Value { get; set; }
    }

    public partial class OperatorOperator
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("internal_name", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalName { get; set; }

        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public Role? Role { get; set; }

        [JsonProperty("ctu", NullValueHandling = NullValueHandling.Ignore)]
        public string Ctu { get; set; }

        [JsonProperty("images", NullValueHandling = NullValueHandling.Ignore)]
        public Images Images { get; set; }
    }

    public partial class Images
    {
        [JsonProperty("badge", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Badge { get; set; }

        [JsonProperty("bust", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Bust { get; set; }

        [JsonProperty("figure", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Figure { get; set; }
    }

    public partial class Profile
    {
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; set; }

        [JsonProperty("ubisoft_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? UbisoftId { get; set; }

        [JsonProperty("uplay_id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? UplayId { get; set; }
    }

    public partial class Progression
    {
        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public long? Level { get; set; }

        [JsonProperty("lootbox_probability", NullValueHandling = NullValueHandling.Ignore)]
        public long? LootboxProbability { get; set; }

        [JsonProperty("total_xp", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalXp { get; set; }
    }

    public partial class DataQueue
    {
        [JsonProperty("queued", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Queued { get; set; }

        [JsonProperty("estimated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Estimated { get; set; }
    }

    public partial class Stat
    {
        [JsonProperty("general", NullValueHandling = NullValueHandling.Ignore)]
        public General General { get; set; }

        [JsonProperty("queue", NullValueHandling = NullValueHandling.Ignore)]
        public StatQueue Queue { get; set; }

        [JsonProperty("gamemode", NullValueHandling = NullValueHandling.Ignore)]
        public Gamemode Gamemode { get; set; }

        [JsonProperty("timestamps", NullValueHandling = NullValueHandling.Ignore)]
        public Timestamps Timestamps { get; set; }
    }

    public partial class Gamemode
    {
        [JsonProperty("bomb", NullValueHandling = NullValueHandling.Ignore)]
        public Bomb Bomb { get; set; }

        [JsonProperty("secure_area", NullValueHandling = NullValueHandling.Ignore)]
        public SecureArea SecureArea { get; set; }

        [JsonProperty("hostage", NullValueHandling = NullValueHandling.Ignore)]
        public Bomb Hostage { get; set; }
    }

    public partial class Bomb
    {
        [JsonProperty("best_score", NullValueHandling = NullValueHandling.Ignore)]
        public long? BestScore { get; set; }

        [JsonProperty("games_played", NullValueHandling = NullValueHandling.Ignore)]
        public long? GamesPlayed { get; set; }

        [JsonProperty("losses", NullValueHandling = NullValueHandling.Ignore)]
        public long? Losses { get; set; }

        [JsonProperty("playtime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Playtime { get; set; }

        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public long? Wins { get; set; }

        [JsonProperty("wl", NullValueHandling = NullValueHandling.Ignore)]
        public double? Wl { get; set; }

        [JsonProperty("extractions_denied", NullValueHandling = NullValueHandling.Ignore)]
        public long? ExtractionsDenied { get; set; }
    }

    public partial class SecureArea
    {
        [JsonProperty("best_score", NullValueHandling = NullValueHandling.Ignore)]
        public long? BestScore { get; set; }

        [JsonProperty("games_played", NullValueHandling = NullValueHandling.Ignore)]
        public long? GamesPlayed { get; set; }

        [JsonProperty("kills_as_attacker_in_objective", NullValueHandling = NullValueHandling.Ignore)]
        public long? KillsAsAttackerInObjective { get; set; }

        [JsonProperty("kills_as_defender_in_objective", NullValueHandling = NullValueHandling.Ignore)]
        public long? KillsAsDefenderInObjective { get; set; }

        [JsonProperty("losses", NullValueHandling = NullValueHandling.Ignore)]
        public long? Losses { get; set; }

        [JsonProperty("playtime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Playtime { get; set; }

        [JsonProperty("times_objective_secured", NullValueHandling = NullValueHandling.Ignore)]
        public long? TimesObjectiveSecured { get; set; }

        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public long? Wins { get; set; }

        [JsonProperty("wl", NullValueHandling = NullValueHandling.Ignore)]
        public double? Wl { get; set; }
    }

    public partial class General
    {
        [JsonProperty("assists", NullValueHandling = NullValueHandling.Ignore)]
        public long? Assists { get; set; }

        [JsonProperty("barricades_deployed", NullValueHandling = NullValueHandling.Ignore)]
        public long? BarricadesDeployed { get; set; }

        [JsonProperty("blind_kills", NullValueHandling = NullValueHandling.Ignore)]
        public long? BlindKills { get; set; }

        [JsonProperty("bullets_fired", NullValueHandling = NullValueHandling.Ignore)]
        public long? BulletsFired { get; set; }

        [JsonProperty("bullets_hit", NullValueHandling = NullValueHandling.Ignore)]
        public long? BulletsHit { get; set; }

        [JsonProperty("dbnos", NullValueHandling = NullValueHandling.Ignore)]
        public long? Dbnos { get; set; }

        [JsonProperty("deaths", NullValueHandling = NullValueHandling.Ignore)]
        public long? Deaths { get; set; }

        [JsonProperty("distance_travelled", NullValueHandling = NullValueHandling.Ignore)]
        public long? DistanceTravelled { get; set; }

        [JsonProperty("draws", NullValueHandling = NullValueHandling.Ignore)]
        public long? Draws { get; set; }

        [JsonProperty("gadgets_destroyed", NullValueHandling = NullValueHandling.Ignore)]
        public long? GadgetsDestroyed { get; set; }

        [JsonProperty("games_played", NullValueHandling = NullValueHandling.Ignore)]
        public long? GamesPlayed { get; set; }

        [JsonProperty("headshots", NullValueHandling = NullValueHandling.Ignore)]
        public long? Headshots { get; set; }

        [JsonProperty("kd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Kd { get; set; }

        [JsonProperty("kills", NullValueHandling = NullValueHandling.Ignore)]
        public long? Kills { get; set; }

        [JsonProperty("losses", NullValueHandling = NullValueHandling.Ignore)]
        public long? Losses { get; set; }

        [JsonProperty("melee_kills", NullValueHandling = NullValueHandling.Ignore)]
        public long? MeleeKills { get; set; }

        [JsonProperty("penetration_kills", NullValueHandling = NullValueHandling.Ignore)]
        public long? PenetrationKills { get; set; }

        [JsonProperty("playtime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Playtime { get; set; }

        [JsonProperty("rappel_breaches", NullValueHandling = NullValueHandling.Ignore)]
        public long? RappelBreaches { get; set; }

        [JsonProperty("reinforcements_deployed", NullValueHandling = NullValueHandling.Ignore)]
        public long? ReinforcementsDeployed { get; set; }

        [JsonProperty("revives", NullValueHandling = NullValueHandling.Ignore)]
        public long? Revives { get; set; }

        [JsonProperty("suicides", NullValueHandling = NullValueHandling.Ignore)]
        public long? Suicides { get; set; }

        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public long? Wins { get; set; }

        [JsonProperty("wl", NullValueHandling = NullValueHandling.Ignore)]
        public double? Wl { get; set; }
    }

    public partial class StatQueue
    {
        [JsonProperty("casual", NullValueHandling = NullValueHandling.Ignore)]
        public Casual Casual { get; set; }

        [JsonProperty("ranked", NullValueHandling = NullValueHandling.Ignore)]
        public Casual Ranked { get; set; }

        [JsonProperty("other", NullValueHandling = NullValueHandling.Ignore)]
        public Casual Other { get; set; }
    }

    public partial class Casual
    {
        [JsonProperty("deaths", NullValueHandling = NullValueHandling.Ignore)]
        public long? Deaths { get; set; }

        [JsonProperty("draws", NullValueHandling = NullValueHandling.Ignore)]
        public long? Draws { get; set; }

        [JsonProperty("games_played", NullValueHandling = NullValueHandling.Ignore)]
        public long? GamesPlayed { get; set; }

        [JsonProperty("kd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Kd { get; set; }

        [JsonProperty("kills", NullValueHandling = NullValueHandling.Ignore)]
        public long? Kills { get; set; }

        [JsonProperty("losses", NullValueHandling = NullValueHandling.Ignore)]
        public long? Losses { get; set; }

        [JsonProperty("playtime", NullValueHandling = NullValueHandling.Ignore)]
        public long? Playtime { get; set; }

        [JsonProperty("wins", NullValueHandling = NullValueHandling.Ignore)]
        public long? Wins { get; set; }

        [JsonProperty("wl", NullValueHandling = NullValueHandling.Ignore)]
        public double? Wl { get; set; }
    }

    public partial class Timestamps
    {
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Created { get; set; }

        [JsonProperty("last_updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdated { get; set; }
    }

    public partial class GenericStats
    {
        [JsonProperty("general", NullValueHandling = NullValueHandling.Ignore)]
        public General General { get; set; }

        [JsonProperty("queue", NullValueHandling = NullValueHandling.Ignore)]
        public Queue Queue { get; set; }

        [JsonProperty("gamemode", NullValueHandling = NullValueHandling.Ignore)]
        public Gamemode Gamemode { get; set; }

        [JsonProperty("timestamps", NullValueHandling = NullValueHandling.Ignore)]
        public Timestamps Timestamps { get; set; }
    }

    public partial class Queue
    {
        [JsonProperty("casual", NullValueHandling = NullValueHandling.Ignore)]
        public Casual Casual { get; set; }

        [JsonProperty("ranked", NullValueHandling = NullValueHandling.Ignore)]
        public Casual Ranked { get; set; }

        [JsonProperty("other", NullValueHandling = NullValueHandling.Ignore)]
        public Casual Other { get; set; }
    }

    public partial class ProgressionStats
    {
        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public long? Level { get; set; }

        [JsonProperty("lootbox_probability", NullValueHandling = NullValueHandling.Ignore)]
        public long? LootboxProbability { get; set; }

        [JsonProperty("total_xp", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalXp { get; set; }
    }

    public partial class SeasonalStats
    {
        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

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
        public long? LastMatchSkillMeanChange { get; set; }

        [JsonProperty("skill_standard_deviation", NullValueHandling = NullValueHandling.Ignore)]
        public double? SkillStandardDeviation { get; set; }

        [JsonProperty("last_match_skill_standard_deviation_change", NullValueHandling = NullValueHandling.Ignore)]
        public long? LastMatchSkillStandardDeviationChange { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("created_for_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreatedForDate { get; set; }
    }

    public enum Role { Attacker, Defender, Recruit };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                RoleConverter.Singleton,
                RegionConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class RoleConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Role) || t == typeof(Role?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "attacker":
                    return Role.Attacker;

                case "defender":
                    return Role.Defender;

                case "recruit":
                    return Role.Recruit;
            }
            throw new Exception("Cannot unmarshal type Role");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Role)untypedValue;
            switch (value)
            {
                case Role.Attacker:
                    serializer.Serialize(writer, "attacker");
                    return;

                case Role.Defender:
                    serializer.Serialize(writer, "defender");
                    return;

                case Role.Recruit:
                    serializer.Serialize(writer, "recruit");
                    return;
            }
            throw new Exception("Cannot marshal type Role");
        }

        public static readonly RoleConverter Singleton = new RoleConverter();
    }
}