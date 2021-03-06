USE [rightcontrol]
GO
/****** Object:  Table [dbo].[t_action]    Script Date: 2019-11-09 19:30:44 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_log]    Script Date: 2019-11-09 19:30:44 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_menu]    Script Date: 2019-11-09 19:30:44 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_menu_action]    Script Date: 2019-11-09 19:30:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_menu_action](
	[MenuId] [int] NULL,
	[ActionId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_menu_role_action]    Script Date: 2019-11-09 19:30:44 ******/
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
/****** Object:  Table [dbo].[t_role]    Script Date: 2019-11-09 19:30:44 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_user]    Script Date: 2019-11-09 19:30:44 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
SET IDENTITY_INSERT [dbo].[t_log] ON 

INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (1, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AA6D017A0318 AS DateTime), N'192.168.1.2', N'本地局域网', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (2, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AA6D017E51D4 AS DateTime), N'192.168.1.2', N'本地局域网', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (3, N'Exit', N'admin', N'超级管理员', NULL, N'安全退出系统', CAST(0x0000AA6D017E57B0 AS DateTime), N'192.168.1.2', N'本地局域网', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (4, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AAFF0182C5F7 AS DateTime), N'192.168.2.100', N'本地局域网', 1)
INSERT [dbo].[t_log] ([Id], [LogType], [UserName], [RealName], [ModuleName], [Description], [CreateOn], [IPAddress], [IPAddressName], [Status]) VALUES (5, N'Login', N'admin', N'超级管理员', N'系统登录', N'登录成功', CAST(0x0000AB0001323127 AS DateTime), N'192.168.2.100', N'本地局域网', 1)
SET IDENTITY_INSERT [dbo].[t_log] OFF
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
SET IDENTITY_INSERT [dbo].[t_menu] OFF
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
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (1, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (1, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 1)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 2)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 1, 8)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (2, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 1)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 2)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 1, 6)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (3, 3, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 1)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 2)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 1, 5)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (4, 3, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 1)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 2)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (5, 3, 3)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (6, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (6, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (7, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (7, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (8, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (8, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (9, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (9, 3, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 1, 0)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 1, 4)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 1, 7)
INSERT [dbo].[t_menu_role_action] ([MenuId], [RoleId], [ActionId]) VALUES (10, 3, 0)
SET IDENTITY_INSERT [dbo].[t_role] ON 

INSERT [dbo].[t_role] ([Id], [RoleCode], [RoleName], [Remark], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (1, N'SysAdmin', N'超级管理员', NULL, 1, CAST(0x0000AA020100CD04 AS DateTime), CAST(0x0000AA020100D1B4 AS DateTime), 0, 0)
INSERT [dbo].[t_role] ([Id], [RoleCode], [RoleName], [Remark], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (2, N'GeneralAdmin', N'普通管理员', NULL, 1, CAST(0x0000AA020100D8BC AS DateTime), CAST(0x0000AA020100D538 AS DateTime), 0, 0)
INSERT [dbo].[t_role] ([Id], [RoleCode], [RoleName], [Remark], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy]) VALUES (3, N'GeneralUser', N'用户', NULL, 1, CAST(0x0000AA020100DD6C AS DateTime), CAST(0x0000AA020100DFC4 AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[t_role] OFF
SET IDENTITY_INSERT [dbo].[t_user] ON 

INSERT [dbo].[t_user] ([Id], [UserName], [RealName], [PassWord], [RoleId], [Status], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [Gender], [Phone], [Email], [Remark], [HeadShot]) VALUES (1, N'admin', N'超级管理员', N'e10adc3949ba59abbe56e057f20f883e', 1, 1, CAST(0x0000AA02010CDA90 AS DateTime), CAST(0x0000AA02010CDA90 AS DateTime), 0, 1, 1, N'13429262043', N'fangyonglubu@qq.com', N'最高权限', N'/Upload/img/502.jpg')
SET IDENTITY_INSERT [dbo].[t_user] OFF
ALTER TABLE [dbo].[t_menu] ADD  CONSTRAINT [DF_t_menu_OrderNo]  DEFAULT ((0)) FOR [OrderNo]
GO
ALTER TABLE [dbo].[t_menu] ADD  CONSTRAINT [DF_t_menu_ParentId]  DEFAULT ((0)) FOR [ParentId]
GO
