using Native.Csharp.App.Model;
using Native.Csharp.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Native.Csharp.Tool.Utils;
using System.IO;

namespace Native.Csharp.App.Command
{
    public class GroupApp
    {
        public static void test(GroupMessageEventArgs args, AnalysisMsg msg)
        {
            
            String res = args.FromQQ + " 发送:" + msg.OriginStr;
            Common.CqApi.SendGroupMessage(args.FromGroup, res);
        }

        public static void chose(GroupMessageEventArgs args, AnalysisMsg msg)
        {
            List<GroupMember> memberInfos = null;
            DateTime now = DateTime.Now;
            String filePath = Common.groupMemberPath + args.FromGroup+".json";
            Encoding encoding = Encoding.UTF8;
            if (File.Exists(filePath))
            {
                DateTime dt = File.GetCreationTime(filePath);
                if (dt.DayOfYear == now.DayOfYear)
                {
                    memberInfos =Newtonsoft.Json.JsonConvert.DeserializeObject<List<GroupMember>>(FileUtil.GetFileText(filePath, encoding));
                }
                else
                {
                    Common.CqApi.GetMemberList(args.FromGroup, out memberInfos);
                    FileUtil.WriteFileText(filePath, encoding, Newtonsoft.Json.JsonConvert.SerializeObject(memberInfos));
                }
                
            }
            else
            {
                Common.CqApi.GetMemberList(args.FromGroup, out memberInfos);
                FileUtil.WriteFileText(filePath, encoding,Newtonsoft.Json.JsonConvert.SerializeObject(memberInfos));
            }
            var str = msg.Who;
            var orderid = Guid.NewGuid().ToString("N").Substring(0, 5);
            Common.CqApi.SendGroupMessage(args.FromGroup,
                $"5S后,开始从{memberInfos.Count()}人中抽取幸运锦鲤{str}!"+
                Environment.NewLine+
                $"锦鲤编号:{orderid}");
            System.Threading.Thread.Sleep(5000);
            int choseQQIndex = RandomUtil.RandomGet(0, memberInfos.Count());
            long choseQQ = memberInfos.ToArray()[choseQQIndex].QQId;
            string choseQQStr = "我自己！没想到吧！";
            if (choseQQ != Common.CqApi.GetLoginQQ())
            {
                choseQQStr = Common.CqApi.CqCode_At(choseQQ)+ Environment.NewLine + "ヽ(●-`Д´-)ノ！" ;
            }
            Common.CqApi.SendGroupMessage(args.FromGroup,
                $"Boom!{orderid}号{str}的锦鲤为！" +
                Environment.NewLine +
                choseQQStr);

        }

        public static void serverRemind(GroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.ServerRemind.GoServerRemind(args.FromGroup, msg.Who);
        }

        public static void serverQuery(GroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.ServerRemind.GoServerQuery(args.FromGroup, msg.Who);
        }

        public static void roll(GroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.CqApi.SendGroupMessage(args.FromGroup, Common.CqApi.CqCode_At(args.FromQQ) + $"Roll了 {RandomUtil.RandomGet(0, 101)} 点");
        }

        public static void advise(GroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.CqApi.SendPrivateMessage(Common.masterQQ, $"来自群{args.FromGroup}的{args.FromQQ}:{msg.Who} {msg.How}");
        }

        public static void menu(GroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.CqApi.SendGroupMessage(args.FromGroup, Common.menuStr);
        }
    }
}
