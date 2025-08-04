USE [BTM.Product.Database]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 7/28/2025 6:51:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](120) NULL,
	[UnitPrice] [decimal](18, 0) NULL
) ON [PRIMARY]
GO


