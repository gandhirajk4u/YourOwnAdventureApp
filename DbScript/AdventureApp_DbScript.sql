USE [AdventureDatabase]
GO

/****** Object:  Table [dbo].[tblAdventures]    Script Date: 22-08-2022 17:28:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblAdventures](
	[AdventureId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](75) NOT NULL,
	[Path] [varchar](250) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblAdventures] PRIMARY KEY CLUSTERED 
(
	[AdventureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




/****** Object:  Table [dbo].[tblAdventureUser]    Script Date: 22-08-2022 17:28:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblAdventureUser](
	[AdventureUserId] [uniqueidentifier] NOT NULL,
	[AdventureId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblAdventureUser] PRIMARY KEY CLUSTERED 
(
	[AdventureUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblAdventureUser]  WITH CHECK ADD  CONSTRAINT [FK_tblAdventureUser_tblAdventures] FOREIGN KEY([AdventureId])
REFERENCES [dbo].[tblAdventures] ([AdventureId])
GO

ALTER TABLE [dbo].[tblAdventureUser] CHECK CONSTRAINT [FK_tblAdventureUser_tblAdventures]
GO


