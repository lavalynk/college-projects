-- ========================================================================================
-- IT-112 – Homework 1 – SQL Script
-- Keith Brock
-- ========================================================================================

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbSQL1;
SET NOCOUNT ON;

-- --------------------------------------------------------------------------------
-- Drop Tables if they exist
-- --------------------------------------------------------------------------------
IF OBJECT_ID('TCustomerJobs') IS NOT NULL DROP TABLE TCustomerJobs;
IF OBJECT_ID('TJobs') IS NOT NULL DROP TABLE TJobs;
IF OBJECT_ID('TCustomers') IS NOT NULL DROP TABLE TCustomers;

-- --------------------------------------------------------------------------------
-- Step #1: Create Tables
-- --------------------------------------------------------------------------------
CREATE TABLE TCustomers (
	intCustomerID	INT					NOT NULL,
	strName			VARCHAR(100)		NOT NULL,
	strPhone		VARCHAR(20),
	strEmail		VARCHAR(100),
	CONSTRAINT TCustomers_PK PRIMARY KEY (intCustomerID)
);

CREATE TABLE TJobs (
	intJobID		INT					NOT NULL,
	strDescription	VARCHAR(250)		NOT NULL,
	dtStartDate		DATE				NOT NULL,
	dtEndDate		DATE,
	CONSTRAINT TJobs_PK PRIMARY KEY (intJobID)
);

CREATE TABLE TCustomerJobs (
	intCustomerID	INT					NOT NULL,
	intJobID		INT					NOT NULL,
	CONSTRAINT TCustomerJobs_PK PRIMARY KEY (intCustomerID, intJobID)
);

-- --------------------------------------------------------------------------------
-- Step #2: Add Foreign Keys
-- --------------------------------------------------------------------------------
ALTER TABLE TCustomerJobs ADD CONSTRAINT FK_CustomerJobs_Customer FOREIGN KEY (intCustomerID) REFERENCES TCustomers(intCustomerID);
ALTER TABLE TCustomerJobs ADD CONSTRAINT FK_CustomerJobs_Job FOREIGN KEY (intJobID) REFERENCES TJobs(intJobID);

-- --------------------------------------------------------------------------------
-- Step #3: Insert Data into TCustomers (NSYNC Members)
-- --------------------------------------------------------------------------------
INSERT INTO TCustomers (intCustomerID, strName, strPhone, strEmail)
VALUES (1, 'Justin Timberlake', '513-555-1111', 'jtimberlake@nsyncmail.com'),
	   (2, 'JC Chasez', '513-555-2222', 'jcchasez@nsyncmail.com'),
	   (3, 'Lance Bass', '513-555-3333', 'lbass@nsyncmail.com');

-- --------------------------------------------------------------------------------
-- Step #4: Insert Data into TJobs (randomized)
-- --------------------------------------------------------------------------------
INSERT INTO TJobs (intJobID, strDescription, dtStartDate, dtEndDate)
VALUES (1, 'Excavate basement foundation', '2024-01-03', '2024-01-28'),
	   (2, 'Electrical rewiring for commercial building', '2024-02-14', NULL),
	   (3, 'HVAC system replacement', '2024-01-22', '2024-02-05');

-- --------------------------------------------------------------------------------
-- Step #5: Assign Jobs to Customers
-- --------------------------------------------------------------------------------
INSERT INTO TCustomerJobs (intCustomerID, intJobID)
VALUES (1, 1), -- Justin Timberlake: Job 1
	   (1, 2), -- Justin Timberlake: Job 2
	   (2, 3); -- JC Chasez: Job 3

-- --------------------------------------------------------------------------------
-- Step #6: Test Duplicate Job Assignment (Should FAIL - Do NOT run)
-- --------------------------------------------------------------------------------
-- INSERT INTO TCustomerJobs (intCustomerID, intJobID) VALUES (1, 1); -- Duplicate PK

-- --------------------------------------------------------------------------------
-- Step #7: Query – Show All Customers and Jobs
-- --------------------------------------------------------------------------------
SELECT TC.intCustomerID, TC.strName, TC.strPhone, TC.strEmail, TJ.intJobID, TJ.strDescription, TJ.dtStartDate, TJ.dtEndDate

FROM TCustomers AS TC

LEFT JOIN TCustomerJobs AS TCJ ON TC.intCustomerID = TCJ.intCustomerID
LEFT JOIN TJobs AS TJ ON TCJ.intJobID = TJ.intJobID;

-- --------------------------------------------------------------------------------
-- Step #8: Query – Jobs Not Completed
-- --------------------------------------------------------------------------------
SELECT intJobID, strDescription, dtStartDate

FROM TJobs

WHERE dtEndDate IS NULL;

-- --------------------------------------------------------------------------------
-- Step #9: Update a NULL End Date to a Valid Date
-- --------------------------------------------------------------------------------
UPDATE TJobs

SET dtEndDate = '2024-03-01'
WHERE intJobID = 2 AND dtEndDate IS NULL;
