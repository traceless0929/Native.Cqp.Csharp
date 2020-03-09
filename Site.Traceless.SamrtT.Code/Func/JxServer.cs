using Site.Traceless.Tools.Utils;
using System;
using System.Linq;
using System.Text;
using System.Timers;

namespace Site.Traceless.SamrtT.Code.Func
{
    public class JxServer
    {
        public static Timer timer;

        public JxServer()
        {
            if (timer == null)
            {
                timer = new Timer
                {
                    Interval = 1000.0
                };
                timer.Elapsed += SerOpenRemind_Tick;
            }
        }

        private void SerOpenRemind_Tick(object sender, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            for (int i = 0; i < Common.SerList.GetLength(0); i++)
            {
                bool flag = Common.SerList[i, 3] != "1";
                if (!flag)
                {
                    string ip = Common.SerList[i, 2];
                    string[] array = Common.SerList[i, 4].Split(new char[]
                    {
                        '|'
                    });
                    string text = string.Empty;
                    string bigSer = Common.SerList[i, 0];
                    string serName = Common.SerList[i, 1];
                    if (PortUitl.IsOpen(ip, 3724))
                    {
                        text = $"[开服监控]" + bigSer + "-" + serName + " 开服了！";
                        array.Where(p => p != "List").ToList().ForEach(p => { Common.CqApi.SendGroupMessage(long.Parse(p), text); });
                        Common.SerList[i, 3] = "0";
                        Common.SerList[i, 4] = "List";
                    }
                }
            }
            timer.Enabled = true;
        }

        public void GoServerQuery(long clu, string serverstr)
        {
            Common.CqApi.SendGroupMessage(clu, $"正在努力寻找机房烧烤的GWW……");
            string serName = serverstr.Trim();
            string ip = string.Empty;
            string bigSer = string.Empty;
            for (int i = 0; i < Common.SerList.GetLength(0); i++)
            {
                if (serName.Equals(Common.SerList[i, 1]))
                {
                    ip = Common.SerList[i, 2];
                    bigSer = Common.SerList[i, 0];
                    string content = $"[开服查询{serName}]" + Environment.NewLine
                        + (PortUitl.IsOpen(ip, 3724) ? (bigSer + " " + serName + "\r\n开") : (bigSer + " " + serName + "\r\n关"));
                    Common.CqApi.SendGroupMessage(clu, content);
                    return;
                }
                else
                {
                    if (i == Common.SerList.GetLength(0) - 1)
                    {
                        Common.CqApi.SendGroupMessage(clu, " 对不起，没有找到服务器 (づ╥﹏╥)づ");
                    }
                }
            }
        }

        public void GoServerRemind(long clu, string serverstr)
        {
            string serName = serverstr.Trim();
            string bigSer = string.Empty;
            for (int i = 0; i < Common.SerList.GetLength(0); i++)
            {
                if (serName.Equals(Common.SerList[i, 1]))
                {
                    bool existflag = false;
                    bigSer = Common.SerList[i, 0];
                    Common.SerList[i, 3] = "1";
                    string[] cluList = Common.SerList[i, 4].Split('|');
                    foreach (var istr in cluList)
                    {
                        if (istr == clu + "") existflag = true;
                    }
                    if (!existflag)
                    {
                        Common.SerList[i, 4] += "|" + clu;
                    }
                    Common.CqApi.SendGroupMessage(clu, $"[开服监控]" + "已为您开启 " + serName + "的监控~请关注群信息，将第一时间通知到群。");
                    timer.Enabled = true;
                    return;
                }
                else if (i == Common.SerList.GetLength(0) - 1)
                {
                    Common.CqApi.SendGroupMessage(clu, " 对不起，没有找到服务器 (づ╥﹏╥)づ \n监控开启失败");
                }
            }
        }

        public static string[,] GetSerList()
        {
            Encoding encoding = Encoding.GetEncoding("GB2312");
            string[] serIni = Site.Traceless.Tools.Utils.FileUtil.GetFileContent(Common.CqApi.AppDirectory + "serverlist.ini", encoding);
            string[,] array = new string[serIni.Length, 5];
            string[] array2 = new string[5];
            int num = 0;
            string[] array3 = serIni;
            for (int i = 0; i < array3.Length; i++)
            {
                string text = array3[i];
                array2 = text.Split(new char[]
                {
                    '\t'
                });
                array[num, 0] = array2[0];
                array[num, 1] = array2[1];
                array[num, 2] = array2[3];
                array[num, 3] = "0";
                array[num, 4] = "List";
                num++;
            }
            return array;
        }
    }
}