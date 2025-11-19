-- ========================================================================================
-- IT-112 – Homework 3 – Stored Procedures continued...
-- Keith Brock
-- ========================================================================================

USE dbSQL1;
SET NOCOUNT ON; 

-- --------------------------------------------------------------------------------
-- Drop existing objects in dependency‐safe order - trying to follow your example a little more closely...
-- --------------------------------------------------------------------------------

-- 1. Drop Stored Procedures
IF OBJECT_ID('uspAddCustomerJob')       IS NOT NULL DROP PROCEDURE uspAddCustomerJob;
IF OBJECT_ID('uspAddCustomerAndJob')    IS NOT NULL DROP PROCEDURE uspAddCustomerAndJob;
IF OBJECT_ID('uspAddCustomer')          IS NOT NULL DROP PROCEDURE uspAddCustomer;
IF OBJECT_ID('uspAddJob')               IS NOT NULL DROP PROCEDURE uspAddJob;


-- 2. Drop Views
IF OBJECT_ID('VCustomerJobCount')		IS NOT NULL DROP VIEW VCustomerJobCount;
IF OBJECT_ID('VCustomerNoJob')			IS NOT NULL DROP VIEW VCustomerNoJob;
IF OBJECT_ID('VCustomerJobs')			IS NOT NULL DROP VIEW VCustomerJobs;
IF OBJECT_ID('VCustomers')				IS NOT NULL DROP VIEW VCustomers;

-- 3. Drop Tables
IF OBJECT_ID('TCustomerJobs')			IS NOT NULL DROP TABLE TCustomerJobs;
IF OBJECT_ID('TJobs')					IS NOT NULL DROP TABLE TJobs;
IF OBJECT_ID('TCustomers')				IS NOT NULL DROP TABLE TCustomers;


-- --------------------------------------------------------------------------------
-- Create Tables (with IDENTITY on primary keys)
-- --------------------------------------------------------------------------------
CREATE TABLE TCustomers
(
    intCustomerID    INT IDENTITY(1,1) NOT NULL,
    strFirstName     VARCHAR(50)       NOT NULL,
    strLastName      VARCHAR(50)       NOT NULL,
    strPhoneNumber   VARCHAR(20),
    strEmail         VARCHAR(100),
    CONSTRAINT TCustomers_PK PRIMARY KEY (intCustomerID)
);

CREATE TABLE TJobs
(
    intJobID         INT IDENTITY(1,1) NOT NULL,
    strDescription   VARCHAR(250)      NOT NULL,
    dtStartDate      DATE              NOT NULL,
    dtEndDate        DATE,
    CONSTRAINT TJobs_PK PRIMARY KEY (intJobID)
);

CREATE TABLE TCustomerJobs
(
    intCustomerJobID INT IDENTITY(1,1) NOT NULL,
    intCustomerID    INT               NOT NULL,
    intJobID         INT               NOT NULL,
    CONSTRAINT CustomerJobs_UQ UNIQUE (intCustomerID, intJobID),
    CONSTRAINT TCustomerJobs_PK PRIMARY KEY (intCustomerJobID)
);


-- --------------------------------------------------------------------------------
-- Create Foreign Keys
-- --------------------------------------------------------------------------------
--    Child           Parent        Column(s)
--    --------------- ------------- ------------
--    TCustomerJobs   TCustomers    intCustomerID
--    TCustomerJobs   TJobs         intJobID

ALTER TABLE TCustomerJobs ADD CONSTRAINT TCustomerJobs_TCustomers_FK
FOREIGN KEY (intCustomerID) REFERENCES TCustomers(intCustomerID);

ALTER TABLE TCustomerJobs ADD CONSTRAINT TCustomerJobs_TJobs_FK
FOREIGN KEY (intJobID) REFERENCES TJobs(intJobID);


-- --------------------------------------------------------------------------------
--  Insert Data into Tables
-- (Because intCustomerID, intJobID, and intCustomerJobID are IDENTITY, we do not specify them here.)
-- --------------------------------------------------------------------------------

INSERT INTO TCustomers (strFirstName, strLastName, strPhoneNumber, strEmail)
VALUES
     ('Justin', 'Timberlake',   '513-432-4299',  'jtimberlake@nsyncmail.com')
    ,('JC',      'Chasez',      '513-324-2293',  'jcchasez@nsyncmail.com')
    ,('Lance',   'Bass',        '513-614-1658',  'lbass@nsyncmail.com')

