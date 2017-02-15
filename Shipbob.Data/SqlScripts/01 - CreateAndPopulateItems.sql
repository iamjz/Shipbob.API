USE [Shipbob]
GO


IF OBJECT_ID(N'[dbo].[Items]', N'U') IS NOT NULL
   DROP TABLE [dbo].[Items]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Items](
	[ItemID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[ItemName] [varchar](250) NULL,
) ON [PRIMARY]

GO

--

insert into items (ItemName) values ('Apple')
insert into items (ItemName) values ('Banana')
insert into items (ItemName) values ('Orange')

select *
from items
