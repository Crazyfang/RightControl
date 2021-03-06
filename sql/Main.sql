USE [rightcontrol]
GO
/****** Object:  Table [dbo].[ApplyBorrowFileList]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApplyBorrowTable]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApplyCopyFileList]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApplyCopyTable]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BorrowHistory]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BorrowList]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeletedRecordTalbe]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FileList]    Script Date: 2019-12-08 23:12:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileList](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[ActiveMark] [bit] NOT NULL,
	[RecordFileType] [int] NOT NULL,
	[ExpirationTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OtherFileListTalbe]    Script Date: 2019-12-08 23:12:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OtherFileListTalbe](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RecordID] [char](16) NOT NULL,
	[RecordFileType] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[ExpirationTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecordFileTypeTable]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecordList]    Script Date: 2019-12-08 23:12:54 ******/
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
	[FileFilterNum] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecordModification]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecordStatus]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RecordTable]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RepeatedlyNotification]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_DepartmentUser]    Script Date: 2019-12-08 23:12:54 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[FileList] ON 

INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType], [ExpirationTime]) VALUES (1, N'身份证', 1, 1, CAST(0x0000ABDB017A0318 AS DateTime))
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType], [ExpirationTime]) VALUES (3, N'营业执照', 1, 1, CAST(0x0000ABDB017A0318 AS DateTime))
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType], [ExpirationTime]) VALUES (4, N'身份证', 1, 2, CAST(0x0000ABDB017A0318 AS DateTime))
INSERT [dbo].[FileList] ([FileID], [FileName], [ActiveMark], [RecordFileType], [ExpirationTime]) VALUES (5, N'营业执照', 1, 2, CAST(0x0000ABDB017A0318 AS DateTime))
SET IDENTITY_INSERT [dbo].[FileList] OFF
SET IDENTITY_INSERT [dbo].[RecordFileTypeTable] ON 

INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (1, N'AA', N'个人信贷类')
INSERT [dbo].[RecordFileTypeTable] ([ID], [RecordFileTypeString], [RecordTypeName]) VALUES (2, N'AB', N'企业信贷类')
SET IDENTITY_INSERT [dbo].[RecordFileTypeTable] OFF
SET IDENTITY_INSERT [dbo].[t_DepartmentUser] ON 

INSERT [dbo].[t_DepartmentUser] ([Id], [DepartmentId], [UserId]) VALUES (1, 1, 1)
SET IDENTITY_INSERT [dbo].[t_DepartmentUser] OFF
