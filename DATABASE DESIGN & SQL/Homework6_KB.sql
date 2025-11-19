-- --------------------------------------------------------------------------------
-- Name: Keith Brock
-- Class: IT-112
-- Abstract: Homework 6 - Functions
-- --------------------------------------------------------------------------------

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbSQL1;     -- Get out of the master database
SET NOCOUNT ON; -- Report only errors

-- --------------------------------------------------------------------------------
-- Drop Tables
-- --------------------------------------------------------------------------------

IF OBJECT_ID( 'TUserFavoriteSongs' )				IS NOT NULL DROP TABLE		TUserFavoriteSongs
IF OBJECT_ID( 'TSongs' )							IS NOT NULL DROP TABLE		TSongs
IF OBJECT_ID( 'TUsers' )							IS NOT NULL DROP TABLE		TUsers


IF OBJECT_ID( 'TCourseBooks' )						IS NOT NULL DROP TABLE		TCourseBooks
IF OBJECT_ID( 'TBooks' )							IS NOT NULL DROP TABLE		TBooks
IF OBJECT_ID( 'TCourseStudents' )					IS NOT NULL DROP TABLE		TCourseStudents
IF OBJECT_ID( 'TStudents' )							IS NOT NULL DROP TABLE		TStudents
IF OBJECT_ID( 'TCourseRooms' )						IS NOT NULL DROP TABLE		TCourseRooms
IF OBJECT_ID( 'TRooms' )							IS NOT NULL DROP TABLE		TRooms
IF OBJECT_ID( 'TCourses' )							IS NOT NULL DROP TABLE		TCourses
IF OBJECT_ID( 'TInstructors' )						IS NOT NULL DROP TABLE		TInstructors


-- --------------------------------------------------------------------------------
-- Step #1.1: Create Tables
-- --------------------------------------------------------------------------------

CREATE TABLE TUsers
(
	 intUserID			INTEGER			NOT NULL
	,strUserName		VARCHAR(50)		NOT NULL
	,strEmailAddress	VARCHAR(50)		NOT NULL
	,CONSTRAINT TUsers_PK PRIMARY KEY ( intUserID )
)

CREATE TABLE TSongs
(
	 intSongID			INTEGER			NOT NULL
	,strSongName		VARCHAR(50)		NOT NULL
	,strArtist			VARCHAR(50)		NOT NULL
	,CONSTRAINT TSongs_PK PRIMARY KEY ( intSongID )
)

CREATE TABLE TUserFavoriteSongs
(
	 intUserFavoriteSongID	INTEGER			NOT NULL
	,intUserID				INTEGER			NOT NULL
	,intSongID				INTEGER			NOT NULL
	,CONSTRAINT	TUserSongs_UQ	UNIQUE	(intUserID, intSongID	)
	,CONSTRAINT TUserSongs_PK PRIMARY KEY ( intUserFavoriteSongID )
)

-- --------------------------------------------------------------------------------
-- Identify and Create Foreign Keys
-- --------------------------------------------------------------------------------
--
-- #	Child								Parent						Column(s)
-- -	-----								------						---------
-- 1	TUserFavoriteSongs					TUsers						intUserID
-- 2	TUserFavoriteSongs					TSongs						intSongID

-- 1
ALTER TABLE TUserFavoriteSongs ADD CONSTRAINT TUserFavoriteSongs_TUsers_FK
FOREIGN KEY ( intUserID ) REFERENCES TUsers ( intUserID )

-- 2
ALTER TABLE TUserFavoriteSongs ADD CONSTRAINT TUserFavoriteSongs_TSongs_FK
FOREIGN KEY ( intSongID ) REFERENCES TSongs ( intSongID )



