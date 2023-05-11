USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_JourneyList_Get') IS NOT NULL
	DROP PROCEDURE dbo.usp_JourneyList_Get
GO

/*===========================================================================*\ 
Description: 
    Return a list of Ferries

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_JourneyList_Get (
	@StartLocationId int = NULL,
    @EndLocationId int = NULL
)AS
			SELECT s.ScheduleId, f.Name, c.Name, DepartureLocationId, ld.Name, DepartureDay, DepartureTime, ArrivalLocationId, la.Name, ArrivalDay, ArrivalTime, CostPerPerson, CostPerVehicle, Description, NumberOfStop
		    FROM Schedule s
		    INNER JOIN Location la
		    ON la.LocationId = s.ArrivalLocationId
            INNER JOIN Location ld
            ON ld.LocationId = s.DepartureLocationId
            INNER JOIN Ferry f
            on s.FerryId = f.FerryId
            INNER JOIN Company c
            on f.CompanyId = c.CompanyId
            FULL OUTER JOIN(
                SELECT ScheduleId, Count(*)+1 as NumberofStop
                FROM ScheduleStop 
                GROUP BY ScheduleId
            )ScheduleCount
                ON s.ScheduleId = ScheduleCount.ScheduleId
            WHERE(@StartLocationId IS NULL OR DepartureLocationId = @StartLocationId)
            AND (@EndLocationId IS NULL OR ArrivalLocationId = @EndLocationId)
GO
