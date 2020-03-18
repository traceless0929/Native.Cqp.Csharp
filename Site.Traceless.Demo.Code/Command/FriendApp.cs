using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.Demo.Code.Model;

namespace Site.Traceless.Demo.Code.Command
{
    public class FriendApp
    {
        public static void funcOne(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(e.FromQQ,$"[这里是私聊方法1]",$"参数数 {msg.OrderCount}\n", $"触发指令(第一参数 what) {msg.What}\n", $"目标(第二参数 who) {msg.Who}\n", $"怎么做(第三参数 how) {msg.How}\n", $"原始信息 {msg.OriginStr}\n", e.ToString());
        }
        public static void funcTwo(CQPrivateMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(e.FromQQ,$"[这里是私聊方法2]", $"参数数 {msg.OrderCount}\n", $"触发指令(第一参数 what) {msg.What}\n", $"目标(第二参数 who) {msg.Who}\n", $"怎么做(第三参数 how) {msg.How}\n", $"原始信息 {msg.OriginStr}\n", e.ToString());
        }
    }
}
