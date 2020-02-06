USE [PUT_SBD]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[Author]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[Authorship]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[Book]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[BookCopy]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[Borrowing]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[Branch]    Script Date: 06.02.2020 23:36:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[branchNumber] [int] IDENTITY(1,1) NOT NULL,
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Edition]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[Genre]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[Librarian]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[PublishingHouse]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[Reader]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[Reservation]    Script Date: 06.02.2020 23:36:16 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 06.02.2020 23:36:16 ******/
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
ALTER TABLE [dbo].[Authorship]  WITH CHECK ADD  CONSTRAINT [FK_Authorship_Author] FOREIGN KEY([authorId])
REFERENCES [dbo].[Author] ([id])
GO
ALTER TABLE [dbo].[Authorship] CHECK CONSTRAINT [FK_Authorship_Author]
GO
ALTER TABLE [dbo].[Authorship]  WITH CHECK ADD  CONSTRAINT [FK_Authorship_Book] FOREIGN KEY([bookId])
REFERENCES [dbo].[Book] ([id])
GO
ALTER TABLE [dbo].[Authorship] CHECK CONSTRAINT [FK_Authorship_Book]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Genre] FOREIGN KEY([genre])
REFERENCES [dbo].[Genre] ([name])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Genre]
GO
ALTER TABLE [dbo].[BookCopy]  WITH CHECK ADD  CONSTRAINT [FK_BookCopy_Branch] FOREIGN KEY([branchNumber])
REFERENCES [dbo].[Branch] ([branchNumber])
GO
ALTER TABLE [dbo].[BookCopy] CHECK CONSTRAINT [FK_BookCopy_Branch]
GO
ALTER TABLE [dbo].[BookCopy]  WITH CHECK ADD  CONSTRAINT [FK_BookCopy_Edition] FOREIGN KEY([editionId])
REFERENCES [dbo].[Edition] ([id])
GO
ALTER TABLE [dbo].[BookCopy] CHECK CONSTRAINT [FK_BookCopy_Edition]
GO
ALTER TABLE [dbo].[Borrowing]  WITH CHECK ADD  CONSTRAINT [FK_Borrowing_BookCopy] FOREIGN KEY([copyId])
REFERENCES [dbo].[BookCopy] ([id])
GO
ALTER TABLE [dbo].[Borrowing] CHECK CONSTRAINT [FK_Borrowing_BookCopy]
GO
ALTER TABLE [dbo].[Borrowing]  WITH CHECK ADD  CONSTRAINT [FK_Borrowing_Reader] FOREIGN KEY([userLogin])
REFERENCES [dbo].[Reader] ([login])
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
GO
ALTER TABLE [dbo].[Edition] CHECK CONSTRAINT [FK_Edition_Book]
GO
ALTER TABLE [dbo].[Edition]  WITH CHECK ADD  CONSTRAINT [FK_Edition_PublishingHouse] FOREIGN KEY([publishingHouse])
REFERENCES [dbo].[PublishingHouse] ([name])
GO
ALTER TABLE [dbo].[Edition] CHECK CONSTRAINT [FK_Edition_PublishingHouse]
GO
ALTER TABLE [dbo].[Librarian]  WITH CHECK ADD  CONSTRAINT [FK_Librarian_Branch] FOREIGN KEY([branchNumber])
REFERENCES [dbo].[Branch] ([branchNumber])
GO
ALTER TABLE [dbo].[Librarian] CHECK CONSTRAINT [FK_Librarian_Branch]
GO
ALTER TABLE [dbo].[Librarian]  WITH CHECK ADD  CONSTRAINT [FK_Librarian_User] FOREIGN KEY([login])
REFERENCES [dbo].[User] ([login])
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
GO
ALTER TABLE [dbo].[Reader] CHECK CONSTRAINT [FK_Reader_User]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_BookCopy] FOREIGN KEY([copyId])
REFERENCES [dbo].[BookCopy] ([id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_BookCopy]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Reader] FOREIGN KEY([userLogin])
REFERENCES [dbo].[Reader] ([login])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Reader]
GO