-- --------------------------------------------------------------------------------
-- Step #1.3: Add at least 3 users
-- --------------------------------------------------------------------------------
INSERT INTO TUsers ( intUserID, strUserName, strEmailAddress )
VALUES	 ( 1, 'JoeJoe1998', 'jj1998@gmail.com' )
		,( 2, 'FreeNClear21', 'FreeNClear21@gmail.com' )
		,( 3, 'IamHere321', 'IamHere321@gmail.com' )
		
		
-- --------------------------------------------------------------------------------
-- Step #1.4: Add at least 3 Songs
-- --------------------------------------------------------------------------------
INSERT INTO TSongs ( intSongID, strSongName, strArtist )
VALUES	 ( 1, 'Hysteria', 'Def Leppard' )
		,( 2, 'Hotel California', 'Eagles' )
		,( 3, 'Shake Me', 'Cinderella' )
		
-- --------------------------------------------------------------------------------
-- Step #1.5: Add at at least 6 User/Song assignments
-- --------------------------------------------------------------------------------
INSERT INTO TUserFavoriteSongs ( intUserFavoriteSongID, intUserID, intSongID )
VALUES	 ( 1, 1, 1 )
		,( 2, 1, 2 )
		,( 3, 2, 1 )
		,( 4, 2, 3 )
		,( 5, 3, 2 )
		,( 6, 3, 3 )


-- --------------------------------------------------------------------------------
-- Step #2.1: Create Tables
-- --------------------------------------------------------------------------------
CREATE TABLE TCourses
(
	 intCourseID 					INTEGER			NOT NULL
	,strCourseName						VARCHAR(50)		NOT NULL
	,strDescription					VARCHAR(50)		NOT NULL
	,intInstructorID				INTEGER			NOT NULL
	,CONSTRAINT TCourses_PK PRIMARY KEY( intCourseID )
)

CREATE TABLE TInstructors
(
	 intInstructorID				INTEGER			NOT NULL
	,strFirstName					VARCHAR(50)		NOT NULL
	,strLastName					VARCHAR(50)		NOT NULL
	,strPhoneNumber					VARCHAR(50)		NOT NULL
	,CONSTRAINT TInstructors_PK PRIMARY KEY ( intInstructorID )
)

CREATE TABLE TCourseRooms
(
	 intCourseRoomID				INTEGER			NOT NULL
	,intCourseID 					INTEGER			NOT NULL
	,intRoomID	 					INTEGER			NOT NULL
	,strMeetingTimes				VARCHAR(50)		NOT NULL
	,CONSTRAINT TCourseRooms_UQ	UNIQUE	(intCourseID, intRoomID	)
	,CONSTRAINT TCourseRooms_PK PRIMARY KEY( intCourseRoomID )
)

CREATE TABLE TRooms
(
	 intRoomID						INTEGER			NOT NULL
	,strRoomNumber					VARCHAR(50)		NOT NULL
	,intRoomCapacity				INTEGER			NOT NULL
	,CONSTRAINT TRooms_PK PRIMARY KEY ( intRoomID )
)

CREATE TABLE TCourseStudents
(
	 intCourseStudentID				INTEGER			NOT NULL
	,intCourseID 					INTEGER			NOT NULL
	,intStudentID	 				INTEGER			NOT NULL
	,CONSTRAINT TCourseStudent_UQ	UNIQUE	(intCourseID, intStudentID	)
	,CONSTRAINT TCourseStudents_PK PRIMARY KEY( intCourseStudentID )
)

CREATE TABLE TStudents
(
	 intStudentID					INTEGER			NOT NULL
	,strFirstName					VARCHAR(50)		NOT NULL
	,strLastName					VARCHAR(50)		NOT NULL
	,strStudentNumber				VARCHAR(50)		NOT NULL
	,strPhoneNumber					VARCHAR(50)		NOT NULL
	,CONSTRAINT TStudents_PK PRIMARY KEY ( intStudentID )
)

