using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Enum;
using Native.Csharp.Sdk.Cqp.Model;
using Site.Traceless.Tools.Utils;

namespace Site.Traceless.SamrtT.Code.Utils
{
    public static class CQUtils
    {
        public static CQCode GetHttpImgCqCode(string url,string name)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "image");
            string fileName = name + ".jpg";
            if (!File.Exists(Path.Combine(path, fileName)))
            {
                Tools.Http.HttpHelper.DownUrlPic(url, path, fileName);
            }
            return new CQCode(CQFunction.Image, new KeyValuePair<string, string>("file", fileName));
        }

        public static CQCode GetHttpImgCqCode(string url)
        {
            return GetHttpImgCqCode(url, DateTime.Now.Ticks.ToString()+new Random().Next());
        }
    }
}
