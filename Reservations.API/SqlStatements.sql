Create Table Providers(
                          ProviderId INTEGER PRIMARY KEY AUTOINCREMENT,
                          ProviderExternalId TEXT NOT NULL,
                          FirstName TEXT NOT NULL CHECK(length(FirstName) <= 125),
                          LastName TEXT NOT NULL CHECK(length(LastName) <= 125));

Create Table Clients(
                        ClientId INTEGER PRIMARY KEY AUTOINCREMENT,
                        ClientExternalId TEXT NOT NULL,
                        FirstName TEXT NOT NULL CHECK(length(FirstName) <= 125),
                        LastName TEXT NOT NULL CHECK(length(LastName) <= 125));

Create Table Schedules(
                          ScheduleId INTEGER PRIMARY KEY AUTOINCREMENT,
                          ScheduleExternalId TEXT NOT NULL,
                          ProviderId INTEGER NOT NULL,
                          StartDateTime TEXT NOT NULL,
                          EndDateTime TEXT NOT NULL,
                          FOREIGN KEY (ProviderId)
                              REFERENCES Providers (ProviderId)
);

Create Table Appointments(
                             AppointmentId INTEGER PRIMARY KEY AUTOINCREMENT,
                             AppointmentExternalId TEXT NOT NULL,
                             ScheduleId INTEGER NOT NULL,
                             ClientId INTEGER NOT NULL,
                             AppointmentDateTime TEXT NOT NULL,
                             CreatedDateTime TEXT NOT NULL,
                             ExpirationDateTime TEXT NOT NULL,
                             Confirmed INTEGER NOT NULL,
                             Expired INTEGER NOT NULL,
                             FOREIGN KEY (ScheduleId)
                                 REFERENCES Schedules (ScheduleId),
                                FOREIGN KEY (ClientId)
                                    REFERENCES Clients (ClientId)
);

DROP Table Appointments;

INSERT INTO Providers (ProviderExternalId, FirstName, LastName)
VALUES ('9a74197d-d550-421f-aab5-eb2fc1981655', 'John', 'Cena');
INSERT INTO Providers (ProviderExternalId, FirstName, LastName)
VALUES ('35ae9cc0-1d37-4441-8201-a27f69aa5397', 'Trish', 'Stratus');
INSERT INTO Providers (ProviderExternalId, FirstName, LastName)
VALUES ('451033cc-5e6b-4a3d-888b-412d57ff86a2', 'David', 'Bautista');
INSERT INTO Providers (ProviderExternalId, FirstName, LastName)
VALUES ('4ff5e509-fb50-42c0-a99d-1f456f9ba1a8', 'Drew', 'McIntyre');

--9a74197d-d550-421f-aab5-eb2fc1981655
--35ae9cc0-1d37-4441-8201-a27f69aa5397
--451033cc-5e6b-4a3d-888b-412d57ff86a2
--4ff5e509-fb50-42c0-a99d-1f456f9ba1a8
select *
from Providers;

--86ea41c6-5fcc-4683-b41f-5e074b9658a4
--28edb0ba-d0ce-4281-9be1-a8bf4fd53287
--470a9a87-342d-490e-9a8e-08b25fcd0231

Insert Into Schedules (ScheduleExternalId, ProviderId, StartDateTime, EndDateTime)
VALUES ('86ea41c6-5fcc-4683-b41f-5e074b9658a4', 1, '2023-08-23 09:00:00', '2023-08-23 17:15:00');

Insert Into Schedules (ScheduleExternalId, ProviderId, StartDateTime, EndDateTime)
VALUES ('28edb0ba-d0ce-4281-9be1-a8bf4fd53287', 1, '2023-08-26 05:00:00', '2023-08-26 12:15:00');

Insert Into Schedules (ScheduleExternalId, ProviderId, StartDateTime, EndDateTime)
VALUES ('470a9a87-342d-490e-9a8e-08b25fcd0231', 1, '2023-08-29 12:00:00', '2023-08-29 19:15:00');
select *
from Schedules;

Select * from Schedules where Date(StartDateTime) = '2023-08-26';

INSERT INTO Clients (ClientExternalId, FirstName, LastName)
VALUES ('f31ec68b-121b-4da2-aa05-55e317c489ab', 'Lloyd', 'Bannings');
INSERT INTO Clients (ClientExternalId, FirstName, LastName)
VALUES ('7990c7ff-a23c-4cad-b62b-bb33c664bd78', 'Rean', 'Schwarzer');

Select * from clients;