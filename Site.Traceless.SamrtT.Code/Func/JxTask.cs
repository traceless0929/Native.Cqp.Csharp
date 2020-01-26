using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Site.Traceless.SamrtT.Code.Model.SmartT;

namespace Site.Traceless.SamrtT.Code.Func
{
    public static class JxTask
    {
        public static string getTask()
        {
            StringBuilder sb = new StringBuilder();
            JObject resp = Tools.Http.HttpHelper.GetAPI<JObject>(@"http://nico.nicemoe.cn/moe/content/");
            if (resp["code"].ToString() != "200")
            {
                sb.AppendLine("[查日常]查询失败，请联系管理员QAQ");
            }
            else
            {
                JToken timeInfo = resp["时间"].FirstOrDefault();
                JToken dayInfo = resp["日常"].FirstOrDefault();
                JToken weekInfo = resp["周常"].FirstOrDefault();
                if (timeInfo.HasValues)
                {
                    sb.AppendLine("[查日常] "+timeInfo["日期"].ToString() + " " + timeInfo["星期"].ToString());
                }
                if (dayInfo.HasValues)
                {
                    sb.AppendLine(Environment.NewLine + "---------日常---------");
                    foreach (var dayItem in dayInfo)
                    {
                        sb.AppendLine("「" + dayItem.Path.Split('.').LastOrDefault() + "」" + dayItem.FirstOrDefault().ToString() + "");
                    }
                    sb.AppendLine("「备注」日常七点刷新");
                }
                if (weekInfo.HasValues)
                {
                    sb.AppendLine(Environment.NewLine + "---------周常---------");
                    foreach (var weekItem in weekInfo)
                    {
                        sb.AppendLine("「" + weekItem.Path.Split('.').LastOrDefault() + "」" + weekItem.FirstOrDefault().ToString() + "");
                    }
                    sb.AppendLine("「备注」周常十二点刷新");
                }

                sb.AppendLine(Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            return sb.ToString();
        }
    }
}
