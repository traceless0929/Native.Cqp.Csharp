using Native.Sdk.Cqp.Interface;
using Site.Traceless.Demo.Code.Event;
using Unity;

namespace Native.Core
{
    /// <summary>
    /// 酷Q应用主入口类
    /// </summary>
    public class CQMain
    {
        /// <summary>
        /// 在应用被加载时将调用此方法进行事件注册, 请在此方法里向 <see cref="IUnityContainer"/> 容器中注册需要使用的事件
        /// </summary>
        /// <param name="container">用于注册的 IOC 容器</param>
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IGroupMessage, Event_GroupMsg>("群消息处理");
            container.RegisterType<IPrivateMessage, Event_PrivateMsg>("私聊消息处理");
            container.RegisterType<IDiscussMessage, Event_DiscussMsg>("讨论组消息处理");
            container.RegisterType<IGroupUpload, Event_GroupUpload>("群文件上传事件处理");
            container.RegisterType<IGroupManageChange, Event_GroupManageChange>("群管理变动事件处理");
            container.RegisterType<IGroupMemberDecrease, Event_GroupMemberDecrease>("群成员减少事件处理");
            container.RegisterType<IGroupMemberIncrease, Event_GroupMemberIncrease>("群成员增加事件处理");
            container.RegisterType<IGroupBanSpeak, Event_GroupBanSpeak>("群禁言事件处理");
            container.RegisterType<IFriendAdd, Event_FriendAdd>("好友已添加事件处理");
            container.RegisterType<IFriendAddRequest, Event_FriendAddRequest>("好友添加请求处理");
            container.RegisterType<IGroupAddRequest, Event_GroupAddRequest>("群添加请求处理");
            container.RegisterType<ICQStartup, Event_CQStartup>("酷Q启动事件");
            container.RegisterType<ICQExit, Event_CQExit>("酷Q关闭事件");
            container.RegisterType<IAppEnable, Event_AppEnable>("应用已被启用");
            container.RegisterType<IAppDisable, Event_AppDisable>("应用将被停用");
        }
    }
}