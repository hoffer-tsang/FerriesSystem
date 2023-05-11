USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Company_Insert') IS NOT NULL
	DROP PROCEDURE dbo.usp_Company_Insert
GO

/*===========================================================================*\ 
Description: 
    Create a new company

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Company_Insert (
    @Name NVARCHAR(256)
)AS
	Insert dbo.Company(Name)
	Values(@Name)

	DECLARE @CompanyId INT
	SET @CompanyId = SCOPE_IDENTITY()

	SELECT CompanyId, Name, RowVersion
	FROM Company
	Where Company.CompanyId = @CompanyId
GO