CREATE TABLE TCourseBooks
(	
	 intCourseBookID				INTEGER			NOT NULL
	,intCourseID 					INTEGER			NOT NULL
	,intBookID	 					INTEGER			NOT NULL
	,CONSTRAINT TCourseBooks_UQ	UNIQUE	(intCourseID, intBookID	)
	,CONSTRAINT TCourseBooks_PK PRIMARY KEY( intCourseBookID )
)

CREATE TABLE TBooks
(
	 intBookID	 					INTEGER			NOT NULL
	,strBookName					VARCHAR(50)		NOT NULL
	,strBookAuthor					VARCHAR(50)		NOT NULL
	,strBookISBN					VARCHAR(50)		NOT NULL
	,CONSTRAINT TBooks_PK PRIMARY KEY( intBookID )
)

-- --------------------------------------------------------------------------------
-- Step #2.2: Identify and Create Foreign Keys
-- --------------------------------------------------------------------------------
--
-- #	Child					Parent					Column(s)
-- -	-----					------					---------
-- 1	TCourses				TInstructors			intInstructorID
-- 2	TCourseRooms			TCourses				intCourseID
-- 3	TCourseRooms			TRooms					intRoomID
-- 4	TCourseStudents			TCourses				intCourseID
-- 5	TCourseStudents			TStudents				intStudentID
-- 6	TCourseBooks			TCourses				intCourseID
-- 7	TCourseBooks			TBooks					intBookID

-- 1
ALTER TABLE TCourses ADD CONSTRAINT TCourses_TInstructors_FK
FOREIGN KEY ( intInstructorID ) REFERENCES TInstructors ( intInstructorID )

-- 2
ALTER TABLE TCourseRooms ADD CONSTRAINT TCourseRooms_TCourses_FK
FOREIGN KEY ( intCourseID ) REFERENCES TCourses ( intCourseID )

-- 3
ALTER TABLE TCourseRooms ADD CONSTRAINT TCourseRooms_TRooms_FK
FOREIGN KEY ( intRoomID ) REFERENCES TRooms ( intRoomID )

-- 4
ALTER TABLE TCourseStudents ADD CONSTRAINT TCourseStudents_TCourses_FK
FOREIGN KEY ( intCourseID ) REFERENCES TCourses ( intCourseID )

-- 5
ALTER TABLE TCourseStudents ADD CONSTRAINT TCourseStudents_TStudents_FK
FOREIGN KEY ( intStudentID ) REFERENCES TStudents ( intStudentID )

-- 6
ALTER TABLE TCourseBooks ADD CONSTRAINT TCourseBooks_TCourses_FK
FOREIGN KEY ( intCourseID ) REFERENCES TCourses ( intCourseID )

-- 7
ALTER TABLE TCourseBooks ADD CONSTRAINT TCourseBooks_TBooks_FK
FOREIGN KEY ( intBookID ) REFERENCES TBooks ( intBookID )

-- --------------------------------------------------------------------------------
-- Step #2.3: Add at least 3 coures (must add instructors first)
-- --------------------------------------------------------------------------------
INSERT INTO TInstructors( intInstructorID, strFirstName, strLastName, strPhoneNumber )
VALUES	 ( 1, 'Tomie', 'Gartland', 'x1751' )
		,( 2, 'Bob', 'Nields', 'x1752' )
		,( 3, 'Ray', 'Harmon', 'x1753' )

INSERT INTO TCourses ( intCourseID, strCourseName, strDescription, intInstructorID )
VALUES	 ( 1, 'IT-101', '.Net Programming #1', 2 )
		,( 2, 'IT-110', 'HTML, CSS and JavaScript', 3 )
		,( 3, 'IT-111', 'Database Design and SQL #1', 1 )
			
-- --------------------------------------------------------------------------------
-- Step #2.4: Add at least 3 rooms and assign at least one room to each course
-- --------------------------------------------------------------------------------
INSERT INTO TRooms( intRoomID, strRoomNumber, intRoomCapacity )
VALUES	 ( 1, 'ATLC 410', 20 )
		,( 2, 'ATLC 414', 20 )
		,( 3, 'Virtual Classrooom', 20 )
		
