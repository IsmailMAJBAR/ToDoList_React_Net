-----------------------------------------------------
-- Create Database
-----------------------------------------------------

USE master;
GO

CREATE DATABASE TodoApp;
GO

USE TodoApp;
GO

CREATE TABLE TodoItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    IsComplete BIT NOT NULL
);
GO

-----------------------------------------------------
-- Insert initial values
-----------------------------------------------------
USE [TodoApp]
GO

INSERT INTO [dbo].[TodoItems] ([Name] ,[IsComplete]) 
	VALUES ('Create app',1),('Update app',0)
GO
-----------------------------------------------------
-- Select from table
-----------------------------------------------------
SELECT * FROM [TodoApp].[dbo].[TodoItems]