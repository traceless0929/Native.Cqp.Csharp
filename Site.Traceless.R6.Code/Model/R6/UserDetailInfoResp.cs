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
        public string avatar_url_146 { get; set; }
        public string avatar_url_256 { get; set; }
        public bool avatar_banned { get; set; }
        public DateTime last_updated { get; set; }
        public Alias[] aliases { get; set; }
        public object[] profiles { get; set; }
        public Progression progression { get; set; }
        public Stat[] stats { get; set; }
        public Operator[] operators { get; set; }
    }

    public class Progression
    {
        public long level { get; set; }
        public long lootbox_probability { get; set; }
        public long total_xp { get; set; }
    }

    public class Alias
    {
        public string username { get; set; }
        public DateTime last_seen_at { get; set; }
    }

    public class Stat
    {
        public General general { get; set; }
        public Queue queue { get; set; }
        public Gamemode gamemode { get; set; }
        public Timestamps timestamps { get; set; }
    }

    public class General
    {
        public long assists { get; set; }
        public long barricades_deployed { get; set; }
        public long blind_kills { get; set; }
        public long bullets_fired { get; set; }
        public long bullets_hit { get; set; }
        public long dbnos { get; set; }
        public long deaths { get; set; }
        public long distance_travelled { get; set; }
        public long draws { get; set; }
        public long gadgets_destroyed { get; set; }
        public long games_played { get; set; }
        public long headshots { get; set; }
        public float kd { get; set; }
        public long kills { get; set; }
        public long losses { get; set; }
        public long melee_kills { get; set; }
        public long penetration_kills { get; set; }
        public long playtime { get; set; }
        public long rappel_breaches { get; set; }
        public long reinforcements_deployed { get; set; }
        public long revives { get; set; }
        public long suicides { get; set; }
        public long wins { get; set; }
        public float wl { get; set; }
    }

    public class Queue
    {
        public Casual casual { get; set; }
        public Ranked ranked { get; set; }
        public Other other { get; set; }
    }

    public class Casual
    {
        public long deaths { get; set; }
        public long draws { get; set; }
        public long games_played { get; set; }
        public float kd { get; set; }
        public long kills { get; set; }
        public long losses { get; set; }
        public long playtime { get; set; }
        public long wins { get; set; }
        public float wl { get; set; }
    }

    public class Ranked
    {
        public long deaths { get; set; }
        public long draws { get; set; }
        public long games_played { get; set; }
        public float kd { get; set; }
        public long kills { get; set; }
        public long losses { get; set; }
        public long playtime { get; set; }
        public long wins { get; set; }
        public float wl { get; set; }
    }

    public class Other
    {
        public long deaths { get; set; }
        public long draws { get; set; }
        public long games_played { get; set; }
        public float kd { get; set; }
        public long kills { get; set; }
        public long losses { get; set; }
        public long playtime { get; set; }
        public long wins { get; set; }
        public float wl { get; set; }
    }

    public class Gamemode
    {
        public Bomb bomb { get; set; }
        public Secure_Area secure_area { get; set; }
        public Hostage hostage { get; set; }
    }

    public class Bomb
    {
        public long best_score { get; set; }
        public long games_played { get; set; }
        public long losses { get; set; }
        public long playtime { get; set; }
        public long wins { get; set; }
        public float wl { get; set; }
    }

    public class Secure_Area
    {
        public long best_score { get; set; }
        public long games_played { get; set; }
        public long kills_as_attacker_in_objective { get; set; }
        public long kills_as_defender_in_objective { get; set; }
        public long losses { get; set; }
        public long playtime { get; set; }
        public long times_objective_secured { get; set; }
        public long wins { get; set; }
        public float wl { get; set; }
    }

    public class Hostage
    {
        public long best_score { get; set; }
        public long games_played { get; set; }
        public long losses { get; set; }
        public long playtime { get; set; }
        public long extractions_denied { get; set; }
        public long wins { get; set; }
        public float wl { get; set; }
    }

    public class Timestamps
    {
        public DateTime created { get; set; }
        public DateTime last_updated { get; set; }
    }

    public class Operator
    {
        public long kills { get; set; }
        public long deaths { get; set; }
        public float kd { get; set; }
        public long wins { get; set; }
        public long losses { get; set; }
        public float wl { get; set; }
        public long headshots { get; set; }
        public long dbnos { get; set; }
        public long melee_kills { get; set; }
        public long experience { get; set; }
        public long playtime { get; set; }
        public Ability[] abilities { get; set; }
        public Operator1 @operator { get; set; }
    }

    public class Operator1
    {
        public string name { get; set; }
        public string longernal_name { get; set; }
        public string role { get; set; }
        public string ctu { get; set; }
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
        public string key { get; set; }
        public string title { get; set; }
        public long value { get; set; }
    }


}
