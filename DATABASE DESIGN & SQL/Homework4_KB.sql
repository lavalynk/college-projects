-- ========================================================================================
-- IT-112 Database Design and SQL #2
-- Homework #4 – Stored Procedures (cont.) - Cursors
-- Keith Brock
-- ========================================================================================

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbSQL1;     -- Get out of the master database
SET NOCOUNT ON; -- Report only errors

-- --------------------------------------------------------------------------------
-- Drop Tables
-- --------------------------------------------------------------------------------
IF OBJECT_ID('TSalaries')        IS NOT NULL DROP TABLE TSalaries;
IF OBJECT_ID('THours')           IS NOT NULL DROP TABLE THours;
IF OBJECT_ID('THourlyPayRate')   IS NOT NULL DROP TABLE THourlyPayRate;
IF OBJECT_ID('TTaxRates')        IS NOT NULL DROP TABLE TTaxRates;
IF OBJECT_ID('TEmployees')       IS NOT NULL DROP TABLE TEmployees;
IF OBJECT_ID('TPayrollStatuses') IS NOT NULL DROP TABLE TPayrollStatuses;
IF OBJECT_ID('TPayrolls')        IS NOT NULL DROP TABLE TPayrolls;

-- --------------------------------------------------------------------------------
-- Drop Procedures
-- --------------------------------------------------------------------------------
IF OBJECT_ID('uspGetGrossPay')     IS NOT NULL DROP PROCEDURE uspGetGrossPay;
IF OBJECT_ID('uspCalculateSalary') IS NOT NULL DROP PROCEDURE uspCalculateSalary;
IF OBJECT_ID('uspCalculateHourly') IS NOT NULL DROP PROCEDURE uspCalculateHourly;
IF OBJECT_ID('uspCalculateTaxes')  IS NOT NULL DROP PROCEDURE uspCalculateTaxes;
IF OBJECT_ID('uspAddPayroll')      IS NOT NULL DROP PROCEDURE uspAddPayroll;

-- --------------------------------------------------------------------------------
-- Step #1: Create Tables
-- --------------------------------------------------------------------------------
CREATE TABLE TEmployees
(
	 intEmployeeID			INTEGER			NOT NULL
	,intPayrollStatusID		INTEGER			NOT NULL		--hourly or salary
	,strEmployeeID			VARCHAR(50)		NOT NULL		--actual employee ID
	,strFirstName			VARCHAR(50)		NOT NULL
	,strLastName			VARCHAR(50)		NOT NULL
	,strAddress				VARCHAR(50)		NOT NULL
	,strCity				VARCHAR(50)		NOT NULL
	,strState				VARCHAR(50)		NOT NULL
	,strZip					VARCHAR(50)		NOT NULL
	,CONSTRAINT TEmployees_PK PRIMARY KEY ( intEmployeeID )

)

CREATE TABLE TPayrollStatuses
(
	 intPayrollStatusID		INTEGER			NOT NULL
	,strStatus				VARCHAR(1)		NOT NULL		--S for salary and H for hourly are only values allowed
	,strDescription			VARCHAR(50)		NOT NULL
	,CONSTRAINT TPayrollStatuses_PK PRIMARY KEY ( intPayrollStatusID	)
	,CONSTRAINT CK_PayrollStatus CHECK ( strStatus = 'H' OR strStatus = 'S')		-- ********CHECK CONSTRAINT ******keeps input to S or H only
)


CREATE TABLE THourlyPayRate
(
	 intEmployeeRateID		INTEGER			NOT NULL
	,intEmployeeID			INTEGER			NOT NULL
	,monRate				MONEY			NOT NULL
	,CONSTRAINT THourlyPayRate_PK PRIMARY KEY ( intEmployeeRateID )
	,CONSTRAINT UQ_EmployeeID UNIQUE( intEmployeeID  )  -- EMPLOYEES SHOULD ONLY HAVE 1 HOURLY RATE
)

CREATE TABLE TSalaries
(
	 intSalaryID			INTEGER			NOT NULL
	,intEmployeeID			INTEGER			NOT NULL
	,monSalary				MONEY			NOT NULL
	,intFrequency			INTEGER			NOT NULL  -- frequency of pay periods # per year for our purpose 52 but could change
	,CONSTRAINT TSalaries_PK PRIMARY KEY ( intSalaryID )
	,CONSTRAINT UQ_intEmployeeID UNIQUE( intEmployeeID  )  -- EMPLOYEES SHOULD ONLY HAVE 1 SALARY

)

