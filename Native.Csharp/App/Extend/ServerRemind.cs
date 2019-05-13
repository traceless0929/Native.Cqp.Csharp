using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Native.Csharp.App.Extend
{
    public class ServerRemind
    {
        public static Timer timer;
        public ServerRemind()
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
                    ip =  Common.SerList[i, 2];
                    bigSer =  Common.SerList[i, 0];
                    string content =$"[开服查询{serName}]"+Environment.NewLine
                        +(Jx3OpenTell.IsOpen(ip, 3724) ? (bigSer + " " + serName + "\r\n开") : (bigSer + " " + serName + "\r\n关"));
                    Common.CqApi.SendGroupMessage(clu, content);
                    return;
                }
                else
                {
                    if (i ==  Common.SerList.GetLength(0) - 1)
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
            for (int i = 0; i <  Common.SerList.GetLength(0); i++)
            {
                if (serName.Equals( Common.SerList[i, 1]))
                {
                    bool existflag = false;
                    bigSer =  Common.SerList[i, 0];
                    Common.SerList[i, 3] = "1";
                    string[] cluList =  Common.SerList[i, 4].Split('|');
                    foreach (var istr in cluList)
                    {
                        if (istr == clu + "") existflag = true;
                    }
                    if (!existflag)
                    {
                         Common.SerList[i, 4] += "|" + clu;
                    }
                    Common.CqApi.SendGroupMessage(clu, $"[开服监控]"+ "已为您开启 " + serName + "的监控~请关注群信息，将第一时间通知到群。");
                    timer.Enabled = true;
                    return;
                }
                else if (i ==  Common.SerList.GetLength(0) - 1)
                {
                    Common.CqApi.SendGroupMessage(clu, " 对不起，没有找到服务器 (づ╥﹏╥)づ \n监控开启失败");
                }
            }
        }

        private void SerOpenRemind_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            for (int i = 0; i <  Common.SerList.GetLength(0); i++)
            {
                bool flag =  Common.SerList[i, 3] != "1";
                if (!flag)
                {
                    string ip =  Common.SerList[i, 2];
                    string[] array =  Common.SerList[i, 4].Split(new char[]
                    {
                        '|'
                    });
                    string text = string.Empty;
                    string bigSer =  Common.SerList[i, 0];
                    string serName =  Common.SerList[i, 1];
                    if (Jx3OpenTell.IsOpen(ip, 3724))
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
    }
}
