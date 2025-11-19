-- --------------------------------------------------------------------------------
-- Name: Keith Brock
-- Class: IT-112
-- Abstract: Final Exam
-- --------------------------------------------------------------------------------
-- 1.	Create a single SQL script with the tables, PK’s, FK’s, inserts for the data
--		provided in the Excel spreadsheet and the following views and stored
--		procedures, at a minimum. (The following views are for you to use in your
--		stored procedures.) 
-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbSQL1;     -- Get out of the master database
SET NOCOUNT ON; -- Report only errors

-- --------------------------------------------------------------------------------
-- Drop Tables
-- --------------------------------------------------------------------------------
IF OBJECT_ID('TDrugKits')            IS NOT NULL DROP TABLE TDrugKits;
IF OBJECT_ID('TVisits')              IS NOT NULL DROP TABLE TVisits;
IF OBJECT_ID('TPatients')            IS NOT NULL DROP TABLE TPatients;
IF OBJECT_ID('TRandomCodes')         IS NOT NULL DROP TABLE TRandomCodes;
IF OBJECT_ID('TSites')               IS NOT NULL DROP TABLE TSites;
IF OBJECT_ID('TWithdrawReasons')     IS NOT NULL DROP TABLE TWithdrawReasons;
IF OBJECT_ID('TGenders')             IS NOT NULL DROP TABLE TGenders;
IF OBJECT_ID('TVisitTypes')          IS NOT NULL DROP TABLE TVisitTypes;
IF OBJECT_ID('TStates')              IS NOT NULL DROP TABLE TStates;
IF OBJECT_ID('TStudies')             IS NOT NULL DROP TABLE TStudies;

-- --------------------------------------------------------------------------------
-- Drop Procedures
-- --------------------------------------------------------------------------------
IF OBJECT_ID('uspAddPatient','P')        IS NOT NULL DROP PROCEDURE uspAddPatient;
IF OBJECT_ID('uspScreenPatient','P')     IS NOT NULL DROP PROCEDURE uspScreenPatient;
IF OBJECT_ID('uspRandomizePatient','P')  IS NOT NULL DROP PROCEDURE uspRandomizePatient;
IF OBJECT_ID('uspWithdrawPatient','P')   IS NOT NULL DROP PROCEDURE uspWithdrawPatient;
IF OBJECT_ID('uspGetRandomCode','P')     IS NOT NULL DROP PROCEDURE uspGetRandomCode;

-- --------------------------------------------------------------------------------
-- Drop Views
-- --------------------------------------------------------------------------------
IF OBJECT_ID('vPatients','V')                IS NOT NULL DROP VIEW vPatients;
IF OBJECT_ID('vRandomizedPatients','V')      IS NOT NULL DROP VIEW vRandomizedPatients;
IF OBJECT_ID('vRandomCodesStudy12345','V')   IS NOT NULL DROP VIEW vRandomCodesStudy12345;
IF OBJECT_ID('vRandomCodesStudy54321','V')   IS NOT NULL DROP VIEW vRandomCodesStudy54321;
IF OBJECT_ID('vRandomView','V')              IS NOT NULL DROP VIEW vRandomView;
IF OBJECT_ID('vATreatments','V')             IS NOT NULL DROP VIEW vATreatments;
IF OBJECT_ID('vPTreatments','V')             IS NOT NULL DROP VIEW vPTreatments;
IF OBJECT_ID('vNextAMin','V')                IS NOT NULL DROP VIEW vNextAMin;
IF OBJECT_ID('vNextPMin','V')                IS NOT NULL DROP VIEW vNextPMin;
IF OBJECT_ID('vAvailableDrug12345','V')      IS NOT NULL DROP VIEW vAvailableDrug12345;
IF OBJECT_ID('vAvailableDrug54321','V')      IS NOT NULL DROP VIEW vAvailableDrug54321;
IF OBJECT_ID('vWithdrawals','V')             IS NOT NULL DROP VIEW vWithdrawals;

-- --------------------------------------------------------------------------------
-- Drop Functions
-- --------------------------------------------------------------------------------
IF OBJECT_ID('fn_GetRandomNumber','FN')   IS NOT NULL DROP FUNCTION fn_GetRandomNumber;
IF OBJECT_ID('fn_GetNextTreatment','FN')  IS NOT NULL DROP FUNCTION fn_GetNextTreatment;

-- --------------------------------------------------------------------------------
-- Create Tables
-- --------------------------------------------------------------------------------
CREATE TABLE TStudies
(
	 intStudyID		INT				NOT NULL
	,strStudyDesc	VARCHAR(50)		NOT NULL
	,CONSTRAINT TStudies_PK PRIMARY KEY ( intStudyID )
)

CREATE TABLE TVisitTypes
(
	 intVisitTypeID	INT				NOT NULL
	,strVisitDesc	VARCHAR(50)		NOT NULL
	,CONSTRAINT TVisitTypes_PK PRIMARY KEY ( intVisitTypeID )
)

