using Site.Traceless.Demo.DB.Model;
using SQLite;
using System.Collections.Generic;

namespace Site.Traceless.Demo.DB
{
    public class SQLiteHelper
    {
        public SQLiteConnection db;

        public SQLiteHelper(string connstr)
        {
            db = new SQLiteConnection(connstr);
            db.CreateTable<T_User>();//表已存在不会重复创建
        }

        public int Add<T>(T model)
        {
            return db.Insert(model);
        }

        public int Update<T>(T model)
        {
            return db.Update(model);
        }

        public int Delete<T>(T model)
        {
            return db.Update(model);
        }

        public List<T> Query<T>(string sql) where T : new()
        {
            return db.Query<T>(sql);
        }

        public int Execute(string sql)
        {
            return db.Execute(sql);
        }
    }
}