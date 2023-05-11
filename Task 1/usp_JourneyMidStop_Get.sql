USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_JourneyMidStop_Get') IS NOT NULL
	DROP PROCEDURE dbo.usp_JourneyMidStop_Get
GO

/*===========================================================================*\ 
Description: 
    Return a list of Ferries

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_JourneyMidStop_Get 
AS
	SELECT ss.ScheduleStopId, ScheduleId, ls.LocationId, ls.Name, DepartureDay, DepartureTime, ArrivalDay, ArrivalTime
	FROM ScheduleStop ss
	INNER JOIN Location ls
	ON ls.LocationId = ss.LocationId 
GO