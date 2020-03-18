using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.Demo.Code.Func;
using Site.Traceless.Demo.Code.Model;
using System;
using System.Text;

namespace Site.Traceless.Demo.Code.Command
{
    public class FriendApp
    {
        public static void funcOne(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(e.FromQQ, $"[这里是私聊方法1]", $"参数数 {msg.OrderCount}\n", $"触发指令(第一参数 what) {msg.What}\n", $"目标(第二参数 who) {msg.Who}\n", $"怎么做(第三参数 how) {msg.How}\n", $"原始信息 {msg.OriginStr}\n", e.ToString());
        }

        public static void funcTwo(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(e.FromQQ, $"[这里是私聊方法2]", $"参数数 {msg.OrderCount}\n", $"触发指令(第一参数 what) {msg.What}\n", $"目标(第二参数 who) {msg.Who}\n", $"怎么做(第三参数 how) {msg.How}\n", $"原始信息 {msg.OriginStr}\n", e.ToString());
        }

        public static void getGNotice(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            //获取公告内容
            GroupNoticeResp resp = QGroupExtend.getGroupNotice(e.CQApi.GetCsrfToken() + "", Convert.ToInt32(msg.Who), e.CQApi.GetCookies("qun.qq.com"));
            if (resp == null || resp.feeds == null)
            {
                //公告为空
                e.CQApi.SendPrivateMessage(e.FromQQ, "抱歉，公告为空");
                return;
            }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.AppendLine($"总计公告({resp.feeds.Length}条");
            foreach (Feed feed in resp.feeds)
            {
                sb.AppendLine($"{i+1}.{feed.msg.title}");
                sb.AppendLine($"内容:");
                sb.AppendLine($"{feed.msg.text}");
                i++;
            }
            e.CQApi.SendPrivateMessage(e.FromQQ.Id, sb.ToString());
        }
    }
}