USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Ferry_Insert') IS NOT NULL
	DROP PROCEDURE dbo.usp_Ferry_Insert
GO

/*===========================================================================*\ 
Description: 
    Create a new Ferry

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Ferry_Insert (
    @Name NVARCHAR(256),
	@CompanyId INT
)AS
	Insert dbo.Ferry(Name, CompanyId)
	Values(@Name, @CompanyId)
	DECLARE @FerryId INT
	SET @FerryId = SCOPE_IDENTITY()
	SELECT FerryId, CompanyId, Name, RowVersion
	FROM Ferry
	Where Ferry.FerryId = @FerryId
GO