INSERT INTO TCourseRooms( intCourseRoomID, intCourseID, intRoomID, strMeetingTimes )
VALUES	 ( 1, 1, 1, 'M/W 8am - 9:50am' )
		,( 2, 1, 2, 'T/R 8am - 9:50am' )
		,( 3, 2, 3, 'N/A - Online' )
		,( 4, 3, 1, 'M/W 8am - 9:50am' )
		,( 5, 3, 2, 'T/R 8am - 9:50am' )

-- --------------------------------------------------------------------------------
-- Step #2.5: Add at least 3 books and assign at least one book to each course
-- --------------------------------------------------------------------------------
INSERT INTO TBooks ( intBookID, strBookName, strBookAuthor, strBookISBN )
VALUES	 ( 1, 'I Love VB.Net', 'Martin Schmitz', 'ABC-123' )
		,( 2, 'I Really Love VB.Net', 'Eric Furniss', 'DEF-456' )
		,( 3, 'CSS Styles Rock it', 'Jeremy Rogers', 'GHI-789' )
		,( 4, 'JavaScript Can Save Your Life', 'Justin Lee', 'JKL-246' )
		,( 5, 'SQL Server For Programmers', 'Jim Oracle', 'JO0-12845' )
		,( 6, 'Advanced SQL', 'Austin Mockabee', 'HIJ-5694/2' )

INSERT INTO TCourseBooks ( intCourseBookID, intCourseID, intBookID )
VALUES	 ( 1, 1, 1 )
		,( 2, 1, 2 )
		,( 3, 2, 3 )
		,( 4, 2, 4 )
		,( 5, 3, 5 )
		,( 6, 3, 6 )

-- --------------------------------------------------------------------------------
-- Step #2.6: Add at least 3 students and assign at least two students to each course
-- --------------------------------------------------------------------------------
INSERT INTO TStudents( intStudentID, strFirstName, strLastName, strStudentNumber, strPhoneNumber )
VALUES	 ( 1, 'Eric', 'James', 'SN-0001', '555-0001' )
		,( 2, 'Susan', 'Little', 'SN-0002', '555-0002' )
		,( 3, 'Stan', 'Hoening', 'SN-0001', '555-0001' )
		,( 4, 'Jill', 'Waters', 'SN-0002', '555-0002' )
		,( 5, 'Fred', 'Smith', 'SN-0001', '555-0001' )
		,( 6, 'Angie', 'Mason', 'SN-0002', '555-0002' )

INSERT INTO TCourseStudents( intCourseStudentID, intCourseID, intStudentID )
VALUES	 ( 1, 1, 1 )
		,( 2, 1, 2 )
		,( 3, 2, 3 )
		,( 4, 2, 4 )
		,( 5, 3, 5 )
		,( 6, 3, 6 )


-- --------------------------------------------------------------------------------
-- Functions
-- --------------------------------------------------------------------------------
-- Problem 1
-- 1.	Create a function called fn_GetUserName which will return the user name
--		from the TUsers table.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('fn_GetUserName') IS NOT NULL DROP FUNCTION fn_GetUserName;
GO

CREATE FUNCTION fn_GetUserName
(
	@intUserID INT
)
RETURNS VARCHAR(50)
AS
BEGIN
	DECLARE @strUserName VARCHAR(50);

	SELECT @strUserName = strUserName
	FROM TUsers
	WHERE intUserID = @intUserID;

	RETURN @strUserName;
END;
GO

-- --------------------------------------------------------------------------------
-- 2.	Create a function called fn_GetSongs which will return the song title
--		and artist from the TSongs table.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('fn_GetSongs') IS NOT NULL DROP FUNCTION fn_GetSongs;
GO

