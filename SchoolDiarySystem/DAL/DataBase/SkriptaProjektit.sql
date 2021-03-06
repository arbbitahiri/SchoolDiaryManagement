USE [master]
GO
/****** Object:  Database [SchoolDiarySystem]    Script Date: 07-Dec-20 5:30:07 PM ******/
CREATE DATABASE [SchoolDiarySystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolDiarySystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SchoolDiarySystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SchoolDiarySystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SchoolDiarySystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SchoolDiarySystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolDiarySystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolDiarySystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolDiarySystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolDiarySystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SchoolDiarySystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolDiarySystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SchoolDiarySystem] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolDiarySystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolDiarySystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolDiarySystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolDiarySystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SchoolDiarySystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SchoolDiarySystem] SET QUERY_STORE = OFF
GO
USE [SchoolDiarySystem]
GO
/****** Object:  Table [dbo].[Absence]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Absence](
	[AbsenceID] [int] IDENTITY(1,1) NOT NULL,
	[AbsenceReasoning] [varchar](25) NULL,
	[StudentID] [int] NOT NULL,
	[ClassID] [int] NOT NULL,
	[SubjectID] [int] NOT NULL,
	[Time] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AbsenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[TeacherID] [int] NOT NULL,
	[Class_No] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class_Schedule]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class_Schedule](
	[ScheduleID] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NOT NULL,
	[SubjectID] [int] NOT NULL,
	[Time] [int] NOT NULL,
	[Day] [varchar](10) NOT NULL,
	[Year] [int] NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ScheduleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[SubjectID] [int] NOT NULL,
	[Time] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
	[StudentID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parents]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parents](
	[ParentID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name_P] [varchar](50) NOT NULL,
	[Last_Name_P] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Parents] PRIMARY KEY CLUSTERED 
(
	[ParentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[ReviewID] [int] IDENTITY(1,1) NOT NULL,
	[CommentID] [int] NOT NULL,
	[Review] [varchar](max) NOT NULL,
	[ReviewDate] [date] NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomID] [int] IDENTITY(1,1) NOT NULL,
	[Room_No] [int] NOT NULL,
	[Room_Type] [varchar](30) NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [varchar](50) NOT NULL,
	[Last_Name] [varchar](50) NOT NULL,
	[Gender] [varchar](25) NOT NULL,
	[Day_of_Birth] [date] NOT NULL,
	[ClassID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[SubjectID] [int] IDENTITY(1,1) NOT NULL,
	[Subject_Title] [varchar](30) NOT NULL,
	[Book] [varchar](50) NOT NULL,
	[Book_Author] [varchar](50) NOT NULL,
	[TeacherID] [int] NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[TeacherID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name_T] [varchar](50) NOT NULL,
	[Last_Name_T] [varchar](50) NOT NULL,
	[Gender] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[Qualification] [varchar](50) NOT NULL,
	[Day_of_Birth] [date] NOT NULL,
	[Email] [varchar](320) NOT NULL,
	[Phone_No] [varchar](50) NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TeacherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[TopicID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [varchar](max) NULL,
	[ClassID] [int] NOT NULL,
	[SubjectID] [int] NOT NULL,
	[Time] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TopicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[UserPass] [varchar](max) NOT NULL,
	[First_Name] [varchar](20) NOT NULL,
	[Last_Name] [varchar](20) NOT NULL,
	[ExpiresDate] [date] NOT NULL,
	[RoleID] [int] NOT NULL,
	[AbsenceDate] [date] NULL,
	[AbsenceReasoning] [varchar](25) NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[LastPasswordChangeDate] [datetime] NULL,
	[IsPasswordChanged] [bit] NULL,
	[LUN] [int] NOT NULL,
	[LUD] [datetime] NULL,
	[LUB] [varchar](50) NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertBy] [varchar](50) NOT NULL,
	[TeacherID] [int] NULL,
	[ParentID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Absence] ADD  DEFAULT ((0)) FOR [LUN]
GO
ALTER TABLE [dbo].[Absence] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Class] ADD  DEFAULT ((0)) FOR [LUN]
GO
ALTER TABLE [dbo].[Class] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Class_Schedule] ADD  DEFAULT ((0)) FOR [LUN]
GO
ALTER TABLE [dbo].[Class_Schedule] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Comment] ADD  DEFAULT ((0)) FOR [LUN]
GO
ALTER TABLE [dbo].[Comment] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ((0)) FOR [LUN]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Rooms] ADD  DEFAULT ((0)) FOR [LUN]
GO
ALTER TABLE [dbo].[Rooms] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Subjects] ADD  DEFAULT ((0)) FOR [LUN]
GO
ALTER TABLE [dbo].[Subjects] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Teachers] ADD  DEFAULT ((0)) FOR [LUN]
GO
ALTER TABLE [dbo].[Teachers] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Topics] ADD  DEFAULT ((0)) FOR [LUN]
GO
ALTER TABLE [dbo].[Topics] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('NONE') FOR [UserName]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('NONE') FOR [UserPass]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('NONE') FOR [First_Name]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('NONE') FOR [Last_Name]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [ExpiresDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('NONE') FOR [AbsenceReasoning]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsPasswordChanged]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [InsertDate]
GO
ALTER TABLE [dbo].[Absence]  WITH CHECK ADD FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[Absence]  WITH CHECK ADD FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subjects] ([SubjectID])
GO
ALTER TABLE [dbo].[Absence]  WITH CHECK ADD  CONSTRAINT [FK_Absence_Students] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([StudentID])
GO
ALTER TABLE [dbo].[Absence] CHECK CONSTRAINT [FK_Absence_Students]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([RoomID])
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teachers] ([TeacherID])
GO
ALTER TABLE [dbo].[Class_Schedule]  WITH CHECK ADD FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[Class_Schedule]  WITH CHECK ADD FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subjects] ([SubjectID])
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([StudentID])
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subjects] ([SubjectID])
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD FOREIGN KEY([CommentID])
REFERENCES [dbo].[Comment] ([CommentID])
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Class]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Parents] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Parents] ([ParentID])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Parents]
GO
ALTER TABLE [dbo].[Subjects]  WITH CHECK ADD FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teachers] ([TeacherID])
GO
ALTER TABLE [dbo].[Topics]  WITH CHECK ADD FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([ClassID])
GO
ALTER TABLE [dbo].[Topics]  WITH CHECK ADD FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subjects] ([SubjectID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Parents] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Parents] ([ParentID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Parents]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Teachers] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teachers] ([TeacherID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Teachers]
GO
/****** Object:  StoredProcedure [dbo].[usp_Absence_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Absence_Create] (
	@absencereasoning varchar(25),
	@nostudents_absence int,
	@classID int,
	@subjectID int,
	@time int,
	@date date,
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Absence
	(
		-- AbsenceID - column value is auto-generated
		AbsenceReasoning,
		NoStudents_Absence,
		ClassID,
		SubjectID,
		[Time],
		[Date],
		LUN,
		LUD,
		LUB,
		InsertDate,
	    InsertBy
	)
	VALUES
	(
		@absencereasoning, -- Content - varchar
		@nostudents_absence, -- Content - int
	    @classID, -- ClassID - int
		@subjectID, -- SubjectID - int
	    @time, -- Time - int
	    @date, -- Date - date
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
		@LUB, -- LUB - varchar
		GETDATE(), -- InsertDate - datetime
		@insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Absence_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Absence_GetList]
AS
BEGIN
	SELECT a.AbsenceID, a.AbsenceReasoning, a.NoStudents_Absence, a.ClassID, a.SubjectID,
		a.[Time], a.[Date], c.Class_No, s.Subject_Title
	FROM dbo.Absence AS a
		INNER JOIN dbo.Class AS c
		ON a.ClassID = c.ClassID
		INNER JOIN dbo.Subjects s
		ON a.SubjectID = s.SubjectID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Absence_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Absence_Update] (
	@absenceID int,
	@absencereasoning varchar(25),
	@nostudents_absence int,
	@classID int,
	@time int,
	@date date,
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Absence
	SET
	    --AbsenceID - column value is auto-generated
	    dbo.Absence.AbsenceReasoning = @absencereasoning, -- varchar
	    dbo.Absence.NoStudents_Absence = @nostudents_absence, -- int
	    dbo.Absence.ClassID = @classID, -- int
		-- SubjectID nuk behet update -- int
	    dbo.Absence.[Time] = @time, -- int
	    dbo.Absence.[Date] = @date, -- date
	    dbo.Absence.LUN = @LUN, -- int
	    dbo.Absence.LUD = GETDATE(), -- datetime
	    dbo.Absence.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Absence.AbsenceID = @absenceID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Authenticate]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Authenticate]
	@username VARCHAR(50),
	@password VARCHAR(MAX)
AS

UPDATE dbo.Users
	SET dbo.Users.LastLoginDate = GETDATE()
	WHERE dbo.Users.UserName = @username AND dbo.Users.UserPass = @password;

SELECT u.*, p.First_Name_P, p.Last_Name_P, t.First_Name_T, t.Last_Name_T
FROM dbo.Users AS u
	LEFT OUTER JOIN dbo.Parents AS p
		ON u.ParentID = p.ParentID
	LEFT OUTER JOIN dbo.Teachers AS t
		ON u.TeacherID = t.TeacherID
WHERE u.UserName = @username AND u.UserPass	= @password;
GO
/****** Object:  StoredProcedure [dbo].[usp_Class_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Class_Create] (
	@teacherID int,
	@classNo int,
	@roomID int,
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Class
	(
	    --ClassID - column value is auto-generated
	    TeacherID,
	    Class_No,
	    RoomID,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- ClassID - int
	    @teacherID, -- TeacherID - int
	    @classNo, -- Class_No - int
	    @roomID, -- RoomID - int
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Class_Delete]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Class_Delete] (
	@classID int
)
AS
BEGIN
	DELETE FROM dbo.Class WHERE [ClassID] = @classID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Class_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Class_Get]
	@classID int
AS
BEGIN
	SELECT c.*, r.Room_Type, t.First_Name_T, t.Last_Name_T
	FROM dbo.Class AS c
		INNER JOIN dbo.Rooms AS r
			ON c.RoomID = r.RoomID
		INNER JOIN dbo.Teachers AS t
			ON c.TeacherID = t.TeacherID
	WHERE c.ClassID = @classID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Class_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Class_GetList]
AS
BEGIN
	SELECT c.*, r.Room_Type, t.First_Name_T, t.Last_Name_T
	FROM dbo.Class AS c
		INNER JOIN dbo.Rooms AS r
			ON c.RoomID = r.RoomID
		INNER JOIN dbo.Teachers AS t
			ON c.TeacherID = t.TeacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Class_GetList_ForTeacher]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Class_GetList_ForTeacher](
	@teacherID int
)
AS
BEGIN
	SELECT c.*, r.Room_Type, t.First_Name_T, t.Last_Name_T
	FROM dbo.Class AS c
		INNER JOIN dbo.Rooms AS r
			ON c.RoomID = r.RoomID
		INNER JOIN dbo.Teachers AS t
			ON c.TeacherID = t.TeacherID
		WHERE c.TeacherID = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Class_Search]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Class_Search](
	@search varchar(10)
)
AS
BEGIN
	SELECT c.* FROM dbo.Class AS c WHERE c.Class_No = @search
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Class_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Class_Update] (
	@classID int,
	@teacherID int,
	@roomID int,
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Class
	SET
	    -- ClassID - column value is auto-generated
	    dbo.Class.TeacherID = @teacherID, -- int
	    -- ClassNo nuk behet update -- int
	    dbo.Class.RoomID = @roomID, -- int
	    dbo.Class.LUN = @LUN, -- int
	    dbo.Class.LUD = GETDATE(), -- datetime
	    dbo.Class.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
	    -- InsertBy nuk behet update -- varchar
		
	WHERE dbo.Class.ClassID = @classID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_ClassSchedule_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClassSchedule_Create] (
	@classID int,
	@subjectID int,
	@time int,
	@date varchar(20),
	@year int,
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Class_Schedule
	(
	    --ScheduleID - column value is auto-generated
	    ClassID,
	    SubjectID,
	    [Time],
	    Day,
	    [Year],
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- ScheduleID - int
	    @classID, -- ClassID - int
	    @subjectID, -- SubjectID - int
	    @time, -- Time - int
	    @date, -- Day - varchar
	    @year, -- Year - int
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
		@LUB, -- LUB - varchar
		GETDATE(), -- InsertDate - datetime
		@insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_ClassSchedule_Delete]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClassSchedule_Delete] (
	@scheduleID int
)
AS
BEGIN
	DELETE FROM dbo.Class_Schedule WHERE [ScheduleID] = @scheduleID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_ClassSchedule_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ClassSchedule_GetList]
AS
BEGIN
	SELECT cs.*, c.Class_No, s.Subject_Title
	FROM dbo.Class_Schedule AS cs
		INNER JOIN dbo.Class AS c
			ON cs.ClassID = c.ClassID
		INNER JOIN dbo.Subjects AS s
			ON cs.SubjectID = s.SubjectID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_ClassSchedule_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_ClassSchedule_Update] (
	@scheduleID int,
	@classID int,
	@subjectID int,
	@time int,
	@date varchar(20),
	@year int,
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Class_Schedule
	SET
	    --ScheduleID - column value is auto-generated
	    dbo.Class_Schedule.ClassID = @classID, -- int
	    dbo.Class_Schedule.SubjectID = @subjectID, -- int
	    dbo.Class_Schedule.[Time] = @time, -- int
	    dbo.Class_Schedule.[Day] = @date, -- varchar
	    dbo.Class_Schedule.[Year] = @year, -- int
	    dbo.Class_Schedule.LUN = @LUN, -- int
	    dbo.Class_Schedule.LUD = GETDATE(), -- datetime
	    dbo.Class_Schedule.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Class_Schedule.ScheduleID = @scheduleID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Comment_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Comment_Create] (
	@coment varchar(MAX),
	@subjectID int,
	@studentID int,
	@time int,
	@date date,
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Comment
	(
	    --CommentID - column value is auto-generated
	    Comment,
	    SubjectID,
		StudentID,
	    [Time],
	    [Date],
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- CommentID - int
	    @coment, -- Comment - varchar
	    @studentID, -- StudentID - int
		@subjectID, -- SubjectID - int
	    @time, -- Time - int
	    @date, -- Date - date
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
		@LUB, -- LUB - varchar
		GETDATE(), -- InsertDate - datetime
		@insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Comment_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Comment_Get] (
	@commentID int
)
AS
BEGIN
	SELECT c.*, s.Subject_Title , re.Review, re.ReviewID, re.ReviewDate, st.First_Name, st.Last_Name
	FROM dbo.Comment AS c
		INNER JOIN dbo.Subjects AS s
			ON c.SubjectID = s.SubjectID
		INNER JOIN dbo.Students AS st
			ON c.StudentID = st.StudentID
		LEFT OUTER JOIN  dbo.Reviews AS re
			ON c.CommentID = re.CommentID
	WHERE c.CommentID = @commentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Comment_Get_ForTeacher]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Comment_Get_ForTeacher](
	@commentID int,
	@teacherID int
)
AS
BEGIN
SELECT c.*, s.Subject_Title , re.Review, re.ReviewID, re.ReviewDate, st.First_Name, st.Last_Name
	FROM dbo.Comment AS c
		INNER JOIN dbo.Subjects AS s
			ON c.SubjectID = s.SubjectID
		INNER JOIN dbo.Students AS st
			ON c.StudentID = st.StudentID
		LEFT OUTER JOIN  dbo.Reviews AS re
			ON c.CommentID = re.CommentID
	WHERE c.CommentID = @commentID AND s.TeacherID = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Comment_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Comment_GetList]
AS
BEGIN
	SELECT c.*, s.Subject_Title , re.Review, re.ReviewID, re.ReviewDate, st.First_Name, st.Last_Name
	FROM dbo.Comment AS c
		INNER JOIN dbo.Subjects AS s
			ON c.SubjectID = s.SubjectID
		INNER JOIN dbo.Students AS st
			ON c.StudentID = st.StudentID
		LEFT OUTER JOIN  dbo.Reviews AS re
			ON c.CommentID = re.CommentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Comment_GetList_ForTeacher]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Comment_GetList_ForTeacher](
	@teacherID int
)
AS
BEGIN
	SELECT c.*, s.Subject_Title , re.Review, re.ReviewID,
	re.ReviewDate, st.First_Name, st.Last_Name, cl.Class_No
	FROM dbo.Comment AS c
		INNER JOIN dbo.Subjects AS s
			ON c.SubjectID = s.SubjectID
		INNER JOIN dbo.Students AS st
			ON c.StudentID = st.StudentID
		LEFT OUTER JOIN  dbo.Reviews AS re
			ON c.CommentID = re.CommentID
			INNER JOIN dbo.Class AS cl
				ON st.ClassID = cl.ClassID
			WHERE cl.TeacherID = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Comment_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Comment_Update] (
	@commentID int,
	@comment varchar(MAX),
	@time int,
	@date date,
	@LUN int,
	@LUB varchar(50)
)
as
BEGIN
	UPDATE dbo.Comment
	SET
	    --CommentID - column value is auto-generated
	    dbo.Comment.Comment = @comment, -- varchar
		-- SubjectID nuk behet update -- int
	    dbo.Comment.[Time] = @time, -- int
	    dbo.Comment.[Date] = @date, -- date
	    dbo.Comment.LUN = @LUN, -- int
	    dbo.Comment.LUD = GETDATE(), -- datetime
	    dbo.Comment.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Comment.CommentID = @commentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Count_Parents]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Count_Parents]
AS
BEGIN
	SELECT COUNT(p.ParentID) FROM dbo.Parents AS p
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Count_Students]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Count_Students]
AS
BEGIN
	SELECT COUNT(s.StudentID) FROM dbo.Students AS s
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Count_Teachers]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Count_Teachers]
AS
BEGIN
	SELECT COUNT(t.TeacherID) FROM dbo.Teachers AS t
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Get_MyStudents]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Get_MyStudents](
	@teacherID int	
)AS
BEGIN
	SELECT s.*, c.Class_No, c.TeacherID, p.First_Name_P, p.Last_Name_P 
	FROM dbo.Students AS s
		LEFT OUTER JOIN dbo.Parents AS p
			ON s.ParentID = p.ParentID
		LEFT OUTER JOIN dbo.Class AS c
			ON s.ClassID = c.ClassID
		WHERE c.TeacherID = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Parent_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Parent_Create] (
	@firstName varchar(50),
	@lastName varchar(50),
	@city varchar(50),
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Parents
	(
	    --ParentID - column value is auto-generated
	    First_Name_P,
	    Last_Name_P,
	    City,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- ParentID - int
	    @firstName, -- First_Name - varchar
	    @lastName, -- Last_Name - varchar
	    @city, -- City - varchar
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Parent_Delete]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Parent_Delete] (
	@parentID int
)
AS
BEGIN
	DELETE FROM dbo.Parents WHERE dbo.Parents.ParentID = @parentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Parent_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   PROCEDURE [dbo].[usp_Parent_Get]
	@parentID int
AS
BEGIN
	SELECT p.* FROM dbo.Parents AS p WHERE p.ParentID = @parentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Parent_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Parent_GetList]
AS
BEGIN
	SELECT p.* from dbo.Parents AS p
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Parent_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Parent_Update] (
	@parentID int,
	@firstName varchar(50),
	@lastName varchar(50),
	@city varchar(50),
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Parents
	SET
	    --ParentID - column value is auto-generated
	    dbo.Parents.First_Name_P = @firstName, -- varchar
	    dbo.Parents.Last_Name_P = @lastName, -- varchar
	    dbo.Parents.City = @city, -- varchar
	    dbo.Parents.LUN = @LUN, -- int
	    dbo.Parents.LUD = GETDATE(), -- datetime
	    dbo.Parents.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Parents.ParentID = @parentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Review_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   PROCEDURE [dbo].[usp_Review_Create] (
	@commentID int,
	@review varchar(MAX),
	@reviewDate date,
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
as
BEGIN
	INSERT INTO Reviews
	(
		CommentID,
		Review,
		ReviewDate,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	values
	(
		@commentID, -- CommentID - int
		@review, -- Review - varchar
		@reviewDate, -- ReviewDate - date
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
		@LUB, -- LUB - varchar
		GETDATE(), -- InsertDate - datetime
		@insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Review_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_Review_Get](
	@reviewID int
)
AS
BEGIN
	SELECT r.*, c.Comment, c.[Date], c.[Time], c.SubjectID,
	c.StudentID, su.Subject_Title, st.First_Name, st.Last_Name
	FROM dbo.Reviews AS r
		INNER JOIN dbo.Comment AS c
			ON r.CommentID = c.CommentID
			INNER JOIN dbo.Students as s
				ON c.StudentID = s.StudentID
			INNER JOIN dbo.Subjects AS su
				ON c.SubjectID = su.SubjectID
			INNER JOIN dbo.Students AS st
				ON c.StudentID = st.StudentID
	WHERE r.ReviewID = @reviewID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Review_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Review_GetList]
AS
BEGIN
SELECT r.*, c.Comment, c.[Date], c.[Time], c.SubjectID,
	c.StudentID, su.Subject_Title, st.First_Name, st.Last_Name
	FROM dbo.Reviews AS r
		INNER JOIN dbo.Comment AS c
			ON r.CommentID = c.CommentID
			INNER JOIN dbo.Students as s
				ON c.StudentID = s.StudentID
			INNER JOIN dbo.Subjects AS su
				ON c.SubjectID = su.SubjectID
			INNER JOIN dbo.Students AS st
				ON c.StudentID = st.StudentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Review_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   PROCEDURE [dbo].[usp_Review_Update] (
	@reviewID int,
	@review varchar(MAX),
	@reviewDate date,
	@LUN int,
	@LUB varchar(50)
)
as
BEGIN
	UPDATE dbo.Reviews
	SET
	    --CommentID - column value is auto-generated
	    dbo.Reviews.Review = @review, -- varchar
	    dbo.Reviews.ReviewDate = @reviewDate, -- date
	    dbo.Reviews.LUN = @LUN, -- int
	    dbo.Reviews.LUD = GETDATE(), -- datetime
	    dbo.Reviews.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Reviews.ReviewID = @reviewID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Room_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Room_Create] (
	@roomNo int,
	@roomType varchar(30),
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Rooms
	(
	    --RoomID - column value is auto-generated
	    Room_No,
	    Room_Type,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- RoomID - int
	    @roomno, -- Room_No - int
	    @roomtype, -- Room_Type - varchar
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Room_Delete]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Room_Delete] (
	@roomID int
)
AS
BEGIN
	DELETE FROM dbo.Rooms WHERE dbo.Rooms.RoomID = @roomID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Room_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[usp_Room_Get]
	@roomID int
AS
BEGIN
	SELECT r.* FROM dbo.Rooms AS r WHERE r.RoomID = @roomID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Room_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Room_GetList]
AS
BEGIN
	SELECT r.* from dbo.Rooms as r
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Room_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Room_Update] (
	@roomID int,
	@roomno int,
	@roomtype varchar(30),
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Rooms
	SET
	    --RoomID - column value is auto-generated
	    dbo.Rooms.Room_No = @roomno, -- int
	    dbo.Rooms.Room_Type = @roomtype, -- varchar
	    dbo.Rooms.LUN = @LUN, -- int
	    dbo.Rooms.LUD = GETDATE(), -- datetime
	    dbo.Rooms.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Rooms.RoomID = @roomID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Staf_Create_Absence]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Staf_Create_Absence] (
	@firstname varchar(50),
	@lastname varchar(50),
	@roleID int,
	@absencedate date,
	@abreasoning varchar(25),
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Users
	(
	    --UserID - column value is auto-generated
	    First_Name,
	    Last_Name,
	    RoleID,
	    AbsenceDate,
	    AbsenceReasoning,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- UserID - int
	    @firstname, -- First_Name - varchar
	    @lastname, -- Last_Name - varchar
	    @roleID, -- RoleID - int
	    @absencedate, -- AbsenceDate - date
	    @abreasoning, -- AbsenceReasoning - varchar
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Staf_GetList_Absence]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Staf_GetList_Absence]
AS
BEGIN
	SELECT u.UserID, u.First_Name, u.Last_Name, u.RoleID,
		u.AbsenceDate, u.AbsenceReasoning, r.RoleName
	FROM dbo.Users AS u
		INNER JOIN dbo.Roles AS r
			ON u.RoleID = r.RoleID
	WHERE u.AbsenceReasoning <> 'NONE'
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Staf_Update_Absence]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Staf_Update_Absence] (
	@userID int,
	@firstname varchar(50),
	@lastname varchar(50),
	@roleID int,
	@absencedate date,
	@abreasoning varchar(25),
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Users
	SET
	    --UserID - column value is auto-generated
	    dbo.Users.First_Name = @firstname, -- varchar
	    dbo.Users.Last_Name = @lastname, -- varchar
	    dbo.Users.RoleID = @roleID, -- int
	    dbo.Users.AbsenceDate = @absencedate, -- date
	    dbo.Users.AbsenceReasoning = @abreasoning, -- varchar
	    dbo.Users.LUN = @LUN, -- int
	    dbo.Users.LUD = GETDATE(), -- datetime
	    dbo.Users.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Users.UserID = @userID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Student_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Student_Create] (
	@firstName varchar(50),
	@lastName varchar(50),
	@gender varchar(25),
	@dob date,
	@classID int,
	@parentID int,
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Students
	(
		--StudentID - column value is auto-generated
		First_Name,
		Last_Name,
		Gender,
		Day_of_Birth,
		ClassID,
		ParentID,
		LUN,
		LUD,
		LUB,
		InsertDate,
		InsertBy
	)
	VALUES
	(
		-- StudentID - int
	    @firstName, -- First_Name - varchar
	    @lastName, -- Last_Name - varchar
		@gender, -- Gender - varchar
		@dob, -- Day_of_Birth - date
		@classID, -- ClassID - int
		@parentID, -- ParentID - int
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Student_Delete]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Student_Delete](
	@studentID int
)
AS
BEGIN
	DELETE FROM dbo.Students WHERE dbo.Students.StudentID = @studentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Student_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Student_Get]
	@studentID int
AS
BEGIN
	SELECT s.*, c.Class_No, p.First_Name_P, p.Last_Name_P
	FROM dbo.Students AS s
		INNER JOIN dbo.Class AS c
			ON s.ClassID = c.ClassID
		INNER JOIN dbo.Parents AS p
			ON s.ParentID = p.ParentID
	WHERE s.StudentID = @studentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Student_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Student_GetList]
AS
BEGIN
	SELECT s.*, c.Class_No, p.First_Name_P, p.Last_Name_P
	FROM dbo.Students AS s
		INNER JOIN dbo.Class AS c
			ON s.ClassID = c.ClassID
		INNER JOIN dbo.Parents AS p
			ON s.ParentID = p.ParentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Student_GetList_ForTeacher]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Student_GetList_ForTeacher](
	@teacherID int
)
AS
BEGIN
	SELECT s.*, c.Class_No, p.First_Name_P, p.Last_Name_P
	FROM dbo.Students AS s
		INNER JOIN dbo.Class AS c
			ON s.ClassID = c.ClassID
		INNER JOIN dbo.Parents AS p
			ON s.ParentID = p.ParentID
		WHERE c.TeacherID = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Student_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Student_Update] (
	@studentID int,
	@firstName varchar(50),
	@lastName varchar(50),
	@dob date,
	@classID int,
	@parentID int,
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Students
	SET
		dbo.Students.First_Name = @firstName, -- varchar
		dbo.Students.Last_Name = @lastName, -- varchar
		dbo.Students.Day_of_Birth = @dob, -- date
		dbo.Students.ClassID = @classID, -- int
		dbo.Students.ParentID = @parentID, -- int
	    dbo.Students.LUN = @LUN, -- int
	    dbo.Students.LUD = GETDATE(), -- datetime
	    dbo.Students.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Students.StudentID = @studentID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Subject_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Subject_Create] (
	@subjecttitle varchar(30),
	@book varchar(50),
	@bookauthor varchar(50),
	@teacherID int,
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Subjects
	(
	    --SubjectID - column value is auto-generated
	    Subject_Title,
	    Book,
	    Book_Author,
	    TeacherID,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- SubjectID - int
	    @subjecttitle, -- Subject_Title - varchar
	    @book, -- Book - varchar
	    @bookauthor, -- Book_Author - varchar
	    @teacherID, -- TeacherID - int
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Subject_Delete]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Subject_Delete] (
	@subjectID int	
)
AS
BEGIN
	DELETE FROM dbo.Subjects WHERE [SubjectID] = @subjectID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Subject_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[usp_Subject_Get]
	@subjectID int
AS
BEGIN
	SELECT s.*, t.First_Name_T, t.Last_Name_T
	FROM dbo.Subjects as s
		INNER JOIN dbo.Teachers as t
			ON s.TeacherID = t.TeacherID
	WHERE s.SubjectID = @subjectID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Subject_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Subject_GetList]
AS
BEGIN
	SELECT s.*, t.First_Name_T, t.Last_Name_T
	FROM dbo.Subjects as s
		INNER JOIN dbo.Teachers as t
			ON s.TeacherID = t.TeacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Subject_GetList_ForTeacher]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Subject_GetList_ForTeacher](
	@teacherID int
)
AS
BEGIN
	SELECT s.*, t.First_Name_T, t.Last_Name_T
	FROM dbo.Subjects as s
		INNER JOIN dbo.Teachers as t
			ON s.TeacherID = t.TeacherID
		WHERE s.TeacherID = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Subject_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Subject_Update] (
	@subjectID int,
	@book varchar(50),
	@bookauthor varchar(50),
	@teacherID int,
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Subjects
	SET
	    --SubjectID - column value is auto-generated
		-- SubjectTitle nuk behet update - varchar
	    dbo.Subjects.Book = @book, -- varchar
	    dbo.Subjects.Book_Author = @bookauthor, -- varchar
	    dbo.Subjects.TeacherID = @teacherID, -- int
	    dbo.Subjects.LUN = @LUN, -- int
	    dbo.Subjects.LUD = GETDATE(), -- datetime
	    dbo.Subjects.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Subjects.SubjectID = @subjectID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Teacher_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Teacher_Create] (
	@firstname varchar(20),
	@lastname varchar(20),
	@gender varchar(20),
	@city varchar(20),
	@qualification varchar(20),
	@dayofbirth date,
	@email varchar(320),
	@phoneno varchar(20),
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Teachers
	(
	    --TeacherID - column value is auto-generated
	    First_Name_T,
	    Last_Name_T,
	    Gender,
	    City,
	    Qualification,
	    Day_of_Birth,
	    Email,
	    Phone_No,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- TeacherID - int
	    @firstname, -- First_Name - varchar
	    @lastname, -- Last_Name - varchar
	    @gender, -- Gender - varchar
	    @city, -- Address - varchar
	    @qualification, -- Qualification - varchar
	    @dayofbirth, -- Day_of_Birth - date
	    @email, -- Email - varchar
	    @phoneno, -- Phone_No - varchar
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Teacher_Delete]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Teacher_Delete]
	@teacherID int
AS
BEGIN
	DELETE FROM dbo.Teachers WHERE [TeacherID] = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Teacher_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[usp_Teacher_Get]
	@teacherID int
AS
BEGIN
	SELECT t.* FROM dbo.Teachers AS t WHERE t.TeacherID = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Teacher_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Teacher_Update] (
	@teacherID int,
	@firstname varchar(20),
	@lastname varchar(20),
	@gender varchar(20),
	@city varchar(20),
	@qualification varchar(20),
	@dayofbirth date,
	@email varchar(320),
	@phoneno varchar(20),
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Teachers
	SET
	    --TeacherID - column value is auto-generated
	    dbo.Teachers.First_Name_T = @firstname, -- varchar
	    dbo.Teachers.Last_Name_T = @lastname, -- varchar
	    dbo.Teachers.Gender = @gender, -- varchar
	    dbo.Teachers.City = @city, -- varchar
	    dbo.Teachers.Qualification = @qualification, -- varchar
	    dbo.Teachers.Day_of_Birth = @dayofbirth, -- date
	    dbo.Teachers.Email = @email, -- varchar
	    dbo.Teachers.Phone_No = @phoneno, -- varchar
	    dbo.Teachers.LUN = @LUN, -- int
	    dbo.Teachers.LUD = GETDATE(), -- datetime
	    dbo.Teachers.LUB = @LUB -- varchar
		-- InsertDate nuk behet update -- datetime
		-- InsertBy nuk behet update -- varchar

	WHERE dbo.Teachers.TeacherID = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Teachers_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Teachers_GetList]
AS
BEGIN 
	SELECT t.* FROM dbo.[Teachers] AS t
END	
GO
/****** Object:  StoredProcedure [dbo].[usp_Topic_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Topic_Create] (
	@content varchar(MAX),
	@classID int,
	@subjectID int,
	@time int,
	@date date,
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
	INSERT INTO dbo.Topics
	(
	    --TopicID - column value is auto-generated
	    Content,
	    ClassID,
	    SubjectID,
	    [Time],
	    [Date],
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- TopicID - int
	    @content, -- Content - varchar
	    @classID, -- ClassID - int
	    @subjectID, -- SubjectID - int
	    @time, -- Time - int
	    @date, -- Date - date
		@LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
		GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Topic_Delete]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Topic_Delete] (
	@topicID int
)
AS
BEGIN
	DELETE FROM dbo.Topics WHERE [TopicID] = @topicID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Topic_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   PROCEDURE [dbo].[usp_Topic_Get]
	@topicID int
AS
BEGIN
	SELECT t.*, c.Class_No, s.Subject_Title
	FROM dbo.Topics AS t
		INNER JOIN dbo.Class AS c
			ON t.ClassID = c.ClassID
		INNER JOIN dbo.Subjects AS s
			ON t.SubjectID = s.SubjectID
	WHERE t.TopicID = @topicID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Topic_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Topic_GetList]
AS
BEGIN
	SELECT t.*, c.Class_No, s.Subject_Title 
	FROM dbo.Topics AS t
		INNER JOIN dbo.Class AS c
			ON t.ClassID = c.ClassID
		INNER JOIN dbo.Subjects AS s
			ON t.SubjectID = s.SubjectID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Topic_GetList_ForTeacher]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[usp_Topic_GetList_ForTeacher](
	@teacherID int
)
AS
BEGIN
	SELECT t.*, c.Class_No, s.Subject_Title 
	FROM dbo.Topics AS t
		INNER JOIN dbo.Class AS c
			ON t.ClassID = c.ClassID
		INNER JOIN dbo.Subjects AS s
			ON t.SubjectID = s.SubjectID
		WHERE c.TeacherID = @teacherID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Topic_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Topic_Update] (
	@topicID int,
	@content varchar(MAX),
	@classID int,
	@time int,
	@date date,
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Topics
	SET
	    --TopicID - column value is auto-generated
	    dbo.Topics.Content = @content, -- varchar
	    dbo.Topics.ClassID = @classID, -- int
	    -- SubjectID nuk behet update -- int
	    dbo.Topics.[Time] = @time, -- int
	    dbo.Topics.[Date] = @date, -- date
	    dbo.Topics.LUN = @LUN, -- int
	    dbo.Topics.LUD = GETDATE(), -- datetime
	    dbo.Topics.LUB = @LUB -- varchar
	    -- InsertDate nuk behet update -- datetime
	    -- InsertBy nuk behet update -- varchar

	WHERE dbo.Topics.TopicID = @topicID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_User_ChangePassword]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_ChangePassword] (
	@userID int,
	@userpass varchar(MAX),
	@lastpasswordchangedate datetime,
	@ispasswordchanged bit,
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN
	UPDATE dbo.Users
	SET
	    --UserID - column value is auto-generated
	    dbo.Users.UserPass = @userpass, -- varchar
		dbo.Users.LastPasswordChangeDate = GETDATE(), -- datetime
		dbo.Users.IsPasswordChanged = @ispasswordchanged, -- bit
	    dbo.Users.LUN = @LUN, -- int
	    dbo.Users.LUD = GETDATE(), -- datetime
	    dbo.Users.LUB = @LUB -- varchar

	WHERE dbo.Users.UserID = @userID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_User_Create]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_User_Create] (
	@username varchar(20),
	@password varchar(MAX),
	@firstname varchar(20),
	@lastname varchar(20),
	@expiresdate date,
	@teacherID int,
	@parentID int,
	@roleID int,
	@LUN int,
	@LUB varchar(50),
	@insertby varchar(50)
)
AS
BEGIN
IF (@teacherID = 0 AND @parentID = 0)
	BEGIN
	INSERT INTO dbo.Users
	(
	    --UserID - column value is auto-generated
	    UserName,
	    UserPass,
	    First_Name,
	    Last_Name,
	    ExpiresDate,
	    RoleID,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- UserID - int
	    @username, -- UserName - varchar
	    @password, -- UserPass - varchar
	    @firstname, -- First_Name - varchar
	    @lastname, -- Last_Name - varchar
	    @expiresdate, -- ExpiresDate - date
	    @roleID, -- RoleID - int
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
	END
	ELSE IF (@parentID = 0)
	BEGIN
	INSERT INTO dbo.Users
	(
	    --UserID - column value is auto-generated
	    UserName,
	    UserPass,
	    ExpiresDate,
		TeacherID,
	    RoleID,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- UserID - int
	    @username, -- UserName - varchar
	    @password, -- UserPass - varchar
	    @expiresdate, -- ExpiresDate - date
		@teacherID, -- TeacherID - int
	    @roleID, -- RoleID - int
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
	END
	ELSE IF (@teacherID = 0)
	BEGIN
	INSERT INTO dbo.Users
	(
	    --UserID - column value is auto-generated
	    UserName,
	    UserPass,
	    ExpiresDate,
		ParentID,
	    RoleID,
	    LUN,
	    LUD,
	    LUB,
	    InsertDate,
	    InsertBy
	)
	VALUES
	(
	    -- UserID - int
	    @username, -- UserName - varchar
	    @password, -- UserPass - varchar
	    @expiresdate, -- ExpiresDate - date
		@parentID, -- ParentID - int
	    @roleID, -- RoleID - int
	    @LUN, -- LUN - int
	    GETDATE(), -- LUD - datetime
	    @LUB, -- LUB - varchar
	    GETDATE(), -- InsertDate - datetime
	    @insertby -- InsertBy - varchar
	)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[usp_User_Delete]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_User_Delete]
	@userID int
AS
BEGIN
	DELETE FROM dbo.Users WHERE [UserID] = @userID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_User_Get]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[usp_User_Get]
	@userID int
AS
BEGIN
	SELECT u.*, r.RoleID, r.RoleName, t.First_Name_T, t.Last_Name_T, p.First_Name_P, p.Last_Name_P
	FROM dbo.Users AS u
		INNER JOIN dbo.Roles AS r
			ON u.RoleID = r.RoleID
		LEFT OUTER JOIN dbo.Teachers AS t
			ON u.TeacherID = t.TeacherID
		LEFT OUTER JOIN dbo.Parents AS p
			ON u.ParentID = p.ParentID
	WHERE u.UserID = @userID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_User_Update]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_User_Update] (
	@userID int,
	@username varchar(20),
	@firstname varchar(20),
	@lastname varchar(20),
	@expiresdate date,
	@teacherID int,
	@parentID int,
	@LUN int,
	@LUB varchar(50)
)
AS
BEGIN 
	IF (@teacherID = 0 AND @parentID = 0)
	BEGIN
		UPDATE dbo.Users
		SET
			--UserID - column value is auto-generated
			dbo.Users.UserName = @username, -- varchar
			-- UserPassword nuk behet update -- varchar
			dbo.Users.First_Name = @firstname, -- varchar
			dbo.Users.Last_Name = @lastname, -- varchar
			dbo.Users.ExpiresDate = @expiresdate, -- date
			-- RoleID nuk behet update -- int
			dbo.Users.LUN = @LUN, -- int
			dbo.Users.LUD = GETDATE(), -- datetime
			dbo.Users.LUB = @LUB -- varchar
			-- InsertDate nuk behet update -- datetime
			-- InsertBy nuk behet update -- varchar
		
		WHERE dbo.Users.UserID = @userID
	END
	ELSE IF (@parentID = 0)
	BEGIN
		UPDATE dbo.Users
		SET
			--UserID - column value is auto-generated
			dbo.Users.UserName = @username, -- varchar
			-- UserPassword nuk behet update -- varchar
			dbo.Users.ExpiresDate = @expiresdate, -- date
			dbo.Users.TeacherID = @teacherID, -- int
			-- RoleID nuk behet update -- int
			dbo.Users.LUN = @LUN, -- int
			dbo.Users.LUD = GETDATE(), -- datetime
			dbo.Users.LUB = @LUB -- varchar
			-- InsertDate nuk behet update -- datetime
			-- InsertBy nuk behet update -- varchar
		
		WHERE dbo.Users.UserID = @userID
	END
	ELSE IF (@teacherID = 0)
	BEGIN
		UPDATE dbo.Users
		SET
			--UserID - column value is auto-generated
			dbo.Users.UserName = @username, -- varchar
			-- UserPassword nuk behet update -- varchar
			dbo.Users.ExpiresDate = @expiresdate, -- date
			dbo.Users.ParentID = @parentID, -- int
			-- RoleID nuk behet update -- int
			dbo.Users.LUN = @LUN, -- int
			dbo.Users.LUD = GETDATE(), -- datetime
			dbo.Users.LUB = @LUB -- varchar
			-- InsertDate nuk behet update -- datetime
			-- InsertBy nuk behet update -- varchar
		
		WHERE dbo.Users.UserID = @userID
	END
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Users_GetList]    Script Date: 07-Dec-20 5:30:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Users_GetList]
AS
BEGIN 
	SELECT u.*, r.RoleID, r.RoleName, t.First_Name_T, t.Last_Name_T, p.First_Name_P, p.Last_Name_P
	FROM dbo.Users AS u
		INNER JOIN dbo.Roles AS r
			ON u.RoleID = r.RoleID
		LEFT OUTER JOIN dbo.Teachers AS t
			ON u.TeacherID = t.TeacherID
		LEFT OUTER JOIN dbo.Parents AS p
			ON u.ParentID = p.ParentID
END	
GO
USE [master]
GO
ALTER DATABASE [SchoolDiarySystem] SET  READ_WRITE 
GO
