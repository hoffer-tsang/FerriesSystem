USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Company_Get') IS NOT NULL
	DROP PROCEDURE dbo.usp_Company_Get
GO

/*===========================================================================*\ 
Description: 
    Get a Company details by name

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Company_Get (
    @CompanyId INT
)AS
	SELECT CompanyId, Name, RowVersion
	FROM Company
	Where CompanyId = @CompanyId
GO
