USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_CompaniesList_Get') IS NOT NULL
	DROP PROCEDURE dbo.usp_CompaniesList_Get
GO

/*===========================================================================*\ 
Description: 
    Return a list of Companies

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_CompaniesList_Get (
    @CompanyName NVARCHAR(256) = NULL,
    @PageNumber int = 1,
    @ItemPerPage int = 5,
    @IsAsec bit = 1
)AS
    If( @IsAsec = 1)
    Begin
	    SELECT CompanyId, Name
        FROM Company
        WHERE (@CompanyName IS NULL OR Name = @CompanyName)
        ORDER BY Company.Name
        OFFSET ((@PageNumber - 1)*@ItemPerPage) ROWS
        FETCH NEXT @ItemPerPage ROWS ONLY;
    End
    If( @IsAsec = 0)
    Begin
	    SELECT CompanyId, Name
        FROM Company
        WHERE (@CompanyName IS NULL OR Name = @CompanyName)
        ORDER BY Company.Name DESC
        OFFSET ((@PageNumber - 1)*@ItemPerPage) ROWS
        FETCH NEXT @ItemPerPage ROWS ONLY;
    End
GO
