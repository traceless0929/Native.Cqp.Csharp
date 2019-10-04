using Native.Csharp.App.Extend;
using Native.Csharp.App.Model.respModel;
using Native.Csharp.Sdk.Cqp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;

namespace Native.Csharp.App
{
    /// <summary>
    /// 用于存放 App 数据的公共类
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 获取 App 在酷Q中显示的名称 (此属性 set 并不能做出实质性的修改)
        /// </summary>
        public static string AppName { get; set; }
        /// <summary>
        /// 获取 App 在酷Q中的版本号 (此属性 set 并不能做出实质性的修改)
        /// </summary>
        public static Version AppVersion { get; set; }
        /// <summary>
        /// 获取或设置 App 在运行期间所使用的数据路径
        /// </summary>
        public static string AppDirectory { get; set; }

        /// <summary>
        /// 获取或设置当前 App 是否处于运行状态
        /// </summary>
        public static bool IsRunning { get; set; }

        /// <summary>
        /// 获取或设置当前 App 使用的 酷Q Api 接口实例
        /// </summary>
        public static CqApi CqApi { get; set; }

        /// <summary>
        /// 发送回复消息，如果是群内发送到群，如果是私聊，发送私聊
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="text"></param>
        public static void sendResult<T>(T type, string text) where T : EventArgs.CqEventArgsBase
        {
            switch (type.Type)
            {
                case 2:
                    {
                        //群内
                        EventArgs.CqGroupMessageEventArgs args = type as EventArgs.CqGroupMessageEventArgs;
                        if (null != args)
                        {
                            Common.CqApi.SendGroupMessage(args.FromGroup, text);
                        }
                    }
                    break;
                case 21:
                    {
                        //私聊
                        EventArgs.CqPrivateMessageEventArgs args = type as EventArgs.CqPrivateMessageEventArgs;
                        Common.CqApi.SendPrivateMessage(args.FromQQ, text);
                    }
                    break;
                default:
                    Common.CqApi.SendPrivateMessage(Common.getSetting<long>("master"), $"[意外的type{type.Type}]{text}");
                    break;

            }
        }

        /// <summary>
        /// 获取或设置当前 App 使用的依赖注入容器实例
        /// </summary>
        public static IUnityContainer UnityContainer { get; set; }
        /// <summary>
        /// 命令映射路由
        /// </summary>
        public static Dictionary<string, string> GCommandDic { get; set; } = new Dictionary<string, string>();
        public static Dictionary<string, string> PCommandDic { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 服务器列表
        /// </summary>
        public static string[,] SerList;
        /// <summary>
        /// 开服监控
        /// </summary>
        public static ServerRemind ServerRemind;
        /// <summary>
        /// 菜单
        /// </summary>
        public static string menuStr = "";
        /// <summary>
        /// 群成员列表缓存
        /// </summary>
        public static string groupMemberPath
        {
            get
            {
                return AppDirectory + "groupMemeber\\";
            }
        }
        /// <summary>
        /// 垃圾分类缓存
        /// </summary>
        public static Dictionary<string, TrashSortResp> TrashDic { get; set; } = new Dictionary<string, TrashSortResp>();
       
        /// <summary>
        /// 设置
        /// </summary>
        public static Dictionary<string, string> settingDic { get; set; } = new Dictionary<string, string>();
        public static T getSetting<T>(string key)
        {
            settingDic.TryGetValue(key, out string value);
            T res = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
            return res;
        }

        public static String getSetting(string key)
        {
            settingDic.TryGetValue(key, out string value);
            return value;
        }
    }
}
