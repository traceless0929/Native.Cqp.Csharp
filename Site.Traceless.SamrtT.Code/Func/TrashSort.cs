using Native.Tool.IniConfig.Linq;
using Site.Traceless.SmartT.Code.Model.SmartT;
using System.Text;

namespace Site.Traceless.SmartT.Code.Func
{
    public static class TrashSort
    {
        public static string goSort(string what)
        {
            TrashSortResp nowSort = null;
            StringBuilder sb = new StringBuilder();
            if (Common.TrashDic.ContainsKey(what))
            {
                Common.TrashDic.TryGetValue(what, out nowSort);
            }
            else
            {
                TxResp<TrashSortResp> resp = Tools.Http.HttpHelper.GetAPI<TxResp<TrashSortResp>>(@"http://api.tianapi.com/txapi/lajifenlei/?key=0491110e04e3fc4260c3ec4df465e848&word=" + what);
                if (resp.code != 200)
                {
                    sb.AppendLine("[垃圾分类]没有关于 " + what + " 的分类结果QAQ");
                    return sb.ToString();
                }
                else
                {
                    string trashSortPath = Common.CqApi.AppDirectory + "trashSort.ini";
                    IniObject iObject = IniObject.Load(trashSortPath, Encoding.Default);
                    resp.newslist.ForEach(p =>
                    {
                        if (!Common.TrashDic.ContainsKey(p.name))
                        {
                            Common.TrashDic.Add(p.name, p);
                            iObject["sortData"].Add(p.name, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            iObject.Save();
                        }
                    });
                    nowSort = resp.newslist[0];
                }
            }

            if (nowSort != null)
            {
                sb.AppendLine("[垃圾分类]" + what);
                sb.AppendLine("名称:" + nowSort.name);
                sb.AppendLine("分类:" + convertTrashType(nowSort.type));
                sb.AppendLine("解释:" + nowSort.explain);
                sb.AppendLine("举例:" + nowSort.contain);
                sb.AppendLine("提示:" + nowSort.tip);
            }
            else
            {
                sb.AppendLine("[垃圾分类]没有关于 " + what + " 的分类结果QAQ");
            }

            return sb.ToString().Trim();
        }

        public static string convertTrashType(int type)
        {
            switch (type)
            {
                case 0:
                    return "可回收垃圾";

                case 1:
                    return "有害垃圾";

                case 2:
                    return "厨余(湿)垃圾";

                default:
                    return "其他(干)垃圾";
            }
        }
    }
}