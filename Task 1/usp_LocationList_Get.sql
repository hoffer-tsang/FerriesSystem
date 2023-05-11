USE[Ferries]
GO

SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO 

/*===========================================================================*\ 
Drop stored proc before re-creating
\*===========================================================================*/
IF OBJECT_ID(N'dbo.usp_LocationList_Get') IS NOT NULL
	DROP PROCEDURE dbo.usp_LocationList_Get
GO

/*===========================================================================*\ 
Description: 
    Return a list of Ferries

Parameters: 

Created:    Januray 2023
\*===========================================================================*/

CREATE PROCEDURE dbo.usp_LocationList_Get 
AS
    Select LocationId, Name, RowVersion
	From Location
GO
