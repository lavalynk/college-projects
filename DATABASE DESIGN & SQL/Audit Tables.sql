-- --------------------------------------------------------------------------------
-- Name: Keith Brock
-- Class: IT-112
-- Abstract: Audit Tables and Triggers
-- --------------------------------------------------------------------------------

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbSQL1;     -- Get out of the master database
SET NOCOUNT ON; -- Report only errors


-- --------------------------------------------------------------------------------
-- Drop Tables
-- --------------------------------------------------------------------------------
IF OBJECT_ID('Z_TTeamPlayers') IS NOT NULL DROP TABLE Z_TTeamPlayers;
IF OBJECT_ID('Z_TPlayers') IS NOT NULL DROP TABLE Z_TPlayers;
IF OBJECT_ID('Z_TTeams') IS NOT NULL DROP TABLE Z_TTeams;
IF OBJECT_ID('TTeamPlayers') IS NOT NULL DROP TABLE TTeamPlayers;
IF OBJECT_ID('TPlayers') IS NOT NULL DROP TABLE TPlayers;
IF OBJECT_ID('TTeams') IS NOT NULL DROP TABLE TTeams;

-- --------------------------------------------------------------------------------
-- Step #1.1: Create Tables
-- --------------------------------------------------------------------------------

-- Start of Homework: 5
-- 1)	Add strModified_Reason column to each table.
CREATE TABLE TTeams
(
	 intTeamID					INTEGER	IDENTITY	NOT NULL
	,strTeam					VARCHAR(50)			NOT NULL
	,strMascot					VARCHAR(50)			NOT NULL
	,strModified_Reason			VARCHAR(1000)
	,CONSTRAINT TTeams_PK		PRIMARY KEY ( intTeamID )
)

CREATE TABLE TPlayers
(
	 intPlayerID				INTEGER	  IDENTITY	NOT NULL
	,strFirstName				VARCHAR(50)			NOT NULL
	,strLastName				VARCHAR(50)			NOT NULL
	,strModified_Reason			VARCHAR(1000)
	,CONSTRAINT TPlayers_PK		PRIMARY KEY ( intPlayerID )
)

CREATE TABLE TTeamPlayers
(
	 intTeamPlayerID			INTEGER IDENTITY	NOT NULL
	,intTeamID					INTEGER				NOT NULL
	,intPlayerID				INTEGER				NOT NULL
	,strModified_Reason			VARCHAR(1000)
	,CONSTRAINT PlayerTeam_UQ	UNIQUE ( intTeamID, intPlayerID )
	,CONSTRAINT TTeamPlayers_PK PRIMARY KEY ( intTeamPlayerID )
)

-- --------------------------------------------------------------------------------
-- Step #1.2: Identify and Create Foreign Keys
-- --------------------------------------------------------------------------------
--
-- #	Child								Parent						Column(s)
-- -	-----								------						---------
-- 1	TTeamPlayers						TTeams						intTeamID
-- 2	TTeamPlayers						TPlayers					intPlayerID

-- 1
ALTER TABLE TTeamPlayers ADD CONSTRAINT TTeamPlayers_TTeams_FK
FOREIGN KEY ( intTeamID ) REFERENCES TTeams ( intTeamID )

-- 2
ALTER TABLE TTeamPlayers ADD CONSTRAINT TTeamPlayers_TPlayers_FK
FOREIGN KEY ( intPlayerID ) REFERENCES TPlayers ( intPlayerID )

-- --------------------------------------------------------------------------------
-- Step #1.3: Add at least 3 teams
-- --------------------------------------------------------------------------------
INSERT INTO TTeams ( strTeam, strMascot )
VALUES	 ( 'Reds', 'Mr. Red' )
		,( 'Bengals', 'Bengal Tiger' )
		,( 'Duke', 'Blue Devils' )
		
-- --------------------------------------------------------------------------------
-- Step #1.4: Add at least 3 players
-- --------------------------------------------------------------------------------
INSERT INTO TPlayers ( strFirstName, strLastName )
VALUES	 ( 'Joey', 'Votto' )
		,( 'Joe', 'Morgn' )
		,( 'Christian', 'Laettner' )
		,( 'Andy', 'Dalton' )
		
-- --------------------------------------------------------------------------------
-- Step #1.5: Add at at least 6 team/player assignments
-- --------------------------------------------------------------------------------
INSERT INTO TTeamPlayers ( intTeamID, intPlayerID )
VALUES	 ( 1, 1 )
		,( 1, 2 )
		,( 2, 4 )
		,( 3, 3 )

-- ============================================================================
-- 2)	Add Z_<table> for each table.
-- ============================================================================
CREATE TABLE Z_TTeams (
    intTeamAuditID INT IDENTITY NOT NULL,
    intTeamID INT NOT NULL,
    strTeam VARCHAR(50) NOT NULL,
    strMascot VARCHAR(50) NOT NULL,
    UpdatedBy VARCHAR(128) NOT NULL,
    UpdatedOn DATETIME NOT NULL,
    strAction VARCHAR(1) NOT NULL,
    strModified_Reason VARCHAR(1000),
    CONSTRAINT Z_TTeams_PK PRIMARY KEY (intTeamAuditID)
);

