-- --------------------------------------------------------------------------------
-- Name: Keith Brock
 
-- Abstract: FlyMe2theMoon
-- --------------------------------------------------------------------------------

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
USE dbFlyMe2theMoon;     
SET NOCOUNT ON;  

-- --------------------------------------------------------------------------------
--						Problem #10
-- --------------------------------------------------------------------------------

-- Drop Table Statements

IF OBJECT_ID ('TPilotFlights')			IS NOT NULL DROP TABLE TPilotFlights
IF OBJECT_ID ('TAttendantFlights')		IS NOT NULL DROP TABLE TAttendantFlights
IF OBJECT_ID ('TFlightPassengers')		IS NOT NULL DROP TABLE TFlightPassengers
IF OBJECT_ID ('TMaintenanceMaintenanceWorkers')			IS NOT NULL DROP TABLE TMaintenanceMaintenanceWorkers

IF OBJECT_ID ('TEmployees')				IS NOT NULL DROP TABLE TEmployees
IF OBJECT_ID ('TPassengers')			IS NOT NULL DROP TABLE TPassengers
IF OBJECT_ID ('TPilots')				IS NOT NULL DROP TABLE TPilots
IF OBJECT_ID ('TAttendants')			IS NOT NULL DROP TABLE TAttendants
IF OBJECT_ID ('TMaintenanceWorkers')	IS NOT NULL DROP TABLE TMaintenanceWorkers

IF OBJECT_ID ('TSeats')					IS NOT NULL DROP TABLE TSeats
IF OBJECT_ID ('TFlights')				IS NOT NULL DROP TABLE TFlights
IF OBJECT_ID ('TMaintenances')			IS NOT NULL DROP TABLE TMaintenances
IF OBJECT_ID ('TPlanes')				IS NOT NULL DROP TABLE TPlanes
IF OBJECT_ID ('TPlaneTypes')			IS NOT NULL DROP TABLE TPlaneTypes
IF OBJECT_ID ('TPilotRoles')			IS NOT NULL DROP TABLE TPilotRoles
IF OBJECT_ID ('TAirports')				IS NOT NULL DROP TABLE TAirports
IF OBJECT_ID ('TStates')				IS NOT NULL DROP TABLE TStates

-- --------------------------------------------------------------------------------
--	Step #1 : Create table 
-- --------------------------------------------------------------------------------

CREATE TABLE TPassengers
(
	 intPassengerID			INTEGER			NOT NULL
	,strFirstName			VARCHAR(255)	NOT NULL
	,strLastName			VARCHAR(255)	NOT NULL
	,strAddress				VARCHAR(255)	NOT NULL
	,strCity				VARCHAR(255)	NOT NULL
	,intStateID				INTEGER			NOT NULL
	,strZip					VARCHAR(255)	NOT NULL
	,strPhoneNumber			VARCHAR(255)	NOT NULL
	,strEmail				VARCHAR(255)	NOT NULL
	,strPassengerLogin		VARCHAR(255)	NOT NULL
	,strPassword			VARCHAR(255)	NOT NULL
	,dtmPassengerDOB		DATETIME		NOT NULL
	,CONSTRAINT TPassengers_PK PRIMARY KEY ( intPassengerID )
)


CREATE TABLE TPilots
(
	 intPilotID				INTEGER			NOT NULL
	,strFirstName			VARCHAR(255)	NOT NULL
	,strLastName			VARCHAR(255)	NOT NULL
	,strEmployeeID			VARCHAR(255)	NOT NULL
	,dtmDateOfHire			DATETIME		NOT NULL
	,dtmDateOfTermination	DATETIME		NOT NULL
	,dtmDateOfLicense		DATETIME		NOT NULL
	,intPilotRoleID			INTEGER			NOT NULL

	,CONSTRAINT TPilots_PK PRIMARY KEY ( intPilotID )
)


CREATE TABLE TAttendants
(
	 intAttendantID			INTEGER			NOT NULL
	,strFirstName			VARCHAR(255)	NOT NULL
	,strLastName			VARCHAR(255)	NOT NULL
	,strEmployeeID			VARCHAR(255)	NOT NULL
	,dtmDateOfHire			DATETIME		NOT NULL
	,dtmDateOfTermination	DATETIME		NOT NULL
	,CONSTRAINT TAttendants_PK PRIMARY KEY ( intAttendantID )
)

