-----------------------------------------------------
-- Create Database
-----------------------------------------------------

USE master;
GO

CREATE DATABASE TodoApp;
GO

USE [TodoApp]
GO

/****** Object:  Table [dbo].[TodoItems]    ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TodoItems]') AND type in (N'U'))
DROP TABLE [dbo].[TodoItems]
GO

/****** Object:  Table [dbo].[TodoItems]    ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TodoItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[IsComplete] [bit] NOT NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-----------------------------------------------------
-- Insert initial values
-----------------------------------------------------
USE [TodoApp]
GO

INSERT INTO [dbo].[TodoItems] ([Name] ,[IsComplete],[Description]) 
	VALUES ('Create app',1,''),('Update app',0,'')
GO
-----------------------------------------------------
-- Select from table
-----------------------------------------------------
SELECT * FROM [TodoApp].[dbo].[TodoItems]