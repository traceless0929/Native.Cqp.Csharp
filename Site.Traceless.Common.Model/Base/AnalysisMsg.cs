using Site.Traceless.Common.Model;

namespace Site.Traceless.Common.Model.Base
{
    public class AnalysisMsg
    {
        private static OrderInfoModel _msg = new OrderInfoModel("");

        public string What
        {
            get => _msg.What.Trim() ?? "";
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