CREATE TABLE TEmployees
(
	 intEmployeeKeyID		INTEGER			NOT NULL
	,strEmployeeLoginID		VARCHAR(255)	NOT NULL
	,strEmployeePassword	VARCHAR(255)	NOT NULL
	,strEmployeeRole		VARCHAR(255)	NOT NULL
	,intEmployeeID			INTEGER			NOT NULL
	,CONSTRAINT TEmployees_PK PRIMARY KEY ( intEmployeeKeyID )
)

CREATE TABLE TMaintenanceWorkers
(
	 intMaintenanceWorkerID	INTEGER			NOT NULL
	,strFirstName			VARCHAR(255)	NOT NULL
	,strLastName			VARCHAR(255)	NOT NULL
	,strEmployeeID			VARCHAR(255)	NOT NULL
	,dtmDateOfHire			DATETIME		NOT NULL
	,dtmDateOfTermination	DATETIME		NOT NULL
	,dtmDateOfCertification	DATETIME		NOT NULL
	,CONSTRAINT TMaintenanceWorkers_PK PRIMARY KEY ( intMaintenanceWorkerID )
)


CREATE TABLE TStates
(
	 intStateID			INTEGER			NOT NULL
	,strState			VARCHAR(255)	NOT NULL
	,CONSTRAINT TStates_PK PRIMARY KEY ( intStateID )
)


CREATE TABLE TFlights
(
	 intFlightID			INTEGER			NOT NULL
	,strFlightNumber		VARCHAR(255)	NOT NULL
	,dtmFlightDate			DATETIME		NOT NULL
	,dtmTimeofDeparture		TIME			NOT NULL
	,dtmTimeOfLanding		TIME			NOT NULL
	,intFromAirportID		INTEGER			NOT NULL
	,intToAirportID			INTEGER			NOT NULL
	,intMilesFlown			INTEGER			NOT NULL
	,intPlaneID				INTEGER			NOT NULL
	,CONSTRAINT TFlights_PK PRIMARY KEY ( intFlightID )
)


CREATE TABLE TMaintenances
(
	 intMaintenanceID		INTEGER			NOT NULL
	,strWorkCompleted		VARCHAR(8000)	NOT NULL
	,dtmMaintenanceDate		DATETIME		NOT NULL
	,intPlaneID				INTEGER			NOT NULL
	,CONSTRAINT TMaintenances_PK PRIMARY KEY ( intMaintenanceID )
)


CREATE TABLE TPlanes
(
	 intPlaneID				INTEGER			NOT NULL
	,strPlaneNumber			VARCHAR(255)	NOT NULL
	,intPlaneTypeID			INTEGER			NOT NULL
	,CONSTRAINT TPlanes_PK PRIMARY KEY ( intPlaneID )
)


CREATE TABLE TPlaneTypes	
(
	 intPlaneTypeID			INTEGER			NOT NULL
	,strPlaneType			VARCHAR(255)	NOT NULL
	,CONSTRAINT TPlaneTypes_PK PRIMARY KEY ( intPlaneTypeID )
)


CREATE TABLE TPilotRoles	
(
	 intPilotRoleID			INTEGER			NOT NULL
	,strPilotRole			VARCHAR(255)	NOT NULL
	,CONSTRAINT TPilotRoles_PK PRIMARY KEY ( intPilotRoleID )
)


CREATE TABLE TAirports
(
	 intAirportID			INTEGER			NOT NULL
	,strAirportCity			VARCHAR(255)	NOT NULL
	,strAirportCode			VARCHAR(255)	NOT NULL
	,CONSTRAINT TAirports_PK PRIMARY KEY ( intAirportID )
)


CREATE TABLE TPilotFlights
(
	 intPilotFlightID		INTEGER			NOT NULL
	,intPilotID				INTEGER			NOT NULL
	,intFlightID			INTEGER			NOT NULL
	,CONSTRAINT TPilotFlights_PK PRIMARY KEY ( intPilotFlightID )
)


