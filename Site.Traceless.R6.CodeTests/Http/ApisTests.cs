using Microsoft.VisualStudio.TestTools.UnitTesting;
using Site.Traceless.R6.Code.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Traceless.R6.Code.Model.R6;

namespace Site.Traceless.R6.Code.Http.Tests
{
    [TestClass()]
    public class ApisTests
    {
        [TestMethod()]
        public void GetUserSeasonInfoTest()
        {
            UserBaseInfoResp req = new UserBaseInfoResp();
            req.uplay_id = "916fa947-d69d-4617-b133-ac6ab2735773";
            UserSeasonResp resp = Apis.GetUserSeasonInfo(req);
            resp.GetSeasons();
        }
    }
}