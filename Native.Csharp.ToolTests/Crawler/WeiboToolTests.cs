using Microsoft.VisualStudio.TestTools.UnitTesting;
using Native.Csharp.Tool.Crawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.Tool.Crawler.Tests
{
    [TestClass()]
    public class WeiboToolTests
    {
        [TestMethod()]
        public void GetWeiboByUidTest()
        {
           var res = WeiBoUtil.GetWeiboByUid("1761587065", "1076031761587065", "#剑网3江湖百晓生#").OrderByDescending(p => p.Time).FirstOrDefault();
        }
    }
}