CREATE TABLE TAttendantFlights
(
	 intAttendantFlightID		INTEGER			NOT NULL
	,intAttendantID				INTEGER			NOT NULL
	,intFlightID				INTEGER			NOT NULL
	,CONSTRAINT TAttendantFlights_PK PRIMARY KEY ( intAttendantFlightID )
)


CREATE TABLE TFlightPassengers
(
	 intFlightPassengerID		INTEGER			NOT NULL
	,intFlightID				INTEGER			NOT NULL
	,intPassengerID				INTEGER			NOT NULL
	,intSeatID					INTEGER			NOT NULL
	,decFlightCost				DECIMAL(10,2)	NOT NULL
	,CONSTRAINT TFlightPassengers_PK PRIMARY KEY ( intFlightPassengerID )
)

CREATE TABLE TSeats
(
	 intSeatID					INTEGER			NOT NULL
	,strSeat					VARCHAR(255)	NOT NULL
	,CONSTRAINT TSeats_PK PRIMARY KEY ( intSeatID )

)

CREATE TABLE TMaintenanceMaintenanceWorkers
(
	 intMaintenanceMaintenanceWorkerID		INTEGER			NOT NULL
	,intMaintenanceID						INTEGER			NOT NULL
	,intMaintenanceWorkerID					INTEGER			NOT NULL
	,intHours								INTEGER			NOT NULL
	,CONSTRAINT TMaintenanceMaintenanceWorkers_PK PRIMARY KEY ( intMaintenanceMaintenanceWorkerID )
)

-- --------------------------------------------------------------------------------
--	Step #2 : Establish Referential Integrity 
-- --------------------------------------------------------------------------------
--
-- #	Child							Parent						Column
-- -	-----							------						---------
-- 1	TPassengers						TStates						intStateID	
-- 2	TFlightPassenger				TPassengers					intPassengerID
-- 3	TFlights						TPlanes						intPlaneID
-- 4	TFlights						TAirports					intFromAiportID
-- 5	TFlights						TAirports					intToAiportID
-- 6	TPilotFlights					TFlights					intFlightID
-- 7	TAttendantFlights				TFlights					intFlightID
-- 8	TPilotFlights					TPilots						intPilotID
-- 9	TAttendantFlights				TAttendants					intAttendantID
-- 10	TPilots							TPilotRoles					intPilotRoleID
-- 11	TPlanes							TPlaneTypes					intPlaneTypeID
-- 12	TMaintenances					TPlanes						intPlaneID
-- 13	TMaintenanceMaintenanceWorkers	TMaintenance				intMaintenanceID
-- 14	TMaintenanceMaintenanceWorkers	TMaintenanceWorker			intMaintenanceWorkerID
-- 15	TFlightPassenger				TFlights					intFlightID
-- 16	TEmployees						TPilots						intEmployeeID
-- 17	TEmployees						TAttendants					intEmployeeID
-- 18	TFlightPassenger				TSeats						intSeatID

--1
ALTER TABLE TPassengers ADD CONSTRAINT TPassengers_TStates_FK 
FOREIGN KEY ( intStateID ) REFERENCES TStates ( intStateID )

--2
ALTER TABLE TFlightPassengers ADD CONSTRAINT TPFlightPassengers_TPassengers_FK 
FOREIGN KEY ( intPassengerID ) REFERENCES TPassengers ( intPassengerID ) ON DELETE CASCADE

--3
ALTER TABLE TFlights	 ADD CONSTRAINT TFlights_TPlanes_FK 
FOREIGN KEY ( intPlaneID ) REFERENCES TPlanes ( intPlaneID )

--4
ALTER TABLE TFlights	 ADD CONSTRAINT TFlights_TFromAirports_FK 
FOREIGN KEY ( intFromAirportID ) REFERENCES TAirports ( intAirportID )

--5
ALTER TABLE TFlights	 ADD CONSTRAINT TFlights_TToAirports_FK 
FOREIGN KEY ( intToAirportID ) REFERENCES TAirports ( intAirportID )

--6
ALTER TABLE TPilotFlights	 ADD CONSTRAINT TPilotFlights_TFlights_FK 
FOREIGN KEY ( intFlightID ) REFERENCES TFlights (intFlightID )  

