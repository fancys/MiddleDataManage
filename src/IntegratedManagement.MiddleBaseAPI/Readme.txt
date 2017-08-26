-----项目结构介绍

IntegratedManagement.MiddleBaseAPI ---------------- 接口项目 负责处理客户端发送过来的请求
IntegratedManageMent.Application ------------------ 业务逻辑项目，针对客户端的请求内容，结合业务情况处理数据
IntegratedManagement.IRepository ------------------ 针对实体类的操作接口
IntegratedManagement.Core ------------------------- 辅助项目，帮助类等

IntegratedManagement.Repository ------------------- 针对实体类的操作的具体实现
IntegratedManagement.RepositoryDapper ------------- 针对实体类的操作的具体实现(ORM框架：Dapper)
IntegratedManagement.Entity ----------------------- 实体类项目，单据类 枚举等
IntegratedManagement.MiddleBaseAPI.Tests ---------- 单元测试项目


