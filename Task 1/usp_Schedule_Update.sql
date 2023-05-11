USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Schedule_Update') IS NOT NULL
	DROP PROCEDURE dbo.usp_Schedule_Update
GO

/*===========================================================================*\ 
Description: 
    Update a Schedule

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Schedule_Update (
	@ScheduleId INT,
    @FerryId INT,
	@Description NVARCHAR(256) = NULL,
	@CostPerPerson DECIMAL(9,2),
	@CostPerVehicle DECIMAL(9,2), 
	@RowVersion TIMESTAMP,
	@DepartureLocationId INT,
	@ArrivalLocationId INT,
	@DepartureDay TINYINT,
	@DepartureTime TIME(7),
	@ArrivalDay TINYINT,
	@ArrivalTime TIME(7)
)AS
	Update dbo.Schedule
	SET
		FerryId = @FerryId, 
		Description = @Description, 
		CostPerPerson = @CostPerPerson, 
		CostPerVehicle = @CostPerVehicle, 
		DepartureLocationId = @DepartureLocationId,
		ArrivalLocationId = @ArrivalLocationId, 
		DepartureDay = @DepartureDay, 
		DepartureTime = @DepartureTime, 
		ArrivalDay = @ArrivalDay, 
		ArrivalTime = @ArrivalTime
	WHERE ScheduleId = @ScheduleId AND RowVersion = @RowVersion 

	IF @@RowCount != 0
    BEGIN
		SELECT ScheduleId, FerryId, Description, CostPerPerson, CostPerVehicle, 
		RowVersion, DepartureLocationId, ArrivalLocationId, DepartureDay, DepartureTime, ArrivalDay, ArrivalTime
		FROM Schedule
		Where Schedule.ScheduleId = @FerryId
	End
GO