CREATE TABLE THours
(
	 intHourID				INTEGER			NOT NULL
	,intEmployeeID			INTEGER			NOT NULL
	,dtmEndDate				DATETIME		NOT NULL	-- date pay period ends
	,decHours				DECIMAL(6, 2)	NOT NULL	-- HOURS WORKED THIS PERIOD (6, 2) is referred to as the precision and scale of the decimal
	,CONSTRAINT THours_PK PRIMARY KEY ( intHourID )		-- precision is the total digits and scale is the # of digits to the right of the decimal
														-- in this case we have 6 total with 2 right of the decimal 1962.53 is how it would look
)

CREATE TABLE TTaxRates
(
	 intTaxRateID			INTEGER			NOT NULL
	,intEmployeeID			INTEGER			NOT NULL
	,decStateRate			DECIMAL(6, 2)			NOT NULL  -- State income tax rate
	,decLocalRate			DECIMAL(6, 2)			NOT NULL  -- Local income tax rate
	,CONSTRAINT TTaxRates_PK PRIMARY KEY ( intTaxRateID )

)

-- --------------------------------------------------------------------------------
-- Step #2: Identify and Create Foreign Keys
-- --------------------------------------------------------------------------------
--
-- #	Child								Parent						Column(s)
-- -	-----								------						---------
-- 1	TEmployees							TPayrollStatuses			intPayrollStatusID
-- 2	THourlyPayRate						TEmployees					intEmployeeID
-- 3	TSalaries							TEmployees					intEmployeeID
-- 4	THours								TEmployees					intEmployeeID
-- 5	TTaxRates							TEmployees					intEmployeeID


-- 1
ALTER TABLE TEmployees ADD CONSTRAINT TEmployees_TPayrollStatuses_FK
FOREIGN KEY ( intPayrollStatusID ) REFERENCES TPayrollStatuses ( intPayrollStatusID )

-- 2
ALTER TABLE THourlyPayRate ADD CONSTRAINT THourlyPayRate_TEmployees_FK
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

-- 3
ALTER TABLE TSalaries ADD CONSTRAINT TSalaries_TEmployees_FK
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

-- 4
ALTER TABLE THours ADD CONSTRAINT THours_TEmployees_FK
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

-- 5
ALTER TABLE TTaxRates ADD CONSTRAINT TTaxRates_TEmployees_FK
FOREIGN KEY ( intEmployeeID ) REFERENCES TEmployees ( intEmployeeID )

-- --------------------------------------------------------------------------------
-- Step #3: Add data
-- --------------------------------------------------------------------------------
INSERT INTO TPayrollStatuses ( intPayrollStatusID, strStatus, strDescription )
VALUES	 ( 1, 'S', 'Salary' )
		,( 2, 'H', 'Hourly')

INSERT INTO TEmployees ( intEmployeeID, intPayrollStatusID, strEmployeeID, strFirstName, strLastName, strAddress, strCity, strState, strZip )
VALUES	 ( 1, 1, 'AC1524', 'James', 'Allen', '1979 Park Place', 'Cincinnati', 'Oh', '45208' )
		,( 2, 2, 'MN0195', 'Sally', 'Frye', '196 Main St.', 'Milford', 'Oh', '45232' )
		,( 3, 1, 'HR5243', 'Fred', 'Mening', '19 Ft Wayne Ave.', 'West Chester', 'Oh', '45069' )
		,( 4, 2, 'MN0645', 'Bill', 'Leford', '174 Chance Ave', 'Cold Spring', 'Ky', '44038' )
		,( 5, 2, 'SH0326', 'Susan', 'Maelle', '109 Forrest St.', 'Lawrenceburg', 'In', '43098' )
		,( 6, 1, 'EX26410', 'John', 'Snowden', '1709 ALes Lane', 'Milan', 'In', '43168' )


INSERT INTO THourlyPayRate ( intEmployeeRateID, intEmployeeID, monRate )
VALUES	 ( 1, 2, 10.00 )
		,( 2, 4, 11.86 )
		,( 3, 5, 10.00 )

INSERT INTO TSalaries ( intSalaryID, intEmployeeID, monSalary, intFrequency )
VALUES	 ( 1, 1, 90000.00, 52 )
		,( 2, 3, 45597.29, 52 )
		,( 3, 6, 255597.29, 52 )

INSERT INTO THours ( intHourID, intEmployeeID, dtmEndDate, decHours )
VALUES	 ( 1, 2, '1/19/2019', 46.25 )
		,( 2, 4, '1/19/2019', 42.55 )
		,( 3, 5, '1/19/2019', 38.00 )
		,( 4, 2, '1/26/2019', 40.00 )
		,( 5, 1, '1/26/2019', 49.89 )
		,( 6, 2, '1/26/2019', 30.00 )
		,( 7, 3, '1/26/2019', 49.89 )
		,( 8, 4, '1/26/2019', 51.23 )
		,( 9, 5, '1/26/2019', 50.00 )
		,( 10, 6, '1/26/2019', 51.23 )