CREATE TABLE TStates
(
	 intStateID		INT				NOT NULL
	,strStateDesc	VARCHAR(50)		NOT NULL
	,CONSTRAINT TStates_PK PRIMARY KEY ( intStateID )
)

CREATE TABLE TGenders
(
	 intGenderID	INT				NOT NULL
	,strGender		VARCHAR(20)		NOT NULL
	,CONSTRAINT TGenders_PK PRIMARY KEY ( intGenderID )
)

CREATE TABLE TWithdrawReasons
(
	 intWithdrawReasonID	INT				NOT NULL
	,strWithdrawDesc		VARCHAR(100)	NOT NULL
	,CONSTRAINT TWithdrawReasons_PK PRIMARY KEY ( intWithdrawReasonID )
)

CREATE TABLE TSites
(
	 intSiteID		INT				NOT NULL
	,intSiteNumber	INT				NOT NULL
	,intStudyID		INT				NOT NULL
	,strName		VARCHAR(100)	NOT NULL
	,strAddress		VARCHAR(100)	NOT NULL
	,strCity		VARCHAR(50)		NOT NULL
	,intStateID		INT				NOT NULL
	,strZip			VARCHAR(10)		NOT NULL
	,strPhone		VARCHAR(20)		NOT NULL
	,CONSTRAINT TSites_PK PRIMARY KEY ( intSiteID )
	,CONSTRAINT TSites_FK_Study FOREIGN KEY ( intStudyID ) REFERENCES TStudies ( intStudyID )
	,CONSTRAINT TSites_FK_State FOREIGN KEY ( intStateID ) REFERENCES TStates ( intStateID )
)

CREATE TABLE TRandomCodes
(
	 intRandomCodeID	INT			NOT NULL
	,intRandomCode		INT			NOT NULL
	,intStudyID			INT			NOT NULL
	,strTreatment		CHAR(1)		NOT NULL	-- A or P
	,blnAvailable		VARCHAR(1)	NOT NULL	-- T or F
	,CONSTRAINT TRandomCodes_PK PRIMARY KEY ( intRandomCodeID )
	,CONSTRAINT TRandomCodes_FK_Study FOREIGN KEY ( intStudyID ) REFERENCES TStudies ( intStudyID )
)

CREATE TABLE TDrugKits
(
	 intDrugKitID		INT			NOT NULL
	,intDrugKitNumber	INT			NOT NULL
	,intSiteID			INT			NOT NULL
	,strTreatment		CHAR(1)		NOT NULL	-- A or P
	,intVisitID			INT			NULL		-- Will be linked to TVisits later
	,CONSTRAINT TDrugKits_PK PRIMARY KEY ( intDrugKitID )
	,CONSTRAINT TDrugKits_FK_Site FOREIGN KEY ( intSiteID ) REFERENCES TSites ( intSiteID )
)

CREATE TABLE TPatients
(
	 intPatientID			INT IDENTITY(1,1) NOT NULL
	,intPatientNumber		INT				NOT NULL
	,intSiteID				INT				NOT NULL
	,dtmDOB					DATE			NOT NULL
	,intGenderID			INT				NOT NULL
	,intWeight				INT				NOT NULL
	,intRandomCodeID		INT				NULL
	,CONSTRAINT TPatients_PK PRIMARY KEY ( intPatientID )
)


CREATE TABLE TVisits
(
	 intVisitID				INT IDENTITY(1,1) NOT NULL
	,intPatientID			INT				NOT NULL
	,dtmVisit				DATE			NOT NULL
	,intVisitTypeID			INT				NOT NULL
	,intWithdrawReasonID	INT				NULL
	,CONSTRAINT TVisits_PK PRIMARY KEY ( intVisitID )
)


-- --------------------------------------------------------------------------------
-- Identify and Create Foreign Keys (TPatients & TVisits)
-- --------------------------------------------------------------------------------
--
-- #	Child			Parent				Column(s)
-- -	-----			------				---------
-- 1	TPatients		TSites				intSiteID
-- 2	TPatients		TGenders			intGenderID
-- 3	TPatients		TRandomCodes		intRandomCodeID
-- 4	TVisits			TPatients			intPatientID
-- 5	TVisits			TVisitTypes			intVisitTypeID
-- 6	TVisits			TWithdrawReasons	intWithdrawReasonID

-- 1
ALTER TABLE TPatients ADD CONSTRAINT TPatients_TSites_FK
FOREIGN KEY ( intSiteID ) REFERENCES TSites ( intSiteID )

-- 2
ALTER TABLE TPatients ADD CONSTRAINT TPatients_TGenders_FK
FOREIGN KEY ( intGenderID ) REFERENCES TGenders ( intGenderID )

-- 3
ALTER TABLE TPatients ADD CONSTRAINT TPatients_TRandomCodes_FK
FOREIGN KEY ( intRandomCodeID ) REFERENCES TRandomCodes ( intRandomCodeID )

-- 4
ALTER TABLE TVisits ADD CONSTRAINT TVisits_TPatients_FK
FOREIGN KEY ( intPatientID ) REFERENCES TPatients ( intPatientID )

