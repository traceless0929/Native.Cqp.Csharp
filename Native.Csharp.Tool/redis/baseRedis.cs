using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Csharp.Tool.redis
{
    public class BaseRedis
    {
        static CSRedis.CSRedisClient rds = null;
        static string _ip;
        static int _port;
        static string _pwd;
        static int _db;
        static string _prefix;
        public BaseRedis(string ip,int port,string pwd,int db,string prefix)
        {
            if (null == rds)
            {
                _ip = ip;
                _port = port;
                _pwd = pwd;
                _db = db;
                _prefix = prefix;
            }
        }
        public static CSRedis.CSRedisClient getRedis()
        {
            if (null == rds)
            {
                rds = new CSRedis.CSRedisClient($"{_ip}:{_port},password={_pwd},defaultDatabase={_db},ssl=false,prefix={_prefix}");
            }
            return rds;
        }
    }
}
