USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_ScheduleStop_Insert') IS NOT NULL
	DROP PROCEDURE dbo.usp_ScheduleStop_Insert
GO

/*===========================================================================*\ 
Description: 
    Create a new ScheduleStop

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_ScheduleStop_Insert (
    @ScheduleId INT,
	@LocationId INT,
	@DepartureDay TINYINT,
	@DepartureTime TIME(7),
	@ArrivalDay TINYINT,
	@ArrivalTime TIME(7)
)AS
	Insert dbo.ScheduleStop(ScheduleId, LocationId, DepartureDay, DepartureTime, ArrivalDay, ArrivalTime)
	Values(@ScheduleId, @LocationId, @DepartureDay, @DepartureTime, @ArrivalDay, @ArrivalTime)
	DECLARE @ScheduleStopId INT
	SET @ScheduleStopId = SCOPE_IDENTITY()
	SELECT ScheduleStopId, ScheduleId, LocationId, DepartureDay, DepartureTime, ArrivalDay, ArrivalTime
	FROM ScheduleStop
	Where ScheduleStopId = @ScheduleStopId
GO