-- 5
ALTER TABLE TVisits ADD CONSTRAINT TVisits_TVisitTypes_FK
FOREIGN KEY ( intVisitTypeID ) REFERENCES TVisitTypes ( intVisitTypeID )

-- 6
ALTER TABLE TVisits ADD CONSTRAINT TVisits_TWithdrawReasons_FK
FOREIGN KEY ( intWithdrawReasonID ) REFERENCES TWithdrawReasons ( intWithdrawReasonID )

-- --------------------------------------------------------------------------------
-- Insert into TStudies
-- --------------------------------------------------------------------------------
INSERT INTO TStudies ( intStudyID, strStudyDesc )
VALUES
	 ( 1, 'Study 12345' ),
	 ( 2, 'Study 54321' )

-- --------------------------------------------------------------------------------
-- Insert into TVisitTypes
-- --------------------------------------------------------------------------------
INSERT INTO TVisitTypes ( intVisitTypeID, strVisitDesc )
VALUES
	 ( 1, 'Screening' ),
	 ( 2, 'Randomization' ),
	 ( 3, 'Withdrawal' )

-- --------------------------------------------------------------------------------
-- Insert into TStates
-- --------------------------------------------------------------------------------
INSERT INTO TStates ( intStateID, strStateDesc )
VALUES
	 ( 1, 'Ohio' ),
	 ( 2, 'Kentucky' ),
	 ( 3, 'Indiana' ),
	 ( 4, 'New Jersey' ),
	 ( 5, 'Virginia' ),
	 ( 6, 'Georgia' ),
	 ( 7, 'Iowa' )

-- --------------------------------------------------------------------------------
-- Insert into TGenders
-- --------------------------------------------------------------------------------
INSERT INTO TGenders ( intGenderID, strGender )
VALUES
	 ( 1, 'Female' ),
	 ( 2, 'Male' )

-- --------------------------------------------------------------------------------
-- Insert into TWithdrawReasons
-- --------------------------------------------------------------------------------
INSERT INTO TWithdrawReasons ( intWithdrawReasonID, strWithdrawDesc )
VALUES
	 ( 1, 'Patient withdrew consent' ),
	 ( 2, 'Adverse event' ),
	 ( 3, 'Health issue-related to study' ),
	 ( 4, 'Health issue-unrelated to study' ),
	 ( 5, 'Personal reason' ),
	 ( 6, 'Completed the study' )

-- --------------------------------------------------------------------------------
-- Insert into TSites
-- --------------------------------------------------------------------------------
INSERT INTO TSites ( intSiteID, intSiteNumber, intStudyID, strName, strAddress, strCity, intStateID, strZip, strPhone )
VALUES
	 ( 1, 101, 1, 'Dr. Stan Heinrich', '123 E. Main St', 'Atlanta', 6, '25869', '1234567890' ),
	 ( 2, 111, 1, 'Mercy Hospital', '3456 Elmhurst Rd.', 'Secaucus', 4, '32659', '5013629564' ),
	 ( 3, 121, 1, 'St. Elizabeth Hospital', '976 Jackson Way', 'Ft. Thomas', 2, '41258', '3026521478' ),
	 ( 4, 501, 2, 'Dr. Robert Adler', '9087 W. Maple Ave.', 'Cedar Rapids', 7, '42365', '6149652574' ),
	 ( 5, 511, 2, 'Dr. Tim Schmitz', '4539 Helena Run', 'Mason', 1, '45040', '5136987462' ),
	 ( 6, 521, 2, 'Dr. Lawrence Snell', '9201 NW. Washington Blvd.', 'Bristol', 5, '20163', '3876510249' )

