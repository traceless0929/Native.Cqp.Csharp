using Native.Sdk.Cqp.Enum;
using Native.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Site.Traceless.SmartT.Code.Utils
{
    public static class CQUtils
    {
        public static CQCode GetHttpImgCqCode(string url, string name)
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
            return GetHttpImgCqCode(url, DateTime.Now.Ticks.ToString() + new Random().Next());
        }
    }
}