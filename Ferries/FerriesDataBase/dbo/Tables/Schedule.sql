CREATE TABLE dbo.Schedule (
	ScheduleId							INT NOT NULL IDENTITY,
	FerryId								INT NOT NULL,
	Description							NVARCHAR(256) NULL,
	CostPerPerson						DECIMAL(9, 2) NOT NULL,
	CostPerVehicle						DECIMAL(9, 2) NOT NULL,
	RowVersion							ROWVERSION NOT NULL,
    DepartureLocationId                 INT NOT NULL,
    DepartureDay                        TINYINT NOT NULL,
    DepartureTime                       time(7) NOT NULL,
    ArrivalLocationId                   INT NOT NULL,
    ArrivalDay                          TINYINT NOT NULL,
    ArrivalTime	                        time(7) NOT NULL
)
GO
ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	FK_Schedule_Ferry
FOREIGN KEY		(FerryId)
REFERENCES		dbo.Ferry
GO
ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	FK_Schedule_Location1
FOREIGN KEY		(DepartureLocationId)
REFERENCES      dbo.Location
GO
ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	FK_Schedule_Location2
FOREIGN KEY		(ArrivalLocationId)
REFERENCES		dbo.Location
GO
ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	CK_Schedule_DepartureDay
CHECK			(DepartureDay BETWEEN 0 AND 6)
GO
ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	CK_Schedule_ArrivalDay
CHECK			(ArrivalDay BETWEEN 0 AND 6)
GO
ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	PK_Schedule
PRIMARY KEY		(ScheduleId)