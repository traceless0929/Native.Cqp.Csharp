using Native.Csharp.App.Model;
using Native.Csharp.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Native.Csharp.Tool.Utils;
using System.IO;
using Native.Csharp.App.EventArgs;
using Native.Csharp.App.Model.respModel;
using Native.Csharp.Tool.IniConfig.Linq;

namespace Native.Csharp.App.Command
{
    public class GroupApp
    {
        public static void test(CqGroupMessageEventArgs args, AnalysisMsg msg)
        {
            
            String res = args.FromQQ + " 发送:" + msg.OriginStr;
            Common.CqApi.SendGroupMessage(args.FromGroup, res);
        }

        public static void chose(CqGroupMessageEventArgs args, AnalysisMsg msg)
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
                    memberInfos = Common.CqApi.GetMemberList(args.FromGroup);
                    GroupMember zero = memberInfos.Where(p => p.GroupId == 0L || p.QQId == 0L).FirstOrDefault();
                    if (null != zero)
                    {
                        memberInfos.Remove(zero);
                    }
                    FileUtil.WriteFileText(filePath, encoding, Newtonsoft.Json.JsonConvert.SerializeObject(memberInfos));
                }
                
            }
            else
            {
                memberInfos = Common.CqApi.GetMemberList(args.FromGroup);
                GroupMember zero = memberInfos.Where(p => p.GroupId == 0L || p.QQId == 0L).FirstOrDefault();
                if (null != zero)
                {
                    memberInfos.Remove(zero);
                }
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

        public static void serverRemind(CqGroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.ServerRemind.GoServerRemind(args.FromGroup, msg.Who);
        }

        public static void serverQuery(CqGroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.ServerRemind.GoServerQuery(args.FromGroup, msg.Who);
        }

        public static void roll(CqGroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.CqApi.SendGroupMessage(args.FromGroup, Common.CqApi.CqCode_At(args.FromQQ) + $"Roll了 {RandomUtil.RandomGet(0, 101)} 点");
        }

        public static void advise(CqGroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.CqApi.SendPrivateMessage(Common.getSetting<long>("master"), $"来自群{args.FromGroup}的{args.FromQQ}:{msg.Who} {msg.How}");
        }

        public static void menu(CqGroupMessageEventArgs args, AnalysisMsg msg)
        {
            Common.CqApi.SendGroupMessage(args.FromGroup, Common.menuStr);
        }

        public static void trashsort(CqGroupMessageEventArgs args, AnalysisMsg msg)
        {
            if (string.IsNullOrEmpty(msg.Who))
            {
                return;
            }
            Common.CqApi.SendGroupMessage(args.FromGroup, Extend.TrashSort.goSort(msg.Who));
        }

        public static void dayTask(CqGroupMessageEventArgs args, AnalysisMsg msg)
        {
            Tool.Crawler.WeiBoContentItem contentItem = Tool.Crawler.WeiBoUtil.GetWeiboByUid("1761587065", "1076031761587065", "#剑网3江湖百晓生#").OrderByDescending(p => p.Time).FirstOrDefault();
            DateTime dt = DateTime.Now;
            if (contentItem != null)
            {
                if (msg.OriginStr.Contains("文")){
                    Common.CqApi.SendGroupMessage(args.FromGroup, "[日常]来自 " + contentItem.Author + "：" + Environment.NewLine +
                        contentItem.ContentStr + Environment.NewLine +
                        $"高清大图-{contentItem.Pic}" + Environment.NewLine +
                        @"本信息由新浪微博-剑网3江湖百晓生-超话提供"
                        );
                }
                else
                {
                    Common.CqApi.SendGroupMessage(args.FromGroup, "[日常]来自 " + contentItem.Author + "：" + Environment.NewLine +
                        contentItem.ContentStr + Environment.NewLine +
                        @"本信息由新浪微博-剑网3江湖百晓生-超话提供"
                        );
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "image");
                    string fileName = "daytask" + contentItem.Time.ToString("yyyyMMdd") + ".jpg";
                    if (!File.Exists(Path.Combine(path, fileName)))
                    {
                        Tool.Http.HttpHelper.DownUrlPic(contentItem.Pic, path, fileName);
                    }
                    Common.CqApi.SendGroupMessage(args.FromGroup, Common.CqApi.CqCode_Image(fileName));
                }
            }
            else
            {
                Common.CqApi.SendGroupMessage(args.FromGroup, "[日常]天哪噜！QAQ官微又偷懒了！");
            }
        }
    }
}
