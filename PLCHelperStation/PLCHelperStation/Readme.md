PLC Helper Station（PLC助手工作站）是一个基于.NET8 使用Webapi+Vue3的前后端分离小项目，项目目的是学习主流的PLC通讯和前后端分离的技术。

项目设计初衷是想要通过优美的架构风格来实现这些功能，希望使用DDD这样的模式，前端也追求好看的、清新的风格。但是由于本人很菜，想追求优美的架构就不会写了，所以那个就当后面重构的目标吧。所以这个项目前端每个页面基本也是写在一个.vue文件中，甚至接口也是写死的，图简单，怕出错。后端Webapi部分也没做分层的设计，用户模块也没做权限管理，本来想加的看了下没明白就放弃了。。。

![image-20250205205758609](C:\Users\nanfengqaq\AppData\Roaming\Typora\typora-user-images\image-20250205205758609.png)

下面简单罗列下“理想”设计的几个关键点：

先看下最后大概的页面![image-20250205205902076](C:\Users\nanfengqaq\AppData\Roaming\Typora\typora-user-images\image-20250205205902076.png)

   * * 登录页：有点丑就不放了，考虑使用3D的技术做炫酷的效果（试过three.js ，提需求叫AI写的算不上很满意），需要有必要的功能：用户名、密码输入框，注册、登录、记住密码（可选）

     * 仪表盘：核心--炫  怎么炫目前没思路

     * PLC工作站：

       *   PLC配置：这个相当于是PLC相关协议（Modbus、S7、OPC...）这些协议的公共参数，通过PLC名称的方式对外暴露给其他的使用；可以考虑前端的字段可以通过配置的方式来新增或去除字段，当数据量大的时候表格也需要支持批量操作
       *   后面的几个页面显示对应的读写记录

     * 数据点运维：

       ![image-20250205212852749](C:\Users\nanfengqaq\AppData\Roaming\Typora\typora-user-images\image-20250205212852749.png)

       *   Modbus数据点配置：
           *   根据PLC名称来选择PLC拿到公有的一些属性；

   * 日志模块：

     * 操作日志：

       > 2025-02-05 21:26:50.5293|INFO|PLCHelperStation.Controller.ModbusConfigController|获取一次所有Modbus配置
       > 2025-02-05 21:32:17.6539|INFO|PLCHelperStation.Controller.PLCConfigController|获取一次PLC配置
       > 2025-02-05 21:32:32.3332|WARN|PLCHelperStation.Controller.ModbusConfigController|获取一次所有PLC的名 称！
       > 2025-02-05 21:32:32.3366|INFO|PLCHelperStation.Controller.ModbusConfigController|获取一次所有Modbus配置

       上面的是打印在控制台上的日志，为了防止控制台滚动太快不好查看还有一份一样的文件版在bin目录下的logs文件夹下

       但是需要在前端将日志处理下展示出来；提取大概的关键字段：序号，时间，日志级别，日志来源，日志内容，操作人（需要支持用户角色权限管理）