CREATE FUNCTION fn_GetSongs()
RETURNS TABLE
AS
RETURN
(
	SELECT 
		intSongID,
		strSongName AS SongTitle, 
		strArtist AS Artist
	FROM TSongs
);
GO


-- --------------------------------------------------------------------------------
-- 3.	Write a Select statement, using the functions, that will pull the user ID,
--		the user name, the song ID and the song and artist from TUserFavoriteSongs
--		table.
-- --------------------------------------------------------------------------------

SELECT 
	TUFS.intUserID,
	dbo.fn_GetUserName(TUFS.intUserID) AS UserName,
	TUFS.intSongID,
	S.SongTitle,
	S.Artist
FROM TUserFavoriteSongs AS TUFS
JOIN fn_GetSongs() AS S
	ON S.SongTitle = (SELECT strSongName FROM TSongs WHERE intSongID = TUFS.intSongID)
	AND S.Artist = (SELECT strArtist FROM TSongs WHERE intSongID = TUFS.intSongID);

-- --------------------------------------------------------------------------------
-- 4.	Create a view from this select statement
-- --------------------------------------------------------------------------------
IF OBJECT_ID('VUserFavoriteSongs') IS NOT NULL DROP VIEW VUserFavoriteSongs;
GO

CREATE VIEW VUserFavoriteSongs AS
SELECT 
	TUFS.intUserID,
	dbo.fn_GetUserName(TUFS.intUserID) AS UserName,
	TUFS.intSongID,
	S.SongTitle,
	S.Artist
FROM TUserFavoriteSongs AS TUFS
JOIN dbo.fn_GetSongs() AS S
	ON TUFS.intSongID = S.intSongID;
GO
-- --------------------------------------------------------------------------------
-- 5.	Write a Select statement to pull all records from the view
--		(you may use * here)
-- --------------------------------------------------------------------------------
SELECT * FROM VUserFavoriteSongs;

-- --------------------------------------------------------------------------------
-- Problem 2
--	1.	Create a function called fn_GetCourse which will return the course name
--		and description from the TCourses table.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('fn_GetCourse') IS NOT NULL DROP FUNCTION fn_GetCourse;
GO

CREATE FUNCTION fn_GetCourse()
RETURNS TABLE
AS
RETURN
(
	SELECT 
		intCourseID,
		strCourseName AS CourseName,
		strDescription AS Description
	FROM TCourses
);
GO

-- --------------------------------------------------------------------------------
-- 2.	Create a function called fn_GetStudents which will return the last name,
--		first name from the TStudents table. Name should be in format of
--		LastName, FirstName.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('fn_GetStudents') IS NOT NULL
    DROP FUNCTION fn_GetStudents;
GO

CREATE FUNCTION fn_GetStudents()
RETURNS TABLE
AS
RETURN
(
	SELECT 
		intStudentID,
		strLastName + ', ' + strFirstName AS StudentName
	FROM TStudents
);
GO

-- --------------------------------------------------------------------------------
-- 3.	Create a function called fn_GetBook which will return the book name
--		and author from the TBooks table.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('fn_GetBook') IS NOT NULL DROP FUNCTION fn_GetBook;
GO

CREATE FUNCTION fn_GetBook()
RETURNS TABLE
AS
RETURN
(
	SELECT 
		intBookID,
		strBookName AS BookName,
		strBookAuthor AS Author
	FROM TBooks
);
GO

-- --------------------------------------------------------------------------------
-- 4.	Create a function called fn_GetInstructor which will return the last name,
--		first name from the TInstructors table. Name should be in format of
--		LastName, FirstName.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('fn_GetInstructor') IS NOT NULL DROP FUNCTION fn_GetInstructor;
GO

CREATE FUNCTION fn_GetInstructor()
RETURNS TABLE
AS
RETURN
(
	SELECT 
		intInstructorID,
		strLastName + ', ' + strFirstName AS InstructorName
	FROM TInstructors
);
GO

