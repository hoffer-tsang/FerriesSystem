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
GO
ALTER TABLE		dbo.Booking
ADD CONSTRAINT	FK_Booking_AspNetUsers_UserId
FOREIGN KEY		(UserId)
REFERENCES		dbo.AspNetUsers
GO
ALTER TABLE		dbo.Booking
ADD CONSTRAINT	PK_Booking
PRIMARY KEY		(BookingId)
GO
ALTER TABLE		dbo.Booking
ADD CONSTRAINT	UQ_Booking_BookingReference
UNIQUE			(BookingReference)