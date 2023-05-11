USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_Ferry_Update') IS NOT NULL
	DROP PROCEDURE dbo.usp_Ferry_Update
GO

/*===========================================================================*\ 
Description: 
    Update a new Ferry

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_Ferry_Update (
    @FerryId INT,
	@CompanyId INT,
    @FerryName NVARCHAR(256),
	@FerryRowVersion TIMESTAMP
)AS
	Update Ferry
	SET 
        Name = @FerryName,
        CompanyId = @CompanyId
	WHERE FerryId = @FerryId AND RowVersion = @FerryRowVersion

    IF @@RowCount != 0
    BEGIN
	    SELECT FerryId, CompanyId, Name, RowVersion
	    FROM Ferry
	    Where FerryId = @FerryId
    END
GO
