-- ========================================================================================
-- IT-112 – Homework 2 – Stored Procedures and Views
-- Keith Brock
-- ========================================================================================

USE dbSQL1;
SET NOCOUNT ON;

IF OBJECT_ID('TCustomerJobs') IS NOT NULL DROP TABLE TCustomerJobs;
IF OBJECT_ID('TJobs')         IS NOT NULL DROP TABLE TJobs;
IF OBJECT_ID('TCustomers')    IS NOT NULL DROP TABLE TCustomers;

-- --------------------------------------------------------------------------------
-- CREATE TABLES
-- --------------------------------------------------------------------------------
CREATE TABLE TCustomers
(
    intCustomerID INT           NOT NULL,
    strFirstName	VARCHAR(50)   NOT NULL,
    strLastName		VARCHAR(50)   NOT NULL,
    strPhoneNumber	VARCHAR(20),
    strEmail		VARCHAR(100),
    CONSTRAINT TCustomers_PK PRIMARY KEY (intCustomerID)
);

CREATE TABLE TJobs
(
    intJobID       INT           NOT NULL,
    strDescription VARCHAR(250)  NOT NULL,
    dtStartDate    DATE          NOT NULL,
    dtEndDate      DATE,
    CONSTRAINT TJobs_PK PRIMARY KEY (intJobID)
);

CREATE TABLE TCustomerJobs
(
    intCustomerJobID INT         NOT NULL,
    intCustomerID    INT         NOT NULL,
    intJobID         INT         NOT NULL,
    CONSTRAINT CustomerJobs_UQ UNIQUE (intCustomerID, intJobID),
    CONSTRAINT TCustomerJobs_PK PRIMARY KEY (intCustomerJobID)
);

-- --------------------------------------------------------------------------------
-- FK TABLE
--
-- Child           Parent        Column(s)
-- --------------- ------------- ------------
-- TCustomerJobs   TCustomers    intCustomerID
-- TCustomerJobs   TJobs         intJobID
-- --------------------------------------------------------------------------------


-- --------------------------------------------------------------------------------
-- Create Foreign Keys.
-- --------------------------------------------------------------------------------
ALTER TABLE TCustomerJobs ADD CONSTRAINT TCustomerJobs_TCustomers_FK
FOREIGN KEY (intCustomerID) REFERENCES TCustomers(intCustomerID);

ALTER TABLE TCustomerJobs ADD CONSTRAINT TCustomerJobs_TJobs_FK
FOREIGN KEY (intJobID) REFERENCES TJobs(intJobID);

-- --------------------------------------------------------------------------------
-- INSERT DATA INTO TABLES
-- --------------------------------------------------------------------------------
INSERT INTO TCustomers (intCustomerID, strFirstName, strLastName, strPhoneNumber, strEmail)
VALUES
    (1, 'Justin', 'Timberlake', '513-432-4299', 'jtimberlake@nsyncmail.com'),
    (2, 'JC',      'Chasez',      '513-324-2293', 'jcchasez@nsyncmail.com'),
    (3, 'Lance',   'Bass',        '513-614-1658', 'lbass@nsyncmail.com');

INSERT INTO TJobs (intJobID, strDescription, dtStartDate, dtEndDate)
VALUES
    (1, 'Excavate basement foundation',               '2024-01-03', '2024-01-28'),
    (2, 'Electrical rewiring for commercial building', '2024-02-14', NULL),
    (3, 'HVAC system replacement',                    '2024-01-22', '2024-02-05');

INSERT INTO TCustomerJobs (intCustomerJobID, intCustomerID, intJobID)
VALUES
    (1, 1, 1),
    (2, 2, 2),
    (3, 1, 3);

-- ========================================================================================
-- STEP 1:		Create a view called VCustomers that will show all customers, their name
--				(last, first) and their email address.
-- ========================================================================================
DROP VIEW IF EXISTS VCustomers;
GO
CREATE VIEW VCustomers AS
SELECT 
	intCustomerID,
	strLastName,
	strFirstName,
	strEmail
FROM TCustomers;
GO

-- ========================================================================================
-- STEP 2:		Create a view called VCustomerJobs that will show all customers with jobs.
-- ========================================================================================
DROP VIEW IF EXISTS VCustomerJobs;
GO
CREATE VIEW VCustomerJobs AS
SELECT 
	TC.intCustomerID,
	TC.strLastName,
	TC.strFirstName,
	TJ.intJobID,
	TJ.strDescription,
	TJ.dtStartDate,
	TJ.dtEndDate