--7
ALTER TABLE TAttendantFlights	 ADD CONSTRAINT TAttendantFlights_TFlights_FK 
FOREIGN KEY ( intFlightID ) REFERENCES TFlights (intFlightID ) 

--8
ALTER TABLE TPilotFlights	 ADD CONSTRAINT TPilotFlights_TPilots_FK 
FOREIGN KEY ( intPilotID ) REFERENCES TPilots (intPilotID ) ON DELETE CASCADE

--9
ALTER TABLE TAttendantFlights	 ADD CONSTRAINT TAttendantFlights_TAttendants_FK 
FOREIGN KEY ( intAttendantID ) REFERENCES TAttendants (intAttendantID )

--10
ALTER TABLE TPilots	 ADD CONSTRAINT TPilots_TPilotRoles_FK 
FOREIGN KEY ( intPilotRoleID ) REFERENCES TPilotRoles (intPilotRoleID )  

--11
ALTER TABLE TPlanes	 ADD CONSTRAINT TPlanes_TPlaneTypes_FK 
FOREIGN KEY ( intPlaneTypeID ) REFERENCES TPlaneTypes (intPlaneTypeID )  

--12
ALTER TABLE TMaintenances	 ADD CONSTRAINT TMaintenances_TPlanes_FK 
FOREIGN KEY ( intPlaneID ) REFERENCES TPlanes (intPlaneID )  

--13
ALTER TABLE TMaintenanceMaintenanceWorkers	 ADD CONSTRAINT TMaintenanceMaintenanceWorkers_TMaintenances_FK 
FOREIGN KEY ( intMaintenanceID ) REFERENCES TMaintenances (intMaintenanceID ) 

--14
ALTER TABLE TMaintenanceMaintenanceWorkers	 ADD CONSTRAINT TMaintenanceMaintenanceWorkers_TMaintenanceWorkers_FK 
FOREIGN KEY ( intMaintenanceWorkerID ) REFERENCES TMaintenanceWorkers (intMaintenanceWorkerID ) 

--15
ALTER TABLE TFlightPassengers	 ADD CONSTRAINT TFlightPassengers_TFlights_FK 
FOREIGN KEY ( intFlightID ) REFERENCES TFlights (intFlightID ) ON DELETE CASCADE 

-- 16
--ALTER TABLE TEmployees ADD CONSTRAINT TEmployees_TPilots_FK 
--FOREIGN KEY (intEmployeeID) REFERENCES TPilots(intPilotID)

-- 17
--ALTER TABLE TEmployees ADD CONSTRAINT TEmployees_TAttendants_FK 
--FOREIGN KEY (intEmployeeID) REFERENCES TAttendants(intAttendantID)

-- 18
ALTER TABLE TFlightPassengers
ADD CONSTRAINT TFlightPassengers_TSeats_FK
FOREIGN KEY (intSeatID) REFERENCES TSeats(intSeatID)



-- --------------------------------------------------------------------------------
--	Step #3 : Add Data - INSERTS
-- --------------------------------------------------------------------------------
INSERT INTO TStates( intStateID, strState)
VALUES				(1, 'Ohio')
				   ,(2, 'Kentucky')
				   ,(3, 'Indiana')


INSERT INTO TPilotRoles( intPilotRoleID, strPilotRole)
VALUES				(1, 'Co-Pilot')
				   ,(2, 'Captain')

				
INSERT INTO TPlaneTypes( intPlaneTypeID, strPlaneType)
VALUES				(1, 'Airbus A350')
				   ,(2, 'Boeing 747-8')
				   ,(3, 'Boeing 767-300F')


INSERT INTO TPlanes( intPlaneID, strPlaneNumber, intPlaneTypeID)
VALUES				(1, '4X887G', 1)
				   ,(2, '5HT78F', 2)
				   ,(3, '5TYY65', 2)
				   ,(4, '4UR522', 1)
				   ,(5, '6OP3PK', 3)
				   ,(6, '67TYHH', 3)


