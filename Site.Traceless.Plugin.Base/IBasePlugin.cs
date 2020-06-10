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
        String Name { get; set; }
        string GCommand { get; set; }
        string PCommand { get; set; }

        string DoGroup(CQGroupMessageEventArgs e, AnalysisMsg msg);

        string DoPrivate(CQPrivateMessageEventArgs e, AnalysisMsg msg);
    }
}