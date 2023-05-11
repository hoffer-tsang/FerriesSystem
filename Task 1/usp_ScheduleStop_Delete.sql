USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_ScheduleStop_Delete') IS NOT NULL
	DROP PROCEDURE dbo.usp_ScheduleStop_Delete
GO

/*===========================================================================*\ 
Description: 
    Delete a Schedule

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_ScheduleStop_Delete (
	@ScheduleStopId Int
)AS
	Delete ScheduleStop
	WHERE ScheduleStopId = @ScheduleStopId
GO
