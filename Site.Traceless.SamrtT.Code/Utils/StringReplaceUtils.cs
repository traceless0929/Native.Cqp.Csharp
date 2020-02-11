using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp;
using Native.Csharp.Sdk.Cqp.Enum;
using Native.Csharp.Sdk.Cqp.Expand;
using Native.Csharp.Sdk.Cqp.Model;
using Site.Traceless.SamrtT.Code.Func;
using Site.Traceless.SamrtT.Code.Model.Extend;
using Site.Traceless.Tools.Crawler;
using Group = Native.Csharp.Sdk.Cqp.Model.Group;

namespace Site.Traceless.SamrtT.Code.Utils
{
    public static class StringReplaceUtils
    {
        public static string ReplaceGroupMemberInfo(this string raw, Group gGroup,QQ fromQQ,QQ beingQQ)
        {
            GroupInfo groupInfo = gGroup.GetGroupInfo();
            GroupMemberInfo fromQQInfo = null== fromQQ?null:gGroup.GetGroupMemberInfo(fromQQ);
            GroupMemberInfo beingQQInfo = null==beingQQ?null:gGroup.GetGroupMemberInfo(beingQQ);
            raw = raw.Replace("[群号]", gGroup.Id + "")
                .Replace("[群名]", groupInfo.Name)
                .Replace("[群人数]", groupInfo.CurrentMemberCount + "")
                .Replace("[群上限]", groupInfo.MaxMemberCount + "");

            if (null != fromQQInfo)
            {
                raw = raw.Replace("[操作者QQ]", fromQQInfo.QQ + "")
                    .Replace("[操作者年龄]", fromQQInfo.Age + "")
                    .Replace("[操作者地区]", fromQQInfo.Area + "")
                    .Replace("[操作者昵称]", fromQQInfo.Nick + "")
                    .Replace("[操作者角色]", fromQQInfo.MemberType.GetDescription())
                    .Replace("[AT操作者]", fromQQ.CQCode_At() + "")
                    .Replace("[操作者性别]", fromQQInfo.Sex.GetDescription() + "")
                    .Replace("[操作者头像]", GetHeadCode(fromQQ.Id).ToSendString() + "");
            }

            if (null != beingQQInfo)
            {
                raw = raw.Replace("[进群者QQ]", beingQQInfo.QQ + "")
                    .Replace("[进群者年龄]", beingQQInfo.Age + "")
                    .Replace("[进群者地区]", beingQQInfo.Area + "")
                    .Replace("[进群者昵称]", beingQQInfo.Nick + "")
                    .Replace("[AT进群者]", beingQQ.CQCode_At() + "")
                    .Replace("[进群者性别]", beingQQInfo.Sex.GetDescription() + "")
                    .Replace("[进群者头像]", GetHeadCode(beingQQ.Id).ToSendString() + "");
            }
            return raw;
        }

        public static string ReplaceNotice(this string raw,long qid, CQApi cqApi)
        {
            Feed[] noticeList = null;
            if (raw.Contains("[公告标题]") || raw.Contains("[公告内容]"))
            {
                GroupNoticeResp groupNotice = QQExtend.getGroupNotice(cqApi.GetCsrfToken() + "", qid, cqApi.GetCookies("qun.qq.com"));
                if (groupNotice.ec == 0)
                {
                    noticeList = groupNotice.feeds;
                }
                raw = raw
                    .Replace("[公告标题]", (noticeList == null || noticeList.Length < 1) ? "" : noticeList[0].msg.title)
                    .Replace("[公告内容]", (noticeList == null || noticeList.Length < 1) ? "" : noticeList[0].msg.text);
            }

            return raw;
        }

        public static string ReplaceTrimAndLine(this string raw)
        {
            raw = raw.Replace("[空格]", " ")
                .Replace("[换行]", Environment.NewLine);
            return raw;
        }

        public static string DeReplaceTrimAndLine(this string raw)
        {
            raw = raw.Replace("\n", "[换行]")
                .Replace(" ", "[空格]");
            return raw;
        }

        public static string ReplaceAtQQ(this string raw)
        {
            var regexCode = @"\[AT([1-9][0-9]{4,})*\]";
            var res = GetRegexStr(raw, regexCode);
            if (null == res || !res.Any())
            {
                return raw;
            }

            bool parseOk = long.TryParse(res[0],out long qqAt);
            return parseOk?raw.Replace("[AT" + qqAt + "]",
                new CQCode(CQFunction.At, new KeyValuePair<string, string>("qq", Convert.ToString(qqAt)))
                    .ToSendString()):raw;
        }

        public static CQCode GetHeadCode(long qq)
        {
            string headUrl = @"http://q1.qlogo.cn/g?b=qq&nk=" + qq + "&s=100";
            return CQUtils.GetHttpImgCqCode(headUrl,"head"+qq);
        }

        private static List<string> GetRegexStr(string reString,string regexCode)
        {
            //注意 reString 请替换为需要处理的字符串
            List<string> strList = new List<string>();
            var reg = new Regex(regexCode);
            MatchCollection mc = reg.Matches(reString);
            for (int i = 0; i < mc.Count; i++)
            {
                GroupCollection gc = mc[i].Groups; //得到所有分组 
                for (int j = 1; j < gc.Count; j++) //多分组 匹配的原始文本不要
                {
                    string temp = gc[j].Value;
                    if (!string.IsNullOrEmpty(temp))
                    {
                        strList.Add(temp); //获取结果   strList中为匹配的值
                    }
                }
            }
            //需要获取匹配的数据,请遍历strList  通常情况下(正则表达式中只有一个分组),只需要取strList[1]即可. 如果有多个分组,依次类推即可.
            return strList;
        }
    }
}