-- --------------------------------------------------------------------------------
-- Insert into TRandomCodes
-- --------------------------------------------------------------------------------
INSERT INTO TRandomCodes ( intRandomCodeID, intRandomCode, intStudyID, strTreatment, blnAvailable )
VALUES
	 ( 1, 1000, 1, 'A', 'T' ),
	 ( 2, 1001, 1, 'P', 'T' ),
	 ( 3, 1002, 1, 'A', 'T' ),
	 ( 4, 1003, 1, 'P', 'T' ),
	 ( 5, 1004, 1, 'P', 'T' ),
	 ( 6, 1005, 1, 'A', 'T' ),
	 ( 7, 1006, 1, 'A', 'T' ),
	 ( 8, 1007, 1, 'P', 'T' ),
	 ( 9, 1008, 1, 'A', 'T' ),
	 ( 10, 1009, 1, 'P', 'T' ),
	 ( 11, 1010, 1, 'P', 'T' ),
	 ( 12, 1011, 1, 'A', 'T' ),
	 ( 13, 1012, 1, 'P', 'T' ),
	 ( 14, 1013, 1, 'A', 'T' ),
	 ( 15, 1014, 1, 'A', 'T' ),
	 ( 16, 1015, 1, 'A', 'T' ),
	 ( 17, 1016, 1, 'P', 'T' ),
	 ( 18, 1017, 1, 'P', 'T' ),
	 ( 19, 1018, 1, 'A', 'T' ),
	 ( 20, 1019, 1, 'P', 'T' ),
	 ( 21, 5000, 2, 'A', 'T' ),
	 ( 22, 5001, 2, 'A', 'T' ),
	 ( 23, 5002, 2, 'A', 'T' ),
	 ( 24, 5003, 2, 'A', 'T' ),
	 ( 25, 5004, 2, 'A', 'T' ),
	 ( 26, 5005, 2, 'A', 'T' ),
	 ( 27, 5006, 2, 'A', 'T' ),
	 ( 28, 5007, 2, 'A', 'T' ),
	 ( 29, 5008, 2, 'A', 'T' ),
	 ( 30, 5009, 2, 'A', 'T' ),
	 ( 31, 5010, 2, 'P', 'T' ),
	 ( 32, 5011, 2, 'P', 'T' ),
	 ( 33, 5012, 2, 'P', 'T' ),
	 ( 34, 5013, 2, 'P', 'T' ),
	 ( 35, 5014, 2, 'P', 'T' ),
	 ( 36, 5015, 2, 'P', 'T' ),
	 ( 37, 5016, 2, 'P', 'T' ),
	 ( 38, 5017, 2, 'P', 'T' ),
	 ( 39, 5018, 2, 'P', 'T' ),
	 ( 40, 5019, 2, 'P', 'T' )

-- --------------------------------------------------------------------------------
-- Insert into TDrugKits
-- --------------------------------------------------------------------------------
INSERT INTO TDrugKits ( intDrugKitID, intDrugKitNumber, intSiteID, strTreatment, intVisitID )
VALUES
	 ( 1, 10000, 1, 'A', NULL ),
	 ( 2, 10001, 1, 'A', NULL ),
	 ( 3, 10002, 1, 'A', NULL ),
	 ( 4, 10003, 1, 'P', NULL ),
	 ( 5, 10004, 1, 'P', NULL ),
	 ( 6, 10005, 1, 'P', NULL ),
	 ( 7, 10006, 2, 'A', NULL ),
	 ( 8, 10007, 2, 'A', NULL ),
	 ( 9, 10008, 2, 'A', NULL ),
	 ( 10, 10009, 2, 'P', NULL ),
	 ( 11, 10010, 2, 'P', NULL ),
	 ( 12, 10011, 2, 'P', NULL ),
	 ( 13, 10012, 3, 'A', NULL ),
	 ( 14, 10013, 3, 'A', NULL ),
	 ( 15, 10014, 3, 'A', NULL ),
	 ( 16, 10015, 3, 'P', NULL ),
	 ( 17, 10016, 3, 'P', NULL ),
	 ( 18, 10017, 3, 'P', NULL ),
	 ( 19, 10018, 4, 'A', NULL ),
	 ( 20, 10019, 4, 'A', NULL ),
	 ( 21, 10020, 4, 'A', NULL ),
	 ( 22, 10021, 4, 'P', NULL ),
	 ( 23, 10022, 4, 'P', NULL ),
	 ( 24, 10023, 4, 'P', NULL ),
	 ( 25, 10024, 5, 'A', NULL ),
	 ( 26, 10025, 5, 'A', NULL ),
	 ( 27, 10026, 5, 'A', NULL ),
	 ( 28, 10027, 5, 'P', NULL ),
	 ( 29, 10028, 5, 'P', NULL ),
	 ( 30, 10029, 5, 'P', NULL ),
	 ( 31, 10030, 6, 'A', NULL ),
	 ( 32, 10031, 6, 'A', NULL ),
	 ( 33, 10032, 6, 'A', NULL ),
	 ( 34, 10033, 6, 'P', NULL ),
	 ( 35, 10034, 6, 'P', NULL ),
	 ( 36, 10035, 6, 'P', NULL )

-- --------------------------------------------------------------------------------
-- 2.	Create the view that will show all patients at all sites for both studies.
--		You can do this together or 1 view for each study.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('vPatients','V') IS NOT NULL DROP VIEW vPatients;
GO


-- (2) vPatients: all patients at all sites for both studies
CREATE VIEW vPatients AS
SELECT 
     TP.intPatientID,
     TP.intPatientNumber,
     TP.dtmDOB,
     TP.intWeight,
     TG.strGender,
     TS.intSiteNumber,
     TS.strName        AS strSiteName,
     TS.strCity,
     TST.strStateDesc  AS strState,
     TS.strPhone,
     TSTU.strStudyDesc AS strStudy
FROM TPatients AS TP
JOIN TGenders  AS TG   ON TP.intGenderID = TG.intGenderID
JOIN TSites    AS TS   ON TP.intSiteID     = TS.intSiteID
JOIN TStates   AS TST  ON TS.intStateID    = TST.intStateID
JOIN TStudies  AS TSTU ON TS.intStudyID    = TSTU.intStudyID;
GO


