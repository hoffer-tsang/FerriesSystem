CREATE TABLE dbo.Location (
	LocationId							INT NOT NULL IDENTITY,
	Name								NVARCHAR (256) NOT NULL,
	RowVersion							ROWVERSION NOT NULL
)
GO
ALTER TABLE		dbo.Location
ADD CONSTRAINT	PK_Location
PRIMARY KEY		(LocationId)