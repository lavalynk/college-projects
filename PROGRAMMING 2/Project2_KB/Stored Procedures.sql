USE dbFlyMe2theMoon
GO

CREATE PROCEDURE uspAddAttendant
    @strFirstName VARCHAR(255),
    @strLastName VARCHAR(255),
    @strEmployeeID VARCHAR(255),
    @dtmDateOfHire DATE,
    @dtmDateOfTermination DATE,
    @strLoginID VARCHAR(255),
    @strPassword VARCHAR(255)
AS
BEGIN
    DECLARE @intAttendantID INT;
    DECLARE @intEmployeeKeyID INT;

    -- Calculate the next ID value for intAttendantID
    SELECT @intAttendantID = ISNULL(MAX(intAttendantID), 0) + 1 FROM TAttendants;

    -- Calculate the next ID value for intEmployeeKeyID
    SELECT @intEmployeeKeyID = ISNULL(MAX(intEmployeeKeyID), 0) + 1 FROM TEmployees;

    -- Insert into TAttendants
    INSERT INTO TAttendants (intAttendantID, strFirstName, strLastName, strEmployeeID, dtmDateofHire, dtmDateofTermination)
    VALUES (@intAttendantID, @strFirstName, @strLastName, @strEmployeeID, @dtmDateOfHire, @dtmDateOfTermination);

    -- Insert into TEmployees table
    INSERT INTO TEmployees (intEmployeeKeyID, intEmployeeID, strEmployeeLoginID, strEmployeePassword, strEmployeeRole)
    VALUES (@intEmployeeKeyID, @intAttendantID, @strLoginID, @strPassword, 'Attendant');
END
GO

CREATE PROCEDURE uspAddEmployee
    @intEmployeeKeyID          AS INTEGER OUTPUT,
    @strEmployeeLoginID        AS VARCHAR(255),
    @strEmployeePassword       AS VARCHAR(255),
    @strEmployeeRole           AS VARCHAR(255),
    @intEmployeeID             AS INTEGER
AS
BEGIN
    SET XACT_ABORT ON -- terminate and rollback if any errors
    BEGIN TRANSACTION
    
    -- Get the next primary key value for intEmployeeKeyID
    SELECT @intEmployeeKeyID = ISNULL(MAX(intEmployeeKeyID), 0) + 1 
    FROM TEmployees (TABLOCKX) -- lock table until end of transaction
    
    -- Insert the new employee record
    INSERT INTO TEmployees (intEmployeeKeyID, strEmployeeLoginID, strEmployeePassword, strEmployeeRole, intEmployeeID)
    VALUES (@intEmployeeKeyID, @strEmployeeLoginID, @strEmployeePassword, @strEmployeeRole, @intEmployeeID)

    COMMIT TRANSACTION
END
GO

CREATE PROCEDURE uspAddPassenger
     @intPassengerID          AS INTEGER OUTPUT,
     @strFirstName            AS VARCHAR(255),
     @strLastName             AS VARCHAR(255),
     @strAddress              AS VARCHAR(255),
     @strCity                 AS VARCHAR(255),
     @intStateID              AS INTEGER,
     @strZip                  AS VARCHAR(255),
     @strPhoneNumber          AS VARCHAR(255),
     @strEmail                AS VARCHAR(255),
     @strPassengerLogin       AS VARCHAR(255),
     @strPassword             AS VARCHAR(255),
     @dtmPassengerDOB         AS DATETIME
AS
SET XACT_ABORT ON -- terminate and rollback if any errors
BEGIN TRANSACTION
    -- Get the next primary key value for intPassengerID
    SELECT @intPassengerID = MAX(intPassengerID) + 1 
    FROM TPassengers (TABLOCKX) -- lock table until end of transaction
    -- Default to 1 if table is empty
    SELECT @intPassengerID = COALESCE(@intPassengerID, 1)
    
    -- Insert the new passenger record
    INSERT INTO TPassengers (intPassengerID, strFirstName, strLastName, strAddress, strCity, intStateID, strZip, strPhoneNumber, strEmail, strPassengerLogin, strPassword, dtmPassengerDOB)
    VALUES (@intPassengerID, @strFirstName, @strLastName, @strAddress, @strCity, @intStateID, @strZip, @strPhoneNumber, @strEmail, @strPassengerLogin, @strPassword, @dtmPassengerDOB)