INSERT INTO TAirports( intAirportID, strAirportCity, strAirportCode)
VALUES				(1, 'Cincinnati', 'CVG')
				   ,(2, 'Miami', 'MIA')
				   ,(3, 'Ft. Meyer', 'RSW')
				   ,(4, 'Louisville',  'SDF')
				   ,(5, 'Denver', 'DEN')
				   ,(6, 'Orlando', 'MCO' )


INSERT INTO TPassengers (intPassengerID, strFirstName, strLastName, strAddress, strCity, intStateID, strZip, strPhoneNumber, strEmail, strPassengerLogin, strPassword, dtmPassengerDOB)
VALUES 
					 (1, 'Knelly', 'Nervious', '321 Elm St.', 'Cincinnati', 1, '45201', '5135553333', 'nnelly@gmail.com', 'knelly.nervious', 'password1', '2010-04-15')  
					,(2, 'Orville', 'Waite', '987 Oak St.', 'Cleveland', 1, '45218', '5135556333', 'owright@gmail.com', 'orville.waite', 'password2', '1955-08-22')   -- Over 65 years old
					,(3, 'Eileen', 'Awnewe', '1569 Windisch Rd.', 'Dayton', 1, '45069', '5135555333', 'eonewe1@yahoo.com', 'eileen.awnewe', 'password3', '2023-07-10') -- Under 5 years old
					,(4, 'Bob', 'Eninocean', '44561 Oak Ave.', 'Florence', 2, '45246', '8596663333', 'bobenocean@gmail.com', 'bob.eninocean', 'password4', '1975-02-28')
					,(5, 'Ware', 'Hyjeked', '44881 Pine Ave.', 'Aurora', 3, '45546', '2825553333', 'Hyjekedohmy@gmail.com', 'ware.hyjeked', 'password5', '1990-11-12')
					,(6, 'Kay', 'Oss', '4484 Bushfield Ave.', 'Lawrenceburg', 3, '45546', '2825553333', 'wehavekayoss@gmail.com', 'kay.oss', 'password6', '1985-05-05')


INSERT INTO TPilots (intPilotID, strFirstName, strLastName, strEmployeeID, dtmDateofHire, dtmDateofTermination, dtmDateofLicense, intPilotRoleID)
VALUES				  (1, 'Tip', 'Seenow', '12121', '1/1/2015', '1/1/2099', '12/1/2014', 1)
					 ,(2, 'Ima', 'Soring', '13322', '1/1/2016', '1/1/2099', '12/1/2015', 1)
					 ,(3, 'Hugh', 'Encharge', '16666', '1/1/2017', '1/1/2099', '12/1/2016', 2)
					 ,(4, 'Iwanna', 'Knapp', '17676', '1/1/2014', '1/1/2015', '12/1/2013', 1)
					 ,(5, 'Rose', 'Ennair', '19909', '1/1/2012', '1/1/2099', '12/1/2011', 2)


INSERT INTO TAttendants (intAttendantID, strFirstName, strLastName, strEmployeeID, dtmDateofHire, dtmDateofTermination)
VALUES				  (1, 'Miller', 'Tyme', '22121', '1/1/2015', '1/1/2099')
					 ,(2, 'Sherley', 'Ujest', '23322', '1/1/2016', '1/1/2099')
					 ,(3, 'Buhh', 'Biy', '26666', '1/1/2017', '1/1/2099')
					 ,(4, 'Myles', 'Amanie', '27676', '1/1/2014', '1/1/2015')
					 ,(5, 'Walker', 'Toexet', '29909', '1/1/2012', '1/1/2099')



INSERT INTO TMaintenanceWorkers (intMaintenanceWorkerID, strFirstName, strLastName, strEmployeeID, dtmDateofHire, dtmDateofTermination, dtmDateOfCertification)
VALUES				  (1, 'Gressy', 'Nuckles', '32121', '1/1/2015', '1/1/2099', '12/1/2014')
					 ,(2, 'Bolt', 'Izamiss', '33322', '1/1/2016', '1/1/2099', '12/1/2015')
					 ,(3, 'Sharon', 'Urphood', '36666', '1/1/2017', '1/1/2099','12/1/2016')
					 ,(4, 'Ides', 'Racrozed', '37676', '1/1/2014', '1/1/2015','12/1/2013')
					

