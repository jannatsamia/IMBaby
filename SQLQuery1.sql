DROP TABLE IF EXISTS Reminders;
DROP TABLE IF EXISTS Growth_Records;
DROP TABLE IF EXISTS Vaccination_Records;
DROP TABLE IF EXISTS Parents;
DROP TABLE IF EXISTS Vaccines;
DROP TABLE IF EXISTS Children;


CREATE TABLE Children (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    date_of_birth DATE NOT NULL,
    gender VARCHAR(10) CHECK (gender IN ('Male','Female','Other')),
    blood_group VARCHAR(5),
    medical_notes VARCHAR(255) DEFAULT '',
    created_at DATETIME DEFAULT GETDATE()
);


CREATE TABLE Parents (
    id INT IDENTITY(1,1) PRIMARY KEY,
    child_id INT NOT NULL REFERENCES Children(id) ON DELETE CASCADE,
    parent_name VARCHAR(100) NOT NULL,
    relationship VARCHAR(50) DEFAULT 'Parent',
    phone VARCHAR(20),
    email VARCHAR(100),
    address VARCHAR(255)
);


CREATE TABLE Vaccines (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    schedule_age VARCHAR(50) NOT NULL,
    doses_total INT DEFAULT 1,
    description VARCHAR(255)
);


CREATE TABLE Vaccination_Records (
    id INT IDENTITY(1,1) PRIMARY KEY,
    child_id INT NOT NULL REFERENCES Children(id) ON DELETE CASCADE,
    vaccine_id INT NOT NULL REFERENCES Vaccines(id),
    status VARCHAR(20) NOT NULL DEFAULT 'Pending'
        CHECK (status IN ('Pending','Completed','Missed')),
    due_date TEXT NOT NULL,
    done_date TEXT NULL,
    given_by VARCHAR(100),
    notes VARCHAR(255)
);


CREATE TABLE Growth_Records (
    id INT IDENTITY(1,1) PRIMARY KEY,
    child_id INT NOT NULL REFERENCES Children(id) ON DELETE CASCADE,
    measure_date DATETIME DEFAULT GETDATE(),
    height_cm FLOAT,
    weight_kg FLOAT,
    head_circum_cm FLOAT,
    recorded_by VARCHAR(100)
);


CREATE TABLE Reminders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    child_id INT NOT NULL REFERENCES Children(id) ON DELETE CASCADE,
    type VARCHAR(20) NOT NULL
        CHECK (type IN ('Vaccination','Checkup','Nutrition','Growth')),
    note VARCHAR(255),
    due_date DATE NOT NULL,
    is_active BIT DEFAULT 1,
    alert_days INT DEFAULT 1
);


DECLARE @ChildID INT;

INSERT INTO Children (name, date_of_birth, gender, blood_group, medical_notes)
VALUES ('John Doe', '2024-01-15', 'Male', 'O+', 'No allergies');

SET @ChildID = SCOPE_IDENTITY();



INSERT INTO Parents (child_id, parent_name, phone)
VALUES (@ChildID, 'Jane Doe', '017XXXXXXXX');


INSERT INTO Vaccination_Records (child_id, vaccine_id, status, due_date)
SELECT 
    @ChildID,
    id,
    'Pending',
    CONVERT(VARCHAR(10), GETDATE(), 120)
FROM Vaccines;