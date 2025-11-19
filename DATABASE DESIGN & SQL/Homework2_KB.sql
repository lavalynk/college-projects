-- ========================================================================================
-- IT-112 – Homework 2 – Stored Procedures and Views
-- Keith Brock
-- ========================================================================================

USE dbSQL1;
SET NOCOUNT ON;  -- Report only errors

-- --------------------------------------------------------------------------------
-- Drop existing objects in dependency-safe order
-- --------------------------------------------------------------------------------

-- 1. Drop Stored Procedures that depend on views/tables
IF OBJECT_ID('uspAddCustomerJob')        IS NOT NULL DROP PROCEDURE uspAddCustomerJob;
IF OBJECT_ID('uspAddCustomerAndJob')     IS NOT NULL DROP PROCEDURE uspAddCustomerAndJob;
IF OBJECT_ID('uspAddCustomer')           IS NOT NULL DROP PROCEDURE uspAddCustomer;
IF OBJECT_ID('uspAddJob')                IS NOT NULL DROP PROCEDURE uspAddJob;

-- 2. Drop Views that depend on tables
IF OBJECT_ID('VCustomerJobCount')        IS NOT NULL DROP VIEW VCustomerJobCount;
IF OBJECT_ID('VCustomerNoJob')           IS NOT NULL DROP VIEW VCustomerNoJob;
IF OBJECT_ID('VCustomerJobs')            IS NOT NULL DROP VIEW VCustomerJobs;
IF OBJECT_ID('VCustomers')               IS NOT NULL DROP VIEW VCustomers;

-- 3. Drop Tables
IF OBJECT_ID('TCustomerJobs')            IS NOT NULL DROP TABLE TCustomerJobs;
IF OBJECT_ID('TJobs')                    IS NOT NULL DROP TABLE TJobs;
IF OBJECT_ID('TCustomers')               IS NOT NULL DROP TABLE TCustomers;


-- --------------------------------------------------------------------------------
-- Create Tables
-- --------------------------------------------------------------------------------
CREATE TABLE TCustomers
(
    intCustomerID    INT           NOT NULL,
    strFirstName     VARCHAR(50)   NOT NULL,
    strLastName      VARCHAR(50)   NOT NULL,
    strPhoneNumber   VARCHAR(20),
    strEmail         VARCHAR(100),
    CONSTRAINT TCustomers_PK PRIMARY KEY (intCustomerID)
);

CREATE TABLE TJobs
(
    intJobID         INT           NOT NULL,
    strDescription   VARCHAR(250)  NOT NULL,
    dtStartDate      DATE          NOT NULL,
    dtEndDate        DATE,
    CONSTRAINT TJobs_PK PRIMARY KEY (intJobID)
);

CREATE TABLE TCustomerJobs
(
    intCustomerJobID INT           NOT NULL,
    intCustomerID    INT           NOT NULL,
    intJobID         INT           NOT NULL,
    CONSTRAINT CustomerJobs_UQ UNIQUE (intCustomerID, intJobID),
    CONSTRAINT TCustomerJobs_PK PRIMARY KEY (intCustomerJobID)
);

-- --------------------------------------------------------------------------------
-- Create Foreign Keys
-- --------------------------------------------------------------------------------
-- #	Child				Parent				Column(s)
--1		TCustomerJobs		TCustomers			intCustomerID
--2		TCustomerJobs		TJobs				intJobID

-- 1 
ALTER TABLE TCustomerJobs ADD CONSTRAINT TCustomerJobs_TCustomers_FK
FOREIGN KEY (intCustomerID) REFERENCES TCustomers(intCustomerID);

-- 2
ALTER TABLE TCustomerJobs ADD CONSTRAINT TCustomerJobs_TJobs_FK
FOREIGN KEY (intJobID) REFERENCES TJobs(intJobID);