-- --------------------------------------------------------------------------------
-- 3.	Create the view that will show all randomized patients, their site and
--		their treatment for both studies. You can do this together or 1 view
--		for each study.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('VRandomizedPatients', 'V') IS NOT NULL DROP VIEW VRandomizedPatients;
GO

CREATE VIEW VRandomizedPatients AS
SELECT 
	 TP.intPatientID,
	 TP.intPatientNumber,
	 TS.intSiteNumber,
	 TS.strName			AS strSiteName,
	 TSTU.strStudyDesc	AS strStudy,
	 TRC.intRandomCode,
	 TRC.strTreatment
FROM TPatients AS TP
JOIN TRandomCodes	AS TRC  ON TP.intRandomCodeID = TRC.intRandomCodeID
JOIN TSites			AS TS   ON TP.intSiteID       = TS.intSiteID
JOIN TStudies		AS TSTU ON TS.intStudyID      = TSTU.intStudyID
WHERE TP.intRandomCodeID IS NOT NULL;
GO

-- --------------------------------------------------------------------------------
-- 4.	Create the view that will show the next available random codes (MIN) 
--		for both studies. This view shows the next code for Study 12345.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('vRandomCodesStudy12345','V') IS NOT NULL
  DROP VIEW vRandomCodesStudy12345;
GO

CREATE VIEW vRandomCodesStudy12345 AS
SELECT 
     MIN(intRandomCode) AS intNextAvailableRandomCode
FROM TRandomCodes
WHERE intStudyID   = 1
  AND blnAvailable = 'T';
GO

-- --------------------------------------------------------------------------------
-- 4.	Create the view that will show the next available random codes (MIN) 
--		for both studies. This view shows the next code for each treatment 
--		in Study 54321.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('vRandomCodesStudy54321','V') IS NOT NULL
  DROP VIEW vRandomCodesStudy54321;
GO

CREATE VIEW vRandomCodesStudy54321 AS
SELECT 
     strTreatment,
     MIN(intRandomCode) AS intNextAvailableRandomCode
FROM TRandomCodes
WHERE intStudyID   = 2
  AND blnAvailable = 'T'
GROUP BY strTreatment;
GO

-- --------------------------------------------------------------------------------
-- 5a.	Create the view that will show all available drug at all sites for 
--		both studies. You can do this together or 1 view for each study.
-- ----------------------------------------------------------------------------
IF OBJECT_ID('vAvailableDrug12345','V') IS NOT NULL DROP VIEW vAvailableDrug12345;
GO

CREATE VIEW vAvailableDrug12345 AS
SELECT
     DK.intDrugKitID,
     DK.intDrugKitNumber,
     DK.intSiteID,
     DK.strTreatment,
     DK.intVisitID
FROM TDrugKits AS DK
JOIN TSites     AS S  ON DK.intSiteID = S.intSiteID
WHERE DK.intVisitID IS NULL
  AND S.intStudyID = 1;
GO


-- --------------------------------------------------------------------------------
-- 5b.	Create the view that will show all available drug at all sites for 
--		both studies. You can do this together or 1 view for each study.
-- ----------------------------------------------------------------------------
IF OBJECT_ID('vAvailableDrug54321','V') IS NOT NULL DROP VIEW vAvailableDrug54321;
GO

CREATE VIEW vAvailableDrug54321 AS
SELECT
     DK.intDrugKitID,
     DK.intDrugKitNumber,
     DK.intSiteID,
     DK.strTreatment,
     DK.intVisitID
FROM TDrugKits AS DK
JOIN TSites     AS S  ON DK.intSiteID = S.intSiteID
WHERE DK.intVisitID IS NULL
  AND S.intStudyID = 2;
GO


-- --------------------------------------------------------------------------------
-- 6.	Create the view that will show all withdrawn patients, their site, 
--		withdrawal date and withdrawal reason for both studies.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('vWithdrawals','V') IS NOT NULL DROP VIEW vWithdrawals;
GO

CREATE VIEW vWithdrawals AS
SELECT 
     TP.intPatientID,
     TP.intPatientNumber,
     TS.intSiteNumber,
     TS.strName           AS strSiteName,
     TSTU.strStudyDesc    AS strStudy,
     TV.dtmVisit          AS dtmWithdrawDate,
     TWR.strWithdrawDesc  AS strWithdrawReason
FROM TVisits AS TV
JOIN TPatients           AS TP   ON TV.intPatientID         = TP.intPatientID
JOIN TSites              AS TS   ON TP.intSiteID             = TS.intSiteID
JOIN TStudies            AS TSTU ON TS.intStudyID            = TSTU.intStudyID
JOIN TWithdrawReasons    AS TWR  ON TV.intWithdrawReasonID   = TWR.intWithdrawReasonID
WHERE TV.intVisitTypeID = 3;  -- Withdrawal
GO

-- --------------------------------------------------------------------------------
-- 7.	Create other views and functions as needed. Put as much as possible 
--		into views and functions so you are pulling from them instead of from tables.
-- --------------------------------------------------------------------------------
-- Drop VPatientStudy if it exists
IF OBJECT_ID('VPatientStudy', 'V') IS NOT NULL DROP VIEW VPatientStudy;
GO