CREATE TABLE Z_TPlayers (
    intPlayerAuditID INT IDENTITY NOT NULL,
    intPlayerID INT NOT NULL,
    strFirstName VARCHAR(50) NOT NULL,
    strLastName VARCHAR(50) NOT NULL,
    UpdatedBy VARCHAR(128) NOT NULL,
    UpdatedOn DATETIME NOT NULL,
    strAction VARCHAR(1) NOT NULL,
    strModified_Reason VARCHAR(1000),
    CONSTRAINT Z_TPlayers_PK PRIMARY KEY (intPlayerAuditID)
);

CREATE TABLE Z_TTeamPlayers (
    intAuditID INT IDENTITY NOT NULL,
    intTeamPlayerID INT NOT NULL,
    intTeamID INT NOT NULL,
    intPlayerID INT NOT NULL,
    UpdatedBy VARCHAR(128) NOT NULL,
    UpdatedOn DATETIME NOT NULL,
    strAction VARCHAR(1) NOT NULL,
    strModified_Reason VARCHAR(1000),
    CONSTRAINT Z_TTeamPlayers_PK PRIMARY KEY (intAuditID)
);

-- =============================================================================
-- 3)	Create Trigger for each table.  
-- =============================================================================

-- =============================================================================
-- Trigger for TTeams
-- =============================================================================
GO
CREATE TRIGGER trg_TTeams_Audit ON TTeams
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Action VARCHAR(1);
    DECLARE @Now DATETIME = GETDATE();

    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @Action = 'U';
    ELSE IF EXISTS (SELECT * FROM inserted)
        SET @Action = 'I';
    ELSE
        SET @Action = 'D';

    IF (@Action = 'I')
    BEGIN
        INSERT INTO Z_TTeams (intTeamID, strTeam, strMascot, UpdatedBy, UpdatedOn, strAction, strModified_Reason)
        SELECT I.intTeamID, I.strTeam, I.strMascot, SUSER_SNAME(), @Now, @Action, I.strModified_Reason
        FROM inserted I
        INNER JOIN TTeams T ON T.intTeamID = I.intTeamID;
    END
    ELSE IF (@Action = 'U')
    BEGIN
        IF EXISTS (
            SELECT TOP 1 I.strModified_Reason
            FROM inserted I
            INNER JOIN TTeams T ON T.intTeamID = I.intTeamID
            WHERE I.strModified_Reason IS NOT NULL AND I.strModified_Reason <> ''
        )
        BEGIN
            INSERT INTO Z_TTeams (intTeamID, strTeam, strMascot, UpdatedBy, UpdatedOn, strAction, strModified_Reason)
            SELECT I.intTeamID, I.strTeam, I.strMascot, SUSER_SNAME(), @Now, @Action, I.strModified_Reason
            FROM inserted I
            INNER JOIN TTeams T ON T.intTeamID = I.intTeamID;

            UPDATE TTeams
            SET strModified_Reason = NULL
            WHERE intTeamID IN (SELECT intTeamID FROM inserted);
        END
        ELSE
        BEGIN
            PRINT 'Update requires a modification reason.';
            ROLLBACK;
        END
    END
    ELSE
    BEGIN
        INSERT INTO Z_TTeams (intTeamID, strTeam, strMascot, UpdatedBy, UpdatedOn, strAction, strModified_Reason)
        SELECT D.intTeamID, D.strTeam, D.strMascot, SUSER_SNAME(), @Now, @Action, ''
        FROM deleted D;
    END
END
GO

-- =============================================================================
-- Trigger for TPlayers
-- =============================================================================
GO
CREATE TRIGGER trg_TPlayers_Audit ON TPlayers
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Action VARCHAR(1);
    DECLARE @Now DATETIME = GETDATE();

    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @Action = 'U';
    ELSE IF EXISTS (SELECT * FROM inserted)
        SET @Action = 'I';
    ELSE
        SET @Action = 'D';

    IF (@Action = 'I')
    BEGIN
        INSERT INTO Z_TPlayers (intPlayerID, strFirstName, strLastName, UpdatedBy, UpdatedOn, strAction, strModified_Reason)
        SELECT I.intPlayerID, I.strFirstName, I.strLastName, SUSER_SNAME(), @Now, @Action, I.strModified_Reason
        FROM inserted I
        INNER JOIN TPlayers T ON T.intPlayerID = I.intPlayerID;
    END
    ELSE IF (@Action = 'U')
    BEGIN
        IF EXISTS (
            SELECT TOP 1 I.strModified_Reason
            FROM inserted I
            INNER JOIN TPlayers T ON T.intPlayerID = I.intPlayerID
            WHERE I.strModified_Reason IS NOT NULL AND I.strModified_Reason <> ''
        )
        BEGIN
            INSERT INTO Z_TPlayers (intPlayerID, strFirstName, strLastName, UpdatedBy, UpdatedOn, strAction, strModified_Reason)
            SELECT I.intPlayerID, I.strFirstName, I.strLastName, SUSER_SNAME(), @Now, @Action, I.strModified_Reason
            FROM inserted I
            INNER JOIN TPlayers T ON T.intPlayerID = I.intPlayerID;

            UPDATE TPlayers
            SET strModified_Reason = NULL
            WHERE intPlayerID IN (SELECT intPlayerID FROM inserted);
        END
        ELSE
        BEGIN
            PRINT 'Update requires a modification reason.';
            ROLLBACK;
        END
    END
    ELSE
    BEGIN
        INSERT INTO Z_TPlayers (intPlayerID, strFirstName, strLastName, UpdatedBy, UpdatedOn, strAction, strModified_Reason)
        SELECT D.intPlayerID, D.strFirstName, D.strLastName, SUSER_SNAME(), @Now, @Action, ''
        FROM deleted D;
    END
