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
            sb.AppendLine($"[代码更新]{repository.name}:{hook_Github._ref.Substring(hook_Github._ref.LastIndexOf("//")+1)}");
            //提交信息
            List<Commit> commits = hook_Github.commits.ToList();
            sb.AppendLine($"有 {commits.Count} 个新的提交 by {hook_Github.head_commit.committer.name}");
            foreach(Commit commit in commits)
            {
                sb.AppendLine($"{commit.id.Substring(0, 8)} {commit.message}-{commit.committer.name}");
            }
            sb.AppendLine("代码都更新了，功能还会远么？");
            sb.AppendLine("更新时间:"+DateTime.Now.ToString());
            return sb.ToString();
        }
    }
}
