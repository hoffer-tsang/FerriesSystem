CREATE TABLE dbo.ScheduleStop (
	ScheduleStopId					INT NOT NULL IDENTITY,
	ScheduleId							INT NOT NULL,
	LocationId							INT NOT NULL,
	DepartureDay						TINYINT NOT NULL,
	DepartureTime						TIME NOT NULL,
	ArrivalDay							TINYINT NOT NULL,
	ArrivalTime							TIME NOT NULL,
)
GO
ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	FK_ScheduleStop_Schedule
FOREIGN KEY		(ScheduleId)
REFERENCES		dbo.Schedule
GO
ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	FK_ScheduleStop_Location
FOREIGN KEY		(LocationId)
REFERENCES		dbo.Location
GO
ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	CK_ScheduleStop_DepartureDay
CHECK			(DepartureDay BETWEEN 0 AND 6)
GO
ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	CK_ScheduleStop_ArrivalDay
CHECK			(ArrivalDay BETWEEN 0 AND 6)
GO
ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	PK_ScheduleStop
PRIMARY KEY		(ScheduleStopId)