using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.R6.Code.Http
{
    public class Utils
    {
        public static string ConvertToRankDes(int? rank)
        {
            if (rank==null||rank == 0) return "无";
            rank = rank - 1;
            int rankAera = rank.Value / 4;
            int rankLevel = 4 - (rank.Value % 4);
            StringBuilder sb = new StringBuilder();
            switch (rankAera)
            {
                case 0:
                    sb.Append("紫铜");
                    break;
                case 1:
                    sb.Append("青铜");
                    break;
                case 2:
                    sb.Append("白银");
                    break;
                case 3:
                    sb.Append("黄金");
                    break;
                case 4:
                    sb.Append("白金");
                    break;
                case 5:
                    sb.Append("钻石");
                    break;

            }
            sb.Append(rankLevel);
            return sb.ToString();
        }

        /// <summary>
        /// 调用GET API
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetAPI(string url)
        {
            System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            System.Net.HttpWebResponse result = request.GetResponse() as System.Net.HttpWebResponse;
            System.IO.StreamReader sr = new System.IO.StreamReader(result.GetResponseStream(), System.Text.Encoding.UTF8);
            string strResult = sr.ReadToEnd();
            sr.Close();
            //Console.WriteLine(strResult);
            return strResult.Replace(" ", "").Replace("\n", "");
        }

        /// <summary>
        /// 调用GET API
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetAPI<T>(string url) where T : class
        {
            System.Net.HttpWebRequest request = System.Net.WebRequest.Create(url) as System.Net.HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            System.Net.HttpWebResponse result = request.GetResponse() as System.Net.HttpWebResponse;
            System.IO.StreamReader sr = new System.IO.StreamReader(result.GetResponseStream(), System.Text.Encoding.UTF8);
            string strResult = sr.ReadToEnd();
            var res = JsonConvert.DeserializeObject<T>(strResult.Replace(" ", "").Replace("\n", ""));
            sr.Close();
            //Console.WriteLine(strResult);
            return res;
        }

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
    }
}