-- --------------------------------------------------------------------------------
-- 5.	Write a Select statement, using the functions, that will pull the student
--		ID, the student name, the course ID, name and description from
--		TCourseStudents table.
-- --------------------------------------------------------------------------------
SELECT 
	TCS.intStudentID,
	S.StudentName,
	TCS.intCourseID,
	C.CourseName,
	C.Description
FROM TCourseStudents AS TCS

JOIN dbo.fn_GetStudents() AS S
ON TCS.intStudentID = S.intStudentID

JOIN dbo.fn_GetCourse() AS C
ON TCS.intCourseID = C.intCourseID;

-- --------------------------------------------------------------------------------
-- 6.	Write a Select statement, using the functions, that will pull the book ID,
--		name, author, the course ID, name and description from TCourseBooks table.
-- --------------------------------------------------------------------------------
SELECT 
	TCB.intBookID,
	B.BookName,
	B.Author,
	TCB.intCourseID,
	C.CourseName,
	C.Description
FROM TCourseBooks AS TCB

JOIN dbo.fn_GetBook() AS B
ON TCB.intBookID = B.intBookID

JOIN dbo.fn_GetCourse() AS C
ON TCB.intCourseID = C.intCourseID;

-- --------------------------------------------------------------------------------
-- 7.	Write a Select statement, using the functions, that will pull the
--		instructor ID, name, the course ID, name and description from
--		TCourses table.
-- --------------------------------------------------------------------------------
SELECT 
	C.intInstructorID,
	I.InstructorName,
	C.intCourseID,
	FC.CourseName,
	FC.Description
FROM TCourses AS C

JOIN dbo.fn_GetInstructor() AS I
ON C.intInstructorID = I.intInstructorID

JOIN dbo.fn_GetCourse() AS FC
ON C.intCourseID = FC.intCourseID;

-- --------------------------------------------------------------------------------
-- 8.	Create a separate view for each of the select statements in steps 5, 6 & 7
-- --------------------------------------------------------------------------------
IF OBJECT_ID('VStudentCourses') IS NOT NULL DROP VIEW VStudentCourses;
GO

CREATE VIEW VStudentCourses AS
SELECT 
	TCS.intStudentID,
	S.StudentName,
	TCS.intCourseID,
	C.CourseName,
	C.Description
FROM TCourseStudents AS TCS

JOIN dbo.fn_GetStudents() AS S
ON TCS.intStudentID = S.intStudentID

JOIN dbo.fn_GetCourse() AS C
ON TCS.intCourseID = C.intCourseID;
GO

IF OBJECT_ID('VCourseBooks') IS NOT NULL DROP VIEW VCourseBooks;
GO

CREATE VIEW VCourseBooks AS
SELECT 
	TCB.intBookID,
	B.BookName,
	B.Author,
	TCB.intCourseID,
	C.CourseName,
	C.Description
FROM TCourseBooks AS TCB

JOIN dbo.fn_GetBook() AS B
ON TCB.intBookID = B.intBookID

JOIN dbo.fn_GetCourse() AS C
ON TCB.intCourseID = C.intCourseID;
GO

IF OBJECT_ID('VCourseInstructors') IS NOT NULL DROP VIEW VCourseInstructors;
GO

CREATE VIEW VCourseInstructors AS
SELECT 
	C.intInstructorID,
	I.InstructorName,
	C.intCourseID,
	FC.CourseName,
	FC.Description
FROM TCourses AS C
JOIN dbo.fn_GetInstructor() AS I
ON C.intInstructorID = I.intInstructorID

JOIN dbo.fn_GetCourse() AS FC
ON C.intCourseID = FC.intCourseID;
GO

-- --------------------------------------------------------------------------------
-- 9.	Write a Select statement to pull all records from the views (you may use * here)
-- --------------------------------------------------------------------------------
SELECT * FROM VStudentCourses;

SELECT * FROM VCourseBooks;

SELECT * FROM VCourseInstructors;
