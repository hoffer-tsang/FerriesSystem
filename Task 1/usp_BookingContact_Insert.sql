USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_BookingContact_Insert') IS NOT NULL
	DROP PROCEDURE dbo.usp_BookingContact_Insert
GO

/*===========================================================================*\ 
Description: 
    Create a new Schedule

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_BookingContact_Insert (
    @BookingId INT,
	@Name NVARCHAR(100),
	@AddressLine1 NVARCHAR(256),
	@AddressLine2 NVARCHAR(256) = NULL,
	@City NVARCHAR(256),
	@PostalCode NVARCHAR(20)
)AS
IF(@AddressLine2 IS NOT NULL)
	BEGIN
		Insert dbo.BookingContact(BookingId, Name, AddressLine1, AddressLine2, City, PostalCode)
		Values(@BookingId, @Name, @AddressLine1, @AddressLine2, @City, @PostalCode)
	END
ELSE
	BEGIN
		Insert dbo.BookingContact(BookingId, Name, AddressLine1, AddressLine2, City, PostalCode)
		Values(@BookingId, @Name, @AddressLine1, @AddressLine2, @City, @PostalCode)
	END
	
GO