INSERT INTO TMaintenances (intMaintenanceID, strWorkCompleted, dtmMaintenanceDate, intPlaneID)
VALUES				  (1, 'Fixed Wing', '1/1/2022', 1)
					 ,(2, 'Repaired Flat Tire', '3/1/2022', 2)
					 ,(3, 'Added Wiper Fluid', '4/1/2022', 3)
					 ,(4, 'Tightened Engine to Wing', '5/1/2022', 2)
					 ,(5, '100,000 mile checkup', '3/10/2022', 4)
					 ,(6, 'Replaced Loose Door', '4/10/2022', 6)
					 ,(7, 'Trapped Raccoon in Cargo Hold', '5/1/2022', 6)


INSERT INTO TFlights (intFlightID, dtmFlightDate, strFlightNumber, dtmTimeofDeparture, dtmTimeOfLanding, intFromAirportID, intToAirportID, intMilesFlown, intPlaneID)
VALUES
					 (1, '4/1/2022', '111', '10:00:00', '12:00:00', 1, 2, 1200, 2)
					,(2, '4/4/2022', '222', '13:00:00', '15:00:00', 1, 3, 1000, 2)
					,(3, '4/5/2022', '333', '15:00:00', '17:00:00', 1, 5, 1200, 3)
					,(4, '4/16/2022', '444', '10:00:00', '12:00:00', 4, 6, 1100, 3)
					,(5, '3/14/2022', '555', '18:00:00', '20:00:00', 2, 1, 1200, 3)
					,(6, '3/21/2022', '666', '19:00:00', '21:00:00', 3, 1, 1000, 1)
					,(7, '3/11/2022', '777', '20:00:00', '22:00:00', 3, 6, 1400, 4)
					,(8, '3/17/2022', '888', '09:00:00', '11:00:00', 6, 4, 1100, 5)
					,(9, '4/19/2022', '999', '08:00:00', '10:00:00', 4, 2, 1000, 6)
					,(10, '4/22/2022', '091', '10:00:00', '12:00:00', 2, 1, 1200, 6)
					,(11, '8/21/2024', '105', '07:00:00', '09:00:00', 5, 3, 900, 1)
					,(12, '8/23/2024', '106', '11:00:00', '13:00:00', 2, 4, 1300, 2)
					,(13, '8/25/2024', '107', '14:00:00', '16:00:00', 6, 1, 1500, 3)
					,(14, '8/27/2024', '108', '18:00:00', '20:00:00', 3, 5, 1200, 4)
					,(15, '8/29/2024', '109', '21:00:00', '23:00:00', 4, 2, 1100, 5)



INSERT INTO TPilotFlights ( intPilotFlightID, intPilotID, intFlightID)
VALUES				 ( 1, 1, 2 )
					,( 2, 1, 3 )
					,( 3, 3, 3 )
					,( 4, 3, 2 )
					,( 5, 5, 1 )
					,( 6, 2, 1 )
					,( 7, 3, 4 )
					,( 8, 2, 4 )
					,( 9, 2, 5 )
					,( 10, 3, 5 )
					,( 11, 5, 6 )
					,( 12, 1, 6 )
					,( 13, 1, 13 )
					,( 14, 2, 14 )
					,( 15, 3, 15 )


INSERT INTO TAttendantFlights ( intAttendantFlightID, intAttendantID, intFlightID)
VALUES				 ( 1, 1, 2 )
					,( 2, 2, 3 )
					,( 3, 3, 3 )
					,( 4, 4, 2 )
					,( 5, 5, 1 )
					,( 6, 1, 1 )
					,( 7, 2, 4 )
					,( 8, 3, 4 )
					,( 9, 4, 5 )
					,( 10, 5, 5 )
					,( 11, 5, 6 )
					,( 12, 1, 6 )
					,( 13, 1, 11 )
					,( 14, 2, 12 )
					,( 15, 3, 13 )
					,( 16, 4, 14 )
					
INSERT INTO TSeats (intSeatID, strSeat)
VALUES 
					 (1, '1A')
					,(2, '1B')
					,(3, '1C')
					,(4, '1D')
					,(5, '2A')
					,(6, '2B')
					,(7, '2C')
					,(8, '2D')
					,(9, '3A')
					,(10, '3B')
					,(11, '3C')
					,(12, '3D')
					,(13, '4A')
					,(14, '4B')
					,(15, '4C')
					,(16, '4D')
					,(17, '5A')
					,(18, '5B')
					,(19, '5C')
					,(20, '5D')


