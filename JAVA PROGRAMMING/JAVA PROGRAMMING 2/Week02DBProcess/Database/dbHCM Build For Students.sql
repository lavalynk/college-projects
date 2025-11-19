-- --------------------------------------------------------------------------------
-- Name:		TLG
-- Class:		IT-161 Java 1
-- Abstract:	DB Intro
-- --------------------------------------------------------------------------------

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbHCM		            -- Don't work in master
SET NOCOUNT ON				-- Report only errors

-- ----------------------------------------------------------------------
-- Drops
-- ----------------------------------------------------------------------
IF OBJECT_ID( 'TEmployees' )						IS NOT NULL		DROP TABLE TEmployees

GO

-- ----------------------------------------------------------------------
-- Tables
-- ----------------------------------------------------------------------
CREATE TABLE TEmployees
(
	intEmployeeID                    INTEGER        NOT NULL,
	strFirstName                     VARCHAR(50)    NOT NULL,
	strLastName                      VARCHAR(50)    NOT NULL,
	strHomePhoneNumber               VARCHAR(50)    NOT NULL,
	dtmDateOfBirth                   DATETIME       NOT NULL,
	strEmailAddress                  VARCHAR(50)    NOT NULL,
	CONSTRAINT TEmployees_PK PRIMARY KEY CLUSTERED ( intEmployeeID ))

-- Employees
INSERT INTO TEmployees( intEmployeeID, strFirstName, strLastName, strHomePhoneNumber, dtmDateOfBirth,  strEmailAddress )
VALUES ( 1, 'Jill', 'Hill', '', '1/2/1992', 'Jill.Hill@Hill.com')

INSERT INTO TEmployees( intEmployeeID, strFirstName, strLastName, strHomePhoneNumber, dtmDateOfBirth,  strEmailAddress )
VALUES( 2, 'Jack', 'Hill', '513-555-1212', '3/4/1995', 'Jack@Hill.com' )

INSERT INTO TEmployees( intEmployeeID, strFirstName, strLastName, strHomePhoneNumber, dtmDateOfBirth,  strEmailAddress )
VALUES( 3, 'Jerry', 'Shaw', '513-705-7878', '1/1/1997', 'Jerry@SomeCo.com' )

GO

-- ----------------------------------------------------------------------
-- Testing
-- ----------------------------------------------------------------------
/*
-- Data
SELECT * FROM TEmployees
*/



SELECT * FROM TEmployees order by strFirstName