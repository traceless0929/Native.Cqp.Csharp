using Native.Sdk.Cqp.EventArgs;
using Site.Traceless.Common.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.Plugin.Base
{
    public interface IBasePlugin
    {
        string Name { get; set; }
        string Author { get; set; }
        string GCommand { get; set; }
        string PCommand { get; set; }

        bool DoGroup(CQGroupMessageEventArgs e, AnalysisMsg msg);

        bool DoPrivate(CQPrivateMessageEventArgs e, AnalysisMsg msg);
    }
}