-- Create VPatientStudy
CREATE VIEW VPatientStudy AS
SELECT 
	 TP.intPatientID,
	 TSTU.intStudyID,
	 TSTU.strStudyDesc
FROM TPatients	AS TP
JOIN TSites		AS TS   ON TP.intSiteID	 = TS.intSiteID
JOIN TStudies	AS TSTU ON TS.intStudyID = TSTU.intStudyID;
GO


-- Drop fnFormatPatientName if it exists
IF OBJECT_ID('fnFormatPatientName', 'FN') IS NOT NULL DROP FUNCTION fnFormatPatientName;
GO

-- Create fnFormatPatientName
CREATE FUNCTION fnFormatPatientName
(
	@intPatientID INT
)
RETURNS VARCHAR(100)
AS
BEGIN
	DECLARE @strName VARCHAR(100);

	SELECT @strName = 'Patient #' + CAST(intPatientNumber AS VARCHAR)
	FROM TPatients
	WHERE intPatientID = @intPatientID;

	RETURN @strName;
END;
GO


-- Drop VPatientsScreenedNotRandomized if it exists
IF OBJECT_ID('VPatientsScreenedNotRandomized', 'V') IS NOT NULL	DROP VIEW VPatientsScreenedNotRandomized;
GO

-- Create VPatientsScreenedNotRandomized
CREATE VIEW VPatientsScreenedNotRandomized AS
SELECT 
	 TP.intPatientID,
	 TP.intPatientNumber,
	 TS.intSiteNumber,
	 TSTU.strStudyDesc
FROM TPatients	AS TP
JOIN TSites		AS TS   ON TP.intSiteID		= TS.intSiteID
JOIN TStudies	AS TSTU ON TS.intStudyID	= TSTU.intStudyID
WHERE TP.intRandomCodeID IS NULL;
GO

--------------------------------------------------------------------------------
-- vATreatments: available “A” codes
--------------------------------------------------------------------------------
IF OBJECT_ID('vATreatments', 'V') IS NOT NULL DROP VIEW vATreatments;
GO

CREATE VIEW vATreatments AS
SELECT
     intRandomCodeID,
     intRandomCode,
     intStudyID,
     strTreatment,
     blnAvailable
FROM TRandomCodes
WHERE strTreatment = 'A'
  AND blnAvailable = 'T';
GO



--------------------------------------------------------------------------------
-- vPTreatments: available “P” codes
--------------------------------------------------------------------------------
IF OBJECT_ID('vPTreatments', 'V') IS NOT NULL DROP VIEW vPTreatments;
GO

CREATE VIEW vPTreatments AS
SELECT
     intRandomCodeID,
     intRandomCode,
     intStudyID,
     strTreatment,
     blnAvailable
FROM TRandomCodes
WHERE strTreatment = 'P'
  AND blnAvailable = 'T';
GO



--------------------------------------------------------------------------------
-- vNextAMin: next available “A” code
--------------------------------------------------------------------------------
IF OBJECT_ID('vNextAMin', 'V') IS NOT NULL DROP VIEW vNextAMin;
GO

CREATE VIEW vNextAMin AS
SELECT
     intStudyID,
     MIN(intRandomCode) AS intNextAvailableRandomCode
FROM TRandomCodes
WHERE strTreatment = 'A'
  AND blnAvailable = 'T'
GROUP BY intStudyID;
GO



--------------------------------------------------------------------------------
-- vNextPMin: next available “P” code
--------------------------------------------------------------------------------
IF OBJECT_ID('vNextPMin', 'V') IS NOT NULL DROP VIEW vNextPMin;
GO

CREATE VIEW vNextPMin AS
SELECT
     intStudyID,
     MIN(intRandomCode) AS intNextAvailableRandomCode
FROM TRandomCodes
WHERE strTreatment = 'P'
  AND blnAvailable = 'T'
GROUP BY intStudyID;
GO



--------------------------------------------------------------------------------
-- vRandomView: combined “next available” for Study 12345 & 54321
--------------------------------------------------------------------------------
IF OBJECT_ID('vRandomView','V') IS NOT NULL DROP VIEW vRandomView;
GO

CREATE VIEW vRandomView AS
SELECT 
     1                            AS intStudyID,
     NULL                         AS strTreatment,
     MIN(intRandomCode)           AS intNextAvailableRandomCode
FROM TRandomCodes
WHERE intStudyID   = 1
  AND blnAvailable = 'T'
UNION ALL
SELECT 
     2                            AS intStudyID,
     strTreatment,
     MIN(intRandomCode)
FROM TRandomCodes
WHERE intStudyID   = 2
  AND blnAvailable = 'T'
GROUP BY strTreatment;
GO

-- --------------------------------------------------------------------------------
-- uspAddPatient
-- --------------------------------------------------------------------------------
IF OBJECT_ID('uspAddPatient','P') IS NOT NULL DROP PROCEDURE uspAddPatient;
GO