END
GO

-- =============================================================================
-- Trigger for TTeamPlayers
-- =============================================================================
GO
CREATE TRIGGER trg_TTeamPlayers_Audit ON TTeamPlayers
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Action VARCHAR(1);
    DECLARE @Now DATETIME = GETDATE();

    -- Determine the action type
    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
        SET @Action = 'U';
    ELSE IF EXISTS (SELECT * FROM inserted)
        SET @Action = 'I';
    ELSE
        SET @Action = 'D';

    -- INSERT logic
    IF (@Action = 'I')
    BEGIN
        INSERT INTO Z_TTeamPlayers (intTeamPlayerID, intTeamID, intPlayerID, UpdatedBy, UpdatedOn, strAction, strModified_Reason)
        SELECT I.intTeamPlayerID, I.intTeamID, I.intPlayerID, SUSER_SNAME(), @Now, @Action, I.strModified_Reason
        FROM inserted I
        INNER JOIN TTeamPlayers T ON T.intTeamPlayerID = I.intTeamPlayerID;
    END

    -- UPDATE logic
    ELSE IF (@Action = 'U')
    BEGIN
        IF EXISTS (
            SELECT TOP 1 I.strModified_Reason
            FROM inserted I
            WHERE I.strModified_Reason IS NOT NULL AND I.strModified_Reason <> ''
        )
        BEGIN
            INSERT INTO Z_TTeamPlayers (intTeamPlayerID, intTeamID, intPlayerID, UpdatedBy, UpdatedOn, strAction, strModified_Reason)
            SELECT I.intTeamPlayerID, I.intTeamID, I.intPlayerID, SUSER_SNAME(), @Now, @Action, I.strModified_Reason
            FROM inserted I;

            UPDATE TTeamPlayers
            SET strModified_Reason = NULL
            WHERE intTeamPlayerID IN (SELECT intTeamPlayerID FROM inserted);
        END
        ELSE
        BEGIN
            PRINT 'Update requires a modification reason.';
            ROLLBACK;
        END
    END

    -- DELETE logic
    ELSE
    BEGIN
        INSERT INTO Z_TTeamPlayers (intTeamPlayerID, intTeamID, intPlayerID, UpdatedBy, UpdatedOn, strAction, strModified_Reason)
        SELECT D.intTeamPlayerID, D.intTeamID, D.intPlayerID, SUSER_SNAME(), @Now, @Action, ''
        FROM deleted D;
    END
END
GO

-- ============================================================================
--Problem 1:
--Using the Script for provided with this assignment do the following. 

--Create Audit tables and Triggers for the 3 tables in the script.

--1.	Create a test for DELECT and UPDATE in each table in the following format

--•	DELETE FROM TTeams
--•	WHERE <some condition>
--•	SELECT * FROM TTeams
--•	SELECT * FROM Z_TTeams

--2.	After you test your calls and confirm they are working comment all test call lines out so just the script runs without the calls to DELETE and UPDATE (from step #1)

-- ============================================================================
 
--UPDATE TTeams 
--SET strMascot = 'Gapper', strModified_Reason = 'Updated to new mascot'
--WHERE strTeam = 'Reds';

--DELETE FROM TTeamPlayers WHERE intTeamID = 1;
--DELETE FROM TTeams WHERE strTeam = 'Reds';

--SELECT * FROM TTeams;
--SELECT * FROM Z_TTeams;

--UPDATE TPlayers 
--SET strFirstName = 'Joey B', strModified_Reason = 'Nickname update'
--WHERE strFirstName = 'Joey';

--DELETE FROM TTeamPlayers WHERE intPlayerID = 1;
--DELETE FROM TPlayers WHERE strFirstName = 'Joey B';

--SELECT * FROM TPlayers;
--SELECT * FROM Z_TPlayers;

--UPDATE TTeamPlayers 
--SET intTeamID = 3, strModified_Reason = 'Moved player to Duke'
--WHERE intTeamPlayerID = 3;

--DELETE FROM TTeamPlayers 
--WHERE intTeamPlayerID = 3;

--SELECT * FROM TTeamPlayers;
--SELECT * FROM Z_TTeamPlayers;
