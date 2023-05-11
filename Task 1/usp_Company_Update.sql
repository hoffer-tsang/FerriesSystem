USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Company_Update') IS NOT NULL
	DROP PROCEDURE dbo.usp_Company_Update
GO

/*===========================================================================*\ 
Description: 
    Update a new company

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Company_Update (
	@CompanyId Int,
    @Name NVARCHAR(256),
	@RowVersion TIMESTAMP
)AS
	Update Company
	SET Name = @Name
	WHERE CompanyId = @CompanyId AND RowVersion = @RowVersion

    IF @@RowCount != 0
    BEGIN
	    SELECT CompanyId, Name, RowVersion
	    FROM Company
	    Where Company.CompanyId = @CompanyId
    END
GO
