CREATE TABLE	dbo.Ferry (
	FerryId								INT NOT NULL IDENTITY,
	CompanyId							INT NOT NULL,
	Name								NVARCHAR(256) NOT NULL,
	RowVersion							ROWVERSION NOT NULL
)
GO
ALTER TABLE		dbo.Ferry
ADD	CONSTRAINT	FK_Ferry_Company
FOREIGN KEY		(CompanyId)
REFERENCES		dbo.Company
GO
ALTER TABLE		dbo.Ferry
ADD CONSTRAINT	PK_Ferry
PRIMARY KEY		(FerryId)
GO
ALTER	TABLE	dbo.Ferry
ADD CONSTRAINT  UQ_CompanyId_Name
UNIQUE(CompanyId, Name)