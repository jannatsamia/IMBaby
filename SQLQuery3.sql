DROP TABLE IF EXISTS Vaccinations;
DROP TABLE IF EXISTS Growth_Records;
DROP TABLE IF EXISTS Children;
DROP TABLE IF EXISTS Users;

USE IMBabyDB;


-- Sob table fresh create koro
CREATE TABLE Users (
    id          INT PRIMARY KEY IDENTITY(1,1),
    username    NVARCHAR(100) NOT NULL UNIQUE,
    password    NVARCHAR(200) NOT NULL,
    full_name   NVARCHAR(200) NOT NULL DEFAULT 'User',
    email       NVARCHAR(200),
    created_at  DATETIME DEFAULT GETDATE()
);

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

-- Admin user insert
INSERT INTO Users (username, password, full_name, email)
VALUES ('admin', 'admin', 'Administrator', 'admin@imbaby.com');

-- Verify
SELECT * FROM Users;

USE IMBabyDB;
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Children';

ALTER TABLE Children
ADD user_id INT NOT NULL DEFAULT 1;

SELECT * FROM Children;
sp_help Children;

SELECT @@SERVERNAME;