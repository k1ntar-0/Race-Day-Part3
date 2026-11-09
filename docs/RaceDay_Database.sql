-- Create Database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'RaceDayDb')
BEGIN
    CREATE DATABASE RaceDayDb;
END
GO

USE RaceDayDb;
GO

-- Drop existing tables to ensure clean schema generation
IF OBJECT_ID('dbo.Results', 'U') IS NOT NULL DROP TABLE dbo.Results;
IF OBJECT_ID('dbo.EventEnrolments', 'U') IS NOT NULL DROP TABLE dbo.EventEnrolments;
IF OBJECT_ID('dbo.Events', 'U') IS NOT NULL DROP TABLE dbo.Events;
IF OBJECT_ID('dbo.EventCategories', 'U') IS NOT NULL DROP TABLE dbo.EventCategories;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL DROP TABLE dbo.Roles;
GO

-- 1. Roles Table
CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName VARCHAR(20) NOT NULL UNIQUE
);

-- 2. Users Table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    RoleId INT NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

-- 3. Event Categories Table
CREATE TABLE EventCategories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(50) NOT NULL,
    EventType VARCHAR(30) NOT NULL
);

-- 4. Events Table
CREATE TABLE Events (
    EventId INT IDENTITY(1,1) PRIMARY KEY,
    OrganiserId INT NOT NULL,
    Title VARCHAR(100) NOT NULL,
    Description VARCHAR(MAX) NULL,
    Location VARCHAR(100) NOT NULL,
    EventDate DATETIME NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Events_Organiser FOREIGN KEY (OrganiserId) REFERENCES Users(UserId)
);

-- 5. Event Enrolments Table
CREATE TABLE EventEnrolments (
    EnrolmentId INT IDENTITY(1,1) PRIMARY KEY,
    EventId INT NOT NULL,
    ParticipantId INT NOT NULL,
    CategoryId INT NOT NULL,
    EnrolmentDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status VARCHAR(20) NOT NULL DEFAULT 'Registered',
    CONSTRAINT FK_Enrolments_Events FOREIGN KEY (EventId) REFERENCES Events(EventId),
    CONSTRAINT FK_Enrolments_Users FOREIGN KEY (ParticipantId) REFERENCES Users(UserId),
    CONSTRAINT FK_Enrolments_Categories FOREIGN KEY (CategoryId) REFERENCES EventCategories(CategoryId),
    CONSTRAINT UQ_Participant_Event UNIQUE (EventId, ParticipantId)
);

-- 6. Results Table
CREATE TABLE Results (
    ResultId INT IDENTITY(1,1) PRIMARY KEY,
    EnrolmentId INT NOT NULL UNIQUE,
    FinishTime TIME NULL,
    Position INT NULL,
    RecordedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Results_Enrolments FOREIGN KEY (EnrolmentId) REFERENCES EventEnrolments(EnrolmentId)
);
GO

-- Seed Roles
INSERT INTO Roles (RoleName) VALUES ('Organiser'), ('Participant');

-- Seed Users (2 Organisers, 2 Participants)
INSERT INTO Users (RoleId, FirstName, LastName, Email, PasswordHash) VALUES
(1, 'Sipho', 'Nkosi', 'sipho.organiser@raceday.co.za', 'hashed_pass_1'),
(1, 'Sarah', 'Van Der Merwe', 'sarah.organiser@raceday.co.za', 'hashed_pass_2'),
(2, 'Kabo', 'Mokoena', 'kabo.runner@gmail.com', 'hashed_pass_3'),
(2, 'Thabo', 'Molefe', 'thabo.cyclist@gmail.com', 'hashed_pass_4');

-- Seed Categories
INSERT INTO EventCategories (CategoryName, EventType) VALUES
('10km Road Run', 'Running'),
('21km Half Marathon', 'Running'),
('50km Cycle Challenge', 'Cycling'),
('5km Community Walk', 'Walking');

-- Seed Events (3 Events)
INSERT INTO Events (OrganiserId, Title, Description, Location, EventDate) VALUES
(1, 'Soweto City Run', 'Annual township running event', 'Soweto, Johannesburg', '2026-10-15 06:00:00'),
(1, 'Free State Cycle Classic', 'Scenic road cycling race across Bloemfontein', 'Bloemfontein', '2026-11-05 07:00:00'),
(2, 'Waterkloof Charity Walk', 'Family community walk for charity', 'Pretoria', '2026-12-01 08:00:00');

-- Seed Enrolments
INSERT INTO EventEnrolments (EventId, ParticipantId, CategoryId) VALUES
(1, 3, 1),
(1, 4, 2),
(2, 4, 3);

-- Seed Results
INSERT INTO Results (EnrolmentId, FinishTime, Position) VALUES
(1, '00:45:30', 12),
(2, '01:52:10', 45);
GO