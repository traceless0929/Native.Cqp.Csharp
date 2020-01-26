using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Site.Traceless.Tools.Http
{
	/// <summary>
	/// Http访问的操作类
	/// </summary>
	public static class HttpHelper
	{

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


        public static string DownUrlPic(string url,string path,string name)
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.Headers.Add("User-Agent", DefaultUserAgent);
                wc.DownloadFile(url, Path.Combine(path,name));
                return name;
            }
        }
    }
}
