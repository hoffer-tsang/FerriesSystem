USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Ferry_Delete') IS NOT NULL
	DROP PROCEDURE dbo.usp_Ferry_Delete
GO

/*===========================================================================*\ 
Description: 
    Delete a Ferry

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Ferry_Delete (
	@FerryId Int
)AS
	Delete Ferry
	WHERE FerryId = @FerryId
GO
