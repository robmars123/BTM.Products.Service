USE [BTM.Product.Database]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 7/28/2025 6:51:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](120) NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY]
GO


