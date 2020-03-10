using Microsoft.VisualStudio.TestTools.UnitTesting;
using Site.Traceless.Tools.Crawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.Tools.Crawler.Tests
{
    [TestClass()]
    public class JavaScriptAnalyzerTests
    {
        [TestMethod()]
        public void EncodeDecAsciiCQTest()
        {
            string value = @"分享&?[],";
            JavaScriptAnalyzer.EncodeDecAsciiCQ(value);
        }
    }
}