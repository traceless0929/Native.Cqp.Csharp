using Site.Traceless.SmartT.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.SmartT.DB.Bll
{
    public class AuthBll
    {
        public static T_Account_Bind CheckWeCahtBind(long qq,string wecheatId)
        {
            T_Account_Bind res = Common.sqliteHelper.db.Table<T_Account_Bind>().Where(p => p.QqId == qq && p.WeChatId == wecheatId).FirstOrDefault();
            if (null == res)
            {
                res = new T_Account_Bind();
                res.QqId = qq;
                res.WeChatId = wecheatId;
                Common.sqliteHelper.Add(res);
            }
            return res;
        }

        public static T_Account_Bind FindBind(long qq,string wecheatId)
        {
             return Common.sqliteHelper.db.Table<T_Account_Bind>().Where(p => p.QqId == qq && p.WeChatId == wecheatId).FirstOrDefault();
        }

        public static T_Account_Bind SetWeChatBind(T_Account_Bind bind)
        {
            if (Common.sqliteHelper.Update(bind)>0)
            {
                return bind;
            }
            return null;
        }
    }
}
