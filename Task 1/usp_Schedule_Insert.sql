USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Schedule_Insert') IS NOT NULL
	DROP PROCEDURE dbo.usp_Schedule_Insert
GO

/*===========================================================================*\ 
Description: 
    Create a new Schedule

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Schedule_Insert (
    @FerryId INT,
	@Description NVARCHAR(256) = NULL,
	@CostPerPerson DECIMAL(9,2),
	@CostPerVehicle DECIMAL(9,2), 
	@DepartureLocationId INT,
	@ArrivalLocationId INT,
	@DepartureDay TINYINT,
	@DepartureTime TIME(7),
	@ArrivalDay TINYINT,
	@ArrivalTime TIME(7)
)AS
	Insert dbo.Schedule(FerryId, Description, CostPerPerson, CostPerVehicle, 
    DepartureLocationId, ArrivalLocationId, DepartureDay, DepartureTime, ArrivalDay, ArrivalTime)
	Values(@FerryId, @Description, @CostPerPerson, @CostPerVehicle, @DepartureLocationId,
	@ArrivalLocationId, @DepartureDay, @DepartureTime, @ArrivalDay, @ArrivalTime)
	DECLARE @ScheduleId INT
	SET @ScheduleId = SCOPE_IDENTITY()
	SELECT ScheduleId, FerryId, Description, CostPerPerson, CostPerVehicle, RowVersion,
    DepartureLocationId, ArrivalLocationId, DepartureDay, DepartureTime, ArrivalDay, ArrivalTime
	FROM Schedule
	Where Schedule.ScheduleId = @ScheduleId
GO
