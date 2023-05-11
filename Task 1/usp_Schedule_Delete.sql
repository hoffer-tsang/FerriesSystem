USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Schedule_Delete') IS NOT NULL
	DROP PROCEDURE dbo.usp_Schedule_Delete
GO

/*===========================================================================*\ 
Description: 
    Delete a Schedule

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Schedule_Delete (
	@ScheduleId Int
)AS
	Delete Schedule
	WHERE ScheduleId = @ScheduleId
	Delete ScheduleStop
	WHERE ScheduleId = @ScheduleId
GO
