using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Site.Traceless.R6.Code.Model.R6
{
    public partial class UserWeaponResp
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public WeaponData Data { get; set; }
    }

    public partial class WeaponData
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

        [JsonProperty("categories", NullValueHandling = NullValueHandling.Ignore)]
        public List<CategoryElement> Categories { get; set; }

        [JsonProperty("weapons", NullValueHandling = NullValueHandling.Ignore)]
        public List<CategoryElement> Weapons { get; set; }
    }

    public partial class CategoryElement
    {
        [JsonProperty("kills", NullValueHandling = NullValueHandling.Ignore)]
        public long? Kills { get; set; }

        [JsonProperty("deaths", NullValueHandling = NullValueHandling.Ignore)]
        public long? Deaths { get; set; }

        [JsonProperty("kd", NullValueHandling = NullValueHandling.Ignore)]
        public double? Kd { get; set; }

        [JsonProperty("headshots", NullValueHandling = NullValueHandling.Ignore)]
        public long? Headshots { get; set; }

        [JsonProperty("headshot_percentage", NullValueHandling = NullValueHandling.Ignore)]
        public double? HeadshotPercentage { get; set; }

        [JsonProperty("times_chosen", NullValueHandling = NullValueHandling.Ignore)]
        public long? TimesChosen { get; set; }

        [JsonProperty("bullets_fired", NullValueHandling = NullValueHandling.Ignore)]
        public long? BulletsFired { get; set; }

        [JsonProperty("bullets_hit", NullValueHandling = NullValueHandling.Ignore)]
        public long? BulletsHit { get; set; }

        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Created { get; set; }

        [JsonProperty("last_updated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastUpdated { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public CategoryCategory Category { get; set; }

        [JsonProperty("weapon", NullValueHandling = NullValueHandling.Ignore)]
        public Weapon Weapon { get; set; }
    }

    public partial class CategoryCategory
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("internal_name", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalName { get; set; }

        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }
    }

    public partial class Weapon
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("internal_name", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalName { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public CategoryCategory Category { get; set; }
    }
}