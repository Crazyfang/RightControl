USE [rightcontrol]
GO
/****** Object:  Table [dbo].[t_user]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_user](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NULL,
	[RealName] [varchar](20) NULL,
	[PassWord] [char](32) NULL,
	[RoleId] [int] NULL,
	[Status] [bit] NULL,
	[CreateOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[CreateBy] [int] NULL,
	[UpdateBy] [int] NULL,
	[Gender] [tinyint] NULL,
	[Phone] [varchar](11) NULL,
	[Email] [varchar](30) NULL,
	[Remark] [varchar](50) NULL,
	[HeadShot] [varchar](50) NULL,
 CONSTRAINT [PK_t_user] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[t_user] ON
INSERT [dbo].[t_user] ([Id], [UserName], [RealName], [PassWord], [RoleId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [Gender], [Phone], [Email], [Remark], [HeadShot]) VALUES (1, N'admin', N'超级管理员', N'e10adc3949ba59abbe56e057f20f883e', 1, 1, CAST(0x0000AA02010CDA90 AS DateTime), CAST(0x0000AB02009AFDCE AS DateTime), 0, 1, 1, N'13429262043', N'fangyonglubu@qq.com', N'最高权限', N'/Upload/img/502.jpg')
INSERT [dbo].[t_user] ([Id], [UserName], [RealName], [PassWord], [RoleId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [Gender], [Phone], [Email], [Remark], [HeadShot]) VALUES (2, N'8287072', N'方勇', N'e10adc3949ba59abbe56e057f20f883e', 2, 1, CAST(0x0000AB1900E3A0A8 AS DateTime), CAST(0x0000AB20010AD604 AS DateTime), 0, 1, 1, N'13429262043', N'fangyonglubu@qq.com', N'测试账号', N'/Upload/img/kenan.jpeg')
INSERT [dbo].[t_user] ([Id], [UserName], [RealName], [PassWord], [RoleId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [Gender], [Phone], [Email], [Remark], [HeadShot]) VALUES (5, N'8280003', N'张锡琴', N'e10adc3949ba59abbe56e057f20f883e', 2, 1, CAST(0x0000AB1E011929D3 AS DateTime), CAST(0x0000AB1E011929D2 AS DateTime), 1, 0, 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[t_user] OFF
/****** Object:  Table [dbo].[t_role]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleCode] [varchar](20) NULL,
	[RoleName] [varchar](30) NULL,
	[Remark] [varchar](50) NULL,
	[Status] [bit] NULL,
	[CreateOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[CreateBy] [int] NULL,
	[UpdateBy] [int] NULL,
 CONSTRAINT [PK_t_role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[t_role] ON
INSERT [dbo].[t_role] ([Id], [RoleCode], [RoleName], [Remark], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (1, N'SysAdmin', N'超级管理员', NULL, 1, CAST(0x0000AA020100CD04 AS DateTime), CAST(0x0000AA020100D1B4 AS DateTime), 0, 0)
INSERT [dbo].[t_role] ([Id], [RoleCode], [RoleName], [Remark], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (2, N'GeneralAdmin', N'普通管理员', NULL, 1, CAST(0x0000AA020100D8BC AS DateTime), CAST(0x0000AA020100D538 AS DateTime), 0, 0)
INSERT [dbo].[t_role] ([Id], [RoleCode], [RoleName], [Remark], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (3, N'GeneralUser', N'用户', NULL, 1, CAST(0x0000AA020100DD6C AS DateTime), CAST(0x0000AA020100DFC4 AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[t_role] OFF
/****** Object:  Table [dbo].[t_menu_role_action]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_menu_role_action](
	[MenuId] [int] NULL,
	[RoleId] [int] NULL,
	[ActionId] [int] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (1, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 3, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 3, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 3, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (6, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (7, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (8, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (9, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (11, 0, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (1, 2, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (11, 2, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (6, 2, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (7, 2, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (8, 2, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (9, 2, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 2, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 2, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 2, 7)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (12, 0, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (13, 0, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (14, 0, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (1, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (15, 0, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 1)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 2)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 8)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 1)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 2)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 6)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 1)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 2)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 5)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 1)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 2)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (11, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (6, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (7, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (8, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (9, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 1, 7)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (12, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (13, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (14, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (15, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (15, 1, 1)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (15, 1, 2)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (15, 1, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (15, 1, 4)
/****** Object:  Table [dbo].[t_menu_action]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_menu_action](
	[MenuId] [int] NULL,
	[ActionId] [int] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (2, 1)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (2, 2)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (2, 4)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (2, 8)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (3, 1)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (3, 2)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (3, 3)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (3, 4)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (3, 6)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (4, 1)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (4, 2)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (4, 3)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (4, 4)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (4, 5)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (5, 1)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (5, 2)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (5, 3)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (5, 4)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (10, 4)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (10, 7)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (15, 1)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (15, 2)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (15, 3)
INSERT [dbo].[t_menu_action] ([MenuId], [ActionId]) VALUES (15, 4)
/****** Object:  Table [dbo].[t_menu]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [varchar](20) NULL,
	[MenuUrl] [varchar](60) NULL,
	[MenuIcon] [varchar](30) NULL,
	[OrderNo] [tinyint] NOT NULL,
	[ParentId] [int] NOT NULL,
	[Status] [bit] NULL,
	[CreateOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[CreateBy] [int] NULL,
	[UpdateBy] [int] NULL,
 CONSTRAINT [PK_t_menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[t_menu] ON
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (1, N'权限管理', NULL, N'icon-setting-permissions', 0, 0, 1, CAST(0x0000AA0200F81498 AS DateTime), CAST(0x0000AA6F011B497C AS DateTime), 0, 1)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (2, N'菜单管理', N'/permissions/menu', N'icon-caidan', 1, 1, 1, CAST(0x0000AA0200F81BA0 AS DateTime), CAST(0x0000AA0200F81F24 AS DateTime), 0, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (3, N'角色管理', N'/permissions/role', N'icon-jiaoseguanli', 2, 1, 1, CAST(0x0000AA0200F8217C AS DateTime), CAST(0x0000AA0200F8262C AS DateTime), 0, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (4, N'用户管理', N'/permissions/user', N'icon-yonghu', 3, 1, 1, CAST(0x0000AA0200F829B0 AS DateTime), CAST(0x0000AA0200F82D34 AS DateTime), 0, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (5, N'操作管理', N'/permissions/action', N'icon-shezhi', 4, 1, 1, CAST(0x0000AA0200F831E4 AS DateTime), CAST(0x0000AA0200F83694 AS DateTime), 0, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (6, N'系统设置', NULL, N'icon-xitong', 0, 0, 1, CAST(0x0000AA0200F83A18 AS DateTime), CAST(0x0000AA0200F83C70 AS DateTime), 0, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (7, N'网站设置', N'/sysset/website', N'icon-ditu', 1, 6, 1, CAST(0x0000AA0200F83FF4 AS DateTime), CAST(0x0000AA0200F8424C AS DateTime), 0, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (8, N'基本资料', N'/SysSet/info', N'icon-jibenziliao', 2, 6, 1, CAST(0x0000AA0200F845D0 AS DateTime), CAST(0x0000AA0200F84828 AS DateTime), 0, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (9, N'修改密码', N'/SysSet/password', N'icon-xiugaimima', 3, 6, 1, CAST(0x0000AA0200F84CD8 AS DateTime), CAST(0x0000AA0200F8505C AS DateTime), 0, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (10, N'日志管理', N'/SysSet/Log', N'icon-xitongrizhi', 4, 6, 1, CAST(0x0000AA0200F852B4 AS DateTime), CAST(0x0000AA0200F85638 AS DateTime), 0, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (11, N'部门管理', N'/Permissions/Department/Index', N'icon-chakan', 5, 1, 1, CAST(0x0000AB1900E2500E AS DateTime), CAST(0x0000AB1900E2500E AS DateTime), 1, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (12, N'档案管理', NULL, N'icon-xitongrizhi', 0, 0, 1, CAST(0x0000AB1901083353 AS DateTime), CAST(0x0000AB1901083353 AS DateTime), 1, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (13, N'档案新增', N'/RecordTrancation/RecordOperation/RecordAdd', N'icon-add', 1, 12, 1, CAST(0x0000AB19010877EB AS DateTime), CAST(0x0000AB19010877EB AS DateTime), 1, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (14, N'银行黑名单', NULL, N'icon-xitongrizhi', 0, 0, 1, CAST(0x0000AB21010DAA8D AS DateTime), CAST(0x0000AB21010DAA8D AS DateTime), 1, 0)
INSERT [dbo].[t_menu] ([Id], [MenuName], [MenuUrl], [MenuIcon], [OrderNo], [ParentId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (15, N'黑名单查看', N'/InforSearch/BlackList/Index', N'icon-chakan', 1, 14, 1, CAST(0x0000AB21010DE4E8 AS DateTime), CAST(0x0000AB21010DE4E8 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[t_menu] OFF
/****** Object:  Table [dbo].[t_log]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogType] [varchar](20) NULL,
	[UserName] [varchar](20) NULL,
	[RealName] [varchar](20) NULL,
	[ModuleName] [varchar](20) NULL,
	[Description] [varchar](200) NULL,
	[CreateOn] [datetime] NULL,
	[IPAddress] [varchar](20) NULL,
	[IPAddressName] [varchar](100) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_t_log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[t_log] ON
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (1, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AA6D017A0318 AS DateTime), N'192.168.1.2', N'本地局域网', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (2, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AA6D017E51D4 AS DateTime), N'192.168.1.2', N'本地局域网', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (3, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AA6D017E57B0 AS DateTime), N'192.168.1.2', N'本地局域网', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (4, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AAFF0182C5F7 AS DateTime), N'192.168.2.100', N'本地局域网', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (5, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB0001323127 AS DateTime), N'192.168.2.100', N'本地局域网', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (6, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB020099ED7B AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (7, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB02009D109D AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (8, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB02009D1645 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (9, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB02009D1BD8 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (10, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB02009D216C AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (11, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB02009D2B7C AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (12, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB02009D3D0D AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (13, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB02009D5CF8 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (14, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB02009D6EFA AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (15, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB02009EEA01 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (16, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB02009EFB78 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (17, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1900B71A8C AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (18, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1900DFC0F8 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (19, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB1900E3B10F AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (20, N'Login', N'8287072', N'方勇', N'系统登录', N'登录成功', CAST(0x0000AB1900E3C93C AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (21, N'Exit', N'8287072', N'方勇', NULL, N'安全退出系统', CAST(0x0000AB1900E3DEBE AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (22, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1900E3F22C AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (23, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1900E54F99 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (24, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1900E690A1 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (25, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB1900E6B0B3 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (26, N'Login', N'8287072', N'方勇', N'系统登录', N'登录成功', CAST(0x0000AB1900E6C72E AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (27, N'Exit', N'8287072', N'方勇', NULL, N'安全退出系统', CAST(0x0000AB1900E73CC7 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (28, N'Login', N'8287072', N'方勇', N'系统登录', N'登录成功', CAST(0x0000AB1900E74CF0 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (29, N'Exit', N'8287072', N'方勇', NULL, N'安全退出系统', CAST(0x0000AB1900E76767 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (30, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1900E778E0 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (31, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1900FC93F8 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (32, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB1900FC9FBE AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (33, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1900FF7B27 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (34, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB190107A2A9 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (35, N'Login', N'admin', N'admin', N'系统登录', N'登录失败，验证码错误，请重新输入', CAST(0x0000AB1A00B83C00 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (36, N'Login', N'admin', N'admin', N'系统登录', N'登录失败，验证码错误，请重新输入', CAST(0x0000AB1A00B8461B AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (37, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1A00B8572F AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (38, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1A00B921F5 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (39, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E00AC9CA9 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (40, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E00DF0EBE AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (41, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E00E2B2C0 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (42, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E00E30C8E AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (43, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E00E42EF7 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (44, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E00E4F97A AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (45, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E00E5AF23 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (46, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E00F3E3DE AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (47, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E00FAA0DD AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (48, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E011192E9 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (49, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E0117AB33 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (50, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E01189A17 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (51, N'Login', N'admin', N'admin', N'系统登录', N'登录失败，验证码错误，请重新输入', CAST(0x0000AB1E011907DD AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (52, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E01190CAB AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (53, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E012BA912 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (54, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB1E013D5F9D AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (55, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2000995812 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (56, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB20009E7FED AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (57, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2000F10EC3 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (58, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB200106DABF AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (59, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2001081F29 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (60, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AB20010ADB03 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (61, N'Login', N'8287072', N'方勇', N'系统登录', N'登录成功', CAST(0x0000AB20010AE1B9 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (62, N'Exit', N'8287072', N'方勇', NULL, N'安全退出系统', CAST(0x0000AB20010AEF40 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (63, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB20010AF5D4 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (64, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB21010D76F6 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (65, N'Login', N'admin', N'admin', N'系统登录', N'登录失败，验证码错误，请重新输入', CAST(0x0000AB21010E5EE1 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (66, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB21010E6F44 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (67, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2101106554 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (68, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB21011133C9 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (69, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB210111873E AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (70, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB210112E82A AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (71, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2500B34329 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (72, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2500E87FF1 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (73, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2500E92AE2 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (74, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2600CEFCCC AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (75, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2600F253C3 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (76, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2600F48570 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (77, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2600FD3305 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (78, N'Login', N'admin', N'admin', N'系统登录', N'登录失败，验证码错误，请重新输入', CAST(0x0000AB2600FD9FF5 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (79, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2600FDA4BF AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (80, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB26010A0F98 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (81, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB260122ABB5 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (82, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB27009787A1 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (83, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2700A81625 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (84, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2800A2E9DC AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (85, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2800AA8F05 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (86, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2800E7AC04 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (87, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB280119E4FF AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (88, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2C00A37DD3 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (89, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2C00A4C505 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (90, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2C011213B2 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (91, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2C0112DD1E AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (92, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2D009E0DFF AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (93, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2D00C111F9 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (94, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2D00CDEC85 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (95, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2D00E88EC8 AS DateTime), N'155.25.1.100', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (96, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2D00EE33FC AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (97, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2D00EF0112 AS DateTime), N'127.0.0.1', N'', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (98, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB2D010C1586 AS DateTime), N'155.25.1.100', N'', 1)
SET IDENTITY_INSERT [dbo].[t_log] OFF
/****** Object:  Table [dbo].[t_DepartmentUser]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_DepartmentUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_t_DepartmentUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[t_DepartmentUser] ON
INSERT [dbo].[t_DepartmentUser] ([Id], [DepartmentId], [UserId]) VALUES (1, 1, 1)
INSERT [dbo].[t_DepartmentUser] ([Id], [DepartmentId], [UserId]) VALUES (2, 2, 5)
INSERT [dbo].[t_DepartmentUser] ([Id], [DepartmentId], [UserId]) VALUES (3, 2, 2)
SET IDENTITY_INSERT [dbo].[t_DepartmentUser] OFF
/****** Object:  Table [dbo].[t_department]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentCode] [varchar](10) NOT NULL,
	[DepartmentName] [nvarchar](50) NOT NULL,
	[ParentId] [int] NOT NULL,
	[CreateOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[CreateBy] [int] NULL,
	[UpdateBy] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_t_department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[t_department] ON
INSERT [dbo].[t_department] ([Id], [DepartmentCode], [DepartmentName], [ParentId], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [Status]) VALUES (1, N'828000', N'镇海农商银行', 0, CAST(0x0000AA0200F2F2EC AS DateTime), CAST(0x0000AA0200F2F2EC AS DateTime), 1, 1, 1)
INSERT [dbo].[t_department] ([Id], [DepartmentCode], [DepartmentName], [ParentId], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [Status]) VALUES (2, N'828010', N'科技信息部', 1, CAST(0x0000AA0200F2F2EC AS DateTime), CAST(0x0000AA0200F2F2EC AS DateTime), 1, 1, 1)
INSERT [dbo].[t_department] ([Id], [DepartmentCode], [DepartmentName], [ParentId], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [Status]) VALUES (3, N'828020', N'人力资源部', 1, CAST(0x0000AB1900E2C923 AS DateTime), CAST(0x0000AB1900E2C922 AS DateTime), 1, 0, 1)
SET IDENTITY_INSERT [dbo].[t_department] OFF
/****** Object:  Table [dbo].[t_action]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_action](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActionCode] [varchar](20) NULL,
	[ActionName] [varchar](20) NULL,
	[Position] [int] NULL,
	[Icon] [varchar](30) NULL,
	[Method] [varchar](20) NULL,
	[Remark] [varchar](50) NULL,
	[OrderBy] [int] NULL,
	[Status] [bit] NULL,
	[CreateOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[CreateBy] [int] NULL,
	[UpdateBy] [int] NULL,
	[ClassName] [varchar](30) NULL,
 CONSTRAINT [PK_t_action] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[t_action] ON
INSERT [dbo].[t_action] ([Id], [ActionCode], [ActionName], [Position], [Icon], [Method], [Remark], [OrderBy], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [ClassName]) VALUES (1, N'Add', N'添加', 1, N'icon-add', N'Add', NULL, 0, 1, CAST(0x0000AA0200F2F2EC AS DateTime), CAST(0x0000AA6D0183D974 AS DateTime), 0, 1, NULL)
INSERT [dbo].[t_action] ([Id], [ActionCode], [ActionName], [Position], [Icon], [Method], [Remark], [OrderBy], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [ClassName]) VALUES (2, N'edit', N'编辑', 0, N'icon-bianji', N'edit', NULL, 0, 1, CAST(0x0000AA0200F2F670 AS DateTime), CAST(0x0000AA0200F32B2C AS DateTime), 0, 0, NULL)
INSERT [dbo].[t_action] ([Id], [ActionCode], [ActionName], [Position], [Icon], [Method], [Remark], [OrderBy], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [ClassName]) VALUES (3, N'detail', N'查看', 0, N'icon-chakan', N'detail', NULL, 0, 1, CAST(0x0000AA0200F2F9F4 AS DateTime), CAST(0x0000AA0200F32EB0 AS DateTime), 0, 0, N'layui-btn-normal')
INSERT [dbo].[t_action] ([Id], [ActionCode], [ActionName], [Position], [Icon], [Method], [Remark], [OrderBy], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [ClassName]) VALUES (4, N'del', N'删除', 0, N'icon-guanbi', N'del', NULL, 0, 1, CAST(0x0000AA0200F2FD78 AS DateTime), CAST(0x0000AA0200F33234 AS DateTime), 0, 0, N'layui-btn-danger')
INSERT [dbo].[t_action] ([Id], [ActionCode], [ActionName], [Position], [Icon], [Method], [Remark], [OrderBy], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [ClassName]) VALUES (5, N'reset', N'重置密码', 0, N'icon-reset', N'reset', NULL, 0, 1, CAST(0x0000AA0200F300FC AS DateTime), CAST(0x0000AA6D01849E54 AS DateTime), 0, 1, N'layui-btn-warm')
INSERT [dbo].[t_action] ([Id], [ActionCode], [ActionName], [Position], [Icon], [Method], [Remark], [OrderBy], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [ClassName]) VALUES (6, N'assign', N'分配权限', 0, N'icon-jiaoseguanli', N'assign', NULL, 0, 1, CAST(0x0000AA0200F30480 AS DateTime), CAST(0x0000AA0200F33A68 AS DateTime), 0, 0, NULL)
INSERT [dbo].[t_action] ([Id], [ActionCode], [ActionName], [Position], [Icon], [Method], [Remark], [OrderBy], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [ClassName]) VALUES (7, N'BatchDel', N'批量删除', 1, N'icon-shanchu', N'BatchDel', NULL, 0, 1, CAST(0x0000AA6D01846F74 AS DateTime), CAST(0x0000AA0200F33A68 AS DateTime), 1, 0, NULL)
INSERT [dbo].[t_action] ([Id], [ActionCode], [ActionName], [Position], [Icon], [Method], [Remark], [OrderBy], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [ClassName]) VALUES (8, N'menu_action', N'菜单权限', 0, N'icon-setting-permissions', N'menu_action', NULL, 0, 1, CAST(0x0000AA6F011848BC AS DateTime), CAST(0x0000AA0200F33A68 AS DateTime), 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[t_action] OFF
/****** Object:  Table [dbo].[SelectType]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SelectType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileType] [int] NOT NULL,
	[SelectTypeNum] [int] NOT NULL,
 CONSTRAINT [PK_SelectType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SelectType] ON
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (1, 1, 1)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (3, 3, 1)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (4, 5, 1)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (5, 6, 1)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (6, 7, 1)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (7, 8, 1)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (8, 9, 1)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (9, 10, 1)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (11, 11, 1)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (12, 2, 2)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (13, 4, 2)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (14, 5, 2)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (15, 6, 2)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (16, 7, 2)
INSERT [dbo].[SelectType] ([Id], [FileType], [SelectTypeNum]) VALUES (17, 8, 2)
SET IDENTITY_INSERT [dbo].[SelectType] OFF
/****** Object:  Table [dbo].[RepeatedlyNotification]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RepeatedlyNotification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](10) NOT NULL,
	[RecordID] [char](16) NOT NULL,
	[PhoneNumber] [varchar](11) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecordTable]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecordTable](
	[RecordID] [char](16) NOT NULL,
	[RecordUserName] [nvarchar](50) NOT NULL,
	[RecordUserCode] [char](21) NOT NULL,
	[RecordManagerDepartment] [char](6) NOT NULL,
	[RecordStatus] [int] NOT NULL,
	[CreditDueDate] [datetime] NOT NULL,
	[RecordManager] [varchar](10) NOT NULL,
	[RecordStorageLoc] [nvarchar](50) NULL,
	[RecordType] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[HandOverTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[RecordTable] ([RecordID], [RecordUserName], [RecordUserCode], [RecordManagerDepartment], [RecordStatus], [CreditDueDate], [RecordManager], [RecordStorageLoc], [RecordType], [CreateTime], [HandOverTime]) VALUES (N'AAAA82801000003 ', N'方勇', N'8287072              ', N'828010', 0, CAST(0x0000AB3400000000 AS DateTime), N'8287072', NULL, 1, CAST(0x0000AB2800E89C4F AS DateTime), CAST(0xFFFF2E4600000000 AS DateTime))
INSERT [dbo].[RecordTable] ([RecordID], [RecordUserName], [RecordUserCode], [RecordManagerDepartment], [RecordStatus], [CreditDueDate], [RecordManager], [RecordStorageLoc], [RecordType], [CreateTime], [HandOverTime]) VALUES (N'AAAA82801000004 ', N'方勇', N'8287072              ', N'828010', 0, CAST(0x0000AB3400000000 AS DateTime), N'8287072', NULL, 2, CAST(0x0000AB2800EA364F AS DateTime), CAST(0xFFFF2E4600000000 AS DateTime))
/****** Object:  Table [dbo].[RecordStatus]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecordStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StatusID] [int] NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecordModification]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecordModification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModificationID] [int] NOT NULL,
	[RecordID] [char](16) NOT NULL,
	[ModificationString] [nvarchar](200) NOT NULL,
	[ModificationTime] [datetime] NOT NULL,
	[ModificationManager] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecordList]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecordList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RecordID] [char](16) NOT NULL,
	[Amount] [int] NOT NULL,
	[FileID] [int] NOT NULL,
	[FileFilterNum] [int] NULL,
	[ExpirationTime] [datetime] NULL,
 CONSTRAINT [PK__RecordLi__3214EC2734C8D9D1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[RecordList] ON
INSERT [dbo].[RecordList] ([ID], [RecordID], [Amount], [FileID], [FileFilterNum], [ExpirationTime]) VALUES (2, N'AAAA82801000003 ', 3, 1, 0, CAST(0x0000AB3400000000 AS DateTime))
INSERT [dbo].[RecordList] ([ID], [RecordID], [Amount], [FileID], [FileFilterNum], [ExpirationTime]) VALUES (3, N'AAAA82801000003 ', 1, 5, 0, CAST(0x0000AB3400000000 AS DateTime))
INSERT [dbo].[RecordList] ([ID], [RecordID], [Amount], [FileID], [FileFilterNum], [ExpirationTime]) VALUES (4, N'AAAA82801000003 ', 1, 112, 0, CAST(0x0000AB3400000000 AS DateTime))
INSERT [dbo].[RecordList] ([ID], [RecordID], [Amount], [FileID], [FileFilterNum], [ExpirationTime]) VALUES (5, N'AAAA82801000003 ', 1, 120, 0, CAST(0xFFFF2E4600000000 AS DateTime))
INSERT [dbo].[RecordList] ([ID], [RecordID], [Amount], [FileID], [FileFilterNum], [ExpirationTime]) VALUES (6, N'AAAA82801000004 ', 1, 1, 0, CAST(0x0000AB3400000000 AS DateTime))
INSERT [dbo].[RecordList] ([ID], [RecordID], [Amount], [FileID], [FileFilterNum], [ExpirationTime]) VALUES (7, N'AAAA82801000004 ', 1, 5, 0, CAST(0x0000AB3400000000 AS DateTime))
INSERT [dbo].[RecordList] ([ID], [RecordID], [Amount], [FileID], [FileFilterNum], [ExpirationTime]) VALUES (8, N'AAAA82801000004 ', 1, 112, 0, CAST(0x0000AB3400000000 AS DateTime))
INSERT [dbo].[RecordList] ([ID], [RecordID], [Amount], [FileID], [FileFilterNum], [ExpirationTime]) VALUES (9, N'AAAA82801000004 ', 1, 120, 0, CAST(0xFFFF2E4600000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[RecordList] OFF
/****** Object:  Table [dbo].[RecordFileTypeTable]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RecordFileTypeTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RecordFileTypeString] [char](2) NOT NULL,
	[RecordTypeName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[RecordFileTypeTable] ON
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (1, N'AA', N'企业授信类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (2, N'AB', N'个人授信类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (3, N'AC', N'企业管理类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (4, N'AD', N'个人管理类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (5, N'AE', N'担保企业管理类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (6, N'AF', N'担保个人管理类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (7, N'AG', N'权证类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (8, N'AH', N'贷款业务要件类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (9, N'AI', N'承兑业务要件类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (10, N'AJ', N'贴现业务要件类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (11, N'AK', N'保函业务要件类')
SET IDENTITY_INSERT [dbo].[RecordFileTypeTable] OFF
/****** Object:  Table [dbo].[OtherFileListTable]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OtherFileListTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RecordID] [char](16) NOT NULL,
	[RecordFileType] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[ExpirationTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[OtherFileListTable] ON
INSERT [dbo].[OtherFileListTable] ([ID], [RecordID], [RecordFileType], [Amount], [FileName], [ExpirationTime]) VALUES (1, N'AAAA82801000003 ', 1, 2, N'测试', CAST(0xFFFF2E4600000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[OtherFileListTable] OFF
/****** Object:  Table [dbo].[InforSearchOperate]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InforSearchOperate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OperaterId] [int] NOT NULL,
	[OperateTime] [datetime] NOT NULL,
	[OperateMsg] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_InforSearchOperate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[InforSearchOperate] ON
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (1, 1, CAST(0x0000AB2600FE0277 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (2, 1, CAST(0x0000AB2600FE1C7B AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (3, 1, CAST(0x0000AB2600FEC526 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (4, 1, CAST(0x0000AB2600FFE6D7 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (5, 1, CAST(0x0000AB26010A1528 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (6, 1, CAST(0x0000AB26010A2656 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (7, 1, CAST(0x0000AB26010AF6E3 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (8, 1, CAST(0x0000AB26010B0829 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (9, 1, CAST(0x0000AB26010CB51A AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (10, 1, CAST(0x0000AB26010EB947 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (11, 1, CAST(0x0000AB26010F095E AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (12, 1, CAST(0x0000AB26010FA08B AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (13, 1, CAST(0x0000AB260110069E AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (14, 1, CAST(0x0000AB260117FE70 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (15, 1, CAST(0x0000AB260118593E AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (16, 1, CAST(0x0000AB260118F992 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (17, 1, CAST(0x0000AB260122B1C5 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (18, 1, CAST(0x0000AB260122CA50 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (19, 1, CAST(0x0000AB2700978F29 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (20, 1, CAST(0x0000AB270097E373 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (21, 1, CAST(0x0000AB2700983B5B AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (22, 1, CAST(0x0000AB2700995ED6 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (23, 1, CAST(0x0000AB27009A174B AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (24, 1, CAST(0x0000AB27009AEFB1 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (25, 1, CAST(0x0000AB27009B045F AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (26, 1, CAST(0x0000AB27009B2439 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (27, 1, CAST(0x0000AB27009B34A8 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (28, 1, CAST(0x0000AB27009D2F9B AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (29, 1, CAST(0x0000AB27009D5996 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (30, 1, CAST(0x0000AB27009DEC63 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (31, 1, CAST(0x0000AB27009DFAE6 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (32, 1, CAST(0x0000AB27009E01B6 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (33, 1, CAST(0x0000AB27009E117F AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (34, 1, CAST(0x0000AB27009E3873 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (35, 1, CAST(0x0000AB27009EED94 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (36, 1, CAST(0x0000AB27009F2C4D AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (37, 1, CAST(0x0000AB2700A140C6 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (38, 1, CAST(0x0000AB2700A1D8ED AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (39, 1, CAST(0x0000AB2700A205D6 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (40, 1, CAST(0x0000AB2700A21A38 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (41, 1, CAST(0x0000AB2700A249CA AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (42, 1, CAST(0x0000AB2700A2853E AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (43, 1, CAST(0x0000AB2700A2C395 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (44, 1, CAST(0x0000AB2700A2DBDC AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (45, 1, CAST(0x0000AB2700A30180 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (46, 1, CAST(0x0000AB2700A33093 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (47, 1, CAST(0x0000AB2700A35526 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (48, 1, CAST(0x0000AB2700A38891 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (49, 1, CAST(0x0000AB2700A3AAA9 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (50, 1, CAST(0x0000AB2700A44091 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (51, 1, CAST(0x0000AB2700A46132 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (52, 1, CAST(0x0000AB2700A481D6 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (53, 1, CAST(0x0000AB2700A49F65 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (54, 1, CAST(0x0000AB2700A4EF73 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (55, 1, CAST(0x0000AB2700A63D09 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (56, 1, CAST(0x0000AB2700A75B26 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (57, 1, CAST(0x0000AB2700A7A42C AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (58, 1, CAST(0x0000AB2700A87FAB AS DateTime), N'添加    银行名测试行社,拉黑原因测试')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (59, 1, CAST(0x0000AB2700A8855E AS DateTime), N'查看    银行名为测试行社,拉黑原因测试')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (60, 1, CAST(0x0000AB2700A8A225 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (61, 1, CAST(0x0000AB2700A8A561 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (62, 1, CAST(0x0000AB2700A933BF AS DateTime), N'查看    银行名为测试行社,拉黑原因今天测试文件使用在这些东西上面可以得到更好的使用体验,或许你还没发现，但是这是一个对未来对自己和对别人非常好的事情，希望你真的好好考虑下，现在只能先这么做吧')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (63, 1, CAST(0x0000AB2700ADA5D1 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (64, 1, CAST(0x0000AB2700ADE1A1 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (65, 1, CAST(0x0000AB2700AF4552 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (66, 1, CAST(0x0000AB2700AFBBDB AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (67, 1, CAST(0x0000AB2700B0B276 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
INSERT [dbo].[InforSearchOperate] ([Id], [OperaterId], [OperateTime], [OperateMsg]) VALUES (68, 1, CAST(0x0000AB2700B0D516 AS DateTime), N'查看    银行名为镇海农商银行,拉黑原因欠钱不还')
SET IDENTITY_INSERT [dbo].[InforSearchOperate] OFF
/****** Object:  Table [dbo].[GetNum]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GetNum](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Msg] [int] NOT NULL,
 CONSTRAINT [PK_GetNum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GetNum] ON
INSERT [dbo].[GetNum] ([Id], [Msg]) VALUES (1, 1)
INSERT [dbo].[GetNum] ([Id], [Msg]) VALUES (2, 1)
INSERT [dbo].[GetNum] ([Id], [Msg]) VALUES (3, 1)
INSERT [dbo].[GetNum] ([Id], [Msg]) VALUES (4, 1)
SET IDENTITY_INSERT [dbo].[GetNum] OFF
/****** Object:  Table [dbo].[FileList]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileList](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[ActiveMark] [bit] NOT NULL,
	[RecordFileType] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[FileList] ON
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (1, N'支行审贷小组审议表', 1, 1)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (3, N'信用报告查询授权书', 1, 1)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (4, N'授信审议及批复表', 1, 1)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (5, N'信用报告', 1, 1)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (6, N'支行审贷小组审议表', 1, 2)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (8, N'信用报告查询授权书', 1, 2)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (9, N'授信审议及批复表', 1, 2)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (10, N'普惠金融对私客户评定表', 1, 2)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (11, N'客户评价结果记录', 1, 2)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (12, N'普惠金融客户信息采集表', 1, 2)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (13, N'对私客户信息附表', 1, 2)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (14, N'信用报告', 1, 2)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (15, N'小额创业贷款申请表', 1, 2)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (17, N'客户信息表', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (18, N'营业执照正副本', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (19, N'开户许可证', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (20, N'法人身份证', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (21, N'法人、股东签字及印鉴样本', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (22, N'企业资信证明', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (23, N'企业章程(合伙协议)', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (24, N'保证函及身份证', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (25, N'送达地址确认书(补充合同)', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (26, N'授权委托书', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (27, N'被授权人身份证', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (28, N'特种行业许可证', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (29, N'企业财务报表', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (30, N'贷后检查报告、影像资料', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (31, N'房地产"四证"', 1, 3)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (32, N'客户信息表', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (33, N'自然人身份证', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (34, N'配偶身份证', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (35, N'户口本', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (36, N'婚姻证明', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (37, N'存款证明', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (38, N'用途证明(经营类)', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (39, N'收入证明(消费类)', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (40, N'客户信用评级测定表', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (41, N'保证函及身份证', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (42, N'送达地址确认书', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (43, N'货后检查报告、影像资料', 1, 4)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (44, N'客户信息表', 1, 5)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (45, N'营业执照正副本', 1, 5)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (46, N'法人身份证', 1, 5)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (48, N'企业章程(合伙协议)', 1, 5)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (49, N'同意担保决议书', 1, 5)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (50, N'开户许可证', 1, 5)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (51, N'保证函及身份证', 1, 5)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (52, N'企业财务报表', 1, 5)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (53, N'客户信息表', 1, 6)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (54, N'身份证', 1, 6)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (55, N'配偶身份证', 1, 6)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (56, N'户口本', 1, 6)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (57, N'婚姻证明', 1, 6)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (58, N'收入证明(资产证明)', 1, 6)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (59, N'保证函及身份证', 1, 6)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (60, N'抵押合同', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (61, N'抵押物清单', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (62, N'房地产价格评估表及作价协议(省办)', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (63, N'房地产价格评估表(外部)', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (64, N'抵押物权证', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (65, N'他项权证与权力凭证', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (66, N'抵押(权利)品保管凭证', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (67, N'保险单及入库凭证', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (68, N'有价证券质押融资、质押物登记止付(撤销)通知书', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (70, N'抵押房产未出租承诺书及出租协议', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (71, N'具结书', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (72, N'抵押物实地照片', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (73, N'建筑物工程款支付情况', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (74, N'抵押授信协议', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (75, N'抵押登记申请表', 1, 7)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (76, N'出账申请书', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (77, N'借款借据', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (78, N'借款申请书', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (79, N'贷款利率综合定价审批书', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (80, N'借款合同', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (81, N'担保合同', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (83, N'贷款调查报告', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (84, N'借款审批书', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (85, N'风险审查单', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (86, N'提款申请书', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (87, N'受托支付审核单', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (88, N'购销合同', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (89, N'支付凭证', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (90, N'保险单', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (91, N'个人客户面谈记录', 1, 8)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (92, N'承兑申请书', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (93, N'承兑协议', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (94, N'审批书', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (95, N'商品交易或贸易合同', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (96, N'增值税发票', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (97, N'保证金进账单', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (98, N'承兑汇票清单', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (99, N'出账通知书', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (100, N'调查报告、影像资料', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (101, N'担保合同', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (102, N'承兑汇票复印件', 1, 9)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (103, N'贴现申请书', 1, 10)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (104, N'贴现合同', 1, 10)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (105, N'审批书', 1, 10)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (106, N'商品交易或贸易合同', 1, 10)
GO
print 'Processed 100 total records'
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (107, N'增值税发票', 1, 10)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (108, N'贴现汇票清单', 1, 10)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (109, N'贴现汇票复印件', 1, 10)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (110, N'贴现凭证', 1, 10)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (111, N'查询复查清单', 1, 10)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (112, N'出具保函申请书', 1, 11)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (114, N'出具保函协议', 1, 11)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (115, N'保函审批书', 1, 11)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (116, N'保函文书', 1, 11)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (117, N'保函所涉项目合同', 1, 11)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (118, N'保函受益人基本资信资料', 1, 11)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (119, N'保函送达通知书回执', 1, 11)
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType]) VALUES (120, N'保函失效通知书回执', 1, 11)
SET IDENTITY_INSERT [dbo].[FileList] OFF
/****** Object:  Table [dbo].[DeletedRecordTalbe]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeletedRecordTalbe](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RecordID] [char](16) NOT NULL,
	[DeletedTime] [datetime] NOT NULL,
	[RecordUserName] [nvarchar](50) NOT NULL,
	[RecordUserCode] [char](21) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BorrowList]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BorrowList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BorrowID] [char](18) NOT NULL,
	[RecordID] [char](16) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BorrowHistory]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BorrowHistory](
	[BorrowID] [char](18) NOT NULL,
	[BorrowUser] [nvarchar](10) NOT NULL,
	[BorrowTime] [datetime] NOT NULL,
	[ReturnTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[BorrowID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BlackList]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlackList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BankName] [nvarchar](100) NOT NULL,
	[RefuseReason] [nvarchar](500) NULL,
	[CreateOn] [datetime] NOT NULL,
	[EditorId] [int] NOT NULL,
	[OtherDescription] [nvarchar](500) NULL,
 CONSTRAINT [PK_BlackList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BlackList] ON
INSERT [dbo].[BlackList] ([Id], [BankName], [RefuseReason], [CreateOn], [EditorId], [OtherDescription]) VALUES (1, N'镇海农商银行', N'欠钱不还', CAST(0x0000ABDB017A0318 AS DateTime), 1, NULL)
INSERT [dbo].[BlackList] ([Id], [BankName], [RefuseReason], [CreateOn], [EditorId], [OtherDescription]) VALUES (5, N'北仑农商银行', N'测试提交', CAST(0x0000AB210112FAB0 AS DateTime), 1, NULL)
INSERT [dbo].[BlackList] ([Id], [BankName], [RefuseReason], [CreateOn], [EditorId], [OtherDescription]) VALUES (6, N'测试行社', N'今天测试文件使用在这些东西上面可以得到更好的使用体验,或许你还没发现，但是这是一个对未来对自己和对别人非常好的事情，希望你真的好好考虑下，现在只能先这么做吧', CAST(0x0000AB2700A87FA1 AS DateTime), 1, N'测试')
SET IDENTITY_INSERT [dbo].[BlackList] OFF
/****** Object:  Table [dbo].[ApplyCopyTable]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplyCopyTable](
	[BorrowID] [char](18) NOT NULL,
	[ApplyTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[FinishTime] [datetime] NULL,
	[RefuseReason] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[BorrowID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApplyCopyFileList]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplyCopyFileList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BorrowID] [char](18) NOT NULL,
	[RecordID] [char](16) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApplyBorrowTable]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplyBorrowTable](
	[BorrowID] [char](18) NOT NULL,
	[ApplyTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[FinishTime] [datetime] NULL,
	[RefuseReason] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[BorrowID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApplyBorrowFileList]    Script Date: 12/24/2019 17:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplyBorrowFileList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BorrowID] [char](18) NOT NULL,
	[RecordID] [char](16) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_t_menu_OrderNo]    Script Date: 12/24/2019 17:27:07 ******/
ALTER TABLE [dbo].[t_menu] ADD  CONSTRAINT [DF_t_menu_OrderNo]  DEFAULT ((0)) FOR [OrderNo]
GO
/****** Object:  Default [DF_t_menu_ParentId]    Script Date: 12/24/2019 17:27:07 ******/
ALTER TABLE [dbo].[t_menu] ADD  CONSTRAINT [DF_t_menu_ParentId]  DEFAULT ((0)) FOR [ParentId]
GO
