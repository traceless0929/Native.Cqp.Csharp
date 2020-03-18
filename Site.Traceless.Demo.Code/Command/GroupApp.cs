using Site.Traceless.Demo.Code.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;

namespace Site.Traceless.Demo.Code.Command
{
    public class GroupApp
    {
        public static void funcOne(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(415206409,$"[这里是群方法1]", $"参数数 {msg.OrderCount}\n", $"触发指令(第一参数 what) {msg.What}\n", $"目标(第二参数 who) {msg.Who}\n", $"怎么做(第三参数 how) {msg.How}\n", $"原始信息 {msg.OriginStr}\n", e.ToString());
        }
        public static void funcTwo(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(415206409, $"[这里是群方法2]\n", $"参数数 {msg.OrderCount}\n", $"触发指令(第一参数 what) {msg.What}\n", $"目标(第二参数 who) {msg.Who}\n", $"怎么做(第三参数 how) {msg.How}\n", $"原始信息 {msg.OriginStr}\n", e.ToString());
        }
    }
}
