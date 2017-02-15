USE [Shipbob]
GO

IF OBJECT_ID(N'[dbo].[Bundles]', N'U') IS NOT NULL
   DROP TABLE [dbo].[Bundles]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].Bundles(
	[BundleName] [varchar](250) NOT NULL,
	[ItemID] int FOREIGN KEY REFERENCES Items([ItemID]),
	[ItemQty] int NOT NULL,
) ON [PRIMARY]

GO

--

insert into bundles values
('Summer Fruit Basket', 1, 2)

insert into bundles values
('Summer Fruit Basket', 2, 2)

insert into bundles values
('Mix Fruit Basket', 1, 1)

insert into bundles values
('Mix Fruit Basket', 2, 1)

insert into bundles values
('Mix Fruit Basket', 3, 1)

select *
from bundles