INSERT INTO TTaxRates ( intTaxRateID, intEmployeeID, decStateRate, decLocalRate )
VALUES	 ( 1, 1, .0495, .021 )
		,( 2, 2, .0495, .021 )
		,( 3, 3, .0495, .021 )
		,( 4, 4, .055, .021 )
		,( 5, 5, .0323, .021 )
		,( 6, 6, .0323, .021 )

GO

CREATE PROCEDURE uspCalculateSalary
    @monGrossSalary MONEY OUTPUT,
    @monSalary      MONEY,
    @intFrequency   INTEGER
AS
BEGIN
    SET XACT_ABORT ON;
    SET @monGrossSalary = @monSalary / @intFrequency;
END;
GO

CREATE PROCEDURE uspCalculateHourly
    @monGrossPay MONEY OUTPUT,
    @decHours    DECIMAL(10,2),
    @decRate     DECIMAL(10,2)
AS
BEGIN
    SET XACT_ABORT ON;
    IF @decHours > 40
        SET @monGrossPay = ((@decHours - 40) * @decRate * 1.5) + (40 * @decRate);
    ELSE
        SET @monGrossPay = @decHours * @decRate;
END;
GO

-- --------------------------------------------------------------------------------
-- Homework #4 – Stored Procedures (cont.) - Cursors

-- 1) Create the stored procedure uspCalculateTaxes that will accept employee ID and
-- weekly gross pay (already calculated for you). Outputs will be federal, state and
-- local tax. Create a cursor within this stored procedure GetTaxRates which will pull
-- state and local rate from TTaxRates for the employee. For federal tax use an IF
-- statement to set the rate based on the weekly gross and then calculate the tax
-- owed. Call this stored proc from within uspGetGrossPay.
-- --------------------------------------------------------------------------------
CREATE PROCEDURE uspCalculateTaxes
    @intEmployeeID INT,
    @monGrossPay   MONEY,
    @monFederalTax MONEY OUTPUT,
    @monStateTax   MONEY OUTPUT,
    @monLocalTax   MONEY OUTPUT
AS
BEGIN
    DECLARE @decStateRate DECIMAL(6,2), @decLocalRate DECIMAL(6,2), @decFedRate DECIMAL(4,2);
    DECLARE GetTaxRates CURSOR LOCAL FOR
    SELECT decStateRate, decLocalRate FROM TTaxRates WHERE intEmployeeID = @intEmployeeID;
    OPEN GetTaxRates;
    FETCH NEXT FROM GetTaxRates INTO @decStateRate, @decLocalRate;
    CLOSE GetTaxRates;
    DEALLOCATE GetTaxRates;
    IF @monGrossPay < 961.54
        SET @decFedRate = 0.07;
    ELSE IF @monGrossPay <= 1923.08
        SET @decFedRate = 0.08;
    ELSE
        SET @decFedRate = 0.09;
    SET @monFederalTax = @monGrossPay * @decFedRate;
    SET @monStateTax   = @monGrossPay * @decStateRate;
    SET @monLocalTax   = @monGrossPay * @decLocalRate;
END;
GO

-- --------------------------------------------------------------------------------
-- 2) Create a Table called TPayrolls. This table should hold employee ID, gross
-- pay (weekly), federal tax, state tax, local tax and the current date 
-- (Use GetDate()) for the value on this). Use the IDENTITY property for the PK
-- key column in TPayrolls.
-- --------------------------------------------------------------------------------
CREATE TABLE TPayrolls (
    intPayrollID		INT IDENTITY(1,1)		PRIMARY KEY,
    intEmployeeID		INT						NOT NULL,
    monGrossPay			DECIMAL(10,2)			NOT NULL,
    monFederalTax		DECIMAL(10,2)			NOT NULL,
    monStateTax			DECIMAL(10,2)			NOT NULL,
    monLocalTax			DECIMAL(10,2)			NOT NULL,
    dtmPayrollDate		DATETIME NOT NULL		DEFAULT GETDATE()
);
GO

-- --------------------------------------------------------------------------------
-- 3) Create the stored procedure uspAddPayroll. Once you call uspGetGrossPay
-- which calls uspCalculateTaxes you will then call uspAddPayroll within
-- uspGetGrossPay and insert the calculated data into TPayrolls. IE – Your calls
-- to uspCalculateTaxes and uspAddPayroll will go in uspGetGrossPay between the
-- last two END statements. This is a one call does it all script. You call
-- uspGetGrossPay and it will calculate all taxes and write the record into the
-- table.
-- --------------------------------------------------------------------------------
CREATE PROCEDURE uspAddPayroll
    @intEmployeeID		INT,
    @monGrossPay		MONEY,
    @monFederalTax		MONEY,
    @monStateTax		MONEY,
    @monLocalTax		MONEY
