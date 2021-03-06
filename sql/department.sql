USE [rightcontrol]
GO
/****** Object:  Table [dbo].[t_department]    Script Date: 2019-11-21 22:30:34 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[t_department] ON 

INSERT [dbo].[t_department] ([Id], [DepartmentCode], [DepartmentName], [ParentId], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [Status]) VALUES (1, N'828000', N'镇海农商银行', 0, CAST(0x0000AA0200F2F2EC AS DateTime), CAST(0x0000AA0200F2F2EC AS DateTime), 1, 1, 1)
INSERT [dbo].[t_department] ([Id], [DepartmentCode], [DepartmentName], [ParentId], [CreateOn], [UpdateOn], [CreateBy], [UpdateBy], [Status]) VALUES (2, N'828010', N'科技信息部', 1, CAST(0x0000AA0200F2F2EC AS DateTime), CAST(0x0000AA0200F2F2EC AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[t_department] OFF
