using Site.Traceless.R6.Code.Model.R6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Site.Traceless.R6.Code.Http
{
    public class Apis
    {
        private const string BASEURL = @"https://r6stats.com/api/";
        private const string BASEINFO = @"player-search/";
        private const string DETAILINFO = @"stats/";
        private const string SEAAONINFO = @"/seasonal";
        private const string WEAPONINFO = @"/weapons";
        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pla"></param>
        /// <returns></returns>
        public static UserBaseInfoResp GetUserBaseInfo(string userName, string pla)
        {
            UserBaseInfoResp res = new UserBaseInfoResp();
            try
            {
                res = Newtonsoft.Json.JsonConvert.DeserializeObject<UserBaseInfoResp>(Utils.GetAPI(
                        BASEURL + BASEINFO + "/" + userName + "/" + pla).Replace("[", "").Replace("]", "").Trim());
            }
            catch
            {
                res = null;
            }

            return res;
        }
        /// <summary>
        /// 获取详细用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pla"></param>
        /// <returns></returns>

        public static UserDetailInfoResp GetUserDetailInfo(UserBaseInfoResp res)
        {
            try
            {
                if (res != null)
                {
                    UserDetailInfoResp userDetailInfoResp =
                        Utils.GetAPI<UserDetailInfoResp>(BASEURL + DETAILINFO + res.uplay_id);
                    return userDetailInfoResp;
                }
            }
            catch
            {
                res = null;
            }

            return null;
        }

        public static UserSeasonResp GetUserSeasonInfo(UserBaseInfoResp res)
        {
            try
            {
                if (res != null)
                {
                    UserSeasonResp userSeasonResp =
                        Utils.GetAPI<UserSeasonResp>(BASEURL + DETAILINFO + res.uplay_id + SEAAONINFO);
                    return userSeasonResp;
                }
            }
            catch (Exception ex)
            {
                res = null;
            }

            return null;
        }


        public static UserWeaponResp GetUserWeaponInfo(UserBaseInfoResp res)
        {
            try
            {
                if (res != null)
                {
                    UserWeaponResp userWeaponResp =
                        Utils.GetAPI<UserWeaponResp>(BASEURL + DETAILINFO + res.uplay_id + WEAPONINFO);
                    return userWeaponResp;
                }
            }
            catch (Exception ex)
            {
                res = null;
            }

            return null;
        }
        /// <summary>  
        /// 转换输入字符串中的任何转义字符。如：Unicode 的中文 \u8be5  
        /// </summary>  
        /// <param name="str"></param>  
        /// <returns></returns>  
        public static string UnicodeDencode(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            return Regex.Unescape(str);
        }
    }
}
