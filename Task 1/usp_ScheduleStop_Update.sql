USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_ScheduleStop_Update') IS NOT NULL
	DROP PROCEDURE dbo.usp_ScheduleStop_Update
GO

/*===========================================================================*\ 
Description: 
    Update a Schedule

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_ScheduleStop_Update (
    @ScheduleStopId INT,
	@ScheduleId INT,
	@LocationId INT,
	@DepartureDay TINYINT,
	@DepartureTime TIME(7),
	@ArrivalDay TINYINT,
	@ArrivalTime TIME(7)
)AS
	Update dbo.ScheduleStop
	SET
		ScheduleId = @ScheduleId, 
		LocationId = @LocationId,
		DepartureDay = @DepartureDay, 
		DepartureTime = @DepartureTime, 
		ArrivalDay = @ArrivalDay, 
		ArrivalTime = @ArrivalTime
	WHERE ScheduleStopId = @ScheduleStopId

	IF @@RowCount != 0
    BEGIN
		SELECT ScheduleStopId, ScheduleId, LocationId, DepartureDay, DepartureTime, ArrivalDay, ArrivalTime
		FROM ScheduleStop
		Where ScheduleStopId = @ScheduleStopId
	End
GO
