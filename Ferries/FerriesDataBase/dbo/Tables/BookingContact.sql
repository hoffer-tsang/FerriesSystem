CREATE TABLE dbo.BookingContact (
	BookingId							INT NOT NULL,
	Name								NVARCHAR(100) NOT NULL,
	AddressLine1						NVARCHAR(256) NOT NULL,
	AddressLine2						NVARCHAR(256) NULL,
	City								NVARCHAR(256) NOT NULL,
	PostalCode							NVARCHAR(20) NOT NULL	
)
GO
ALTER TABLE		dbo.BookingContact
ADD CONSTRAINT	FK_BookingContact_Booking
FOREIGN KEY		(BookingId)
REFERENCES		dbo.Booking
GO
ALTER TABLE		dbo.BookingContact
ADD CONSTRAINT	PK_BookingContact
PRIMARY KEY		(BookingId)