INSERT INTO TJobs (strDescription, dtStartDate, dtEndDate)
VALUES
     ('Excavate basement foundation',					'2024-01-03',	'2024-01-28')
    ,('Electrical rewiring for commercial building',	'2024-02-14',	 NULL)
    ,('HVAC system replacement',						'2024-01-22',	'2024-02-05')

INSERT INTO TCustomerJobs (intCustomerID, intJobID)
VALUES
     (1, 1)   -- Justin -- Excavate basement foundation
    ,(2, 2)   -- JC -- Electrical rewiring
    ,(1, 3)   -- Justin -- HVAC system replacement


-- ========================================================================================
-- Create VCustomers view
-- ========================================================================================
GO
CREATE VIEW VCustomers AS
SELECT 
    intCustomerID,
    strLastName + ', ' + strFirstName AS [Last, First Name],
    strEmail
FROM TCustomers
GO


-- ========================================================================================
-- Create VCustomerJobs view
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
JOIN TJobs AS TJ         ON TCJ.intJobID    = TJ.intJobID
GO


-- ========================================================================================
-- Create VCustomerNoJob View
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
)
GO


-- ========================================================================================
-- Create VCustomerJobCount view
-- ========================================================================================
GO
CREATE VIEW VCustomerJobCount AS
SELECT 
    TC.intCustomerID,
    TC.strLastName,
    TC.strFirstName,
    COUNT(TCJ.intJobID) AS JobCount
FROM TCustomers AS TC
LEFT JOIN TCustomerJobs AS TCJ
    ON TC.intCustomerID = TCJ.intCustomerID
GROUP BY 
    TC.intCustomerID,
    TC.strLastName,
    TC.strFirstName
GO


-- ========================================================================================
-- Create uspAddCustomer stored procedure
--  (inserts into TCustomers; IDENTITY column auto-increments)
-- ========================================================================================
DROP PROCEDURE IF EXISTS uspAddCustomer;
GO

CREATE PROCEDURE uspAddCustomer
    @strFirstName   VARCHAR(50),
    @strLastName    VARCHAR(50),
    @strPhoneNumber VARCHAR(20),
    @strEmail       VARCHAR(100),
    @NewCustomerID  INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRANSACTION;

    INSERT INTO TCustomers (strFirstName, strLastName, strPhoneNumber, strEmail)
    VALUES (@strFirstName, @strLastName, @strPhoneNumber, @strEmail);

    SELECT @NewCustomerID = SCOPE_IDENTITY();

    COMMIT TRANSACTION;
END;
GO

-- ========================================================================================
-- Test:
-- ========================================================================================
--DECLARE @NewCustomerID INT;

--EXEC uspAddCustomer
--     @strFirstName   = 'Chris',
--     @strLastName    = 'Kirkpatrick',
--     @strPhoneNumber = '513-240-1001',
--     @strEmail       = 'chris.k@nsync.com',
--     @NewCustomerID  = @NewCustomerID OUTPUT;

--PRINT 'New CustomerID = ' + CAST(@NewCustomerID AS VARCHAR(10));
-- ========================================================================================
-- Create uspAddJob stored procedure
--  (inserts into TJobs; IDENTITY column auto‐increments)
-- ========================================================================================
DROP PROCEDURE IF EXISTS uspAddJob;
GO

CREATE PROCEDURE uspAddJob
    @strDescription VARCHAR(250),
    @dtStartDate    DATE,
    @dtEndDate      DATE,
    @NewJobID       INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRANSACTION;

    INSERT INTO TJobs (strDescription, dtStartDate, dtEndDate)
    VALUES (@strDescription, @dtStartDate, @dtEndDate);

    SELECT @NewJobID = SCOPE_IDENTITY();

    COMMIT TRANSACTION;
END;
GO

-- ========================================================================================
-- Test:
-- ========================================================================================
-- DECLARE @NewJobID INT;
-- EXEC uspAddJob 
--     @strDescription = 'Remodel Kitchen', 
--     @dtStartDate    = '2025-06-01', 
--     @dtEndDate      = NULL, 
--     @NewJobID       = @NewJobID OUTPUT;

--PRINT 'New JobID = ' + CAST(@NewJobID AS VARCHAR(10));

