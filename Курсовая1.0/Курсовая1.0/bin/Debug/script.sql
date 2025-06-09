-- Создание базы данных (если нужно)
CREATE DATABASE KBPClassBeta;
GO

USE KBPClassBeta;
GO

-- Таблица Teachers
CREATE TABLE Teachers (
    IDTeacher INT IDENTITY(1,1) PRIMARY KEY,
    Name NCHAR(30) NOT NULL
);

-- Таблица Disciplines
CREATE TABLE Disciplines (
    IDDisciplines INT IDENTITY(1,1) PRIMARY KEY,
    Name NCHAR(50) NOT NULL,
    IDTeacher INT NOT NULL,
    Hours INT NOT NULL,
    CONSTRAINT FK_Disciplines_Teachers FOREIGN KEY (IDTeacher)
        REFERENCES Teachers(IDTeacher)
);

-- Таблица Tasks
CREATE TABLE Tasks (
    IDTask INT IDENTITY(1,1) PRIMARY KEY,
    Description NVARCHAR(MAX) NOT NULL,
    IssuanceDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    IDTeacher INT NOT NULL,
    CONSTRAINT FK_Tasks_Teachers FOREIGN KEY (IDTeacher)
        REFERENCES Teachers(IDTeacher)
);
