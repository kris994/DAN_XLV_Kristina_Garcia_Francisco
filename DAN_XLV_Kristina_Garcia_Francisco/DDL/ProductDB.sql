-- Dropping the tables before recreating the database in the order depending how the foreign keys are placed.
IF OBJECT_ID('tblProduct', 'U') IS NOT NULL DROP TABLE tblProduct;

-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ProductDB')
CREATE DATABASE ProductDB;
GO

USE ProductDB
CREATE TABLE tblProduct(
	ProductID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	ProductCode VARCHAR(40) UNIQUE				NOT NULL,
	ProductName VARCHAR(40)						NOT NULL,
	Quantity INT								NOT NULL,
	Price VARCHAR(20)							NOT NULL,
	Stored BIT DEFAULT 0									
);