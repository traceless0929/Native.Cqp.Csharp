using Microsoft.VisualStudio.TestTools.UnitTesting;
using Site.Traceless.SamrtT.Code.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.SamrtT.Code.Func.Tests
{
    [TestClass()]
    public class JxTaskTests
    {
        [TestMethod()]
        public void getTaskTest()
        {
            JxTask.getTask();
        }
    }
}