AS
BEGIN
    INSERT INTO TPayrolls (intEmployeeID, monGrossPay, monFederalTax, monStateTax, monLocalTax)
    VALUES (@intEmployeeID, @monGrossPay, @monFederalTax, @monStateTax, @monLocalTax);
END;
GO

CREATE PROCEDURE uspGetGrossPay
    @monGrossPay		MONEY OUTPUT,
    @intEmployeeID		INT
AS
BEGIN
    SET XACT_ABORT ON;
    DECLARE 
		@intPayrollStatusID		INT,
		@monSalary				MONEY,
		@intFrequency			INT,
		@decHours				DECIMAL(10,2),
		@monRate				MONEY;
    DECLARE 
		@monFedTax				MONEY,
		@monStTax				MONEY,
		@monLocTax				MONEY;
    SELECT @intPayrollStatusID = intPayrollStatusID FROM TEmployees WHERE intEmployeeID = @intEmployeeID;
    IF @intPayrollStatusID = 1
    BEGIN
        SELECT @monSalary = monSalary, @intFrequency = intFrequency FROM TSalaries WHERE intEmployeeID = @intEmployeeID;
        EXEC uspCalculateSalary @monGrossSalary = @monGrossPay OUTPUT, @monSalary = @monSalary, @intFrequency = @intFrequency;
    END
    ELSE
    BEGIN
        SELECT @monRate = TER.monRate, @decHours = TH.decHours
          FROM THourlyPayRate TER
          JOIN THours TH ON TER.intEmployeeID = TH.intEmployeeID
         WHERE TH.intHourID = (SELECT MAX(intHourID) FROM THours WHERE intEmployeeID = @intEmployeeID);
        EXEC uspCalculateHourly @monGrossPay = @monGrossPay OUTPUT, @decHours = @decHours, @decRate = @monRate;
    END
    EXEC uspCalculateTaxes
        @intEmployeeID = @intEmployeeID,
        @monGrossPay   = @monGrossPay,
        @monFederalTax = @monFedTax OUTPUT,
        @monStateTax   = @monStTax OUTPUT,
        @monLocalTax   = @monLocTax OUTPUT;
    EXEC uspAddPayroll
        @intEmployeeID = @intEmployeeID,
        @monGrossPay   = @monGrossPay,
        @monFederalTax = @monFedTax,
        @monStateTax   = @monStTax,
        @monLocalTax   = @monLocTax;
END;
GO

-- --------------------------------------------------------------------------------
--  4) Create a test call to the stored procedure after you create it to make sure
--	it works correctly for each type of employee. This means you will need to call
--  3 salary and 3 hourly employees. You will need 1 each for the federal tax rates
--  of .07, .08 and .09 as listed above for each. Set your data up accordingly.
--  Once you have tested your code please comment your test code out prior to 
--  submitting. 
-- --------------------------------------------------------------------------------
--DECLARE @monGrossPay MONEY;

-- EXEC uspGetGrossPay @monGrossPay OUTPUT, @intEmployeeID = 1;  -- salary, .08 (1730.77/week)
-- EXEC uspGetGrossPay @monGrossPay OUTPUT, @intEmployeeID = 2;  -- hourly, .07 (300.00/week)
-- EXEC uspGetGrossPay @monGrossPay OUTPUT, @intEmployeeID = 3;  -- salary, .07 (876.87/week)
-- EXEC uspGetGrossPay @monGrossPay OUTPUT, @intEmployeeID = 4;  -- hourly, .07 (674.18/week)
-- EXEC uspGetGrossPay @monGrossPay OUTPUT, @intEmployeeID = 5;  -- hourly, .07 (550.00/week)
-- EXEC uspGetGrossPay @monGrossPay OUTPUT, @intEmployeeID = 6;  -- salary, .09 (4915.33/week)

--SELECT
--  TPS.strDescription AS PayrollType,
--  COUNT(*)            AS CountPerType
--FROM TPayrolls AS TP
--JOIN TEmployees AS TE ON TP.intEmployeeID = TE.intEmployeeID
--JOIN TPayrollStatuses AS TPS ON TE.intPayrollStatusID = TPS.intPayrollStatusID
--GROUP BY TPS.strDescription;

--SELECT * FROM TPayrolls TP
--ORDER BY TP.intEmployeeID;



