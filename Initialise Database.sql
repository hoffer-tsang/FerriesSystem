CREATE TABLE	dbo.Company (
	CompanyId							INT NOT NULL IDENTITY,
	Name								NVARCHAR(256) NOT NULL,
	RowVersion							ROWVERSION NOT NULL
)

ALTER TABLE		dbo.Company
ADD CONSTRAINT	PK_Company
PRIMARY KEY		(CompanyId)

ALTER	TABLE	dbo.Company
ADD CONSTRAINT  UQ_Name
UNIQUE(Name)

CREATE TABLE	dbo.Ferry (
	FerryId								INT NOT NULL IDENTITY,
	CompanyId							INT NOT NULL,
	Name								NVARCHAR(256) NOT NULL,
	RowVersion							ROWVERSION NOT NULL
)

ALTER TABLE		dbo.Ferry
ADD CONSTRAINT	PK_Ferry
PRIMARY KEY		(FerryId)

ALTER TABLE		dbo.Ferry
ADD	CONSTRAINT	FK_Ferry_Company
FOREIGN KEY		(CompanyId)
REFERENCES		dbo.Company

ALTER	TABLE	dbo.Ferry
ADD CONSTRAINT  UQ_CompanyId_Name
UNIQUE(CompanyId, Name)

CREATE TABLE dbo.Location (
	LocationId							INT NOT NULL IDENTITY,
	Name								NVARCHAR (256) NOT NULL,
	RowVersion							ROWVERSION NOT NULL
)

ALTER TABLE		dbo.Location
ADD CONSTRAINT	PK_Location
PRIMARY KEY		(LocationId)


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

ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	PK_Schedule
PRIMARY KEY		(ScheduleId)

ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	FK_Schedule_Ferry
FOREIGN KEY		(FerryId)
REFERENCES		dbo.Ferry

ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	FK_Schedule_Location1
FOREIGN KEY		(DepartureLocationId)
REFERENCES      dbo.location
ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	FK_Schedule_Location2
FOREIGN KEY		(ArrivalLocationId)
REFERENCES		dbo.Location
ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	CK_Schedule_DepartureDay
CHECK			(DepartureDay BETWEEN 0 AND 6)
ALTER TABLE		dbo.Schedule
ADD CONSTRAINT	CK_Schedule_ArrivalDay
CHECK			(ArrivalDay BETWEEN 0 AND 6)



CREATE TABLE dbo.ScheduleStop (
	ScheduleStopId					INT NOT NULL IDENTITY,
	ScheduleId							INT NOT NULL,
	LocationId							INT NOT NULL,
	DepartureDay						TINYINT NOT NULL,
	DepartureTime						TIME NOT NULL,
	ArrivalDay							TINYINT NOT NULL,
	ArrivalTime							TIME NOT NULL
)


ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	PK_ScheduleStop
PRIMARY KEY		(ScheduleStopId)

ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	FK_ScheduleStop_Schedule
FOREIGN KEY		(ScheduleId)
REFERENCES		dbo.Schedule

ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	FK_ScheduleStop_Location
FOREIGN KEY		(LocationId)
REFERENCES		dbo.Location

ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	CK_ScheduleStop_DepartureDay
CHECK			(DepartureDay BETWEEN 0 AND 6)

ALTER TABLE		dbo.ScheduleStop
ADD CONSTRAINT	CK_ScheduleStop_ArrivalDay
CHECK			(ArrivalDay BETWEEN 0 AND 6)

CREATE TABLE dbo.Booking (
	BookingId							INT NOT NULL IDENTITY,
	UserId								NVARCHAR(128) NOT NULL,
	BookingReference					NCHAR(5) NOT NULL,
	Cars								INT NOT NULL,
	Passengers							INT NOT NULL,
	Cost								DECIMAL(9,2) NOT NULL,
	CompanyName							NVARCHAR(256) NOT NULL,
    FerryName							NVARCHAR(256) NOT NULL,
	DepartureDate						DATETIME2 NOT NULL,
	DepartureLocation					NVARCHAR(256) NOT NULL,
	ArrivalDate							DATETIME2 NOT NULL,
	ArrivalLocation						NVARCHAR(256) NOT NULL
)

ALTER TABLE		dbo.Booking
ADD CONSTRAINT	PK_Booking
PRIMARY KEY		(BookingId)

ALTER TABLE		dbo.Booking
ADD CONSTRAINT	UQ_Booking_BookingReference
UNIQUE			(BookingReference)

CREATE TABLE dbo.BookingContact (
	BookingId							INT NOT NULL,
	Name								NVARCHAR(100) NOT NULL,
	AddressLine1						NVARCHAR(256) NOT NULL,
	AddressLine2						NVARCHAR(256) NULL,
	City								NVARCHAR(256) NOT NULL,
	PostalCode							NVARCHAR(20) NOT NULL	
)

ALTER TABLE		dbo.BookingContact
ADD CONSTRAINT	PK_BookingContact
PRIMARY KEY		(BookingId)

ALTER TABLE		dbo.BookingContact
ADD CONSTRAINT	FK_BookingContact_Booking
FOREIGN KEY		(BookingId)
REFERENCES		dbo.Booking

CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NULL,
	[LastName] [nvarchar](256) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE		dbo.Booking
ADD CONSTRAINT	FK_Booking_AspNetUsers_UserId
FOREIGN KEY		(UserId)
REFERENCES		dbo.AspNetUsers

ALTER TABLE [dbo].[AspNetUserClaims]  ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins]  ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles]  ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles]  ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
