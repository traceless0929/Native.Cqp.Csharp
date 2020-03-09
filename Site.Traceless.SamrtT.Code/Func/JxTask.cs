using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;

namespace Site.Traceless.SamrtT.Code.Func
{
    public static class JxTask
    {
        public static string getTask()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                JObject resp = Tools.Http.HttpHelper.GetAPI<JObject>(@"https://www.nicemoe.cn/dailylist.php");
                if (resp["code"].ToString() != "1")
                {
                    sb.AppendLine("[查日常]查询失败，请联系管理员QAQ");
                }
                else
                {
                    sb.AppendLine("[查日常] " + resp["时间"].ToString() + " " + resp["星期"].ToString());
                    sb.AppendLine(Environment.NewLine + "---------日常---------");
                    sb.AppendLine(" 「 大战 」" + resp["秘境大战"].ToString());
                    sb.AppendLine(" 「 战场 」" + resp["今日战场"].ToString());
                    sb.AppendLine(" 「 公共 」" + resp["公共任务"].ToString());
                    sb.AppendLine(" 「 备注 」日常七点刷新");
                    sb.AppendLine(Environment.NewLine + "---------周常---------");
                    sb.AppendLine(" 「 公共任务 」");
                    resp["武林通鉴·公共任务"].ToString().Split(',').ToList().ForEach(p =>
                    {
                        sb.AppendLine("  " + p);
                    });
                    sb.AppendLine(" 「 秘境任务 」");
                    resp["武林通鉴·秘境任务"].ToString().Split(',').ToList().ForEach(p =>
                    {
                        sb.AppendLine("  " + p);
                    });
                    sb.AppendLine(" 「 团队秘境 」");
                    resp["武林通鉴·团队秘境"].ToString().Split(',').ToList().ForEach(p =>
                    {
                        sb.AppendLine("  " + p);
                    });
                    sb.AppendLine("「备注」周常十二点刷新");

                    sb.AppendLine(Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                Common.CqApi.SendPrivateMessage(long.Parse(Common.settingDic["master"]), e.ToString());
                sb.AppendLine("[查日常]QAQ日常服务器异常，已通知管理员");
                return sb.ToString();
            }
        }
    }
}