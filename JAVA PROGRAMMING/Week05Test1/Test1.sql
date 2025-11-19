use dbHCM
SET NOCOUNT ON;

IF OBJECT_ID('TStudents')	IS NOT NULL		DROP TABLE TStudents
IF OBJECT_ID('TMajors')		IS NOT NULL		DROP TABLE TMajors


CREATE TABLE TStudents
(
		 intStudentID			INTEGER			NOT NULL
		,strFirstName			VARCHAR(50)		NOT NULL
		,strLastName			VARCHAR(50)		NOT NULL
		,strAddress				VARCHAR(50)		NOT NULL
		,strCity				VARCHAR(50)		NOT NULL
		,strState				VARCHAR(50)		NOT NULL
		,strZip					VARCHAR(50)		NOT NULL
		,strPhoneNumber			VARCHAR(50)		NOT NULL
		,strEmail				VARCHAR(50)		NOT NULL
		,CONSTRAINT TStudents_PK PRIMARY KEY ( intStudentID )
)


INSERT INTO TStudents (intStudentID, strFirstName, strLastName, strAddress, strCity, strState, strZip, strPhoneNumber, strEmail )
VALUES	
		 ( 1, 'Jack', 'Spratt', '101 Main St.', 'Cincinnati', 'OH', '45251', '5135551212', 'jspratt@email.com')
		,( 2, 'Jill', 'Hill', '12345 Rapid Run', 'Cincinnati', 'OH', '45211', '5135251010', 'jill.hill@email.com')


CREATE TABLE TMajors
(
		 intMajorID					INTEGER				NOT NULL
		,strMajor					varchar(255)		NOT NULL
		,strDescription  			varchar(255)		NOT NULL
		,CONSTRAINT TMajors_PK PRIMARY KEY (intMajorID)
)

INSERT INTO TMajors(intMajorID, strMajor, strDescription)
VALUES	
		 (1, 'ACCT', 'Accounting')
		,(2, 'IT', 'Information Technology')
		,(3, 'MAT', 'Math')
				   
select * from TStudents

select * from TMajors