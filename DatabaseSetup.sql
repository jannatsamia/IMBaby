-- ============================================================
-- IMBaby - Child Health Management System
-- Database Setup Script for SQL Server (SSMS)
-- Run this script in SSMS to create the database manually
-- (The app also auto-creates it on startup)
-- ============================================================

-- Create database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'IMBabyDB')
    CREATE DATABASE IMBabyDB;
GO

USE IMBabyDB;
GO

-- ===================== USERS =====================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
CREATE TABLE Users (
    id          INT PRIMARY KEY IDENTITY(1,1),
    username    NVARCHAR(100) NOT NULL UNIQUE,
    password    NVARCHAR(200) NOT NULL,
    full_name   NVARCHAR(200) NOT NULL,
    email       NVARCHAR(200),
    created_at  DATETIME DEFAULT GETDATE()
);
GO

-- Default admin user (username: admin, password: admin)
IF NOT EXISTS (SELECT * FROM Users WHERE username='admin')
    INSERT INTO Users (username, password, full_name, email)
    VALUES ('admin', 'admin', 'Administrator', 'admin@imbaby.com');
GO

-- ===================== CHILDREN =====================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Children' AND xtype='U')
CREATE TABLE Children (
    id              INT PRIMARY KEY IDENTITY(1,1),
    user_id         INT NOT NULL DEFAULT 1,
    name            NVARCHAR(200) NOT NULL,
    date_of_birth   DATE NOT NULL,
    gender          NVARCHAR(10) NOT NULL,
    blood_group     NVARCHAR(5),
    medical_notes   NVARCHAR(MAX),
    created_at      DATETIME DEFAULT GETDATE()
);
GO

-- ===================== GROWTH RECORDS =====================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Growth_Records' AND xtype='U')
CREATE TABLE Growth_Records (
    id              INT PRIMARY KEY IDENTITY(1,1),
    child_id        INT NOT NULL,
    measure_date    DATE NOT NULL,
    height_cm       FLOAT,
    weight_kg       FLOAT,
    head_circum_cm  FLOAT,
    recorded_by     NVARCHAR(100),
    FOREIGN KEY (child_id) REFERENCES Children(id) ON DELETE CASCADE
);
GO

-- ===================== VACCINATIONS =====================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Vaccinations' AND xtype='U')
CREATE TABLE Vaccinations (
    id              INT PRIMARY KEY IDENTITY(1,1),
    child_id        INT NOT NULL,
    vaccine_name    NVARCHAR(200) NOT NULL,
    due_age_months  INT NOT NULL,
    due_date        DATE,
    given_date      DATE,
    status          NVARCHAR(20) DEFAULT 'Upcoming',
    notes           NVARCHAR(MAX),
    FOREIGN KEY (child_id) REFERENCES Children(id) ON DELETE CASCADE
);
GO

-- Verify
SELECT 'Users' AS TableName, COUNT(*) AS Rows FROM Users
UNION ALL
SELECT 'Children', COUNT(*) FROM Children
UNION ALL
SELECT 'Growth_Records', COUNT(*) FROM Growth_Records
UNION ALL
SELECT 'Vaccinations', COUNT(*) FROM Vaccinations;
GO

PRINT 'IMBabyDB setup complete!';
GO
