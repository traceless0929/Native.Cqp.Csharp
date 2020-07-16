using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Site.Traceless.R6.Code.Model.R6
{
    public partial class UserBaseInfoResp
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Datum> Data { get; set; }
    }

    public partial class Datum
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

        [JsonProperty("avatar_banned", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AvatarBanned { get; set; }

        [JsonProperty("claimed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Claimed { get; set; }

        [JsonProperty("last_updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdated { get; set; }

        [JsonProperty("progressionStats", NullValueHandling = NullValueHandling.Ignore)]
        public ProgressionStats ProgressionStats { get; set; }

        [JsonProperty("genericStats", NullValueHandling = NullValueHandling.Ignore)]
        public GenericStats GenericStats { get; set; }

        [JsonProperty("seasonalStats", NullValueHandling = NullValueHandling.Ignore)]
        public SeasonalStats SeasonalStats { get; set; }
    }
}