-- --------------------------------------------------------------------------------
-- Insert Data into Tables
-- --------------------------------------------------------------------------------
INSERT INTO TCustomers (intCustomerID, strFirstName, strLastName, strPhoneNumber, strEmail)
VALUES
    (1, 'Justin', 'Timberlake', '513-432-4299',  'jtimberlake@nsyncmail.com'),
    (2, 'JC',      'Chasez',      '513-324-2293',  'jcchasez@nsyncmail.com'),
    (3, 'Lance',   'Bass',        '513-614-1658',  'lbass@nsyncmail.com');

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
-- STEP 1: Create VCustomers view
-- ========================================================================================
GO
CREATE VIEW VCustomers AS
SELECT 
    intCustomerID,
    strLastName + ', ' + strFirstName AS [Last, First Name],
    strEmail
FROM TCustomers;
GO

-- ========================================================================================
-- STEP 2: Create VCustomerJobs view
-- ========================================================================================
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
JOIN TJobs AS TJ         ON TCJ.intJobID    = TJ.intJobID;
GO

-- ========================================================================================
-- STEP 3: Create VCustomerNoJob view
-- ========================================================================================
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
-- STEP 4: Create VCustomerJobCount view
-- ========================================================================================
GO
CREATE VIEW VCustomerJobCount AS
SELECT 
    TC.intCustomerID,
    TC.strLastName,
    TC.strFirstName,
    COUNT(TCJ.intJobID) AS JobCount
FROM TCustomers AS TC
LEFT JOIN TCustomerJobs AS TCJ ON TC.intCustomerID = TCJ.intCustomerID
GROUP BY 
    TC.intCustomerID,
    TC.strLastName,
    TC.strFirstName;
GO

-- ========================================================================================
-- STEP 5: Create uspAddCustomer
-- ========================================================================================
DROP PROCEDURE IF EXISTS uspAddCustomer;
GO
CREATE PROCEDURE uspAddCustomer
    @strFirstName    VARCHAR(50),
    @strLastName     VARCHAR(50),
    @strPhoneNumber  VARCHAR(20),
    @strEmail        VARCHAR(100)
AS
BEGIN
    DECLARE @NextID INT;
    SELECT @NextID = ISNULL(MAX(intCustomerID), 0) + 1 FROM TCustomers;

    INSERT INTO TCustomers (intCustomerID, strFirstName, strLastName, strPhoneNumber, strEmail)
    VALUES (@NextID, @strFirstName, @strLastName, @strPhoneNumber, @strEmail);
END;
GO

-- ========================================================================================
-- Test uspAddCustomer (comment out before submitting)
-- ========================================================================================
-- SELECT * FROM VCustomers;
-- EXEC uspAddCustomer 'Chris', 'Kirkpatrick', '513-240-1001', 'chris.k@nsync.com';
-- SELECT * FROM VCustomers;

-- ========================================================================================
-- STEP 6: Create uspAddJob
-- ========================================================================================
DROP PROCEDURE IF EXISTS uspAddJob;
GO
CREATE PROCEDURE uspAddJob
    @strDescription  VARCHAR(255),
    @dtStartDate     DATE,
    @dtEndDate       DATE = NULL
AS
BEGIN
    DECLARE @NextJobID INT;
    SELECT @NextJobID = ISNULL(MAX(intJobID), 0) + 1 FROM TJobs;

    INSERT INTO TJobs (intJobID, strDescription, dtStartDate, dtEndDate)
    VALUES (@NextJobID, @strDescription, @dtStartDate, @dtEndDate);
END;
GO

-- ========================================================================================
-- Test uspAddJob (comment out before submitting)
-- ========================================================================================
 --SELECT * FROM TJobs;
 --EXEC uspAddJob 'Remodel Kitchen', '2025-06-01', NULL;
 --SELECT * FROM TJobs;

-- ========================================================================================
-- Testing: View All Data in Views
-- ========================================================================================
 --SELECT * FROM VCustomers;
 --SELECT * FROM VCustomerJobs;
 --SELECT * FROM VCustomerNoJob;
 --SELECT * FROM VCustomerJobCount;
