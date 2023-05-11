USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Company_Delete') IS NOT NULL
	DROP PROCEDURE dbo.usp_Company_Delete
GO

/*===========================================================================*\ 
Description: 
    Delete a new company

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Company_Delete (
	@CompanyId Int
)AS
	Delete Company
	WHERE CompanyId = @CompanyId
GO
