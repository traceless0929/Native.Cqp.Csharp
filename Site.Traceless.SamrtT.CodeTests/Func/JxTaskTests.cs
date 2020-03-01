using Microsoft.VisualStudio.TestTools.UnitTesting;
using Site.Traceless.SamrtT.Code.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Site.Traceless.SamrtT.Code.Utils;

namespace Site.Traceless.SamrtT.Code.Func.Tests
{
    [TestClass()]
    public class JxTaskTests
    {
        [TestMethod()]
        public void getTaskTest()
        {
            JObject resp = Tools.Http.HttpHelper.GetAPI<JObject>(@"https://www.nicemoe.cn/dailylist.php");
        }
    }
}