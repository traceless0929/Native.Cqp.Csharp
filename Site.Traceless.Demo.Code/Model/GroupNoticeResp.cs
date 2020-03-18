using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.Demo.Code.Model
{

    public class GroupNoticeResp
    {
        public int ec { get; set; }
        public string em { get; set; }
        public int ltsm { get; set; }
        public int srv_code { get; set; }
        public int read_only { get; set; }
        public int role { get; set; }
        public Feed[] feeds { get; set; }
        public Group group { get; set; }
        public int sta { get; set; }
        public int gln { get; set; }
        public int tst { get; set; }
        public long server_time { get; set; }
        public int svrt { get; set; }
        public int ad { get; set; }
    }

    public class Group
    {
        public int group_id { get; set; }
        public int class_ext { get; set; }
    }

    public class Feed
    {
        public int u { get; set; }
        public string fid { get; set; }
        public int pubt { get; set; }
        public Msg msg { get; set; }
        public int type { get; set; }
        public int fn { get; set; }
        public int cn { get; set; }
        public int vn { get; set; }
        public Settings settings { get; set; }
        public int read_num { get; set; }
        public int is_read { get; set; }
        public int is_all_confirm { get; set; }
    }

    public class Msg
    {
        public string text { get; set; }
        public string text_face { get; set; }
        public string title { get; set; }
    }

    public class Settings
    {
        public int is_show_edit_card { get; set; }
        public int remind_ts { get; set; }
        public int tip_window_type { get; set; }
        public int confirm_required { get; set; }
    }

}
