USE [rightcontrol]
GO
/****** Object:  Table [dbo].[BlackList]    Script Date: 2019-12-11 21:22:35 ******/
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
 CONSTRAINT [PK_BlackList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[BlackList] ON 

INSERT [dbo].[BlackList] ([Id], [BankName], [RefuseReason], [CreateOn], [EditorId]) VALUES (1, N'镇海农商银行', N'欠钱不还', CAST(0x0000ABDB017A0318 AS DateTime), 1)
INSERT [dbo].[BlackList] ([Id], [BankName], [RefuseReason], [CreateOn], [EditorId]) VALUES (4, N'123', N'123', CAST(0x0000ABDB017A0318 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[BlackList] OFF
