USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Booking_Insert') IS NOT NULL
	DROP PROCEDURE dbo.usp_Booking_Insert
GO

/*===========================================================================*\ 
Description: 
    Create a new Schedule

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Booking_Insert (
    @UserId NVARCHAR(128),
	@BookingReference NVARCHAR(5),
	@Cars INT,
	@Passengers INT,
	@Cost DECIMAL(9,2),
	@CompanyName NVARCHAR(256),
	@FerryName NVARCHAR(256),
	@DepartureDate DATETIME2(7),
	@DepartureLocation NVARCHAR(256),
	@ArrivalDate DATETIME2(7),
	@ArrivalLocation NVARCHAR(256)
)AS
	Insert dbo.Booking(UserId, BookingReference, Cars, Passengers, Cost, CompanyName, FerryName, DepartureDate, DepartureLocation, ArrivalDate, ArrivalLocation)
	Values(@UserId, @BookingReference, @Cars, @Passengers, @Cost, @CompanyName, @FerryName, @DepartureDate, @DepartureLocation, @ArrivalDate, @ArrivalLocation)
	DECLARE @BookingId INT
	SET @BookingId = SCOPE_IDENTITY()
	SELECT BookingId
	FROM Booking
	Where Booking.BookingId = @BookingId
GO
