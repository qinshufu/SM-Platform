# SM-Platform

短信平台

通过对服务的解耦，通讯方式的升级来实现系统的吞吐量，同时支持多种短信业务，提供简单易行的操作与维护功能与接口。

项目分为三个微服务：

1. 系统管理用户接口提供通道，模板等相关的管理服务
2. 用户 API 接口提供供其他系统调用的短信发送服务
3. 短信发送服务，实际完成短信发送的任务。

不同服务间通过 HTTP 调用，事件通知来进行通信。

使用技术和核心功能：

1. 使用 ASP.NET Core 以及 DotNetty 来分别提供 HTTP 以及 TCP 接口
2. 使用 Redis 的 list 数据结构完成队列功能，防止大量的短信发送通知冲击下系统崩溃
3. 事件通知，异步调用，以及多线程短信发送提升系统的吞吐量
4. 定时任务扫描定时消息表以及消息队列完成，定时/实时的消息发送功能
6. 分布式锁，防止异步多线的短信发送冲突

主要使用的技术：
ASP.NET Core, DotNetty, MassTransit, AutoMapper, Redis, MediatoR， Quartz.NET, 
Polly, Autofac, FluentValidation，EntityFramework Core，EntityFrameworkCore.Cacheable（二级缓存）

.NET 版本 7.0

## 项目结构

1. SmPlatform.Api 短信平台管理接口 API
2. SmPlatform.BuildingBlock 构建块
3. SmPlatform.Server 短信发送服务（他完成发送短信的工作）
4. SmPlatform.Instruture 基础设施
5. SmPlatform.Model 模型
6. SmPlatform.Domain 领域
7. SmPlatform.SmApi 发送短信接口（给客户调用）