FROM TCustomers AS TC
JOIN TCustomerJobs AS TCJ ON TC.intCustomerID = TCJ.intCustomerID
JOIN TJobs AS TJ ON TCJ.intJobID = TJ.intJobID;
GO

-- ========================================================================================
-- STEP 3:		Create a view called VCustomerNoJob that will show all customers without a job.
-- ========================================================================================
DROP VIEW IF EXISTS VCustomerNoJob;
GO
CREATE VIEW VCustomerNoJob AS
SELECT 
	intCustomerID,
	strLastName,
	strFirstName,
	strEmail
FROM TCustomers
WHERE NOT EXISTS (
	SELECT 1 
	FROM TCustomerJobs AS TCJ 
	WHERE TCJ.intCustomerID = TCustomers.intCustomerID
);
GO

-- ========================================================================================
-- STEP 4:		Create a view called VCustomerJobCount to show the count of jobs for each customer. 
-- ========================================================================================
DROP VIEW IF EXISTS VCustomerJobCount;
GO
CREATE VIEW VCustomerJobCount AS
SELECT 
	TC.intCustomerID,
	TC.strLastName,
	TC.strFirstName,
	COUNT(TCJ.intJobID) AS JobCount
FROM TCustomers AS TC
LEFT JOIN TCustomerJobs AS TCJ ON TC.intCustomerID = TCJ.intCustomerID
GROUP BY TC.intCustomerID, TC.strLastName, TC.strFirstName;
GO

-- ========================================================================================
-- STEP 5:		Create the stored procedure uspAddCustomer that will add a record to
--				TCustomers.  Call the stored procedure after you create it to make sure
--				it works correctly. Comment your test code out prior to submitting.
-- ========================================================================================
DROP PROCEDURE IF EXISTS uspAddCustomer;
GO
CREATE PROCEDURE uspAddCustomer
	@strFirstName VARCHAR(50),
	@strLastName VARCHAR(50),
	@strPhoneNumber VARCHAR(20),
	@strEmail VARCHAR(100)
AS
BEGIN
	DECLARE @NextID INT;
	SELECT @NextID = ISNULL(MAX(intCustomerID), 0) + 1 FROM TCustomers;

	INSERT INTO TCustomers (intCustomerID, strFirstName, strLastName, strPhoneNumber, strEmail)
	VALUES (@NextID, @strFirstName, @strLastName, @strPhoneNumber, @strEmail);
END;
GO

-- ========================================================================================
-- STEP 5b:		Test uspAddCustomer with View Before/After
-- ========================================================================================

 --SELECT * FROM VCustomers;

 --EXEC uspAddCustomer 'Chris', 'Kirkpatrick', '513-240-1001', 'chris.k@nsync.com';

 --SELECT * FROM VCustomers;

-- ========================================================================================
-- STEP 6: 		Create the stored procedure uspAddJob that will add a record to TJobs.
--				Call the stored procedure after you create it to make sure it works correctly.
--				Comment your test code out prior to submitting.
-- ========================================================================================
DROP PROCEDURE IF EXISTS uspAddJob;
GO
CREATE PROCEDURE uspAddJob
	@strDescription VARCHAR(255),
	@dtStartDate DATE,
	@dtEndDate DATE = NULL
AS
BEGIN
	DECLARE @NextJobID INT;
	SELECT @NextJobID = ISNULL(MAX(intJobID), 0) + 1 FROM TJobs;

	INSERT INTO TJobs (intJobID, strDescription, dtStartDate, dtEndDate)
	VALUES (@NextJobID, @strDescription, @dtStartDate, @dtEndDate);
END;
GO


-- ========================================================================================
-- Step 6b: Test uspAddJob with View Before/After
-- ========================================================================================

-- SELECT * FROM TJobs;

-- EXEC uspAddJob 'Remodel Kitchen', '2025-06-01', NULL;

-- SELECT * FROM TJobs;


-- ========================================================================================
-- TESTING: View All Data in Views --
-- ========================================================================================


-- View all customers
-- SELECT * FROM VCustomers;

-- View all customers with jobs
-- SELECT * FROM VCustomerJobs;

-- View all customers with no jobs
-- SELECT * FROM VCustomerNoJob;

-- View job count per customer
-- SELECT * FROM VCustomerJobCount;
