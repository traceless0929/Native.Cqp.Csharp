using Native.Sdk.Cqp.Model;
using Newtonsoft.Json;
using Site.Traceless.RestService.Interface;
using Site.Traceless.RestService.Model;
using Site.Traceless.RestService.Model.Req;
using Site.Traceless.SamrtT.Code;
using Site.Traceless.SamrtT.Code.Func;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Site.Traceless.RestService.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MainService : IHook, IInfo, IGroup, IFriend
    {
        #region 机器人QQ相关

        public BaseResp<int> GetCsrfToken(BaseReq<object> req)
        {
            return BaseResp<int>.respSuc(Common.CqApi.GetCsrfToken());
        }

        public BaseResp<List<FriendInfo>> GetFriendList(BaseReq<object> req)
        {
            return BaseResp<List<FriendInfo>>.respSuc(Common.CqApi.GetFriendList());
        }

        public BaseResp<List<GroupInfo>> GetGroupList(BaseReq<object> req)
        {
            return BaseResp<List<GroupInfo>>.respSuc(Common.CqApi.GetGroupList());
        }

        public BaseResp<string> GetLoginNick(BaseReq<object> req)
        {
            return BaseResp<string>.respSuc(Common.CqApi.GetLoginNick());
        }

        public BaseResp<long> GetLoginQQId(BaseReq<object> req)
        {
            return BaseResp<long>.respSuc(Common.CqApi.GetLoginQQId());
        }

        #endregion 机器人QQ相关

        #region 好友相关

        public BaseResp<int> SendPrivateMsg(BaseReq<Send_Msg> req)
        {
            Send_Msg p = req.data;
            return BaseResp<int>.respSuc(Common.CqApi.SendPrivateMessage(p.target, p.msg));
        }

        public BaseResp<bool> SendPraise(BaseReq<Send_Praise> req)
        {
            Send_Praise p = req.data;
            return BaseResp<bool>.respSuc(Common.CqApi.SendPraise(p.target, p.count));
        }

        public BaseResp<StrangerInfo> GetStrangerInfo(BaseReq<Get_Info> req)
        {
            Get_Info p = req.data;
            return BaseResp<StrangerInfo>.respSuc(Common.CqApi.GetStrangerInfo(p.targetFst, p.isCache));
        }

        #endregion 好友相关

        #region 群相关

        public BaseResp<int> SendGroupMsg(BaseReq<Send_Msg> req)
        {
            Send_Msg p = req.data;
            return BaseResp<int>.respSuc(Common.CqApi.SendGroupMessage(p.target, p.msg));
        }

        public BaseResp<GroupInfo> GetGroupInfo(BaseReq<Get_Info> req)
        {
            Get_Info p = req.data;
            return BaseResp<GroupInfo>.respSuc(Common.CqApi.GetGroupInfo(p.targetFst, p.isCache));
        }

        public BaseResp<GroupMemberInfo> GetGroupMemberInfo(BaseReq<Get_Info> req)
        {
            Get_Info p = req.data;
            return BaseResp<GroupMemberInfo>.respSuc(Common.CqApi.GetGroupMemberInfo(p.targetFst, p.targetSec, p.isCache));
        }

        public BaseResp<List<GroupMemberInfo>> GetGroupMemberList(BaseReq<Get_Info> req)
        {
            Get_Info p = req.data;
            return BaseResp<List<GroupMemberInfo>>.respSuc(Common.CqApi.GetGroupMemberList(p.targetFst));
        }

        public BaseResp<bool> SetGroupBanSpeak(BaseReq<Set_Info> req)
        {
            Set_Info p = req.data;
            return BaseResp<bool>.respSuc(Common.CqApi.SetGroupBanSpeak(p.targetFst));
        }

        public BaseResp<bool> SetGroupMemberBanSpeak(BaseReq<Set_Info> req)
        {
            Set_Info p = req.data;
            return BaseResp<bool>.respSuc(Common.CqApi.SetGroupMemberBanSpeak(p.targetFst, p.targetSec, TimeSpan.FromMilliseconds(p.tSapnMs)));
        }

        public BaseResp<bool> SetGroupMemberVisitingCard(BaseReq<Set_Info> req)
        {
            Set_Info p = req.data;
            return BaseResp<bool>.respSuc(Common.CqApi.SetGroupMemberVisitingCard(p.targetFst, p.targetSec, p.para));
        }

        #endregion 群相关

        #region hook业务

        public BaseResp<bool> GithubHook(Hook_Github req)
        {
            if (Common.gmGroupId > 0&&req.@ref.Contains("SmartT_V2"))
            {
                 Common.CqApi.SendGroupMessage(Common.gmGroupId, Hooks.OptHookCommit(req));
            }
            return BaseResp<bool>.respSuc(true);
        }

        #endregion hook业务
    }
}