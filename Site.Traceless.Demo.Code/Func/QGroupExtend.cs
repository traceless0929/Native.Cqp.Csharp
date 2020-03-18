using Newtonsoft.Json;
using Site.Traceless.Demo.Code.Model;
using Site.Traceless.Demo.Code.Tools;

namespace Site.Traceless.Demo.Code.Func
{
    public static class QGroupExtend
    {
        public static string GROUP_NOTICE_STR = @"https://web.qun.qq.com/cgi-bin/announce/get_t_list";

        public static GroupNoticeResp getGroupNotice(string bkn, long gid, string cookieStr)
        {
            string url = $"{GROUP_NOTICE_STR}?bkn={bkn}&qid={gid}&ft=23&s=-1&n=10&ni=1&i=1";
            return JsonConvert.DeserializeObject<GroupNoticeResp>(JsonConvert.SerializeObject(HttpHelper.PostAPI<GroupNoticeResp>(url, cookieStr)));
        }
    }
}