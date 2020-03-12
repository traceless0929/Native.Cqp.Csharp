using Site.Traceless.RestService.Model.Req;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.SamrtT.Code.Func
{
    public class Hooks
    {
        public static string OptHookCommit(Hook_Github hook_Github)
        {
            StringBuilder sb = new StringBuilder();
            Repository repository = hook_Github.repository;
            //仓库信息
            sb.AppendLine($"[代码更新]{repository.name}:SmartT_V2");
            //提交信息
            List<Commit> commits = hook_Github.commits.ToList();
            sb.AppendLine($"有 {commits.Count} 个新的提交 by {hook_Github.head_commit.committer.name}");
            string tag = "";
            foreach(Commit commit in commits)
            {
                string tagContent = GetTagContent(commit.message,"ct");
                if (!string.IsNullOrEmpty(tagContent))
                {
                    tag += tagContent;
                }
                sb.AppendLine($"{commit.id.Substring(0, 8)} {commit.message.Replace("[ct]"+tagContent,"")}-{commit.committer.name}");
            }
            sb.AppendLine($"总计修改文件 {commits.Select(P=>P.modified.Length).Sum()} 个\n");
            if (!string.IsNullOrEmpty(tag))
            {
                sb.AppendLine(tag+"\n");
            }
            sb.AppendLine("更新时间:"+DateTime.Now.ToString());
            return sb.ToString();
        }

        private static string GetTagContent(string raw,string tagName) {
            string tag = $"[{tagName}]";
            return raw.Substring(raw.LastIndexOf(tag + tag.Length + 1));
        }
    }
}
