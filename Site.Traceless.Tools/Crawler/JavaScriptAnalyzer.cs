using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Site.Traceless.Tools.Crawler
{
    public class JavaScriptAnalyzer
    {
        private static Regex reUnicode = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);

        public static string Decode(string s)
        {
            var str = reUnicode.Replace(s, m =>
            {
                short c;
                if (short.TryParse(m.Groups[1].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out c))
                {
                    return "" + (char)c;
                }
                return m.Value;
            });
            str = HttpUtility.HtmlDecode(str);
            return str;
        }

        private static Regex reUnicodeDec = new Regex(@"\&\#([0-9]{2,4})\;", RegexOptions.Compiled);

        public static string EncodeDecAsciiCQ(string s)
        {
            StringBuilder sb = new StringBuilder();
            char[] charBuf = s.ToArray();
            ASCIIEncoding charToASCII = new ASCIIEncoding();
            foreach(var b in charBuf)
            {
                //byte[] TxdBuf = new byte[1];    // 定义发送缓冲区；

                //TxdBuf = charToASCII.GetBytes(charBuf); 　　 // 转换为各字符对应的ASCII
                short c = (short)b;
                if (c == 44 || c == 91 || c == 93)
                {
                    sb.Append(@"&#" + c + ";");
                }
                else if (c == 38)
                {
                    sb.Append(@"&amp");
                }
                else
                {
                    sb.Append(b);
                }
            }
            
            return sb.ToString();
        }
    }
}