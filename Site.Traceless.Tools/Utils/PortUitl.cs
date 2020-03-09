using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Site.Traceless.Tools.Utils
{
    public class PortUitl
    {
        public static bool Ping(string ip)
        {
            Ping ping = new Ping();
            PingOptions pingOptions = new PingOptions()
            {
                DontFragment = true
            };
            string s = "Test Data!";
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            int timeout = 1000;
            PingReply pingReply = ping.Send(ip, timeout, bytes, pingOptions);
            return pingReply.Status == IPStatus.Success;
        }

        public static bool IsOpen(string ip, int port)
        {
            bool flag = !string.IsNullOrEmpty(ip);
            bool result;
            if (flag)
            {
                IPAddress address = IPAddress.Parse(ip);
                IPEndPoint remoteEP = new IPEndPoint(address, port);
                try
                {
                    TcpClient tcpClient = new TcpClient();
                    tcpClient.Connect(remoteEP);
                    result = true;
                    return result;
                }
                catch
                {
                    result = false;
                    return result;
                }
            }
            result = false;
            return result;
        }
    }
}