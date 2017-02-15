USE [Shipbob]
GO

IF OBJECT_ID(N'[dbo].[Orders]', N'U') IS NOT NULL
   DROP TABLE [dbo].[Orders]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].Orders(
	[OrderID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[TrackingID] [varchar](250) NOT NULL,
	[ItemID] int FOREIGN KEY REFERENCES Items([ItemID]),
	[Qty] int NOT NULL,
	[RecipFirstName] [varchar](50) NULL,
	[RecipLastName] [varchar](50) NULL,
	[RecipAddress] [varchar](250) NULL,
	[RecipCity] [varchar](50) NULL,
	[RecipState] [varchar](50) NULL,
	[RecipCountry] [varchar](50) NULL,
	[RecipZip] [varchar](10) NULL
) ON [PRIMARY]

GO

--

insert into orders
(TrackingID, ItemID, Qty, RecipFirstName, RecipLastName, RecipAddress, RecipCity, RecipState, RecipCountry, RecipZip)
values
('12345XYZ', 1, 1, 'Jacky', 'Zhao', '13401 Legendary Drive', 'Austin', 'TX','United States', '78727')
--
insert into orders
(TrackingID, ItemID, Qty, RecipFirstName, RecipLastName, RecipAddress, RecipCity, RecipState, RecipCountry, RecipZip)
values
('99999ABC', 2, 3, 'Chuck', 'Norris', '99 Parmer Lane', 'Austin', 'TX','United States', '78785')
--
insert into orders
(TrackingID, ItemID, Qty, RecipFirstName, RecipLastName, RecipAddress, RecipCity, RecipState, RecipCountry, RecipZip)
values
('987654321JZ', 3, 100000, 'Jessica', 'Alba', '123 Fake Street', 'Los Angeles', 'CA','United States', '12345')
--
insert into orders
(TrackingID, ItemID, Qty, RecipFirstName, RecipLastName, RecipAddress, RecipCity, RecipState, RecipCountry, RecipZip)
values
('NFL123', 3, 500, 'Tom', 'Brady', '456 Patriots Avenue', 'Boston', 'MA','United States', '87654')

select *
from orders