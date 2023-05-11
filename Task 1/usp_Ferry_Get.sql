USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Ferry_Get') IS NOT NULL
	DROP PROCEDURE dbo.usp_Ferry_Get
GO

/*===========================================================================*\ 
Description: 
    Get a Ferry details by id

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Ferry_Get (
    @FerryId INT
)AS
	SELECT FerryId, F.Name, F.RowVersion, F.CompanyId, C.Name, C.RowVersion
	FROM Ferry F
	INNER JOIN Company C
    ON F.CompanyId = C.CompanyId
	WHERE F.FerryId = @FerryId
GO
