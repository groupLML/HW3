CREATE TABLE [Flats_2022] (
	    [id] smallint IDENTITY (1, 1) NOT NULL ,
        [city] nvarchar (20) NOT NULL ,
		[address] nvarchar (30) NOT NULL ,
        [price] float check(price>0) NOT NULL,
		[numberOfRooms] smallint check(numberOfRooms>0) NOT NULL,
	    Primary key (id)
)


CREATE TABLE [Users_2022] (
	    [email] nvarchar(30) check(email like '[0-9a-zA-Z]%@__%.__%') NOT NULL ,
        [firstName] nvarchar(20) NOT NULL ,
		[lastName] nvarchar(20) NOT NULL ,
		[password] nvarchar(10) NOT NULL ,
	    Primary key (email)
)


CREATE TABLE [Vacations_2022] (
	    [id] smallint IDENTITY (1, 1) NOT NULL ,
        [userId] nvarchar (30) REFERENCES [Users_2022](email) NOT NULL  ,
		[flatId] smallint REFERENCES [Flats_2022](id) NOT NULL,
		[startDate] DATETIME check(startDate >= getdate()) NOT NULL,
        [endDate] DATETIME NOT NULL,
		check(startDate <= endDate),
	    Primary key (id)
)


--Drop TABLE [Vacations_2022] 