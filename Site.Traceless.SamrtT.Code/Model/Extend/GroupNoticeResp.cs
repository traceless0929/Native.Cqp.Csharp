namespace Site.Traceless.SmartT.Code.Model.Extend
{
    // { "ec": 0, "em": "", "ltsm": 1581237450, "srv_code": 0, "read_only": 0, "role": 1, "feeds": [
    // { "u": 415206409, "fid": "27b5c92900000000625af05d5e220900", "pubt": 1576032866, "msg": {
    // "text": "测试", "text_face": "测试", "title": "测试公告" }, "type": 6, "fn": 0, "cn": 0, "vn": 0,
    // "settings": { "is_show_edit_card": 0, "remind_ts": 0, "tip_window_type": 0 }, "read_num": 6 }
    // ], "group": { "group_id": 701084967, "class_ext": 33 }, "sta": 1, "gln": 0, "tst": 10, "ui":
    // { "415206409": { "n": "return&nbsp;true;", "f":
    // "http://thirdqq.qlogo.cn/g?b=oidb&amp;k=Z6ByEIFFnFl1PAzSpAictnw&amp;s=40" }, "3164170991": {
    // "n": "影妹", "f": "http://thirdqq.qlogo.cn/g?b=oidb&amp;k=0e0c3Ef2tzkku3638WiavjQ&amp;s=40" }
    // }, "server_time": 1581237450000, "svrt": 1581237450, "ad": 0 }
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
    }
}