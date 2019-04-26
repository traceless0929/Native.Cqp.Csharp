using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Model
{
    public class AnalysisMsg
    {
        private static OrderInfoModel _msg = new OrderInfoModel("");

        public string What
        {
            get => _msg.What.Trim() ?? "";
        }

        public string GCommand
        {
            get
            {
                string _gcommand = null;
                Common.GCommandDic.TryGetValue(_msg.What.Trim(),out _gcommand);
                return _gcommand;
            }
        }

        public string PCommand
        {
            get
            {
                string _pcommand = null;
                Common.PCommandDic.TryGetValue(_msg.What.Trim(), out _pcommand);
                return _pcommand;
            }
        }

        public string How
        {
            get => _msg.How.Trim() ?? "";
        }

        public string Who
        {
            get => _msg.Who.Trim() ?? "";
        }

        public int OrderCount
        {
            get => _msg.OrderCount;
        }

        public string OriginStr
        {
            get; set;
        }

        /// <summary>
        /// 解析消息结构
        /// </summary>
        /// <param name="msg"></param>
        public AnalysisMsg(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                string str = new System.Text.RegularExpressions.Regex("[\\s]+").Replace(msg, " ");
                _msg = new OrderInfoModel(str);
            }
            else
            {
                _msg = new OrderInfoModel("");
            }

            OriginStr = msg;
        }

        ~AnalysisMsg()
        {
        }
    }
}
