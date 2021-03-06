USE [tinder_user_test]
GO
/****** Object:  Table [dbo].[foods]    Script Date: 3/6/2017 8:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[foods](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[food] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[genders]    Script Date: 3/6/2017 8:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[genders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gender] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[likes]    Script Date: 3/6/2017 8:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[likes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userLiking_id] [int] NULL,
	[userLiked_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 3/6/2017 8:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[description] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users_foods]    Script Date: 3/6/2017 8:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_foods](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[food_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users_genders]    Script Date: 3/6/2017 8:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_genders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[gender_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users_works]    Script Date: 3/6/2017 8:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_works](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[work_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[works]    Script Date: 3/6/2017 8:41:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[works](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[work] [varchar](255) NULL
) ON [PRIMARY]

GO