COMMIT TRANSACTION
GO

CREATE PROCEDURE uspAddPilot
    @intPilotID                AS INTEGER OUTPUT,
    @strFirstName              AS VARCHAR(255),
    @strLastName               AS VARCHAR(255),
    @strEmployeeID             AS VARCHAR(255),
    @dtmDateOfHire             AS DATETIME,
    @dtmDateOfTermination      AS DATETIME,
    @dtmDateOfLicense          AS DATETIME,
    @intPilotRoleID            AS INTEGER
AS
BEGIN
    SET XACT_ABORT ON -- terminate and rollback if any errors
    BEGIN TRANSACTION

    -- Check if EmployeeID already exists in TPilots or TAttendants
    IF EXISTS (SELECT 1 FROM TPilots WHERE strEmployeeID = @strEmployeeID)
    BEGIN
        RAISERROR ('Employee ID already exists in TPilots', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END
    IF EXISTS (SELECT 1 FROM TAttendants WHERE strEmployeeID = @strEmployeeID)
    BEGIN
        RAISERROR ('Employee ID already exists in TAttendants', 16, 1)
        ROLLBACK TRANSACTION
        RETURN
    END

    -- Get the next primary key value for intPilotID
    SELECT @intPilotID = ISNULL(MAX(intPilotID), 0) + 1 
    FROM TPilots (TABLOCKX) -- lock table until end of transaction
    
    -- Insert the new pilot record
    INSERT INTO TPilots (intPilotID, strFirstName, strLastName, strEmployeeID, dtmDateOfHire, dtmDateOfTermination, dtmDateOfLicense, intPilotRoleID)
    VALUES (@intPilotID, @strFirstName, @strLastName, @strEmployeeID, @dtmDateOfHire, @dtmDateOfTermination, @dtmDateOfLicense, @intPilotRoleID)

    COMMIT TRANSACTION
END
GO

CREATE PROCEDURE uspDeletePilot
     @intPilotID			AS INTEGER  
          
AS
SET XACT_ABORT ON --terminate and rollback if any errors
BEGIN TRANSACTION
  
    Delete  FROM TPilots
	WHERE  intPilotID = @intPilotID

COMMIT TRANSACTION
GO

CREATE PROCEDURE uspGetAttendantsMiles
AS
BEGIN
    SELECT TA.strFirstName + ' ' + TA.strLastName AS AttendantName, ISNULL(SUM(TF.intMilesFlown), 0) AS TotalMiles 
    FROM TAttendants AS TA 
    LEFT JOIN TAttendantFlights AS TAF ON TA.intAttendantID = TAF.intAttendantID 
    LEFT JOIN TFlights AS TF ON TAF.intFlightID = TF.intFlightID AND TF.dtmFlightDate <= GETDATE()
    GROUP BY TA.strFirstName, TA.strLastName
END
GO

CREATE PROCEDURE uspGetAverageMiles
AS
BEGIN
    SELECT AVG(TF.intMilesFlown) AS AvgMiles 
    FROM TFlightPassengers AS TFP 
    JOIN TFlights AS TF ON TFP.intFlightID = TF.intFlightID
END
GO

CREATE PROCEDURE uspGetNextAttendantID
AS
BEGIN
    SELECT ISNULL(MAX(intAttendantID), 0) + 1 AS intNextPrimaryKey
    FROM TAttendants
END
GO

CREATE PROCEDURE uspGetNextPilotID
AS
BEGIN
    SELECT ISNULL(MAX(intPilotID), 0) + 1 AS intNextPrimaryKey
    FROM TPilots
END
GO

CREATE PROCEDURE uspGetPassengerData
    @passengerID AS INTEGER
AS
BEGIN
    SELECT intPassengerID, strFirstName, strLastName, strAddress, strCity, intStateID, strZip, strPhoneNumber, strEmail, strPassengerLogin, strPassword, dtmPassengerDOB
    FROM TPassengers 
    WHERE intPassengerID = @passengerID;
END
GO

CREATE PROCEDURE uspGetAllPilots
AS
BEGIN
    SELECT intPilotID, (strFirstName + ' ' + strLastName) AS strPilotName
    FROM TPilots
END
GO

CREATE PROCEDURE uspGetPilotData
    @pilotID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        P.strFirstName,
        P.strLastName,
        P.strEmployeeID,
        P.dtmDateOfHire,
        P.dtmDateOfTermination,
        P.dtmDateOfLicense,
        P.intPilotRoleID,
        E.strEmployeeLoginID,
        E.strEmployeePassword
    FROM TPilots as P
    INNER JOIN TEmployees as E ON P.intPilotID = E.intEmployeeID
    WHERE P.intPilotID = @pilotID AND E.strEmployeeRole = 'Pilot'

    SET NOCOUNT OFF;
END
GO

CREATE PROCEDURE uspGetPilotRoles
AS
BEGIN
    SELECT intPilotRoleID, strPilotRole 
    FROM TPilotRoles;
END
GO

CREATE PROCEDURE uspGetPilotsList
AS
BEGIN
    SELECT
        intPilotID,
        strFirstName + ' ' + strLastName AS PilotName
    FROM
        TPilots
END
GO

CREATE PROCEDURE uspGetPilotsMiles
AS
BEGIN
    SELECT TP.strFirstName + ' ' + TP.strLastName AS PilotName, ISNULL(SUM(TF.intMilesFlown), 0) AS TotalMiles 
    FROM TPilots AS TP 
    LEFT JOIN TPilotFlights AS TPF ON TP.intPilotID = TPF.intPilotID 
    LEFT JOIN TFlights AS TF ON TPF.intFlightID = TF.intFlightID AND TF.dtmFlightDate <= GETDATE()
    GROUP BY TP.strFirstName, TP.strLastName
END
GO

CREATE PROCEDURE uspGetStates
AS
BEGIN
    SELECT intStateID, strState 
    FROM TStates
END
GO

CREATE PROCEDURE uspGetTotalCustomers
AS
BEGIN
    SELECT COUNT(intPassengerID) AS TotalCustomers FROM TPassengers
END
GO

CREATE PROCEDURE uspGetTotalFlights
AS
BEGIN
    SELECT COUNT(intFlightID) AS TotalFlights FROM TFlightPassengers
END
GO

CREATE PROCEDURE uspUpdatePassenger
     @intPassengerID         AS INTEGER,
     @strFirstName           AS VARCHAR(255),
     @strLastName            AS VARCHAR(255),
     @strAddress             AS VARCHAR(255),
     @strCity                AS VARCHAR(255),
     @intStateID             AS INTEGER,
     @strZip                 AS VARCHAR(255),
     @strPhoneNumber         AS VARCHAR(255),
     @strEmail               AS VARCHAR(255),
     @strPassengerLogin      AS VARCHAR(255),
     @strPassword			 AS VARCHAR(255),
     @dtmPassengerDOB        AS DATETIME
AS
SET XACT_ABORT ON --terminate and rollback if any errors
BEGIN TRANSACTION

    UPDATE TPassengers
    SET 
        strFirstName = @strFirstName,
        strLastName = @strLastName,
        strAddress = @strAddress,
        strCity = @strCity,
        intStateID = @intStateID,
        strZip = @strZip,
        strPhoneNumber = @strPhoneNumber,
        strEmail = @strEmail,
        strPassengerLogin = @strPassengerLogin,
        strPassword = @strPassword,
        dtmPassengerDOB = @dtmPassengerDOB
    WHERE intPassengerID = @intPassengerID

COMMIT TRANSACTION
GO

CREATE PROCEDURE uspUpdatePilot
    @intPilotID INTEGER,
    @strFirstName VARCHAR(255),
    @strLastName VARCHAR(255),
    @strEmployeeID VARCHAR(255),
    @dtmDateOfHire DATETIME,
    @dtmDateOfTermination DATETIME,
    @dtmDateOfLicense DATETIME,
    @intPilotRoleID INTEGER,
    @strEmployeeLoginID VARCHAR(255),
    @strEmployeePassword VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Update pilot data in TPilots
    UPDATE TPilots
    SET 
        strFirstName = @strFirstName,
        strLastName = @strLastName,
        strEmployeeID = @strEmployeeID,
        dtmDateOfHire = @dtmDateOfHire,
        dtmDateOfTermination = @dtmDateOfTermination,
        dtmDateOfLicense = @dtmDateOfLicense,
        intPilotRoleID = @intPilotRoleID
    WHERE intPilotID = @intPilotID;

    -- Update login data in TEmployees
    UPDATE TEmployees
    SET 
        strEmployeeLoginID = @strEmployeeLoginID,
        strEmployeePassword = @strEmployeePassword
    WHERE intEmployeeID = @intPilotID AND strEmployeeRole = 'Pilot';

    SET NOCOUNT OFF;
END;
GO




CREATE PROCEDURE uspUpdatePilots
    @intPilotID INT,
    @strFirstName NVARCHAR(255),
    @strLastName NVARCHAR(255),
    @strEmployeeID NVARCHAR(255),
    @dtmDateOfHire DATE,
    @dtmDateOfTermination DATE,
    @dtmDateOfLicense DATE,
    @intPilotRoleID INT,
    @strEmployeeLoginID NVARCHAR(255),
    @strEmployeePassword NVARCHAR(255)
AS
BEGIN
    UPDATE TPilots
    SET 
        strFirstName = @strFirstName,
        strLastName = @strLastName,
        strEmployeeID = @strEmployeeID,
        dtmDateOfHire = @dtmDateOfHire,
        dtmDateOfTermination = @dtmDateOfTermination,
        dtmDateOfLicense = @dtmDateOfLicense,
        intPilotRoleID = @intPilotRoleID
    WHERE intPilotID = @intPilotID;

    UPDATE TEmployees
    SET
        strEmployeeLoginID = @strEmployeeLoginID,
        strEmployeePassword = @strEmployeePassword
    WHERE intEmployeeID = @intPilotID;
END
GO

CREATE PROCEDURE uspValidateEmployeeLogin
    @LoginID VARCHAR(255),
    @Password VARCHAR(255)
AS
BEGIN
    SELECT intEmployeeID, strEmployeeRole
    FROM TEmployees
    WHERE strEmployeeLoginID = @LoginID AND strEmployeePassword = @Password
END
GO

CREATE PROCEDURE uspValidatePassengerLogin
    @PassengerLoginID VARCHAR(255),
    @PassengerPassword VARCHAR(255)
AS
BEGIN
    SELECT intPassengerID
    FROM TPassengers
    WHERE strPassengerLogin = @PassengerLoginID
      AND strPassword = @PassengerPassword
END
GO

CREATE PROCEDURE uspInsertFlight
    @FlightNumber VARCHAR(10),
    @FlightDate DATE,
    @TimeOfDeparture TIME,
    @TimeOfLanding TIME,
    @DepartingAirportID INTEGER,
    @ArrivingAirportID INTEGER,
    @MilesFlown INTEGER,
    @PlaneID INTEGER
AS
BEGIN
    DECLARE @NextFlightID INTEGER;

    -- Find the next available primary key for intFlightID
    SELECT @NextFlightID = ISNULL(MAX(intFlightID), 0) + 1 FROM TFlights;

    -- Insert the new flight record into TFlights table
    INSERT INTO TFlights (intFlightID, strFlightNumber, dtmFlightDate, dtmTimeOfDeparture, dtmTimeOfLanding, intFromAirportID, intToAirportID, intMilesFlown, intPlaneID)
    VALUES (@NextFlightID, @FlightNumber, @FlightDate, @TimeOfDeparture, @TimeOfLanding, @DepartingAirportID, @ArrivingAirportID, @MilesFlown, @PlaneID);
END
GO