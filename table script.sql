
/****** Object:  Table [dbo].[tbl_Users]    Script Date: 09-11-2020 01:31:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_Users](
	[SchemeId] [varchar](100) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[Mobile] [bigint] NULL,
	[Error] [varchar](50) NULL
) ON [PRIMARY]
GO