INSERT INTO TFlightPassengers ( intFlightPassengerID, intFlightID, intPassengerID, intSeatID, decFlightCost)
VALUES				  ( 1, 1, 1, 12, 250)
					, ( 2, 1, 2, 18, 250 )
					, ( 3, 1, 3, 7, 250 )
					, ( 4, 1, 4, 19, 250 )
					, ( 5, 1, 5, 8, 250 )
					, ( 6, 2, 5, 3, 250 )
					, ( 7, 2, 4, 16, 250 )
					, ( 8, 2, 3, 2, 250 )
					, ( 9, 3, 1, 13, 250 )
					, ( 10, 3, 2, 17, 250 )
					, ( 11, 3, 3, 6, 250 )
					, ( 12, 3, 4, 18, 250 )
					, ( 13, 3, 5, 1, 250 )
					, ( 14, 4, 2, 16, 250 )
					, ( 15, 4, 3, 11, 250 )
					, ( 16, 4, 4, 4, 250 )
					, ( 17, 4, 5, 10, 250 )
					, ( 18, 5, 1, 9, 250 )
					, ( 19, 5, 2, 14, 250 )
					, ( 20, 5, 3, 5, 250 )
					, ( 21, 5, 4, 6, 250 )
					, ( 22, 6, 1, 15, 250 )
					, ( 23, 6, 2, 17, 250 )
					, ( 24, 6, 3, 20, 250)
					, ( 25, 7, 1, 12, 250 )
					, ( 26, 8, 1, 18, 250 )
					, ( 27, 9, 1, 7, 250 )
					, ( 28, 10, 1, 19, 250 )
					, ( 29, 11, 1, 8, 250 )
					, ( 30, 12, 1, 3, 250 )
					, ( 31, 13, 1, 16, 250 )
					, ( 32, 14, 1, 2, 250 )
					, ( 33, 15, 1, 13, 250 )
					, ( 34, 1, 1, 17, 250 )
					, ( 35, 2, 1, 6, 250 )
					, ( 36, 3, 1, 18, 250 )
					, ( 37, 4, 1, 1, 250 )
					, ( 38, 5, 1, 15, 250 )
					, ( 39, 6, 1, 4, 250 )

					

INSERT INTO TMaintenanceMaintenanceWorkers ( intMaintenanceMaintenanceWorkerID, intMaintenanceID, intMaintenanceWorkerID, intHours)
VALUES				 ( 1, 2, 1, 2 )
					,( 2, 4, 1, 3 )
					,( 3, 2, 3, 4 )
					,( 4, 1, 4, 2 )
					,( 5, 3, 4, 2 )
					,( 6, 4, 3, 5 )
					,( 7, 5, 1, 7 )
					,( 8, 6, 1, 2 )
					,( 9, 7, 3, 4 )
					,( 10, 4, 4, 1 )
					,( 11, 3, 3, 4 )
					,( 12, 7, 3, 8 )		

INSERT INTO TEmployees (intEmployeeKeyID, strEmployeeRole, strEmployeeLoginID, strEmployeePassword, intEmployeeID)
VALUES				 (1, 'Admin', 'admin', 'admin', 1000) -- Admin record
					,(2, 'Pilot', 'TipSeenow', 'password1', 1)
					,(3, 'Pilot', 'ImaSoring', 'password2', 2)
					,(4, 'Pilot', 'HughEncharge', 'password3', 3)
					,(5, 'Pilot', 'IwannaKnapp', 'password4', 4)
					,(6, 'Pilot', 'RoseEnnair', 'password5', 5)
					,(7, 'Attendant', 'MillerTyme', 'password6', 1)
					,(8, 'Attendant', 'SherleyUjest', 'password7', 2)
					,(9, 'Attendant', 'BuhhBiy', 'password8', 3)
					,(10, 'Attendant', 'MylesAmanie', 'password9', 4)
					,(11, 'Attendant', 'WalkerToexet', 'password10', 5)