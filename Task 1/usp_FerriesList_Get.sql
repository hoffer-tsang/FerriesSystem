USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_FerriesList_Get') IS NOT NULL
	DROP PROCEDURE dbo.usp_FerriesList_Get
GO

/*===========================================================================*\ 
Description: 
    Return a list of Ferries

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_FerriesList_Get (
    @FerryName NVARCHAR(256) = NULL,
	@CompanyName NVARCHAR(256) = NULL,
    @PageNumber int = 1,
    @ItemPerPage int = 5,
	@IsAsec bit = 1,
    @ColumnSorting NVARCHAR(256) = 'Ferry.Name'
)AS
    If( @ColumnSorting = 'Ferry.Name' OR @ColumnSorting = 'Company.Name')
    BEGIN
		    SELECT FerryId, Ferry.Name, Ferry.RowVersion, Ferry.CompanyId, Company.Name, Company.RowVersion
		    FROM Ferry
		    INNER JOIN Company
		    ON Ferry.CompanyId = Company.CompanyId
            WHERE(@FerryName IS NULL OR Ferry.Name = @FerryName)
            AND (@CompanyName IS NULL OR Company.Name = @CompanyName)
		    ORDER BY 
                CASE
                WHEN @IsAsec = 1 AND @ColumnSorting = 'Ferry.Name' THEN Ferry.Name
                END ASC,
                CASE
                WHEN @IsAsec = 0 AND @ColumnSorting = 'Ferry.Name' THEN Ferry.Name
                END DESC,
                CASE
                WHEN @IsAsec = 1 AND @ColumnSorting = 'Company.Name' THEN Company.Name
                END ASC,
                CASE
                WHEN @IsAsec = 0 AND @ColumnSorting = 'Company.Name' THEN Company.Name
                END DESC
		    OFFSET ((@PageNumber - 1)*@ItemPerPage) ROWS
		    FETCH NEXT @ItemPerPage ROWS ONLY;
    END
GO
