
-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbDBMSCollege;     -- Get out of the master database
SET NOCOUNT ON; -- Report only errors

-- --------------------------------------------------------------------------------
--						Problem #1
-- --------------------------------------------------------------------------------

-- Drop Table Statements

IF OBJECT_ID ('TStudentCourses')		IS NOT NULL DROP TABLE TStudentCourses
IF OBJECT_ID ('TStudents')				IS NOT NULL DROP TABLE TStudents
IF OBJECT_ID ('TCourses')				IS NOT NULL DROP TABLE TCourses
IF OBJECT_ID ('TInstructors')			IS NOT NULL DROP TABLE TInstructors



-- --------------------------------------------------------------------------------
--	Step #1 : Create table 
-- --------------------------------------------------------------------------------

CREATE TABLE TStudents
(
	 intStudentID		INTEGER			NOT NULL
	,strFirstName		VARCHAR(25)		NOT NULL
	,strLastName		VARCHAR(25)		NOT NULL
	,strAddress			VARCHAR(25)		NOT NULL
	,strCity			VARCHAR(25)		NOT NULL
	,strState			VARCHAR(25)		NOT NULL
	,strZip				VARCHAR(25)		NOT NULL
	,dtmDateOfBirth		DATETIME		NOT NULL
	,strRace			VARCHAR(25)		NOT NULL
	,strSSN				VARCHAR(25)		NOT NULL
	,CONSTRAINT TStudents_PK PRIMARY KEY ( intStudentID )
)

CREATE TABLE TCourses
(
	 intCourseID		INTEGER			NOT NULL
	,intInstructorID	INTEGER			NOT NULL
	,strCourseName		VARCHAR(50)		NOT NULL
	,strCourseNumber	VARCHAR(50)		NOT NULL
	,dtmStatDate		DATETIME		NOT NULL
	,CONSTRAINT TCourses_PK PRIMARY KEY ( intCourseID )
)

CREATE TABLE TStudentCourses
(
	 intStudentCourseID	INTEGER			NOT NULL
	,intStudentID		INTEGER			NOT NULL
	,intCourseID		INTEGER			NOT NULL
	,CONSTRAINT TStudentCourses_PK PRIMARY KEY ( intStudentCourseID )
)

CREATE TABLE TInstructors
(
	 intInstructorID	INTEGER			NOT NULL
	,strFirstName		VARCHAR(25)		NOT NULL
	,strLastName		VARCHAR(25)		NOT NULL
	,strAddress			VARCHAR(25)		NOT NULL
	,strCity			VARCHAR(25)		NOT NULL
	,strState			VARCHAR(25)		NOT NULL
	,strZip				VARCHAR(25)		NOT NULL
	,dtmDateOfHire		DATETIME		NOT NULL
	,CONSTRAINT TInstructors_PK PRIMARY KEY ( intInstructorID )
)

-- --------------------------------------------------------------------------------
--	Step #2 : Establish Referential Integrity 
-- --------------------------------------------------------------------------------
--
-- #	Child							Parent						Column
-- -	-----							------						---------
-- 1	TCourses						TInstructors				intInstructorID	
-- 2	TStudentCourses					TStudents					intStudentID
-- 3	TStudentCourses					TCourses					intCourseID

--1
ALTER TABLE TCourses ADD CONSTRAINT TCourses_TInstructors_FK 
FOREIGN KEY ( intInstructorID ) REFERENCES TInstructors ( intInstructorID )

--2
ALTER TABLE TStudentCourses	 ADD CONSTRAINT TStudentCourses_TStudents_FK 
FOREIGN KEY ( intStudentID ) REFERENCES TStudents ( intStudentID )

--3
ALTER TABLE TStudentCourses	 ADD CONSTRAINT TStudentCourses_TCourses_FK 
FOREIGN KEY ( intCourseID ) REFERENCES TCourses ( intCourseID )


-- --------------------------------------------------------------------------------
--	Step #3 : Add Sample Data - INSERTS
-- --------------------------------------------------------------------------------

INSERT INTO TStudents (intStudentID, strFirstName, strLastName, strAddress, strCity, strState, strZip, dtmDateOfBirth, strRace, strSSN)
VALUES				  (1, 'James', 'Jones', '321 Elm St.', 'Cincinnati', 'Oh', '45201', '1/1/1997', 'Hispanic', '123-45-6789')
					 ,(2, 'Sally', 'Smith', '987 Main St.', 'Norwood', 'Oh', '45218', '12/1/1999', 'African-American', '987-65-4321')
					 ,(3, 'Jose', 'Hernandez', '1569 Windisch Rd.', 'West Chester', 'Oh', '45069', '9/23/1998', 'Hispanic', '315-56-4020')
					 ,(4, 'Lan', 'Kim', '44561 Oak Ave.', 'Milford', 'Oh', '45246', '6/11/1999', 'Asian', '589-28-4526')

INSERT INTO TInstructors( intInstructorID, strFirstName, strLastName, strAddress, strCity, strState, strZip, dtmDateOfHire)
VALUES				  (1, 'Bob', 'Nields', '4563 RR8', 'Covington', 'Ky', '44034','6/6/2002')
					 ,(2, 'Tomie', 'Gartland', '12 Nuance Way', 'Maineville', 'Oh', '45040','8/11/2014')
					 ,(3, 'Jim', 'Smith', '1694 Washington Ave.', 'Florence', 'Ky', '44122','1/3/2011')

INSERT INTO TCourses ( intCourseID, intInstructorID, strCourseName, strCourseNumber, dtmStatDate)
VALUES				 ( 1, 2, '.Net Programming', 'IT-101', '8/28/2017')
					,( 2, 1, '.Net Programming 2', 'IT-102', '8/28/2017')
					,( 3, 3, 'Math -  Calculus', 'MAT-161', '1/8/2018')
					,( 4, 2, 'Database Design and SQL', 'IT-111', '1/8/2018')

INSERT INTO TStudentCourses ( intStudentCourseID, intStudentID, intCourseID )
VALUES				 ( 1, 1, 1)
					,( 2, 2, 1)
					,( 3, 1, 3)
					,( 4, 3, 4)
					,( 5, 3, 1)
					,( 6, 3, 2)




