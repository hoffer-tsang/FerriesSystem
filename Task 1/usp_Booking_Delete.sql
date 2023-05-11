USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Booking_Delete') IS NOT NULL
	DROP PROCEDURE dbo.usp_Booking_Delete
GO

/*===========================================================================*\ 
Description: 
    Delete a Schedule

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Booking_Delete (
	@BookingId Int
)AS
	Delete Booking
	WHERE BookingId = @BookingId
GO
