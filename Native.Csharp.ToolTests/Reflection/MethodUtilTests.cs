using Microsoft.VisualStudio.TestTools.UnitTesting;
using Native.Csharp.Tool.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.Tool.Reflection.Tests
{
    [TestClass()]
    public class MethodUtilTests
    {
        [TestMethod()]
        public void runMethodTest()
        {
            string res = MethodUtil.runMethod<string>("Native.Csharp.Tool", "Native.Csharp.Tool.Reflection.MethodUtil", "test", "hhhh", 1);
            if (res != "hhhh1")
            {
                Assert.Fail();
            }
            
        }
    }
}