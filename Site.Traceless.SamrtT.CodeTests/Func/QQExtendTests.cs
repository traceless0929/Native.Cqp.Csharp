using Microsoft.VisualStudio.TestTools.UnitTesting;
using Site.Traceless.SamrtT.Code.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Site.Traceless.SamrtT.Code.Model.Extend;
using Site.Traceless.Tools.Crawler;

namespace Site.Traceless.SamrtT.Code.Func.Tests
{
    [TestClass()]
    public class QQExtendTests
    {
        [TestMethod()]
        public void getGroupNoticeTest()
        {
            GroupNoticeResp groupNotice = QQExtend.getGroupNotice("45409227", 701084967,
                @"uin=o3164170991; skey=MQEs1SDqiN; p_uin=o3164170991; p_skey=7j4jkIKTCBnD1cXv5oYCWQuor3nvgBtZjW*335c5Zds_");
            JavaScriptAnalyzer.Decode(JsonConvert.SerializeObject(groupNotice));
        }
    }
}