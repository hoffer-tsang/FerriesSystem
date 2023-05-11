USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_FerriesSchedulesList_Get') IS NOT NULL
	DROP PROCEDURE dbo.usp_FerriesSchedulesList_Get
GO

/*===========================================================================*\ 
Description: 
    Return a list of Ferries

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_FerriesSchedulesList_Get (
    @FerryId int,
    @ScheduleId int = NULL,
	@StartLocationId int = NULL,
    @EndLocationId int = NULL,
    @StartDay tinyint = NULL,
    @EndDay tinyint = NULL,
    @PageNumber int = 1,
    @ItemPerPage int = 5,
	@IsAsec bit = 1,
    @ColumnSorting NVARCHAR(256) = 'StartLocation'
)AS
    If( @ColumnSorting = 'StartDay' OR @ColumnSorting = 'EndDay' OR @ColumnSorting = 'StartLocation' OR @ColumnSorting = 'EndLocation')
    BEGIN
		    SELECT s.ScheduleId, s.RowVersion, DepartureLocationId, ld.Name, DepartureDay, DepartureTime, ArrivalLocationId, la.Name, ArrivalDay, ArrivalTime, CostPerPerson, CostPerVehicle, Description, NumberOfStop
		    FROM Schedule s
		    INNER JOIN Location la
		    ON la.LocationId = s.ArrivalLocationId
            INNER JOIN Location ld
            ON ld.LocationId = s.DepartureLocationId
            FULL OUTER JOIN(
                SELECT ScheduleId, Count(*)+1 as NumberofStop
                FROM ScheduleStop 
                GROUP BY ScheduleId
            )ScheduleCount
                ON s.ScheduleId = ScheduleCount.ScheduleId
            WHERE (FerryId = @FerryId)
            AND (@ScheduleId IS NULL OR s.ScheduleId = @ScheduleId)
            AND (@StartLocationId IS NULL OR DepartureLocationId = @StartLocationId)
            AND (@EndLocationId IS NULL OR ArrivalLocationId = @EndLocationId)
            AND (@StartDay IS NULL OR s.DepartureDay = @StartDay)
            AND (@EndDay IS NULL OR s.ArrivalDay = @EndDay)
		    ORDER BY 
                CASE
                WHEN @IsAsec = 1 AND @ColumnSorting =  'StartDay' THEN DepartureDay
                END ASC,
                CASE
                WHEN @IsAsec = 0 AND @ColumnSorting =  'StartDay' THEN DepartureDay
                END DESC,
                CASE
                WHEN @IsAsec = 1 AND @ColumnSorting =  'EndDay' THEN ArrivalDay
                END ASC,
                CASE
                WHEN @IsAsec = 0 AND @ColumnSorting =  'EndDay' THEN ArrivalDay
                END DESC,
                CASE
                WHEN @IsAsec = 1 AND @ColumnSorting =  'StartLocation' THEN ld.Name
                END ASC,
                CASE
                WHEN @IsAsec = 0 AND @ColumnSorting =  'StartLocation' THEN ld.Name
                END DESC,
                CASE
                WHEN @IsAsec = 1 AND @ColumnSorting =  'EndLocation' THEN la.Name
                END ASC,
                CASE
                WHEN @IsAsec = 0 AND @ColumnSorting =  'EndLocation' THEN la.Name
                END DESC
		    OFFSET ((@PageNumber - 1)*@ItemPerPage) ROWS
		    FETCH NEXT @ItemPerPage ROWS ONLY;
    END
GO