-- ========================================================================================
--  Assignment 3 Step 1
-- 1. uspAddCustomerJob - Stored procedure to add a record to TCustomersJobs. Call the 
--    procedure after creation to ensure it works correctly. Comment out before 
--    submitting. 
-- ========================================================================================
GO
DROP PROCEDURE IF EXISTS uspAddCustomerJob;
GO

CREATE PROCEDURE uspAddCustomerJob
    @intCustomerJobID INTEGER OUTPUT,
    @intCustomerID    INTEGER,
    @intJobID         INTEGER
AS
    SET NOCOUNT ON      -- Report only errors
    SET XACT_ABORT ON   -- Terminate and rollback entire transaction on error

    BEGIN TRANSACTION

        INSERT INTO TCustomerJobs WITH (TABLOCKX) (intCustomerID, intJobID)
        VALUES (@intCustomerID, @intJobID);

        SELECT @intCustomerJobID = MAX(intCustomerJobID)
        FROM TCustomerJobs;

    COMMIT TRANSACTION
GO

-- ========================================================================================
-- Test:
-- ========================================================================================
 --DECLARE @intCustomerJobID INTEGER = 0;
 --EXEC uspAddCustomerJob @intCustomerJobID OUTPUT, 1, 2;
 --PRINT 'New CustomerJobID = ' + CONVERT(VARCHAR, @intCustomerJobID);
 --PRINT 'Job ID = ' + CONVERT( VARCHAR, @intCustomerJobID )
 
-- ========================================================================================
-- 2. uspAddCustomerAndJob - Stored procedure to add a record to TCustomers, TJobs 
--    and TCustomersJobs. Call the procedure after creation to ensure it works 
--    correctly. Comment out before submitting. 
-- ========================================================================================
GO
DROP PROCEDURE IF EXISTS uspAddCustomerAndJob;
GO

CREATE PROCEDURE uspAddCustomerAndJob
    @strFirstName          VARCHAR(50),
    @strLastName           VARCHAR(50),
    @strPhoneNumber        VARCHAR(20),
    @strEmail              VARCHAR(100),
    @strJobDescription     VARCHAR(250),
    @dtStartDate           DATE,
    @dtEndDate             DATE,
    @NewCustomerJobID      INT            OUTPUT
AS
BEGIN
    SET NOCOUNT ON;    
    SET XACT_ABORT ON; 
    BEGIN TRANSACTION;

        DECLARE @NewCustomerID INT;
        DECLARE @NewJobID      INT;

        -- 1) Add customer, capture new CustomerID
        EXEC uspAddCustomer
            @strFirstName   = @strFirstName,
            @strLastName    = @strLastName,
            @strPhoneNumber = @strPhoneNumber,
            @strEmail       = @strEmail,
            @NewCustomerID  = @NewCustomerID OUTPUT;

        -- 2) Add job, capture new JobID
        EXEC uspAddJob
            @strDescription = @strJobDescription,
            @dtStartDate    = @dtStartDate,
            @dtEndDate      = @dtEndDate,
            @NewJobID       = @NewJobID OUTPUT;

        -- 3) Link customer & job, capture CustomerJobID
        EXEC uspAddCustomerJob
            @intCustomerJobID = @NewCustomerJobID OUTPUT,
            @intCustomerID    = @NewCustomerID,
            @intJobID         = @NewJobID;

    COMMIT TRANSACTION;
END;
GO


-- ========================================================================================
-- Test
-- ========================================================================================
--SELECT * FROM TCustomers
--SELECT * FROM TJobs
--SELECT * FROM TCustomerJobs
--DECLARE @NewCJID INT = 0;

--EXEC uspAddCustomerAndJob
--    @strFirstName      = 'Chris',
--    @strLastName       = 'Kirkpatrick',
--    @strPhoneNumber    = '513-240-1001',
--    @strEmail          = 'chris.kirkpatrick@nsync.com',
--    @strJobDescription = 'Lighting Setup',
--    @dtStartDate       = '2025-09-01',
--    @dtEndDate         = '2025-09-05',
--    @NewCustomerJobID  = @NewCJID OUTPUT;

--PRINT 'New CustomerJobID = ' + CAST(@NewCJID AS VARCHAR(10));

--SELECT * FROM TCustomers
--SELECT * FROM TJobs
--SELECT * FROM TCustomerJobs