CREATE PROCEDURE uspAddPatient
    @intSiteID      INT,
    @dtmDOB         DATE,
    @intGenderID    INT,
    @intWeight      INT,
    @dtmVisitDate   DATE,
    @intPatientID   INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @intPatientNumber INT,
        @intSiteNumber    INT;

    -- 1) Lookup the site’s display number
    SELECT @intSiteNumber = intSiteNumber
      FROM TSites
     WHERE intSiteID = @intSiteID;

    -- 2) Determine next patient number for that site
    SELECT @intPatientNumber = 
           ISNULL(MAX(intPatientNumber), @intSiteNumber * 1000)
      FROM TPatients
     WHERE intSiteID = @intSiteID;

    SET @intPatientNumber = @intPatientNumber + 1;

    -- 3) Insert new patient
    INSERT INTO TPatients (
         intPatientNumber,
         intSiteID,
         dtmDOB,
         intGenderID,
         intWeight,
         intRandomCodeID
    )
    VALUES (
         @intPatientNumber,
         @intSiteID,
         @dtmDOB,
         @intGenderID,
         @intWeight,
         NULL
    );

    -- 4) Capture the new surrogate key
    SET @intPatientID = SCOPE_IDENTITY();

    -- 5) Log the screening visit
    INSERT INTO TVisits (
         intPatientID,
         dtmVisit,
         intVisitTypeID,
         intWithdrawReasonID
    )
    VALUES (
         @intPatientID,
         @dtmVisitDate,
         1,   -- Screening
         NULL
    );
END;
GO


-- --------------------------------------------------------------------------------
--	8.	Create the stored procedure(s) that will screen a patient for both studies.
--		You can do this together or 1 for each study.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('uspScreenPatient', 'P') IS NOT NULL
	DROP PROCEDURE uspScreenPatient;
GO

CREATE PROCEDURE uspScreenPatient
	 @intSiteID			INT
	,@dtmDOB			DATE
	,@intGenderID		INT
	,@intWeight			INT
	,@dtmVisitDate		DATE
	,@intPatientID		INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE 
		 @intPatientNumber	INT,
		 @intSiteNumber		INT;

	-- Get the SiteNumber for the site
	SELECT @intSiteNumber = intSiteNumber
	FROM TSites
	WHERE intSiteID = @intSiteID;

	-- Determine next available patient number
	SELECT @intPatientNumber = ISNULL(MAX(intPatientNumber), @intSiteNumber * 1000)
	FROM TPatients
	WHERE intSiteID = @intSiteID;

	-- Increment for the new patient
	SET @intPatientNumber = @intPatientNumber + 1;

	-- Insert patient into TPatients
	INSERT INTO TPatients (
		 intPatientNumber,
		 intSiteID,
		 dtmDOB,
		 intGenderID,
		 intWeight,
		 intRandomCodeID
	)
	VALUES (
		 @intPatientNumber,
		 @intSiteID,
		 @dtmDOB,
		 @intGenderID,
		 @intWeight,
		 NULL
	);

	-- Capture new patient ID
	SET @intPatientID = SCOPE_IDENTITY();

	-- Insert screening visit into TVisits
	INSERT INTO TVisits (
		 intPatientID,
		 dtmVisit,
		 intVisitTypeID,
		 intWithdrawReasonID
	)
	VALUES (
		 @intPatientID,
		 @dtmVisitDate,
		 1,  -- Screening
		 NULL
	);
END
GO
-- --------------------------------------------------------------------------------
--	9.	Create the stored procedure(s) that will withdraw a patient for both
--		studies. You can do this together or 1 for each study. Remember a patient
--		can go from Screening Visit to Withdrawal without being randomized. This
--		will be up to the Doctor. Your code just has to be able to do it.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('uspWithdrawPatient','P') IS NOT NULL
    DROP PROCEDURE uspWithdrawPatient;
GO

CREATE PROCEDURE uspWithdrawPatient
    @intPatientID        INT,
    @dtmWithdrawDate     DATE,
    @intWithdrawReasonID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if patient exists
    IF NOT EXISTS (
        SELECT 1
          FROM TPatients
         WHERE intPatientID = @intPatientID
    )
    BEGIN
        RAISERROR('Patient ID not found.', 16, 1);
        RETURN;
    END

    -- Get latest visit date to ensure withdrawal is not before other visits
    DECLARE @dtmLastVisit DATE;
    SELECT @dtmLastVisit = MAX(dtmVisit)
      FROM TVisits
     WHERE intPatientID = @intPatientID;

    -- Optional: enforce that withdrawal date comes after last visit
    IF @dtmLastVisit IS NOT NULL
       AND @dtmWithdrawDate < @dtmLastVisit
    BEGIN
        RAISERROR('Withdrawal date cannot be before last recorded visit.', 16, 1);
        RETURN;
    END

    -- Insert withdrawal visit
    INSERT INTO TVisits (
        intPatientID,
        dtmVisit,
        intVisitTypeID,
        intWithdrawReasonID
    )
    VALUES (
        @intPatientID,
        @dtmWithdrawDate,
        3,  -- Withdrawal
        @intWithdrawReasonID
    );
