USE [PUT_SBD]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 13.02.2020 15:39:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](256) NOT NULL,
	[lastName] [varchar](256) NOT NULL,
	[country] [varchar](64) NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authorship]    Script Date: 13.02.2020 15:39:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authorship](
	[authorId] [int] NOT NULL,
	[bookId] [int] NOT NULL,
 CONSTRAINT [PK_Authorship] PRIMARY KEY CLUSTERED 
(
	[authorId] ASC,
	[bookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[BookAuthors]    Script Date: 13.02.2020 15:39:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[BookAuthors] 
(	
	@bookId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT *
	FROM Author
	WHERE id IN
	(
		SELECT authorId
		FROM Authorship
		WHERE bookId = @bookId
	)
)
GO
/****** Object:  Table [dbo].[Borrowing]    Script Date: 13.02.2020 15:39:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Borrowing](
	[userLogin] [varchar](64) NOT NULL,
	[copyId] [int] NOT NULL,
	[borrowDate] [datetimeoffset](7) NOT NULL,
	[returnDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Borrowing] PRIMARY KEY CLUSTERED 
(
	[userLogin] ASC,
	[copyId] ASC,
	[borrowDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Edition]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Edition](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bookId] [int] NOT NULL,
	[releaseDate] [date] NOT NULL,
	[publishingHouse] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Edition] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Edition] UNIQUE NONCLUSTERED 
(
	[bookId] ASC,
	[releaseDate] ASC,
	[publishingHouse] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookCopy]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookCopy](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[editionId] [int] NOT NULL,
	[branchNumber] [int] NOT NULL,
	[condition] [varchar](256) NULL,
 CONSTRAINT [PK_BookCopy] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[userLogin] [varchar](64) NOT NULL,
	[copyId] [int] NOT NULL,
	[reservationDate] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[userLogin] ASC,
	[copyId] ASC,
	[reservationDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BookCopyState]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BookCopyState]
AS
SELECT dbo.BookCopy.id AS id, dbo.Edition.bookId AS bookId, state =
	CASE
		WHEN EXISTS(SELECT * FROM dbo.Borrowing
					WHERE dbo.BookCopy.id = dbo.Borrowing.copyId AND dbo.Borrowing.returnDate IS NULL)
			THEN 'borrowed'
		WHEN EXISTS(SELECT * FROM dbo.Reservation
					WHERE dbo.BookCopy.id = dbo.Reservation.copyId)
			THEN 'reserved'
		ELSE 'available'
	END
FROM dbo.BookCopy INNER JOIN
     dbo.Edition ON dbo.BookCopy.editionId = dbo.Edition.id
GO
/****** Object:  Table [dbo].[Book]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](256) NOT NULL,
	[genre] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[FilterBooks]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[FilterBooks] 
(
	@titleFilter varchar(256), 
	@publishingHouseFilter varchar(256), 
	@genre varchar(256), 
	@branchNumber int, 
	@state varchar(16)
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT DISTINCT Book.id AS id, Book.title AS title, Book.genre AS genre
	FROM Book INNER JOIN
		Edition ON Book.id = Edition.bookId INNER JOIN
		BookCopy ON Edition.id = BookCopy.editionId INNER JOIN
		BookCopyState ON BookCopy.id = BookCopyState.id
	WHERE Book.title LIKE CONCAT(@titleFilter, '%') AND
		Edition.publishingHouse LIKE CONCAT(@publishingHouseFilter, '%') AND
		(@genre IS NULL OR Book.genre = @genre) AND
		(@branchNumber IS NULL OR BookCopy.branchNumber = @branchNumber) AND
		(@state IS NULL OR BookCopyState.state = @state)
)
GO
/****** Object:  Table [dbo].[Address]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[street] [varchar](256) NOT NULL,
	[city] [varchar](256) NOT NULL,
	[country] [varchar](64) NOT NULL,
	[zipCode] [varchar](16) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Address] UNIQUE NONCLUSTERED 
(
	[street] ASC,
	[city] ASC,
	[country] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[branchNumber] [int] NOT NULL,
	[addressId] [int] NOT NULL,
	[name] [varchar](256) NOT NULL,
	[openingHours] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[branchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Branch] UNIQUE NONCLUSTERED 
(
	[addressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Branch_Name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[name] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Librarian]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Librarian](
	[login] [varchar](64) NOT NULL,
	[employmentDate] [date] NOT NULL,
	[branchNumber] [int] NOT NULL,
 CONSTRAINT [PK_Librarian] PRIMARY KEY CLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PublishingHouse]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PublishingHouse](
	[name] [varchar](256) NOT NULL,
	[addressId] [int] NOT NULL,
 CONSTRAINT [PK_PublishingHouse] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_PublishingHouse] UNIQUE NONCLUSTERED 
(
	[addressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reader]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reader](
	[login] [varchar](64) NOT NULL,
	[birthDate] [date] NOT NULL,
 CONSTRAINT [PK_Reader] PRIMARY KEY CLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[login] [varchar](64) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[firstName] [varchar](256) NOT NULL,
	[lastName] [varchar](256) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Author]    Script Date: 13.02.2020 15:39:18 ******/
CREATE NONCLUSTERED INDEX [IX_Author] ON [dbo].[Author]
(
	[lastName] ASC,
	[firstName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Book]    Script Date: 13.02.2020 15:39:18 ******/
CREATE NONCLUSTERED INDEX [IX_Book] ON [dbo].[Book]
(
	[title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookCopy]    Script Date: 13.02.2020 15:39:18 ******/
CREATE NONCLUSTERED INDEX [IX_BookCopy] ON [dbo].[BookCopy]
(
	[editionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Borrowing]    Script Date: 13.02.2020 15:39:18 ******/
CREATE NONCLUSTERED INDEX [IX_Borrowing] ON [dbo].[Borrowing]
(
	[copyId] ASC,
	[borrowDate] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Edition]    Script Date: 13.02.2020 15:39:18 ******/
CREATE NONCLUSTERED INDEX [IX_Edition] ON [dbo].[Edition]
(
	[bookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Librarian]    Script Date: 13.02.2020 15:39:18 ******/
CREATE NONCLUSTERED INDEX [IX_Librarian] ON [dbo].[Librarian]
(
	[branchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reservation]    Script Date: 13.02.2020 15:39:18 ******/
CREATE NONCLUSTERED INDEX [IX_Reservation] ON [dbo].[Reservation]
(
	[copyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User]    Script Date: 13.02.2020 15:39:18 ******/
CREATE NONCLUSTERED INDEX [IX_User] ON [dbo].[User]
(
	[lastName] ASC,
	[firstName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Authorship]  WITH CHECK ADD  CONSTRAINT [FK_Authorship_Author] FOREIGN KEY([authorId])
REFERENCES [dbo].[Author] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Authorship] CHECK CONSTRAINT [FK_Authorship_Author]
GO
ALTER TABLE [dbo].[Authorship]  WITH CHECK ADD  CONSTRAINT [FK_Authorship_Book] FOREIGN KEY([bookId])
REFERENCES [dbo].[Book] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Authorship] CHECK CONSTRAINT [FK_Authorship_Book]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Genre] FOREIGN KEY([genre])
REFERENCES [dbo].[Genre] ([name])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Genre]
GO
ALTER TABLE [dbo].[BookCopy]  WITH CHECK ADD  CONSTRAINT [FK_BookCopy_Branch] FOREIGN KEY([branchNumber])
REFERENCES [dbo].[Branch] ([branchNumber])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[BookCopy] CHECK CONSTRAINT [FK_BookCopy_Branch]
GO
ALTER TABLE [dbo].[BookCopy]  WITH CHECK ADD  CONSTRAINT [FK_BookCopy_Edition] FOREIGN KEY([editionId])
REFERENCES [dbo].[Edition] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[BookCopy] CHECK CONSTRAINT [FK_BookCopy_Edition]
GO
ALTER TABLE [dbo].[Borrowing]  WITH CHECK ADD  CONSTRAINT [FK_Borrowing_BookCopy] FOREIGN KEY([copyId])
REFERENCES [dbo].[BookCopy] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Borrowing] CHECK CONSTRAINT [FK_Borrowing_BookCopy]
GO
ALTER TABLE [dbo].[Borrowing]  WITH CHECK ADD  CONSTRAINT [FK_Borrowing_Reader] FOREIGN KEY([userLogin])
REFERENCES [dbo].[Reader] ([login])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Borrowing] CHECK CONSTRAINT [FK_Borrowing_Reader]
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_Branch_Address] FOREIGN KEY([addressId])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Branch_Address]
GO
ALTER TABLE [dbo].[Edition]  WITH CHECK ADD  CONSTRAINT [FK_Edition_Book] FOREIGN KEY([bookId])
REFERENCES [dbo].[Book] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Edition] CHECK CONSTRAINT [FK_Edition_Book]
GO
ALTER TABLE [dbo].[Edition]  WITH CHECK ADD  CONSTRAINT [FK_Edition_PublishingHouse] FOREIGN KEY([publishingHouse])
REFERENCES [dbo].[PublishingHouse] ([name])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Edition] CHECK CONSTRAINT [FK_Edition_PublishingHouse]
GO
ALTER TABLE [dbo].[Librarian]  WITH CHECK ADD  CONSTRAINT [FK_Librarian_Branch] FOREIGN KEY([branchNumber])
REFERENCES [dbo].[Branch] ([branchNumber])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Librarian] CHECK CONSTRAINT [FK_Librarian_Branch]
GO
ALTER TABLE [dbo].[Librarian]  WITH CHECK ADD  CONSTRAINT [FK_Librarian_User] FOREIGN KEY([login])
REFERENCES [dbo].[User] ([login])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Librarian] CHECK CONSTRAINT [FK_Librarian_User]
GO
ALTER TABLE [dbo].[PublishingHouse]  WITH CHECK ADD  CONSTRAINT [FK_PublishingHouse_Address] FOREIGN KEY([addressId])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[PublishingHouse] CHECK CONSTRAINT [FK_PublishingHouse_Address]
GO
ALTER TABLE [dbo].[Reader]  WITH CHECK ADD  CONSTRAINT [FK_Reader_User] FOREIGN KEY([login])
REFERENCES [dbo].[User] ([login])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reader] CHECK CONSTRAINT [FK_Reader_User]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_BookCopy] FOREIGN KEY([copyId])
REFERENCES [dbo].[BookCopy] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_BookCopy]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Reader] FOREIGN KEY([userLogin])
REFERENCES [dbo].[Reader] ([login])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Reader]
GO
ALTER TABLE [dbo].[Borrowing]  WITH CHECK ADD  CONSTRAINT [CK_Borrowing] CHECK  (([borrowDate]<=[returnDate] OR [returnDate] IS NULL))
GO
ALTER TABLE [dbo].[Borrowing] CHECK CONSTRAINT [CK_Borrowing]
GO
/****** Object:  StoredProcedure [dbo].[BorrowCopy]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BorrowCopy]
	@copyId int, 
	@login varchar(64)
AS
BEGIN
	SET NOCOUNT ON;
	IF @login IS NULL
		RETURN(1)
	DECLARE @state varchar(16) = (SELECT BookCopyState.state FROM BookCopyState WHERE id = @copyId)
	IF @state NOT IN ('available', 'reserved')
		RETURN(2)
	IF @state = 'reserved'
	BEGIN
		IF (SELECT userLogin FROM Reservation WHERE copyId = @copyId) = @login
			DELETE FROM Reservation WHERE copyId = @copyId
		ELSE
			RETURN(2)
	END
	INSERT INTO Borrowing(userLogin, copyId, borrowDate) VALUES (@login, @copyId, SYSDATETIMEOFFSET())
	RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[ReserveCopy]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ReserveCopy]
	@copyId int, 
	@login varchar(64)
AS
BEGIN
	SET NOCOUNT ON;
	IF @login IS NULL
		RETURN(1)
	DECLARE @state varchar(16) = (SELECT BookCopyState.state FROM BookCopyState WHERE id = @copyId)
	IF @state NOT IN ('available')
		RETURN(2)
	INSERT INTO Reservation(userLogin, copyId, reservationDate) VALUES (@login, @copyId, SYSDATETIMEOFFSET())
	RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[ReturnCopy]    Script Date: 13.02.2020 15:39:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ReturnCopy]
	@copyId int
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @state varchar(16) = (SELECT BookCopyState.state FROM BookCopyState WHERE id = @copyId)
	print @state
	IF @state NOT IN ('reserved', 'borrowed')
		RETURN(1)
	IF @state = 'reserved'
		DELETE FROM Reservation WHERE copyId = @copyId
	IF @state = 'borrowed'
		UPDATE Borrowing SET returnDate = SYSDATETIMEOFFSET() WHERE copyId = @copyId AND returnDate IS NULL
	RETURN(0)
END
GO
