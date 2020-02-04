﻿using Native.Csharp.Sdk.Cqp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.Enum;
using Native.Csharp.Sdk.Cqp.Model;
using Native.Csharp.Tool.IniConfig.Linq;
using Site.Traceless.Gmanger.Enum;

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
                    },
                    //功能开关
                    new IniSection("switch")
                    {
                        //群管总开关
                        {SwitchEnum.gmopen.ToString("G"), false},
                        {SwitchEnum.welopen.ToString("G"), false}
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
                    }
                };
                iniObject.Save(dataPath);
            }

            ;
            iniObject = IniObject.Load(dataPath, Encoding.Default);
            var gCommand = iniObject["gcommands"];
            this.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            members = Common.CqApi.GetGroupMemberList(groupId);
            Common.GroupDataDic[groupId]= this;
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
        public string SetTemplate(SwitchEnum switchEnum,string newTemplate)
        {
            var ini = iniObject["templates"];
            ini[switchEnum.ToString("G")] = new IniValue(Tools.Crawler.JavaScriptAnalyzer.Decode(newTemplate).Trim());
            iniObject.Save(dataPath);
            ini.TryGetValue(switchEnum.ToString("G"), out IniValue iniValue);
            return iniValue.ToString().Trim();
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
            ini[switchEnum.ToString("G")]=new IniValue(newStatus);
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
                res.AddRange(this.members.Where(p => p.MemberType == managerEnum).Select(p=>p.QQ.Id).ToList());
            }
            return res;
        }
    }
}