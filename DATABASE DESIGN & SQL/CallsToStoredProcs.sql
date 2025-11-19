-- ========================================================================================
-- IT-112 – Final Project
-- CallsToStoredProcs.sql
-- Keith Brock
-- ========================================================================================
USE dbSQL1;
SET NOCOUNT ON;

-- --------------------------------------------------------------------------------
-- a) 8 patients for each study for screening (16 total)
-- --------------------------------------------------------------------------------
DECLARE @intPatientID INT;

-- Study 12345 (SiteIDs 1, 2, 3)
EXEC uspScreenPatient 1, '1980-01-01', 1, 150, '2025-07-01', @intPatientID OUTPUT; -- 1
EXEC uspScreenPatient 1, '1990-02-02', 2, 180, '2025-07-01', @intPatientID OUTPUT; -- 2
EXEC uspScreenPatient 2, '1985-03-03', 1, 160, '2025-07-01', @intPatientID OUTPUT; -- 3
EXEC uspScreenPatient 2, '1991-04-04', 2, 170, '2025-07-01', @intPatientID OUTPUT; -- 4
EXEC uspScreenPatient 3, '1982-05-05', 1, 155, '2025-07-01', @intPatientID OUTPUT; -- 5
EXEC uspScreenPatient 3, '1993-06-06', 2, 165, '2025-07-01', @intPatientID OUTPUT; -- 6
EXEC uspScreenPatient 1, '1987-07-07', 1, 175, '2025-07-01', @intPatientID OUTPUT; -- 7
EXEC uspScreenPatient 2, '1995-08-08', 2, 185, '2025-07-01', @intPatientID OUTPUT; -- 8

-- Study 54321 (SiteIDs 4, 5, 6)
EXEC uspScreenPatient 4, '1980-09-09', 1, 150, '2025-07-01', @intPatientID OUTPUT; -- 9
EXEC uspScreenPatient 4, '1990-10-10', 2, 180, '2025-07-01', @intPatientID OUTPUT; -- 10
EXEC uspScreenPatient 5, '1985-11-11', 1, 160, '2025-07-01', @intPatientID OUTPUT; -- 11
EXEC uspScreenPatient 5, '1991-12-12', 2, 170, '2025-07-01', @intPatientID OUTPUT; -- 12
EXEC uspScreenPatient 6, '1982-01-13', 1, 155, '2025-07-01', @intPatientID OUTPUT; -- 13
EXEC uspScreenPatient 6, '1993-02-14', 2, 165, '2025-07-01', @intPatientID OUTPUT; -- 14
EXEC uspScreenPatient 4, '1987-03-15', 1, 175, '2025-07-01', @intPatientID OUTPUT; -- 15
EXEC uspScreenPatient 5, '1995-04-16', 2, 185, '2025-07-01', @intPatientID OUTPUT; -- 16

-- --------------------------------------------------------------------------------
-- b) 5 patients randomized for each study (10 total)
-- --------------------------------------------------------------------------------
-- Study 12345
EXEC uspRandomizePatient @intStudyID = 1;
EXEC uspRandomizePatient @intStudyID = 1;
EXEC uspRandomizePatient @intStudyID = 1;
EXEC uspRandomizePatient @intStudyID = 1;
EXEC uspRandomizePatient @intStudyID = 1;

-- Study 54321
EXEC uspRandomizePatient @intStudyID = 2;
EXEC uspRandomizePatient @intStudyID = 2;
EXEC uspRandomizePatient @intStudyID = 2;
EXEC uspRandomizePatient @intStudyID = 2;
EXEC uspRandomizePatient @intStudyID = 2;

-- --------------------------------------------------------------------------------
-- c) 4 patients (2 randomized + 2 not randomized) withdrawn from each study
-- --------------------------------------------------------------------------------
-- Study 12345
EXEC uspWithdrawPatient 1, '2025-08-20', 1; -- randomized
EXEC uspWithdrawPatient 2, '2025-08-20', 2; -- randomized
EXEC uspWithdrawPatient 6, '2025-08-20', 3; -- not randomized
EXEC uspWithdrawPatient 7, '2025-08-20', 5; -- not randomized

-- Study 54321
EXEC uspWithdrawPatient 9,  '2025-08-21', 1; -- randomized
EXEC uspWithdrawPatient 10, '2025-08-21', 2; -- randomized
EXEC uspWithdrawPatient 14, '2025-08-21', 3; -- not randomized
EXEC uspWithdrawPatient 16, '2025-08-21', 5; -- not randomized
GO

-- --------------------------------------------------------------------------------
-- Final Project Query
-- --------------------------------------------------------------------------------
SELECT * FROM vPatients
SELECT * FROM vRandomizedPatients
SELECT * FROM vRandomCodes
SELECT * FROM vAvailableRandomCodes
SELECT * FROM vVisits
SELECT * FROM vDrugKits
SELECT * FROM vAvailableDrugKits
SELECT * FROM vWithdrawals

SELECT
     TP.intPatientID
    ,TR.intRandomCodeID
    ,TV.intVisitID
    ,TR.intRandomCode
    ,TR.strTreatment
    ,TD.intDrugKitNumber
    ,TD.strTreatment
    ,TD.intSiteID       AS DrugSite
    ,TP.intSiteID       AS PatSite
    ,TS.intSiteNumber
    ,TP.intPatientNumber
FROM
     TPatients    AS TP
    ,TVisits      AS TV
    ,TRandomCodes AS TR
    ,TDrugKits    AS TD
    ,TSites       AS TS
WHERE
     TP.intPatientID      = TV.intPatientID
 AND TP.intRandomCodeID  = TR.intRandomCodeID
 AND TV.intVisitID       = TD.intVisitID
 AND TS.intSiteID        = TP.intSiteID;
GO