END
GO


-- --------------------------------------------------------------------------------
-- 10.	Create the stored procedure that will randomize a patient for both studies.
--		Includes logic to select the correct random code and assign a drug kit.
-- --------------------------------------------------------------------------------
IF OBJECT_ID('uspRandomizePatient', 'P') IS NOT NULL
    DROP PROCEDURE uspRandomizePatient;
GO

CREATE PROCEDURE uspRandomizePatient
    @intStudyID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @intPatientID     INT,
        @intSiteID        INT,
        @strTreatment     CHAR(1),
        @intRandomCodeID  INT,
        @intVisitID       INT,
        @intDrugKitID     INT;

    -- Randomly select an eligible patient from the given study
    SELECT TOP 1 @intPatientID = P.intPatientID
    FROM TPatients P
    JOIN TSites S ON P.intSiteID = S.intSiteID
    LEFT JOIN TVisits V ON P.intPatientID = V.intPatientID AND V.intVisitTypeID = 3 -- withdrawal
    WHERE S.intStudyID = @intStudyID
      AND P.intRandomCodeID IS NULL
      AND V.intVisitID IS NULL
    ORDER BY NEWID();

    IF @intPatientID IS NULL
    BEGIN
        RAISERROR('No eligible patient found for study %d.', 16, 1, @intStudyID);
        RETURN;
    END

    -- Get site for patient
    SELECT @intSiteID = intSiteID
    FROM TPatients
    WHERE intPatientID = @intPatientID;

    -- Pick a random code based on study logic
    IF @intStudyID = 1
    BEGIN
        -- Sequential for Study 12345
        SELECT TOP 1 
            @intRandomCodeID = RC.intRandomCodeID,
            @strTreatment    = RC.strTreatment
        FROM TRandomCodes RC
        WHERE RC.intStudyID = 1 AND RC.blnAvailable = 'T'
        ORDER BY RC.intRandomCode;
    END
    ELSE
    BEGIN
        -- Balanced A vs P for Study 54321 or others
        DECLARE @intCountA INT, @intCountP INT, @charPick CHAR(1);

        SELECT @intCountA = COUNT(*)
        FROM TPatients P
        JOIN TRandomCodes RC ON P.intRandomCodeID = RC.intRandomCodeID
        WHERE RC.intStudyID = @intStudyID AND RC.strTreatment = 'A';

        SELECT @intCountP = COUNT(*)
        FROM TPatients P
        JOIN TRandomCodes RC ON P.intRandomCodeID = RC.intRandomCodeID
        WHERE RC.intStudyID = @intStudyID AND RC.strTreatment = 'P';

        IF @intCountA - @intCountP >= 2
            SET @charPick = 'P';
        ELSE IF @intCountP - @intCountA >= 2
            SET @charPick = 'A';
        ELSE
            SET @charPick = CASE WHEN RAND() < 0.5 THEN 'A' ELSE 'P' END;

        SELECT TOP 1 
            @intRandomCodeID = RC.intRandomCodeID,
            @strTreatment    = RC.strTreatment
        FROM TRandomCodes RC
        WHERE RC.intStudyID = @intStudyID
          AND RC.blnAvailable = 'T'
          AND RC.strTreatment = @charPick
        ORDER BY RC.intRandomCode;
    END

    IF @intRandomCodeID IS NULL
    BEGIN
        RAISERROR('No available random code for study %d.', 16, 1, @intStudyID);
        RETURN;
    END

    -- Assign code and mark it unavailable
    UPDATE TPatients
    SET intRandomCodeID = @intRandomCodeID
    WHERE intPatientID = @intPatientID;

    UPDATE TRandomCodes
    SET blnAvailable = 'F'
    WHERE intRandomCodeID = @intRandomCodeID;

    -- Log randomization visit
    INSERT INTO TVisits (intPatientID, dtmVisit, intVisitTypeID, intWithdrawReasonID)
    VALUES (@intPatientID, GETDATE(), 2, NULL);

    SET @intVisitID = SCOPE_IDENTITY();

    -- Step 6: Assign drug kit
    SELECT TOP 1
        @intDrugKitID = DK.intDrugKitID
    FROM TDrugKits DK
    WHERE DK.intSiteID = @intSiteID
      AND DK.strTreatment = @strTreatment
      AND DK.intVisitID IS NULL
    ORDER BY DK.intDrugKitNumber;

    IF @intDrugKitID IS NULL
    BEGIN
        RAISERROR('No drug kit available for patient %d.', 16, 1, @intPatientID);
        RETURN;
    END

    UPDATE TDrugKits
    SET intVisitID = @intVisitID
    WHERE intDrugKitID = @intDrugKitID;

    PRINT 'Randomized Patient ID: ' + CAST(@intPatientID AS VARCHAR);
END
GO
