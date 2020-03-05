
## 代码库介绍
base分支为我自用的新建App的模板，引入了一些自己的机制简化开发流程

> 1. 增加指令消息解析类，采用 (wwh) what who how模式
> 2. 反射机制优化流程
> 3. 同步更新Native.SDK
> 4. 是个Native.SDK的DEMO（只是进行了~~看得懂的魔改~~）

### 样例应用

| 应用名                                                       | 描述                                    | 备注 |
| ------------------------------------------------------------ | --------------------------------------- | ---- |
| [彩虹六号战绩查询](https://github.com/traceless0929/Native.Cqp.Csharp/tree/rainbow6) | 彩虹六号战绩查询插件，数据来源于R6stats |      |

### 消息解析

将原始指令使用空格（可更改）进行解析
> 如“攻击 麻花疼 50”
解析为
> what（要干啥）：攻击
> who（对谁干）：麻花疼
> how（怎么干）：50
无需自己解析指令！

### 反射机制

还在使用？？？？

```c#
if(msg=="攻击"){
    goAttack(fromQQ,target);
}
else if(msg="防御"){
    goDef(fromQQ,target);
}
```
不！可！以！

触发关键字修改还要改代码？？
NO！！

多关键字触发同一个方法还在？？？
```c#
if(msg=="攻击"||msg=="打击"){
    goAttack(fromQQ,target);
}
else if(msg="防御"){
    goDef(fromQQ,target);
}
```
OH MY GOD！！

这里帮你解决！

1. 应用启动处增加映射关系（进行初始化，仅在应用第一次运行的机器中进行）
```c#
public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
        //此处仅演示 私聊 和 群聊
            Common.CqApi = e.CQApi;
            string commandPath = Common.CqApi.AppDirectory + "command.ini";
            IniObject iObject;
            if (!File.Exists(commandPath))
            {
                iObject = new IniObject
                {
                    new IniSection("gcommands")
                    {
                        { "攻击","funcOne"},
                        { "打击","funcOne"},
                        { "防御","funcTwo"},
                    },
                    new IniSection("pcommands")
                    {
                        { "功能1","funcOne"},
                        { "功能2","funcTwo"}
                    }
                };
                iObject.Save(commandPath);
            };
            iObject = IniObject.Load(commandPath, Encoding.Default);
            IniSection pCommand = iObject["pcommands"];
            Common.PCommandDic = pCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
            IniSection gCommand = iObject["gcommands"];
            Common.GCommandDic = gCommand.ToDictionary(p => p.Key, p => p.Value.ToString());
```

2. 书写反射代码（直接Clone本仓库无需书写）
```c#
public class Event_GroupMsg : IGroupMessage
    {
    //群聊
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
            if (String.IsNullOrEmpty(nowModel.GCommand))
            {
                e.Handler = false;
                return;     // 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
            }
            var gapp = Activator.CreateInstance(typeof(GroupApp)) as GroupApp;
            var method = gapp.GetType().GetMethod(nowModel.GCommand);
            object result = method.Invoke(null, new object[] { e, nowModel });

            e.Handler = false;

        }

    }
```
```c#
 public class Event_PrivateMsg : IPrivateMessage
    {
    //私聊
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            AnalysisMsg nowModel = new AnalysisMsg(e.Message.Text);
            if (String.IsNullOrEmpty(nowModel.PCommand))
            {
                e.Handler = false;
                return;     // 因为 e.Handled = true 只是起到标识作用, 因此还需要手动返回
            }
            var papp = Activator.CreateInstance(typeof(FriendApp)) as FriendApp;
            var method = papp.GetType().GetMethod(nowModel.PCommand);
            object result = method.Invoke(null, new object[] { e, nowModel });

            e.Handler = false;
        }
    }
```

3. 书写业务代码（群聊为例）
```c#
public class GroupApp
    {
        public static void funcOne(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(415206409,$"[这里是群方法 攻击]", $"参数数 {msg.OrderCount}\n", $"触发指令(第一参数 what) {msg.What}\n", $"目标(第二参数 who) {msg.Who}\n", $"怎么做(第三参数 how) {msg.How}\n", $"原始信息 {msg.OriginStr}\n", e.ToString());
        }
        public static void funcTwo(CQGroupMessageEventArgs e, AnalysisMsg msg)
        {
            e.CQApi.SendPrivateMessage(415206409, $"[这里是群方法 防御]\n", $"参数数 {msg.OrderCount}\n", $"触发指令(第一参数 what) {msg.What}\n", $"目标(第二参数 who) {msg.Who}\n", $"怎么做(第三参数 how) {msg.How}\n", $"原始信息 {msg.OriginStr}\n", e.ToString());
        }
    }
```
4. 体验快感！
> 群聊“打击 麻花疼 50”效果等于“攻击 麻花疼 50”！（一个方法多个触发）
> 修改酷Q“\data\app\site.traceless.nativedemo\command.ini”重启应用即可更新指令映射！无需修改代码！

# 以下部分为框架作者的原文（包括打赏码）

## Native.SDK 优点介绍

Native.SDK  是为了方便 .Net 平台开发者高效开发 酷Q应用 的开发框架。封装酷Q 提供的接口，提供了安全高效的Api，同时抽象了事件中的基础数据类型，并且提供了完整的托管异常处理，提供了优秀的开发环境。

# 特点

* 支持原生导出函数，不需要前置插件作为服务端。(能够在 .Net 平台中导出 C/C++ 可用的导出函数)
* 支持包括 WebServices 在内的所有 .Net 项目进行交互。
* 支持编译整合DLL。(在编译的同时，对所有使用到的程序集进行整合打包，保证最终结果只有 app.dll)
* 支持自由调整 .Net Framework 版本
* 支持 C# 和 VB.NET 两种语言编写代码
* 使用 UTF-8 编码，并且在托管和非托管之间启用了 GB18030 编码的转换
* 可以在 <a href="https://cqp.cc/t/42164">酷Q on Docker</a> 中运行。(目前仅支持 .Net Framework 4.5)

# 维基

<a href="https://github.com/Jie2GG/Native.Framework/wiki">Native 维基百科</a><br/>
同时包括了 Native.SDK 的安装方法、编程规范、编译部署及常见错误和解决方案

# 更新日志

[Native 更新日志](UPDATE.md)

## Native.SDK 调试流程

    1. 打开 酷Q Air/Pro, 并且登录机器人账号
    2. 打开 Native.Csharp 项目, 修改 "生成" 中的 "输出路径" 为 酷Q的 "dev" 路径
    3. 重新生成 Native.Csharp 项目
    4. 在酷Q上使用 "重载应用" 功能, 重载所有应用
    5. 依次选择VS的菜单项: "调试" -> "附加到进程"
    6. 选择 CQA.exe/CQP.exe 的托管进程, 选择附加
    7. 附加成功后进入调试模式, 即可进行断点 (注: 仅在只加载一个 .Net 应用的酷Q可以进行调试)

## Native.SDK 已知问题
	
> 1. ~~对于 "EnApi.GetMsgFont" 方法, 暂时无法根据酷Q回传的指针获取字体信息, 暂时无法使用~~ <span style="color:red">(由于酷Q不解析此参数, 弃用)</span>
> 2. ~~对于 "HttpHelper.GetData" 方法, 抛出异常, 暂时无法使用~~ <font color=#FF0000>(已经修复, 但是封装了新的HTTP类, 弃用)</font>
> 3. ~~对于 "AuthCode" 被多插件共用, 导致应用之间串数据~~ <font color=#FF0000>(已修复)</font>
> 4. ~~对于接收消息时, 颜文字表情, 特殊符号乱码, 当前正在寻找转换方式~~ <font color=#FF0000>(已修复)</font>
> 5. ~~对于 Visual Studio 弹出安全警告导致编译不通过的问题~~ <font color=#FF0000>(用 git 克隆到VS即可)</font>

## Native.SDK 更新日志

### [查看更新日志](UPDATE.md)

**您的支持就是我更新的动力!**

<img src="https://jie2gg.github.io/Image/AliPlay.png" alt="支付宝二维码" width="260" height="350"><img src="https://jie2gg.github.io/Image/WeChat.png" alt="微信二维码" width="260" height="350">
