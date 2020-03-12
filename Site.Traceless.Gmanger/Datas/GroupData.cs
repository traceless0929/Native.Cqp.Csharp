using Native.Sdk.Cqp.Enum;
using Native.Sdk.Cqp.Model;
using Native.Tool.IniConfig.Linq;
using Site.Traceless.Gmanger.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Site.Traceless.Gmanger.Datas
{
    public class GroupData
    {
        private string dataPath = "";
        private IniObject iniObject = null;
        private long gId = -1;
        private List<GroupMemberInfo> members;

        public GroupData(long groupId)
        {
            gId = groupId;
            InitGroupData(groupId);
        }

        private void InitGroupData(long groupId)
        {
            string dicPath = Common.CqApi.AppDirectory + "gdata\\";
            dataPath = dicPath + groupId + ".ini";
            if (!File.Exists(dataPath))
            {
                if (!Directory.Exists(dicPath))
                {
                    Directory.CreateDirectory(dicPath);
                }
                iniObject = new IniObject
                {
                    new IniSection("gcommands")
                    {
                        {"开启群管", "gmAllOpen"},
                        {"关闭群管", "gmAllClose"},
                        {"开启群欢迎", "gmWelOpen"},
                        {"关闭群欢迎", "gmWelClose"},
                        {"设置群欢迎", "gmWelSet"},
                        {"开启词库", "gmThesureOpen"},
                        {"关闭词库", "gmThesureClose"},
                        {"添加词库", "gmThesureAdd"},
                        {"删除词库", "gmThesureDel"},
                        {"查看词库", "gmThesureRead"}
                    },
                    //功能开关
                    new IniSection("switch")
                    {
                        //群管总开关
                        {SwitchEnum.gmopen.ToString("G"), false},
                        {SwitchEnum.welopen.ToString("G"), false},
                        {SwitchEnum.thesureopen.ToString("G"), false}
                    },
                    //自定义管理员
                    new IniSection("manager")
                    {
                        //超级管理员（同群主）
                        {QQGroupMemberType.Creator.ToString("G"), "415206409"},
                        //普通管理员（同管理员）
                        {QQGroupMemberType.Manage.ToString("G"), ""}
                    },
                    new IniSection("templates")
                    {
                        { SwitchEnum.welopen.ToString("G"),"[进群者头像]欢迎[AT进群者]加入群[群名]([群号])"}
                    },
                    //模糊词库
                    new IniSection("thesurelike")
                    {
                        {"测试模糊","这是测试词库" }
                    },
                    //精确词库
                    new IniSection("thesure")
                    {
                        {"测试精确","这是测试词库" }
                    }
                };
                iniObject.Save(dataPath);
            }
            else
            {
                iniObject = IniObject.Load(dataPath, Encoding.Default);
            }
            var gCommand = iniObject["gcommands"];
            this.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            members = Common.CqApi.GetGroupMemberList(groupId);
            Common.GroupDataDic[groupId] = this;
        }

        /// <summary>
        /// 命令映射路由(群聊)
        /// </summary>
        public Dictionary<string, string> GCommandDic { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///根据指令获取方法名
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string GetFuncName(string command)
        {
            return this.GCommandDic.TryGetValue(command, out string cmd) ? cmd : null;
        }

        /// <summary>
        /// 获取群管模板
        /// </summary>
        /// <param name="switchEnum"></param>
        /// <returns></returns>
        public string GetTemplate(SwitchEnum switchEnum)
        {
            var ini = iniObject["templates"];
            ini.TryGetValue(switchEnum.ToString("G"), out IniValue iniValue);
            return iniValue.ToString();
        }

        /// <summary>
        /// 设置群管模板
        /// </summary>
        /// <param name="switchEnum"></param>
        /// <returns></returns>
        public string SetTemplate(SwitchEnum switchEnum, string newTemplate)
        {
            var ini = iniObject["templates"];
            ini[switchEnum.ToString("G")] = new IniValue(Tools.Crawler.JavaScriptAnalyzer.Decode(newTemplate).Trim());
            iniObject.Save(dataPath);
            ini.TryGetValue(switchEnum.ToString("G"), out IniValue iniValue);
            return iniValue.ToString().Trim();
        }

        /// <summary>
        /// 添加词库
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isLike"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> AddThesure(string key, string value, bool isLike)
        {
            var thesureName = isLike ? "thesurelike" : "thesure";
            var ini = iniObject[thesureName];
            ini[key] = new IniValue(Tools.Crawler.JavaScriptAnalyzer.Decode(value).Trim());
            iniObject.Save(dataPath);
            ini.TryGetValue(key, out IniValue iniValue);
            return new KeyValuePair<string, string>(key, iniValue.ToString());
        }

        /// <summary>
        /// 删除词库
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isLike"></param>
        public void DelThesure(string key, bool isLike)
        {
            var thesureName = isLike ? "thesurelike" : "thesure";
            var ini = iniObject[thesureName];
            ini.Remove(key);
            iniObject.Save(dataPath);
        }

        /// <summary>
        /// 浏览词库
        /// </summary>
        /// <param name="isLike"></param>
        /// <returns></returns>
        public Dictionary<string, string> ReadThesure(bool isLike)
        {
            var thesureName = isLike ? "thesurelike" : "thesure";
            var ini = iniObject[thesureName];
            return ini.AsEnumerable().ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.ToString());
        }

        /// <summary>
        /// 检索词库
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ScanThesure(string key)
        {
            var thesureName = "thesure";
            try
            {
                var ini = iniObject[thesureName];
                var res = ini.TryGetValue(key, out IniValue iniValue);
                if (res)
                {
                    return iniValue.ToString();
                }
                thesureName = "thesurelike";
                ini = iniObject[thesureName];
                string likeKey = ini.Keys.FirstOrDefault(key.Contains);
                return string.IsNullOrEmpty(likeKey) ? null : ini[likeKey].Value.ToString();
            }
            catch (Exception ex)
            {
                return ScanThesure(key);
            }
        }

        /// <summary>
        /// 获取开关状态
        /// </summary>
        /// <param name="switchEnum"></param>
        /// <returns></returns>
        public bool GetSwitch(SwitchEnum switchEnum)
        {
            var ini = iniObject["switch"];
            return ini.TryGetValue(switchEnum.ToString("G"), out IniValue iniValue) && iniValue.ToBoolean();
        }

        /// <summary>
        /// 改变开关状态
        /// </summary>
        /// <param name="switchEnum"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public bool ChangeSwitch(SwitchEnum switchEnum, bool newStatus)
        {
            var ini = iniObject["switch"];
            ini[switchEnum.ToString("G")] = new IniValue(newStatus);
            iniObject.Save(dataPath);
            return ini.TryGetValue(switchEnum.ToString("G"), out IniValue iniValue) && iniValue.ToBoolean();
        }

        /// <summary>
        /// 获取拥有传入权限的QQ号
        /// </summary>
        /// <param name="enums"></param>
        /// <returns></returns>
        public List<long> GetManager(params QQGroupMemberType[] enums)
        {
            var ini = iniObject["manager"];
            var res = new List<long>();
            foreach (var managerEnum in enums)
            {
                string rawList = ini.TryGetValue(managerEnum.ToString("G"), out IniValue iniValue) ? iniValue.ToString() : null;
                if (string.IsNullOrEmpty(rawList))
                {
                    continue;
                }

                var listStr = rawList.Split(',');
                var list = listStr.Where(p => long.TryParse(p, out var _)).Select(long.Parse).ToList();
                res.AddRange(list);
                res.AddRange(this.members.Where(p => p.MemberType == managerEnum).Select(p => p.QQ.Id).ToList());
            }
            return res;
        }

        /// <summary>
        /// 添加管理
        /// </summary>
        /// <param name="qqGroupMemberType"></param>
        /// <param name="qq"></param>
        /// <returns></returns>
        public List<long> AddManager(QQGroupMemberType qqGroupMemberType, params long[] qq)
        {
            List<long> managerList = GetManager(qqGroupMemberType);
            var ini = iniObject["manager"];
            managerList.AddRange(qq);
            ini[qqGroupMemberType.ToString("G")] = new IniValue(string.Join(",", qq.Distinct().Select(p => p + "").ToList()));
            iniObject.Save(dataPath);
            return GetManager(qqGroupMemberType);
        }

        public void upsertMenu(Dictionary<string, string> menus)
        {
            var gCommand = iniObject["gcommands"];
            foreach (var keyValuePair in menus)
            {
                if (gCommand.ContainsKey(keyValuePair.Key))
                {
                    gCommand[keyValuePair.Key] = new IniValue(keyValuePair.Value);
                }
                else
                {
                    gCommand.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            iniObject.Save(dataPath);
            this.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
        }
    }
}